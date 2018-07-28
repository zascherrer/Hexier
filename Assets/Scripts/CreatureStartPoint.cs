using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStartPoint : MonoBehaviour {

    //private List<CreatureStatsManager> creatures;
    private GameObject leashedCreature;
    private PlayerController thePlayer;
    private PlayerCreatureManager playerCreatures;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        playerCreatures = thePlayer.GetComponent<PlayerCreatureManager>();
        //creatures = new List<CreatureStatsManager>();

        if (thePlayer.hasCreatureLeashed)
        {
            foreach (GameObject creature in playerCreatures.creaturesOwned)
            {
                if (creature.GetComponent<CreatureStatsManager>().isLeashed)
                {
                    leashedCreature = creature;
                }
            }

            if (leashedCreature != null)
            {
                //Instantiate(leashedCreature, this.transform.position, Quaternion.Euler(Vector3.zero));
                leashedCreature.transform.position = this.transform.position;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
