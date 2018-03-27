using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GradingCONSTANTS{
    public static string INPUT = "INPUTSWITCH";
    public static string OUTPUT = "OUTPUTLED";
}
public class LabOneGrader : MonoBehaviour {
    public Button Finish;
    GameObject InputA, InputB, InputC, OutputF;
	// Use this for initialization



	void Start () {
		for(int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            switch (this.gameObject.transform.GetChild(i).name)
            {
                case "InputA":
                    InputA = this.gameObject.transform.GetChild(i).gameObject;
                    InputA.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "InputB":
                    InputB = this.gameObject.transform.GetChild(i).gameObject;
                    InputB.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "InputC":
                    InputC = this.gameObject.transform.GetChild(i).gameObject;
                    InputC.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "OutputF":
                    OutputF = this.gameObject.transform.GetChild(i).gameObject;
                    OutputF.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
                    break;
            }
        }
        Finish.onClick.AddListener(GradeCheckInitializer);

    }

    private void GradeCheckInitializer()
    {
        Debug.Log("Finish button clicked! Checking input and output.");
        StartCoroutine(FinishChecker());
    }

    IEnumerator FinishChecker()
    {
        CheckerTagScript InputATag = InputA.GetComponent<CheckerTagScript>();
        CheckerTagScript InputBTag = InputB.GetComponent<CheckerTagScript>();
        CheckerTagScript InputCTag = InputC.GetComponent<CheckerTagScript>();
        CheckerTagScript OutputFTag = OutputF.GetComponent<CheckerTagScript>();
        if(InputATag.GetCollidingObject() == null || InputBTag.GetCollidingObject() == null 
            || InputCTag.GetCollidingObject() == null || OutputFTag.GetCollidingObject() == null)
        {
            Debug.Log("All tags are not SNAPPED!");
            yield break;
        }
        Switch InputASwitch = InputATag.GetCollidingObject().GetComponent<Switch>();
        Switch InputBSwitch = InputBTag.GetCollidingObject().GetComponent<Switch>();
        Switch InputCSwitch = InputCTag.GetCollidingObject().GetComponent<Switch>();
        LEDScript OutputFLED = OutputFTag.GetCollidingObject().GetComponent<LEDScript>();

        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(false);
        yield return new WaitForSecondsRealtime(2);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(true);
        yield return new WaitForSecondsRealtime(2);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(false);
        yield return new WaitForSecondsRealtime(2);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(true);
        yield return new WaitForSecondsRealtime(2);
        if (!OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(false);
        yield return new WaitForSecondsRealtime(2);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(true);
        yield return new WaitForSecondsRealtime(2);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(false);
        yield return new WaitForSecondsRealtime(2);
        if (!OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }
        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(true);
        yield return new WaitForSecondsRealtime(2);
        if (!OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            yield break;
        }


        Debug.Log("Correct output!");
    }





    // Update is called once per frame
    void Update () {
		
	}
}
