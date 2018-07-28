using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreature : MonoBehaviour {

    private string creatureName;
    public GameObject creature;
    private TextToColors textToColors;
    private TextToColors.ColorAverages averageColor;
    private CreatureList creatureTypes;
    private List<CreatureList.Creature> creatures;
    private DialogueBox dialogueBox;
    private GameObject player;
    

	// Use this for initialization
	void Start () {
        creature = new GameObject();
        textToColors = GetComponentInParent<TextToColors>();
        averageColor = new TextToColors.ColorAverages();

        player = GameObject.FindGameObjectWithTag("Player");
        dialogueBox = player.GetComponentInChildren<DialogueBox>();

        creatureTypes = GetComponentInParent<CreatureList>();

        //creatureName = "Ratcher";
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Spawn()
    {
        averageColor = textToColors.averageColors;
        SetCreatureName();

        for (int j = 0; j < 2; j++)             //This loop and the Destroy coroutine at the end are a workaround for a bug causing creature statistics not to update when spawned the first time
        {
            GameObject newCreature = CreateCreature();

            for (int i = 0; i < creatureTypes.creatures.Count; i++)
            {
                if (creatureName == creatureTypes.creatures[i].GivenName)
                {
                    CreatureStatsManager creatureStats = newCreature.GetComponent<CreatureStatsManager>();
                    creatureStats.SetStats(creatureTypes.creatures[i]);
                    CreatureConfirmationDialogue(creatureStats);

                    Debug.Log("I'm a " + creatureStats.stats.GivenName + "!");
                    creatureStats.SetName();
                }
            }

            if(j == 0)
            {
                StartCoroutine(DestroyCreature(newCreature));
            }
        }
    }

    GameObject CreateCreature()
    {
        creature = (GameObject)Resources.Load(creatureName);

        var clone = Instantiate(creature, this.transform.position, Quaternion.Euler(Vector3.zero));

        return clone;
    }

    IEnumerator DestroyCreature(GameObject creature)
    {
        yield return null;
        Destroy(creature);
    }

    void CreatureConfirmationDialogue(CreatureStatsManager creatureStats)
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(npcs[0].transform.position.x, npcs[0].transform.position.y), Vector2.zero, 0);

        foreach(GameObject npc in npcs)
        {
            if(npc.name == "Aine")
            {
                hits = Physics2D.RaycastAll(new Vector2(npc.transform.position.x, npc.transform.position.y), Vector2.zero, 0);
            }
        }

        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.name + " hit");
            if (hit.collider.name == "Aine")
            {
                NPCDialogueManager dialogueManager = hit.collider.gameObject.GetComponent<NPCDialogueManager>();
                List<string> dialogue = dialogueManager.Aine_CreatureConfirmation_Default();
                List<string> dialogueYes = dialogueManager.Aine_CreatureConfirmation_Default_Yes();
                List<string> dialogueNo = dialogueManager.Aine_CreatureConfirmation_Default_No();

                dialogueBox.isTalking = true;
                dialogueBox.dialogueManager = dialogueManager;
                dialogueBox.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                dialogueBox.InspectNPC(hit, dialogue, dialogueYes, dialogueNo);
            }
        }
    }

    void SetCreatureName()
    {
        if (averageColor.r >= 150)
        {
            creatureName = "Laretyme";
        }
        else if(averageColor.r == 139 && averageColor.g == 127)
        {
            creatureName = "Cool Murderdeathatron";
        }
        else if (averageColor.r >= 125)
        {
            if (averageColor.b >= 125)
                creatureName = "Ice Ratcher";
            else if (averageColor.g >= 125)
                creatureName = "Wind Ratcher";
            else
                creatureName = "Ratcher";
        }
        else if (averageColor.r >= 120)
        {
            creatureName = "Toxic Boi";
        }
        else if(averageColor.r >= 115)
        {
            if (averageColor.g > 250 && averageColor.b > 250)
                creatureName = "Cool Murderdeathatron";
            else
                creatureName = "Murderdeathatron";
        }
        else if(averageColor.r >= 110)
        {
            creatureName = "Burdek";
        }
        else if(averageColor.r >= 105)
        {
            creatureName = "Krocosilisk";
        }
        else if(averageColor.r >= 100)
        {
            creatureName = "Spiny Anteater";
        }
        else if(averageColor.r >= 80)
        {
            creatureName = "Hermit Squid";
        }
        else if(averageColor.r == 0)
        {
            if (averageColor.g == 0 && averageColor.b == 0)
                creatureName = "Void Ratcher";
            else
                creatureName = "Burdek";
        }
        else
        {
            creatureName = "Burdek";
        }
    }
}
