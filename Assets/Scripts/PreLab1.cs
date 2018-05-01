using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreLab1 : MonoBehaviour {

    public Button button;
    public Text text;
    public GameObject inputfield000;
    public GameObject inputfield001;
    public GameObject inputfield010;
    public GameObject inputfield011;
    public GameObject inputfield100;
    public GameObject inputfield101;
    public GameObject inputfield110;
    public GameObject inputfield111;
    DataInsert Grader;
    string Owner;
    double prelabGrade = 10;
    // Use this for initialization
    void Start () {
        GameObject grader = new GameObject("Grader");
        Grader = grader.AddComponent<DataInsert>();
        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void TaskOnClick()
    {
        Debug.Log("Clicked");
        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        InputField field000 = inputfield000.GetComponent<InputField>();
        InputField field001 = inputfield001.GetComponent<InputField>();
        InputField field010 = inputfield010.GetComponent<InputField>();
        InputField field011 = inputfield011.GetComponent<InputField>();
        InputField field100 = inputfield100.GetComponent<InputField>();
        InputField field101 = inputfield101.GetComponent<InputField>();
        InputField field110 = inputfield110.GetComponent<InputField>();
        InputField field111 = inputfield111.GetComponent<InputField>();

        if(field000.text == "0" &&
            field001.text == "0" &&
            field010.text == "0" &&
            field011.text == "1" &&
            field100.text == "0" &&
            field101.text == "0" &&
            field110.text == "1" &&
            field111.text == "1")
        {
            message.text = "That's right!";
            DataInsert.inputLab1Grade = (int)prelabGrade;
            StartCoroutine(TransitionToLab1());
        }
        else
        {
            message.text = "That's wrong. " +
                "Try again.";
            if(prelabGrade > 0)
            {
                prelabGrade--;
            }
        }

    }
    private IEnumerator TransitionToLab1()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scenes/Lab1");
        yield break;
    }
}
