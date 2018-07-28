using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopulateMenu : MonoBehaviour {

    public GameObject prefab; // This is our prefab object that will be exposed in the inspector

    public int numberToCreate; // number of objects to create. Exposed in inspector

    void Start()
    {
        
    }

    void Update()
    {

    }

    public List<GameObject> Populate(List<Sprite> sprites, List<string> descriptions, UnityAction methodToCallOnClick)
    {
        List<GameObject> newObj; // Create GameObject instance
        newObj = new List<GameObject>();

        for (int i = 0; i < numberToCreate; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj.Add((GameObject)Instantiate(prefab, transform));
            
            newObj[i].GetComponentInChildren<Text>().text = descriptions[i];
            newObj[i].GetComponentInChildren<Image>().sprite = sprites[i];
            newObj[i].GetComponentInChildren<Button>().onClick.AddListener(methodToCallOnClick);
        }

        return newObj;
    }
}
