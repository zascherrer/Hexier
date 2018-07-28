using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    private GameObject objectToPlace;
    private GameObject currentPlaceableObject;
    private PopulateMenu menuPopulator;
    public List<GameObject> trainerPrefabs;    //Assigned in the inspector
    private List<GameObject> buildingMenuItems;
    private List<Sprite> equipmentSprites;
    private List<string> equipmentDescriptions;
    private int childIndex;
    private bool menuPopulated;

	// Use this for initialization
	void Start () {
        childIndex = 2;

        buildingMenuItems = new List<GameObject>();
        equipmentSprites = new List<Sprite>();
        equipmentDescriptions = new List<string>();

        transform.GetChild(childIndex).GetChild(0).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(BuildingMenu);

	}
	
	// Update is called once per frame
	void Update () {
        CheckIfStorageUnit();
	}

    private void CheckIfStorageUnit()
    {
        if (SceneManager.GetActiveScene().name == "storageUnit1")
        {
            if (!transform.GetChild(childIndex).GetChild(0).gameObject.activeInHierarchy)
            {
                transform.GetChild(childIndex).GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            transform.GetChild(childIndex).GetChild(0).gameObject.SetActive(false);
        }
    }

    private void BuildingMenu()
    {
        transform.GetChild(childIndex).GetChild(0).GetChild(1).gameObject.SetActive(true);

        if (!menuPopulated)
        {
            menuPopulator = transform.GetChild(childIndex).GetChild(0).GetChild(1).GetChild(0).GetComponentInChildren<PopulateMenu>();
            
            foreach (var item in trainerPrefabs)
            {
                equipmentSprites.Add(item.GetComponent<SpriteRenderer>().sprite);
                equipmentDescriptions.Add(item.GetComponent<CreatureTrainer>().activeEquipment.Name + "\n" +
                        "$" + item.GetComponent<CreatureTrainer>().activeEquipment.Price + "\n" +
                        "Stats: " + item.GetComponent<CreatureTrainer>().activeEquipment.Stats);
            }

            buildingMenuItems = menuPopulator.Populate(equipmentSprites, equipmentDescriptions, DoNothing);

            AssignValuesToButtons(buildingMenuItems, trainerPrefabs);

            menuPopulated = true;
        }
    }

    private void DoNothing()
    {

    }

    void AssignValuesToButtons(List<GameObject> menuItems, List<GameObject> valuesToAssign)
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            GameObject temporary = valuesToAssign[i];
            menuItems[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { InstantiateEquipment(temporary); });
        }
    }

    void InstantiateEquipment(GameObject equipment)
    {

    }
}
