using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudentSubsystem : MonoBehaviour {
    public Button SandboxMode, Lab1Button, Lab2Button, Lab3Button;
    // Use this for initialization
    void Start () {
        SandboxMode.onClick.AddListener(EnterSandboxMode);
        Lab1Button.onClick.AddListener(EnterLab1);
        Lab2Button.onClick.AddListener(EnterLab2);
        Lab3Button.onClick.AddListener(EnterLab3);
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
    }

    private void EnterLab3()
    {
        Debug.Log("Lab 3 Button Clicked");
    }


	// Update is called once per frame
	void Update () {
		
	}
}
