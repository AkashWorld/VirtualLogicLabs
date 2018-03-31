using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Postlab1UnitTest
{

    public Button button;
    public Text text;
    public GameObject inputfield0000;
    public GameObject inputfield0001;
    public GameObject inputfield0010;
    public GameObject inputfield0011;
    public GameObject inputfield0100;
    public GameObject inputfield0101;
    public GameObject inputfield0110;
    public GameObject inputfield0111;
    public GameObject inputfield1000;
    public GameObject inputfield1001;
    public GameObject inputfield1010;
    public GameObject inputfield1011;
    public GameObject inputfield1100;
    public GameObject inputfield1101;
    public GameObject inputfield1110;
    public GameObject inputfield1111;

    [UnityTest]
    public IEnumerator Input1()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        for (int i = 0; i < 16; i++)
        {
            input[i].text = "0";
        }

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input2()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        for (int i = 0; i < 16; i++)
        {
            input[i].text = "1";
        }

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input3()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "0";
        input[8].text = "1";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "1";
        input[14].text = "1";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input4()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "0";
        input[7].text = "0";
        input[8].text = "1";
        input[9].text = "1";
        input[10].text = "1";
        input[11].text = "0";
        input[12].text = "1";
        input[13].text = "1";
        input[14].text = "1";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input5()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "1";
        input[6].text = "0";
        input[7].text = "1";
        input[8].text = "0";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "1";
        input[14].text = "1";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input6()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "0";
        input[8].text = "1";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "1";
        input[14].text = "0";
        input[15].text = "0";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input7()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "1";
        input[6].text = "0";
        input[7].text = "1";
        input[8].text = "0";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "1";
        input[14].text = "0";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input8()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "0";
        input[8].text = "1";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "1";
        input[14].text = "1";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input9()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "0";
        input[8].text = "1";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "0";
        input[14].text = "0";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    [UnityTest]
    public IEnumerator Input10()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();

        inputfield0000 = GameObject.Find("InputField (0000)");
        inputfield0001 = GameObject.Find("InputField (0001)");
        inputfield0010 = GameObject.Find("InputField (0010)");
        inputfield0011 = GameObject.Find("InputField (0011)");
        inputfield0100 = GameObject.Find("InputField (0100)");
        inputfield0101 = GameObject.Find("InputField (0101)");
        inputfield0110 = GameObject.Find("InputField (0110)");
        inputfield0111 = GameObject.Find("InputField (0111)");
        inputfield1000 = GameObject.Find("InputField (1000)");
        inputfield1001 = GameObject.Find("InputField (1001)");
        inputfield1010 = GameObject.Find("InputField (1010)");
        inputfield1011 = GameObject.Find("InputField (1011)");
        inputfield1100 = GameObject.Find("InputField (1100)");
        inputfield1101 = GameObject.Find("InputField (1101)");
        inputfield1110 = GameObject.Find("InputField (1110)");
        inputfield1111 = GameObject.Find("InputField (1111)");

        InputField[] input = new InputField[16];

        input[0] = inputfield0000.GetComponent<InputField>();
        input[1] = inputfield0001.GetComponent<InputField>();
        input[2] = inputfield0010.GetComponent<InputField>();
        input[3] = inputfield0011.GetComponent<InputField>();
        input[4] = inputfield0100.GetComponent<InputField>();
        input[5] = inputfield0101.GetComponent<InputField>();
        input[6] = inputfield0110.GetComponent<InputField>();
        input[7] = inputfield0111.GetComponent<InputField>();
        input[8] = inputfield1000.GetComponent<InputField>();
        input[9] = inputfield1001.GetComponent<InputField>();
        input[10] = inputfield1010.GetComponent<InputField>();
        input[11] = inputfield1011.GetComponent<InputField>();
        input[12] = inputfield1100.GetComponent<InputField>();
        input[13] = inputfield1101.GetComponent<InputField>();
        input[14] = inputfield1110.GetComponent<InputField>();
        input[15] = inputfield1111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "1";
        input[8].text = "1";
        input[9].text = "1";
        input[10].text = "0";
        input[11].text = "1";
        input[12].text = "0";
        input[13].text = "1";
        input[14].text = "1";
        input[15].text = "1";


        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "1" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           input[8].text == "0" &&
           input[9].text == "1" &&
           input[10].text == "0" &&
           input[11].text == "1" &&
           input[12].text == "0" &&
           input[13].text == "1" &&
           input[14].text == "0" &&
           input[15].text == "1" &&
           function.text == "That's Right!\t" + "The reduced function can be found, which in this case is: F = D")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield break;
        }


        Assert.Fail();
    }

    private void SetupScene()
    {
        SceneManager.LoadScene("Scenes/Postlab1");
    }
}