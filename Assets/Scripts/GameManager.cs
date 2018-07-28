using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameManagerExists;

	// Use this for initialization
	void Start () {

        if (!gameManagerExists)
        {
            gameManagerExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
