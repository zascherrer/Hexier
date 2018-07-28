using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {

    SpriteRenderer npcSprite;
    SpriteRenderer playerSprite;
    PlayerController player;
    public NPCDialogueManager dialogueManager;
    //NPCStatsManager npcStats;

    float textSpeed;
    public bool isTalking;

    public class DialogueParameters
    {
        public List<string> dialogue;
        public Transform canvasTransform;
        public bool moreConfirmationBoxes;

        public DialogueParameters(List<string> dialogue, Transform canvasTransform, bool moreConfirmationBoxes)
        {
            this.dialogue = dialogue;
            this.canvasTransform = canvasTransform;
            this.moreConfirmationBoxes = moreConfirmationBoxes;
        }
    }

    // Use this for initialization
    void Start () {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        playerSprite = this.gameObject.GetComponentInParent<SpriteRenderer>();
        player = this.gameObject.GetComponentInParent<PlayerController>();

        textSpeed = 75;             //This is equivalent to characters per second
        textSpeed = 1 / textSpeed;
        isTalking = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.tag == "NPC")
                {
                    Debug.Log(hit.collider.name + " has been clicked by mouse");
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    isTalking = true;
                    dialogueManager = hit.collider.gameObject.GetComponent<NPCDialogueManager>();
                    InspectNPC(hit, dialogueManager.Aine_Introduction_AtPortal());
                }
            }
            else if (isTalking)
            {
                //Do nothing; clicking does something later on in the code.
            }
            else
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void InspectNPC(RaycastHit2D hit, List<string> dialogue)
    {
        npcSprite = hit.collider.gameObject.GetComponent<SpriteRenderer>();
        //npcStats = hit.collider.gameObject.GetComponent<NPCStatsManager>();
        Transform canvasTransform = this.gameObject.transform.GetChild(0);

        canvasTransform.GetChild(1).GetComponent<Image>().sprite = playerSprite.sprite;
        canvasTransform.GetChild(2).GetComponent<Image>().sprite = npcSprite.sprite;

        canvasTransform.GetChild(4).GetComponent<Text>().text = hit.collider.name;
        canvasTransform.GetChild(5).GetComponent<Text>().text = player.playerName;

        DialogueParameters defaultParameters = new DialogueParameters(dialogue, canvasTransform, false);
        StartCoroutine(StartDialogue(defaultParameters));
    }

    public void InspectNPC(RaycastHit2D hit, List<string> dialogue, List<string> dialogueYes, List<string> dialogueNo)
    {
        npcSprite = hit.collider.gameObject.GetComponent<SpriteRenderer>();
        //npcStats = hit.collider.gameObject.GetComponent<NPCStatsManager>();
        Transform canvasTransform = this.gameObject.transform.GetChild(0);

        canvasTransform.GetChild(1).GetComponent<Image>().sprite = playerSprite.sprite;
        canvasTransform.GetChild(2).GetComponent<Image>().sprite = npcSprite.sprite;

        canvasTransform.GetChild(4).GetComponent<Text>().text = hit.collider.name;
        canvasTransform.GetChild(5).GetComponent<Text>().text = player.playerName;
        
        DialogueParameters yesButtonParameters = new DialogueParameters(dialogueYes, canvasTransform, false);
        canvasTransform.GetChild(7).GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(StartDialogue(yesButtonParameters)); });
        
        DialogueParameters noButtonParameters = new DialogueParameters(dialogueNo, canvasTransform, false);
        canvasTransform.GetChild(8).GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(StartDialogue(noButtonParameters)); });

        DialogueParameters defaultParamaters = new DialogueParameters(dialogue, canvasTransform, true);
        StartCoroutine(StartDialogue(defaultParamaters));
    }

    IEnumerator StartDialogue(DialogueParameters parameters)
    {
        int counter = 0;

        if (!parameters.moreConfirmationBoxes)
        {
            parameters.canvasTransform.GetChild(7).GetComponent<Button>().gameObject.SetActive(false);
            parameters.canvasTransform.GetChild(8).GetComponent<Button>().gameObject.SetActive(false);
        }

        foreach(string text in parameters.dialogue)
        {
            if (counter > 0)
            {
                yield return StartCoroutine(dialogueManager.WaitForMouseClick());
                yield return StartCoroutine(dialogueManager.WaitForMouseClick());       //I don't know why this second WaitForMouseClick is necessary, but if it's
                                                                                        //not here, the dialogue tries to play two lines at the same time.
            }

            StartCoroutine(TextScroll(text, parameters.canvasTransform));

            counter++;
        }

        if (parameters.moreConfirmationBoxes)
        {
            parameters.canvasTransform.GetChild(7).GetComponent<Button>().gameObject.SetActive(true);
            parameters.canvasTransform.GetChild(8).GetComponent<Button>().gameObject.SetActive(true);
        }
        else
        {
            isTalking = false;
        }

    }

    IEnumerator TextScroll(string text, Transform canvasTransform)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(textSpeed);
            stringBuilder.Append(text[i]);
            canvasTransform.GetChild(6).GetComponent<Text>().text = stringBuilder.ToString();

            if (Input.GetMouseButtonDown(0))
            {
                canvasTransform.GetChild(6).GetComponent<Text>().text = text;
                break;
            }
        }
    }
}
