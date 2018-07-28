using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class TextToColors : MonoBehaviour {

    private InputField input;
    private string Url { get; set; }
    private Color[] colors;
    private Color[] colors32;
    public ColorAverages averageColors;
    private SpawnCreature creatureSpawn;

    
    public class ColorAverages
    {
        public int r = 0;
        public int g = 0;
        public int b = 0;
        public int a = 0;
    }

	// Use this for initialization
	void Start () {

        input = this.GetComponentInChildren<InputField>();
        input.text = "Enter an image URL here...";
        input.gameObject.SetActive(false);

        colors = new Color[1];
        averageColors = new ColorAverages();
        creatureSpawn = this.GetComponentInChildren<SpawnCreature>();

        input.onEndEdit.AddListener(SubmitURL);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.tag == "Portal")
                {
                    Debug.Log("Portal has been clicked by mouse");

                    input.gameObject.SetActive(true);
                }
            }
        }
        
	}

    void SubmitURL(string userUrl)
    {
        Debug.Log(userUrl);

        Url = userUrl;
        StartCoroutine(GetColorsFromImageOnWeb(Url));

        input.gameObject.SetActive(false);
    }

    IEnumerator GetColorsFromImageOnWeb(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        byte[] imageData = www.bytes;

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        GetComponent<Renderer>().material.mainTexture = texture;
        colors = texture.GetPixels(0, 0, texture.width, texture.height);

        Debug.Log(colors[0].r);

        ConvertColorsToColors32();
        SetColorAverages();

        creatureSpawn.Spawn();                           //Okay, putting this here violates SOLID principles a little, but it makes everything easier

        //yield return null;
    }
    
    void ConvertColorsToColors32()
    {
        colors32 = new Color[colors.Length];

        for(int i = 0; i < colors.Length; i++)
        {
            colors32[i].r = colors[i].r * 255;
            colors32[i].g = colors[i].g * 255;
            colors32[i].b = colors[i].b * 255;
            colors32[i].a = colors[i].a * 255;
        }
    }

    void SetColorAverages()
    {
        averageColors.r = 0;
        averageColors.g = 0;
        averageColors.b = 0;
        averageColors.a = 0;

        for(int i = 0; i < colors.Length; i++)
        {
            averageColors.r += Convert.ToInt32(colors32[i].r);
            averageColors.g += Convert.ToInt32(colors32[i].g);
            averageColors.b += Convert.ToInt32(colors32[i].b);
            averageColors.a += Convert.ToInt32(colors32[i].a);
        }

        averageColors.r /= colors32.Length;
        averageColors.g /= colors32.Length;
        averageColors.b /= colors32.Length;
        averageColors.a /= colors32.Length;

        Debug.Log(averageColors.r);
        Debug.Log(averageColors.g);
        Debug.Log(averageColors.b);
        Debug.Log(averageColors.a);
    }
}
