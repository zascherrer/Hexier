using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingManager : MonoBehaviour {

    /// <summary>
    /// Outline of which training does what:
    /// 
    /// Basic Training:
    /// PunchingBag = ^Strength
    /// Pounce = ^Dexterity
    /// Puzzles = ^Intelligence
    /// Run = ^Fitness
    /// 
    /// Paired Training:
    /// Sparring Ring = ^ST, ^DX for both creatures, chance of injury
    /// Tactical Games = ^^IQ for both creatures, vFitness
    /// Competitive Swim = ^^FT for one creature, ^FT for other
    /// Tug-of-War = ^^ST for one creature, ^ST for other
    /// Catch = ^^DX for both creatures, vIQ
    /// 
    /// </summary>
    CreatureTrainer[] playerTrainingEquipment;
    List<Sprite> equipmentSprites;
    List<string> equipmentDescriptions;
    List<Sprite> creatureSprites;
    List<string> creatureDescriptions;
    PopulateMenu equipmentMenuPopulater;
    PopulateMenu creatureAssignmentMenuPopulater;
    CreatureInstanceManager creatureInstanceManager;
    List<GameObject> equipmentMenuItems;
    List<GameObject> creatureMenuItems;
    public List<GameObject> selectedEquipment;
    public List<GameObject> selectedCreature;

    bool menuPopulated;
    bool creatureMenuPopulated;
    
	// Use this for initialization
	void Start () {

        menuPopulated = false;
        creatureMenuPopulated = false;
        equipmentSprites = new List<Sprite>();
        equipmentDescriptions = new List<string>();
        creatureSprites = new List<Sprite>();
        creatureDescriptions = new List<string>();
        creatureInstanceManager = GetComponent<CreatureInstanceManager>();
        equipmentMenuItems = new List<GameObject>();
        creatureMenuItems = new List<GameObject>();
	}

    // Update is called once per frame
    void Update () {
        if (SceneManager.GetActiveScene().name == "storageUnit1")
        {
            if (!transform.GetChild(0).GetChild(0).GetChild(0).gameObject.activeInHierarchy)
            {
                transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);

                transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(EquipmentMenu);
            }
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    void EquipmentMenu()
    {
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);

        if (!menuPopulated)
        {
            playerTrainingEquipment = FindObjectsOfType<CreatureTrainer>();

            equipmentMenuPopulater = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponentInChildren<PopulateMenu>();
            creatureAssignmentMenuPopulater = transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponentInChildren<PopulateMenu>();

            equipmentMenuPopulater.numberToCreate = playerTrainingEquipment.Length;

            for (int i = 0; i < playerTrainingEquipment.Length; i++)
            {
                CreatureTrainer item = playerTrainingEquipment[i];

                equipmentSprites.Add(item.gameObject.GetComponent<SpriteRenderer>().sprite);
                equipmentDescriptions.Add(item.activeEquipment.Name + "\n" +
                    "$" + item.activeEquipment.Price + "\n" +
                    "Stats: " + item.activeEquipment.Stats);
            }

            equipmentMenuItems = equipmentMenuPopulater.Populate(equipmentSprites, equipmentDescriptions, CreatureAssignmentMenu);

            List<GameObject> equipmentList = new List<GameObject>();
            for(int i = 0; i < playerTrainingEquipment.Length; i++)
            {
                equipmentList.Add(playerTrainingEquipment[i].gameObject);
            }

            AssignValuesToButtons(equipmentMenuItems, equipmentList);

            menuPopulated = true;
        }
    }
    
    void CreatureAssignmentMenu()
    {
        transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);

        if (!creatureMenuPopulated)
        {
            creatureAssignmentMenuPopulater.numberToCreate = creatureInstanceManager.playerCreatures.creaturesOwned.Count;

            for (int i = 0; i < creatureInstanceManager.playerCreatures.creaturesOwned.Count; i++)
            {
                GameObject item = creatureInstanceManager.playerCreatures.creaturesOwned[i];
                CreatureStatsManager itemStats = item.GetComponent<CreatureStatsManager>();

                creatureSprites.Add(item.GetComponent<SpriteRenderer>().sprite);
                creatureDescriptions.Add(itemStats.stats.GivenName + "\n" +
                    "ST: " + itemStats.stats.stats.strength + "     DX: " + itemStats.stats.stats.dexterity + "\n" +
                    "IQ: " + itemStats.stats.stats.intelligence + "     FT: " + itemStats.stats.stats.fitness + "\n");

            }

            creatureMenuItems = creatureAssignmentMenuPopulater.Populate(creatureSprites, creatureDescriptions, DoNothing);

            AssignValuesToButtons(creatureMenuItems, creatureInstanceManager.playerCreatures.creaturesOwned);

            creatureMenuPopulated = true;
        }
    }

    void DoNothing()
    {
        //Placeholder because the method that was going to be assigned in its place needs to come after AssignValuesToButtons
    }

    void AssignCreatureToTraining(int index)
    {
        GameObject selectedCreature = selectedEquipment[index].GetComponent<CreatureTrainer>().activeEquipment.creatureOne;

        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);

        selectedCreature.transform.position = selectedEquipment[index].transform.GetChild(0).position;
        selectedCreature.gameObject.GetComponent<CreatureController>().isWandering = false;
        selectedCreature.gameObject.GetComponent<CreatureController>().isTraining = true;
        selectedCreature.gameObject.GetComponent<CreatureController>().trainingPosition = selectedEquipment[index].transform.GetChild(0);

        
    }

    void AssignValuesToButtons(List<GameObject> menuItems, List<GameObject> valuesToAssign)
    {
        for(int i = 0; i < menuItems.Count; i++)
        {
            GameObject temporary = valuesToAssign[i];
            menuItems[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { DetermineIfEquipmentOrCreature(temporary); });
        }
    }

    void DetermineIfEquipmentOrCreature(GameObject value)
    {
        if(value.GetComponent<CreatureStatsManager>() == null)
        {
            selectedEquipment.Add(value);
        }
        else
        {
            selectedEquipment[selectedEquipment.Count - 1].GetComponent<CreatureTrainer>().activeEquipment.creatureOne = value;
            AssignCreatureToTraining(selectedEquipment.Count - 1);
        }
    }
}
