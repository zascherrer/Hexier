using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController thePlayer;
    private CameraController theCamera;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();

        thePlayer.transform.position = this.transform.position;
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
