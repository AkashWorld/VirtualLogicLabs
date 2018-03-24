using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruthTable : MonoBehaviour
{
    public Button button;
    public Text text;
    public GameObject inputField00;
    public GameObject inputField01;
    public GameObject inputField10;
    public GameObject inputField11;


    // Use this for initialization
    void Start()
    {       
        inputField00 = GameObject.Find("InputField (00)");
        inputField01 = GameObject.Find("InputField (01)");
        inputField10 = GameObject.Find("InputField (10)");
        inputField11 = GameObject.Find("InputField (11)");

        GameObject button00 = GameObject.Find("Button");
        Button btn = button00.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        Debug.Log("Clicked");
        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        InputField field00 = inputField00.GetComponent<InputField>();
        InputField field01 = inputField01.GetComponent<InputField>();
        InputField field10 = inputField01.GetComponent<InputField>();
        InputField field11 = inputField11.GetComponent<InputField>();

        if (field00.text == "0" && field01.text == "1" && field10.text == "1" && field11.text == "1")
        {
            message.text = "That's Right!";
        }
        else
        {
            message.text = "That's Wrong";
        }
    }
}
