using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthManager : MonoBehaviour {

    public int maxHP;
    public int currentHP;

    public int crushingResist;
    public int cuttingResist;
    public int burningResist;

    public float flammability;
    public float flameCounter;

    public float timeBetweenBurns;
    private float timeBetweenBurnsCounter;
    private float burnRecoverRate;

    public bool isBurning;
    private GameObject fireEffect;

	// Use this for initialization
	void Start () {

        currentHP = maxHP;

        flameCounter = flammability;
        timeBetweenBurnsCounter = timeBetweenBurns;

        burnRecoverRate = 0.5f;
        fireEffect = Resources.Load("FireParticle") as GameObject;

    }
	
	// Update is called once per frame
	void Update () {
		
        if(currentHP <= 0)
        {

            gameObject.SetActive(false);
        }

        if(flameCounter<=0)
        {
            isBurning = true;
        }

        if(flameCounter < flammability)
        {
            flameCounter += burnRecoverRate * Time.deltaTime;
        }
        else if(flameCounter > flammability)
        {
            flameCounter = flammability;
        }

        if (flameCounter > 0)
        {
            isBurning = false;
        }

        if (isBurning)
        {
            timeBetweenBurnsCounter -= Time.deltaTime;

            if (timeBetweenBurnsCounter <= 0.50)
            {

                Instantiate(fireEffect, transform.position, Quaternion.Euler(Vector3.zero), gameObject.transform);
            }
        }
	}

    public int Hurt(int crushing, int cutting, int burning)
    {

        if(crushing < crushingResist)
        {
            crushing = crushingResist;
        }
        if (cutting < cuttingResist)
        {
            cutting = cuttingResist;
        }
        if (burning < burningResist)
        {
            burning = burningResist;
        }

        int damageTaken = ((crushing - crushingResist) + (cutting - cuttingResist) + (burning - burningResist));
        currentHP -= damageTaken;
        flameCounter -= (burning - burningResist);

        return damageTaken;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(isBurning && timeBetweenBurnsCounter <= 0)
        {
            if (other.name != "Collision")
            {
                other.GetComponent<HealthManager>().Hurt(0, 0, 3);
            }
            this.Hurt(0, 0, 3);
            timeBetweenBurnsCounter = timeBetweenBurns;
        }

    }
}
