using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour {

    public bool isWandering;
    public bool isTraining;
    private float moveX;
    private float moveY;
    private float moveSpeed;
    private float timeBetweenWanders;
    private float timeBetweenWandersBase;
    private float timeToWander;
    private float timeToWanderBase;
    private Rigidbody2D creatureRigidbody;
    private PlayerController player;
    public Transform trainingPosition;

	// Use this for initialization
	void Start () {

        isWandering = true;
        creatureRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();

        moveSpeed = 3;
        timeBetweenWandersBase = 4;
        timeBetweenWanders = timeBetweenWandersBase + Random.Range(-1f, 1f);
        moveX = Random.Range(-1, 1);
        moveY = Random.Range(-1, 1);

        timeToWanderBase = 2;
	}
	
	// Update is called once per frame
	void Update () {

        if (isWandering)
        {
            timeBetweenWanders -= Time.deltaTime;

            if(timeBetweenWanders <= 0)
            {
                timeBetweenWanders = timeBetweenWandersBase + Random.Range(-1f, 1f);
                timeToWander = timeToWanderBase + Random.Range(-1f, 1f);
                moveX = Random.Range(-1f, 1f);
                moveY = Random.Range(-1f, 1f);

                creatureRigidbody.velocity = Vector2.zero;
               
            }
            else if(timeToWander > 0)
            {
                creatureRigidbody.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
                timeToWander -= Time.deltaTime;

                if (moveX < 0)
                {
                    this.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
                    this.gameObject.transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(Vector3.zero);
                    this.gameObject.transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
                }
            }
            else
            {
                creatureRigidbody.velocity = Vector2.zero;
            }
        }
        else if (isTraining)
        {
            creatureRigidbody.velocity = Vector2.zero;
            transform.position = trainingPosition.position;
        }
        else
        {
            creatureRigidbody.velocity = player.myRigidbody.velocity;

            if (creatureRigidbody.velocity.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
                this.gameObject.transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(Vector3.zero);
                this.gameObject.transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
            }
        }

	}

    private void LateUpdate()
    {
    }
}
