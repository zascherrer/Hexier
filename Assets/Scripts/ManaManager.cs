using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManaManager : NetworkBehaviour {

    public int maxMana;

    [SyncVar]
    public float currentMana;
    public float manaRefreshRate;

    // Use this for initialization
    void Start () {

        currentMana = (float) maxMana;

	}
	
	// Update is called once per frame
	void Update () {

        manaRefreshRate = 4f / (currentMana + 1f); //The less mana you have, the faster your mana recharges. The 1f is so you can't divide by infinity, as well as making it so you don't recharge mana too quickly when below 1 mana.

        if(currentMana <= maxMana)
        {
            currentMana += manaRefreshRate * Time.deltaTime;
        }

        if(currentMana > maxMana)
        {
            currentMana = (float) maxMana;
        }
	}

    public bool CanCastSpell(int cost)
    {
        if (cost <= currentMana)
        {
            currentMana -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
