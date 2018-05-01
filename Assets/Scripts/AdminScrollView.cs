using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminScrollView : MonoBehaviour {
    DataInsert dataInsert;
    public GameObject scrollViewContent;
    private Boolean isDatafound = false;
    public Button CSVButton;
    // Use this for initialization
    void Start () {
        GameObject dataInsertGO = new GameObject("dbConn");
        dataInsertGO.transform.parent = this.gameObject.transform;
        dataInsert = dataInsertGO.AddComponent<DataInsert>();
        CSVButton.onClick.AddListener(CSVOutput);
    }
	
	// Update is called once per frame
	void Update () {
		if(isDatafound == false && dataInsert.isDataObtained == true)
        {
            string[] studentNames = dataInsert.getAllStudentsName();
            Debug.Log("Scroll view, studentNames size: " + studentNames.Length);
            for(int i = 0; i < studentNames.Length; i++)
            {
                string studentName = studentNames[i];
                int grade1 = dataInsert.GetStudentLab1Grade(studentName);
                int grade2 = dataInsert.GetStudentLab2Grade(studentName);
                GameObject studentNameGO = new GameObject("StudentName" + i);
                Text studentNameText = studentNameGO.AddComponent<Text>();
                GameObject grade1GO = new GameObject("grade1" + i);
                Text grade1Text = grade1GO.AddComponent<Text>();
                GameObject grade2GO = new GameObject("grade2" + i);
                Text grade2Text = grade2GO.AddComponent<Text>();
                studentNameText.text = studentName;
                grade1Text.text = grade1 + "";
                grade2Text.text = grade2 + "";
                studentNameText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                grade1Text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                grade2Text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                studentNameText.color = Color.black;
                grade1Text.color = Color.black;
                grade2Text.color = Color.black;
                studentNameGO.transform.parent = scrollViewContent.transform;
                grade1GO.transform.parent = scrollViewContent.transform;
                grade2GO.transform.parent = scrollViewContent.transform;
                studentNameGO.transform.localPosition = new Vector3(82.38f, -80f - i*20f, 0);
                grade1GO.transform.localPosition = new Vector3(194.97f, -80f - i*20f, 0);
                grade2GO.transform.localPosition = new Vector3(304.97f, -80f - i*20f, 0);
            }
            isDatafound = true;
        }
	}

    private void CSVOutput()
    {
        dataInsert.PrintToCSVFormat();
    }
}
