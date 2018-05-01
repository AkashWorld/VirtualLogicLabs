using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminButtonManager : MonoBehaviour {
    public Button MainUIButton, ChatButton, LogoutButton, CreateUserButton;
    public GameObject scrollView, createUser;
	// Use this for initialization
	void Start () {
        MainUIButton.onClick.AddListener(MainUI);
        ChatButton.onClick.AddListener(ChatUI);
        LogoutButton.onClick.AddListener(Logout);
        CreateUserButton.onClick.AddListener(AddUser);
        MainUI();
    }
	

    private void MainUI()
    {
        Destroy(scrollView);
        Destroy(createUser);
        GameObject newPrefab = Resources.Load<GameObject>("Prefabs/Subsystems/GradeScroll");
        scrollView = Instantiate(newPrefab) as GameObject;
        GameObject canvas = GameObject.Find("Canvas");
        scrollView.transform.parent = canvas.transform;
        RectTransform rectTransform = scrollView.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(65f, 10f, 0);
       
    }
    
    private void ChatUI()
    {
        Destroy(scrollView);
        Destroy(createUser);
    }

    private void Logout()
    {
        Destroy(scrollView);
        Destroy(createUser);
        SceneManager.LoadScene("Scenes/Login");    
    }

    private void AddUser()
    {
        Destroy(scrollView);
        Destroy(createUser);
        GameObject newPrefab = Resources.Load<GameObject>("Prefabs/Subsystems/CreateUser");
        scrollView = Instantiate(newPrefab) as GameObject;
        GameObject canvas = GameObject.Find("Canvas");
        scrollView.transform.parent = canvas.transform;
        RectTransform rectTransform = scrollView.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(65f, 10f, 0);

    }
	// Update is called once per frame
	void Update () {
		
	}
}
