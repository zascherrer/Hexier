using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour {

    public List<string> returnDialogue;

	// Use this for initialization
	void Start () {
        returnDialogue = new List<string>();

        returnDialogue = Aine_Introduction_AtPortal();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator WaitForMouseClick()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }

    public List<string> Aine_Introduction_AtPortal()
    {
        List<string> dialogue = new List<string>();

        dialogue.Add("Hey you! Get out of here! I don't have any scraps to feed you.");
        dialogue.Add("What's that? You're a hexier? Let me see your license...");
        dialogue.Add("Wow, you weren't lying. Go figure. Well, I guess I should introduce myself. I'm Aine, the portal technician at this lab.");
        dialogue.Add("Basically, I get to look inside your head, pick out the image you're thinking of and put it into the portal, then we both get to see what pops out. Exciting, isn't it?");
        dialogue.Add("...Well there's no point in waiting around, let's find you a creature!");
        dialogue.Add("[[Click on the portal, then enter the direct URL of an image on the web. You'll know you have the right URL when, if you enter the URL into your browser, you only see the image with no other web content.]]");

        return dialogue;
    }

    public List<string> Aine_CreatureConfirmation_Default()
    {
        List<string> dialogue = new List<string>();

        dialogue.Add("Cool creature! Do you want to keep it?");

        return dialogue;
    }

    public List<string> Aine_CreatureConfirmation_Default_Yes()
    {
        List<string> dialogue = new List<string>();

        dialogue.Add("Great! Well, all that's left is to give it a name and take it out of here. And you, uh, might want to leash it before it wanders anywhere on its own.");
        dialogue.Add("[[Right click on your creature to open the Creature Inspector. From there, you can rename and leash it to claim it as your own.]]");
        dialogue.Add("[[To leave or enter any room with your creature, you must have it leashed. Otherwise, it will remain in the room you left it in.]]");

        return dialogue;
    }

    public List<string> Aine_CreatureConfirmation_Default_No()
    {
        List<string> dialogue = new List<string>();

        dialogue.Add("No? If you say so. The homeless won't go hungry tonight.");
        dialogue.Add("Hah! I'm kidding. We sell them to restaurants.");

        return dialogue;
    }
}
