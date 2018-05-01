using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Webcam : MonoBehaviour
{

    int currentCamIndex = 0;
    WebCamTexture tex;
    public RawImage display;
    private string _SavePath = "C://WebcamSnaps/";
    int _CaptureCounter = 0;


    public void RecordClicked()
    {
        Texture2D snap = new Texture2D(tex.width, tex.height);
        snap.SetPixels(tex.GetPixels());
        snap.Apply();
        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        Debug.Log(" Saved to " + _SavePath + _CaptureCounter.ToString() + ".png");
        ++_CaptureCounter;
    }




    // Use this for initialization
    void Start()
    {
        _SavePath = Directory.GetCurrentDirectory() + "/WebcamSnaps/";
        if (!Directory.Exists(_SavePath))
        {
            Directory.CreateDirectory(_SavePath);
        }
        if (tex != null)
        {
            display.texture = null;
            tex.Stop();
            tex = null;
        }
        else
        {

            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;
            tex.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}