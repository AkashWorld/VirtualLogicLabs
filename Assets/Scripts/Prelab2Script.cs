using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prelab2Script : MonoBehaviour
{

    private Button button;
    private Text text;
    private GameObject SInput0, SInput1, SInput2, SInput3, SInput4, SInput5, SInput6, SInput7;
    private GameObject CoInput0, CoInput1, CoInput2, CoInput3, CoInput4, CoInput5, CoInput6, CoInput7;
    int prelab2Grade = 10;
    // Use this for initialization
    void Start()
    {
        SInput0 = GameObject.Find("SInput0");
        SInput1 = GameObject.Find("SInput1");
        SInput2 = GameObject.Find("SInput2");
        SInput3 = GameObject.Find("SInput3");
        SInput4 = GameObject.Find("SInput4");
        SInput5 = GameObject.Find("SInput5");
        SInput6 = GameObject.Find("SInput6");
        SInput7 = GameObject.Find("SInput7");
        CoInput0 = GameObject.Find("CoInput0");
        CoInput1 = GameObject.Find("CoInput1");
        CoInput2 = GameObject.Find("CoInput2");
        CoInput3 = GameObject.Find("CoInput3");
        CoInput4 = GameObject.Find("CoInput4");
        CoInput5 = GameObject.Find("CoInput5");
        CoInput6 = GameObject.Find("CoInput6");
        CoInput7 = GameObject.Find("CoInput7");

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();
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

        InputField IFSInput0 = SInput0.GetComponent<InputField>();
        InputField IFSInput1 = SInput1.GetComponent<InputField>();
        InputField IFSInput2 = SInput2.GetComponent<InputField>();
        InputField IFSInput3 = SInput3.GetComponent<InputField>();
        InputField IFSInput4 = SInput4.GetComponent<InputField>();
        InputField IFSInput5 = SInput5.GetComponent<InputField>();
        InputField IFSInput6 = SInput6.GetComponent<InputField>();
        InputField IFSInput7 = SInput7.GetComponent<InputField>();
        InputField IFCoInput0 = CoInput0.GetComponent<InputField>();
        InputField IFCoInput1 = CoInput1.GetComponent<InputField>();
        InputField IFCoInput2 = CoInput2.GetComponent<InputField>();
        InputField IFCoInput3 = CoInput3.GetComponent<InputField>();
        InputField IFCoInput4 = CoInput4.GetComponent<InputField>();
        InputField IFCoInput5 = CoInput5.GetComponent<InputField>();
        InputField IFCoInput6 = CoInput6.GetComponent<InputField>();
        InputField IFCoInput7 = CoInput7.GetComponent<InputField>();

        if (IFSInput0.text == "0" &&
            IFSInput1.text == "1" &&
            IFSInput2.text == "1" &&
            IFSInput3.text == "0" &&
            IFSInput4.text == "1" &&
            IFSInput5.text == "0" &&
            IFSInput6.text == "0" &&
            IFSInput7.text == "1" &&
            IFCoInput0.text == "0" &&
            IFCoInput1.text == "0" &&
            IFCoInput2.text == "0" &&
            IFCoInput3.text == "1" &&
            IFCoInput4.text == "0" &&
            IFCoInput5.text == "1" &&
            IFCoInput6.text == "1" &&
            IFCoInput7.text == "1")
        {
            message.text = "That's right!";
            DataInsert.inputLab2Grade = prelab2Grade;
            StartCoroutine(TransitionToLab2());
        }
        else
        {
            message.text = "That's wrong. " +
                "Try again.";
            if(prelab2Grade > 0)
            {
                prelab2Grade--;
            }
        }

    }
    private IEnumerator TransitionToLab2()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scenes/Lab2");
        yield break;
    }
}