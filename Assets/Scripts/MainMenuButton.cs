using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {
    public Button mainMenu;
    // Use this for initialization
    void Start () {
        mainMenu = GameObject.Find("MainMenu").GetComponent<Button>();
        mainMenu.onClick.AddListener(TransitionToStudentSubsystem);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void TransitionToStudentSubsystem()
    {
        SceneManager.LoadScene("Scenes/StudentSubsystem");
    }
}
