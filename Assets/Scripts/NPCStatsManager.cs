using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCStatsManager : MonoBehaviour {

    public bool hasDialogue;
    public Sprite portrait;

	// Use this for initialization
	void Start () {
        portrait = IdentifyCharacter();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Sprite IdentifyCharacter()
    {
        if(this.gameObject.name == "Aine")
        {
            hasDialogue = true;
            Debug.Log("Portrait found!");
            return Resources.Load("Portraits/Aine") as Sprite;
        }
        else
        {
            return Resources.Load("Portraits/Aine") as Sprite;
        }
    }
}
