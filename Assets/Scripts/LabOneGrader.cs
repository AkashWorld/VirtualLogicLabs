using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GradingCONSTANTS{
    public static string INPUT = "INPUTSWITCH";
    public static string OUTPUT = "OUTPUTLED";
}
public class LabOneGrader : MonoBehaviour {
    public Button Finish;
    GameObject InputA, InputB, InputC, OutputF;
    List<GameObject> MarksList; //List that stores checkmark/cross game objects
    LogicManager logicManager;
    Sprite checkMarkSprite, crossMarkSprite;
	// Use this for initialization



	void Start () {
        logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        MarksList = new List<GameObject>();
        checkMarkSprite = Resources.Load<Sprite>("Sprites/002-tick");
        crossMarkSprite = Resources.Load<Sprite>("Sprites/001-close");
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
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

    private void AddCheckMarkOrCross(bool isCheckMark)
    {
        int count = MarksList.Count;
        if (isCheckMark)
        {
            GameObject check = new GameObject("Check");
            check.transform.parent = this.gameObject.transform;
            check.transform.position = new Vector3(-3.3f + count*.9f, 3.10f, 0);
            SpriteRenderer spriteRenderer = check.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = checkMarkSprite;
            MarksList.Add(check);
            
        }
        else if (!isCheckMark)
        {
            GameObject cross = new GameObject("Cross");
            cross.transform.parent = this.gameObject.transform;
            cross.transform.position = new Vector3(-3.3f + count * .9f, 3.10f, 0);
            SpriteRenderer spriteRenderer = cross.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = crossMarkSprite;
            MarksList.Add(cross);
        }
    }


    IEnumerator FinishChecker()
    {
        for(int i = 0; i < MarksList.Count; i++)
        {
            Destroy(MarksList[i]);
        }
        MarksList.Clear();
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
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(true);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(false);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(true);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(false);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(true);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(false);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        InputASwitch.ToggleSwitch(true); InputBSwitch.ToggleSwitch(true); InputCSwitch.ToggleSwitch(true);
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!OutputFLED.isLEDON())
        {
            Debug.Log("Incorrect Output");
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);


        Debug.Log("Correct output!");
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scenes/Postlab1");
    }





    // Update is called once per frame
    void Update () {
		
	}
}
