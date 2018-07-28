using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatureInspector : MonoBehaviour {

    SpriteRenderer sprite;
    CreatureStatsManager creatureStats;
    CreatureController creatureController;
    PlayerController thePlayer;
    PlayerCreatureManager playerCreatures;
    CreatureInstanceManager creatureInstanceManager;

	// Use this for initialization
	void Start () {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        thePlayer = FindObjectOfType<PlayerController>();
        playerCreatures = thePlayer.GetComponent<PlayerCreatureManager>();
        creatureInstanceManager = FindObjectOfType<CreatureInstanceManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.tag == "Creature" || hit.transform.tag == "UI")
                {
                    Debug.Log(hit.collider.name + " has been clicked by mouse");
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    InspectCreature(hit);
                }
            }
            else
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }

    void InspectCreature(RaycastHit2D hit)
    {
        sprite = hit.collider.gameObject.GetComponent<SpriteRenderer>();
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = sprite.sprite;

        creatureStats = hit.collider.gameObject.GetComponent<CreatureStatsManager>();
        creatureController = hit.collider.gameObject.GetComponent<CreatureController>();

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().text = "ST: " + creatureStats.stats.stats.strength;
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Text>().text = "DX: " + creatureStats.stats.stats.dexterity;
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>().text = "IQ: " + creatureStats.stats.stats.intelligence;
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(5).GetComponent<Text>().text = "FT: " + creatureStats.stats.stats.fitness;
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(6).GetComponent<Text>().text = "Name: " + creatureStats.stats.GivenName;
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(7).GetComponent<Text>().text = "Species: " + creatureStats.stats.species;

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetComponent<Button>().onClick.AddListener(delegate { Rename(creatureStats); });
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).gameObject.SetActive(false);

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(10).GetComponent<Text>().text = creatureStats.stats.bio;

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<Button>().onClick.AddListener(Leash);
    }

    void Rename(CreatureStatsManager creature)
    {
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(1).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).gameObject.SetActive(true);

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).GetComponent<InputField>().onEndEdit.AddListener(GiveName);
    }

    void GiveName(string givenName)
    {
        foreach(GameObject item in playerCreatures.creaturesOwned)
        {
            if(item == creatureStats.gameObject)
            {
                item.GetComponent<CreatureStatsManager>().stats.GivenName = givenName;
            }
        }

        creatureStats.stats.GivenName = givenName;

        creatureStats.SetName();
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(6).GetComponent<Text>().text = "Name: " + creatureStats.stats.GivenName;

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).gameObject.SetActive(false);
    }

    void Leash()
    {
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<Button>().gameObject.SetActive(false);

        GameObject clone = creatureStats.gameObject;
        if (!creatureStats.isOwnedByPlayer)
        {
            creatureStats.isOwnedByPlayer = true;
            DontDestroyOnLoad(creatureStats.gameObject);
            creatureInstanceManager.levelCreatureLastSeenOn.Add(SceneManager.GetActiveScene().buildIndex);
            playerCreatures.creaturesOwned.Add(creatureStats.gameObject);
        }

        creatureStats.isLeashed = true;
        creatureController.isWandering = false;

        thePlayer.hasCreatureLeashed = true;
        thePlayer.leashedCreature = clone;

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(12).GetComponent<Button>().gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(12).GetComponent<Button>().onClick.AddListener(Release);
    }

    void Release()
    {
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(12).GetComponent<Button>().gameObject.SetActive(false);

        creatureStats.isLeashed = false;
        thePlayer.hasCreatureLeashed = false;
        thePlayer.leashedCreature = new GameObject();
        creatureController.isWandering = true;

        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<Button>().gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<Button>().onClick.AddListener(Leash);
    }
}
