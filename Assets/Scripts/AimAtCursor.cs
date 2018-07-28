using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtCursor : MonoBehaviour {

    public GameObject cursor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -(transform.position.x - Camera.main.transform.position.x);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        */
        Vector3 worldPos = cursor.transform.position;
        transform.LookAt(worldPos);


    }
}
