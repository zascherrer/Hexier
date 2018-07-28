using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public GameObject gameCursor;

    private float cameraToCursor;
    private float rotation;
    private Vector3 target;
    private Vector3 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 temp = Input.mousePosition;
        temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        gameCursor.transform.position = Camera.main.ScreenToWorldPoint(temp);

               
    }
}
