using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class ReadImageFromWeb : MonoBehaviour {

    //GetTextFromPlayer playerText;

	// Use this for initialization
	void Start () {

        //playerText = GetComponent<GetTextFromPlayer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public byte[] DownloadData(string url)
    {
        byte[] downloadedData = new byte[0];
        try
        {
            //Get a data stream from the url
            WebRequest req = WebRequest.Create(url);
            WebResponse response = req.GetResponse();
            Stream stream = response.GetResponseStream();

            //Download in chunks
            byte[] buffer = new byte[1024];

            //Get Total Size
            int dataLength = (int)response.ContentLength;
            
            //Download to memory
            //Note: adjust the streams here to download directly to the hard drive
            MemoryStream memStream = new MemoryStream();
            while (true)
            {
                //Try to read the data
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }
                else
                {
                    //Write the downloaded data
                    memStream.Write(buffer, 0, bytesRead);
                    
                }
            }

            //Convert the downloaded stream to a byte array
            downloadedData = memStream.ToArray();

            //Clean up
            stream.Close();
            memStream.Close();
        }
        catch (Exception)
        {
            //May not be connected to the internet
            //Or the URL might not exist
            Debug.Log("There was an error accessing the URL.");
        }

        return downloadedData;
    }
 
}
