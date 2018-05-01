using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserScript : MonoBehaviour {
    public InputField username, password;
    public Button createUserButton;
    DataInsert dataInsert;
    // Use this for initialization
    void Start () {
        createUserButton.onClick.AddListener(CreateUser);
        GameObject dataInsertGO = new GameObject("dbConn");
        dataInsertGO.transform.parent = this.gameObject.transform;
        dataInsert = dataInsertGO.AddComponent<DataInsert>();
    }
	
    private void CreateUser()
    {
        string usr = username.text;
        string pw = password.text;
        if(usr.Equals("") || pw.Equals(""))
        {
            return;
        }
        if (!dataInsert.CheckIfStudentExists(usr))
        {
            dataInsert.InsertStudentGrade(usr, pw, 0, 0);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
