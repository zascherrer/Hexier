using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatureInstanceManager : MonoBehaviour {

    public PlayerCreatureManager playerCreatures;
    public List<int> levelCreatureLastSeenOn;
    CreatureStatsManager[] creaturesInSceneStats;
    List<GameObject> creaturesInScene;

    PlayerController thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        playerCreatures = thePlayer.GetComponent<PlayerCreatureManager>();
        creaturesInSceneStats = FindObjectsOfType<CreatureStatsManager>();
        creaturesInScene = new List<GameObject>();

        for(int i = 0; i < creaturesInSceneStats.Length; i++)
        {
            creaturesInScene.Add(creaturesInSceneStats[i].gameObject);
        }

        levelCreatureLastSeenOn = new List<int>();
	}

    private void OnEnable()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        SceneManager.sceneLoaded += TrackCreatureThroughScene;
        SceneManager.sceneLoaded += ToggleCreatures;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= TrackCreatureThroughScene;
        SceneManager.sceneLoaded -= ToggleCreatures;
    }

    // Update is called once per frame
    void Update () {
        //if (thePlayer.hasCreatureLeashed)
        //{
        //    bool creatureFound = false;
        //    foreach (GameObject creature in playerCreatures.creaturesOwned)
        //    {
        //        if (creature == thePlayer.leashedCreature)
        //        {
        //            creatureFound = true;
        //        }
        //    }

        //    if (!creatureFound)
        //    {
        //        //playerCreatures.Add(thePlayer.leashedCreature);
        //        levelCreatureLastSeenOn.Add(SceneManager.GetActiveScene().buildIndex);
        //        creatureFound = true;
        //    }
        //}
    }

    void TrackCreatureThroughScene(Scene scene, LoadSceneMode mode)
    {
        if (thePlayer.hasCreatureLeashed)
        {
            for (int i = 0; i < playerCreatures.creaturesOwned.Count; i++)
            {
                if (playerCreatures.creaturesOwned[i] == thePlayer.leashedCreature)
                {
                    levelCreatureLastSeenOn[i] = scene.buildIndex;
                }
            }
        }
    }

    void ToggleCreatures(Scene scene, LoadSceneMode mode)
    {
        for(int i = 0; i < levelCreatureLastSeenOn.Count; i++)
        {
            if(levelCreatureLastSeenOn[i] == SceneManager.GetActiveScene().buildIndex)
            {
                playerCreatures.creaturesOwned[i].SetActive(true);
            }
            else
            {
                playerCreatures.creaturesOwned[i].SetActive(false);
            }
        }
    }
}
