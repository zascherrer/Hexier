using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatureStatsManager : MonoBehaviour {

    private bool creatureExists;
    public bool isOwnedByPlayer;
    public bool isLeashed = false;

    private int lastLeashedOnLevel;

    public CreatureList.Creature stats;
    //private CreatureController creatureController;
    private Transform displayNameTransform;
    private Text displayName;


	// Use this for initialization
	void Start () {
        if (stats == null)
        {
            stats = new CreatureList.Creature();
        }

        //creatureController = GetComponent<CreatureController>();

        //if (isLeashed)
        //{
        //    creatureExists = true;
        //    DontDestroyOnLoad(gameObject);
        //}
        if (!creatureExists)
        {
            creatureExists = true;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    /*
    private void OnLevelWasLoaded(int level)
    {
        if (creatureExists && creatureController.isWandering && isOwnedByPlayer)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            lastLeashedOnLevel = level;
        }

        if(lastLeashedOnLevel == level)
        {
            this.gameObject.SetActive(true);
        }
    }
    */
    // Update is called once per frame
    void Update () {



    }

    public void SetName()
    {
        displayNameTransform = this.transform.GetChild(0).GetChild(0);
        displayName = displayNameTransform.GetComponent<Text>();

        if (stats.GivenName == "Default_Name")
        {
            displayName.text = stats.species;
        }
        else
        {
            displayName.text = stats.GivenName;
        }
    }

    public void SetStats(CreatureList.Creature creatureType)
    {
        //yield return new WaitForSeconds(0.1f);

        stats = creatureType;

    }
}
