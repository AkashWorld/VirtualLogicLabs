using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudentSubsystem : MonoBehaviour {
    public Button SandboxMode, Lab1Button, Lab2Button, CameraButton, ChatButton;
    public Text Lab1Avg, Lab2Avg, currScore1, currScore2, username;
    DataInsert dataInsert;
    // Use this for initialization
    void Start () {
        dataInsert = new DataInsert();
        Debug.Log("Username: " + DataInsert.inputStudent + " Password: " 
            + DataInsert.inputPassword + " Lab1: " + DataInsert.inputLab1Grade 
            + " Lab2: " + DataInsert.inputLab2Grade);
        SandboxMode.onClick.AddListener(EnterSandboxMode);
        Lab1Button.onClick.AddListener(EnterLab1);
        Lab2Button.onClick.AddListener(EnterLab2);
        CameraButton.onClick.AddListener(EnterCamera);
        Lab1Avg = GameObject.Find("Lab1Avg").GetComponent<Text>();
        Lab2Avg = GameObject.Find("Lab2Avg").GetComponent<Text>();
        currScore1 = GameObject.Find("currScore1").GetComponent<Text>();
        currScore2 = GameObject.Find("currScore2").GetComponent<Text>();
        username = GameObject.Find("Username").GetComponent<Text>();
        currScore1.text = DataInsert.inputLab1Grade + "";
        currScore2.text = DataInsert.inputLab2Grade + "";
        Lab1Avg.text = DataInsert.lab1avg + "";
        Lab2Avg.text = DataInsert.lab2avg + "";
        username.text = DataInsert.inputStudent;

    }
	
    private void EnterSandboxMode()
    {
        Debug.Log("Sandbox Button Clicked");
        SceneManager.LoadScene("Scenes/SandboxLab");
    }

    private void EnterLab1()
    {
        Debug.Log("Lab 1 Button Clicked");
        SceneManager.LoadScene("Scenes/Prelab1");
    }

    private void EnterLab2()
    {
        Debug.Log("Lab 2 Button Clicked");
        SceneManager.LoadScene("Scenes/Prelab2");
    }

    private void EnterCamera()
    {
        Debug.Log("Camera Button Clicked");
        SceneManager.LoadScene("Scenes/Camera");
    }

    private void EnterChat()
    {
        Debug.Log("Chat button clicked");
        
    }


	// Update is called once per frame
	void Update () {
		
	}
}
