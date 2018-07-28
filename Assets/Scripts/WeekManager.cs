using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Week
{
    private int days;
    public int Days
    {
        get
        {
            return days;
        }
    }

    public Week()
    {
        days = 7;
    }



}

public class WeekManager : MonoBehaviour {

    private static List<Week> weeks = new List<Week>();
    private PlayerCreatureManager playerCreatures;
    private TrainingManager trainingManager;

	// Use this for initialization
	void Start () {
        playerCreatures = FindObjectOfType<PlayerCreatureManager>();

        trainingManager = GetComponent<TrainingManager>();

        transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(NewWeek);

        NewWeek();
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "storageUnit1")
        {
            if (!transform.GetChild(1).GetChild(0).gameObject.activeInHierarchy)
            {
                transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
    }

    private void NewWeek()
    {
        weeks.Add(new Week());

        foreach(var creature in playerCreatures.creaturesOwned)
        {
            creature.GetComponent<CreatureStatsManager>().stats.lifeExpectancy -= weeks[weeks.Count - 1].Days;
        }

        TrainCreatures();
    }

    void TrainCreatures()
    {
        foreach (var equipment in trainingManager.selectedEquipment)
        {
            Trainer activeEquipment = equipment.GetComponent<CreatureTrainer>().activeEquipment;

            if (activeEquipment.creatureOne != null)
            {
                CreatureList.Creature creatureOne = activeEquipment.creatureOne.GetComponent<CreatureStatsManager>().stats;
                CreatureList.Creature creatureTwo = null;

                if (activeEquipment.creatureTwo != null)
                {
                    creatureTwo = activeEquipment.creatureTwo.GetComponent<CreatureStatsManager>().stats;
                }

                activeEquipment.Train(creatureOne, creatureTwo);

                ResetCreature(activeEquipment, creatureOne, activeEquipment.creatureOne);
                activeEquipment.creatureOne = null;

                if (activeEquipment.creatureTwo != null)
                {
                    ResetCreature(activeEquipment, creatureTwo, activeEquipment.creatureTwo);
                    activeEquipment.creatureTwo = null;
                }
            }
        }
    }

    void ResetCreature(Trainer activeEquipment, CreatureList.Creature creatureGiven, GameObject creatureGameObject)
    {
        activeEquipment.creatureOne.GetComponent<CreatureController>().isTraining = false;
        activeEquipment.creatureOne.GetComponent<CreatureController>().isWandering = true;

        foreach (var creature in playerCreatures.creaturesOwned)
        {
            if (creature.GetComponent<CreatureStatsManager>().stats.GivenName == creatureGameObject.GetComponent<CreatureStatsManager>().stats.GivenName && creature.GetComponent<CreatureStatsManager>().stats.species == creatureGameObject.GetComponent<CreatureStatsManager>().stats.species)
            {
                creature.GetComponent<CreatureStatsManager>().stats = creatureGiven;
            }
        }
    }
    
}
