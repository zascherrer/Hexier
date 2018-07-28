using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    private float currentMoveSpeed;

    public float jumpVelocity;
    private bool jumping;

    private Animator anim;
    public Rigidbody2D myRigidbody;

    //private bool playerMoving;
    //private Vector2 playerFacing;


    private static bool playerExists;

    public bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public bool attacking2;
    public bool attacking5;

    public string startPoint;
    public string playerName;

    public bool hasCreatureLeashed;
    public CreatureStatsManager[] creaturesInScene;
    public GameObject leashedCreature;

    // Use this for initialization
    void Start()
    {
        hasCreatureLeashed = false;

        creaturesInScene = FindObjectsOfType<CreatureStatsManager>();
        foreach (CreatureStatsManager creature in creaturesInScene)
        {
            if (creature.isLeashed)
            {
                leashedCreature = creature.gameObject;
            }
        }

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentMoveSpeed = moveSpeed;
        //playerFacing = new Vector2(0, -1);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LeashCheck;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LeashCheck;
    }

    // Update is called once per frame
    void Update()
    {

        //playerMoving = false;
        

        attacking = false;
        attacking2 = false;
        attacking5 = false;

        if (!attacking && !attacking2 && !attacking5)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myRigidbody.velocity.y);

                //playerMoving = true;

                //playerFacing = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }

            
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);

                //playerMoving = true;

                //playerFacing = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }
            /*
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                if (!jumping)
                {
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpVelocity);
                    jumping = true;
                }
            }
            */
            Fire();
        }
        
        if (attackTimeCounter >= 0f)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        else
        {
            attacking = false;
            //anim.SetBool("PlayerAttacking", false);
        }
        
        anim.SetFloat("HorizontalMovement", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("VerticalMovement", Input.GetAxisRaw("Vertical"));/*
        anim.SetFloat("FacingX", playerFacing.x);
        anim.SetFloat("FacingY", playerFacing.y);
        anim.SetBool("PlayerMoving", playerMoving);
        */
    }
    
    public void Fire()
    {

        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            attacking = true;

            attackTimeCounter = attackTime;

            //myRigidbody.velocity = Vector2.zero;

            //anim.SetBool("PlayerAttacking", true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            attacking2 = true;

            //attackTimeCounter = attackTime;

            //myRigidbody.velocity = Vector2.zero;

            //anim.SetBool("PlayerAttacking", true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            attacking5 = true;
        }
    }
    
    void LeashCheck(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.buildIndex);
    }

}
