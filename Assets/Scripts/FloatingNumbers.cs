using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{

    public float moveSpeed;
    public int dmgNumber;
    public Text displayNumber;

    // Use this for initialization
    void Start()
    {

        displayNumber.text = "" + dmgNumber;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);

    }
}
