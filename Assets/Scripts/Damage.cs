using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public int crushing;
    public int cutting;
    public int burning;

    public float timeBetweenDamage;
    private float timeBetweenDamageCounter;

    private Rigidbody2D myRigidbody;

    private GameObject damageNumber;


	// Use this for initialization
	void Start () {
        timeBetweenDamageCounter = 0;
        myRigidbody = GetComponent<Rigidbody2D>();
        damageNumber = (GameObject) Resources.Load("DamageNumbers");
	}
	
	// Update is called once per frame
	void Update () {
        timeBetweenDamageCounter -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Collision" && timeBetweenDamageCounter <= 0)
        {
            //int currentHealth = other.GetComponent<HealthManager>().currentHP;
            //int maxHealth = other.GetComponent<HealthManager>().maxHP;

            crushing = Mathf.Abs(Mathf.RoundToInt(((myRigidbody.velocity.x + myRigidbody.velocity.y) * (float)crushing) / 10f)); //Alters the damage based on the velocity, rounds it to an int, then takes the absolute value of it
            cutting = Mathf.Abs(Mathf.RoundToInt(((10.0f + myRigidbody.velocity.x + myRigidbody.velocity.y) * (float)cutting) / 10f));

            int totalDamage = other.GetComponent<HealthManager>().Hurt(crushing, cutting, burning);

            if (other.tag == "Player" && totalDamage > 0) //Creates floating damage numbers if a player is damaged
            {
                var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().dmgNumber = totalDamage;
                clone.transform.position = new Vector2(other.transform.position.x, other.transform.position.y);
            }


            timeBetweenDamageCounter = timeBetweenDamage;
        }
    }

    //Only burning damage applies over time, so we can ignore crushing and cutting damage here
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name != "Collision" && timeBetweenDamageCounter <= 0)
        {
            //int currentHealth = other.GetComponent<HealthManager>().currentHP;
            //int maxHealth = other.GetComponent<HealthManager>().maxHP;

            //crushing = Mathf.Abs(Mathf.RoundToInt(((myRigidbody.velocity.x + myRigidbody.velocity.y) * (float)crushing) / 10f)); //Alters the damage based on the velocity, rounds it to an int, then takes the absolute value of it
            //cutting = Mathf.Abs(Mathf.RoundToInt(((10.0f + myRigidbody.velocity.x + myRigidbody.velocity.y) * (float)cutting) / 10f));

            int totalDamage = other.GetComponent<HealthManager>().Hurt(0, 0, burning);

            if (other.tag == "Player" && totalDamage > 0) //Creates floating damage numbers if a player is damaged
            {
                var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().dmgNumber = totalDamage;
                clone.transform.position = new Vector2(other.transform.position.x, other.transform.position.y);
            }


            timeBetweenDamageCounter = timeBetweenDamage;
        }
    }
}
