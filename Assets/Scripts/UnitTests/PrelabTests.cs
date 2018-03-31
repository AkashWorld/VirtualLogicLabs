using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Prelab1UnitTest
{

    public GameObject inputfield000;
    public GameObject inputfield001;
    public GameObject inputfield010;
    public GameObject inputfield011;
    public GameObject inputfield100;
    public GameObject inputfield101;
    public GameObject inputfield110;
    public GameObject inputfield111;

    [UnityTest]
    public IEnumerator Input1()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        for (int i = 0; i < 8; i++)
        {
            input[i].text = "0";
        }

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "0" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "1" &&
           input[7].text == "1" &&
           message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        for (int i = 0; i < 8; i++)
        {
            input[i].text = "1";
        }
        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "1";
        input[6].text = "0";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "0";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "0";
        input[2].text = "0";
        input[3].text = "0";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "1";
        input[1].text = "1";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "0";
        input[2].text = "1";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "1";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "0";
        input[3].text = "0";
        input[4].text = "1";
        input[5].text = "0";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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

        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        InputField[] input = new InputField[8];

        input[0] = inputfield000.GetComponent<InputField>();
        input[1] = inputfield001.GetComponent<InputField>();
        input[2] = inputfield010.GetComponent<InputField>();
        input[3] = inputfield011.GetComponent<InputField>();
        input[4] = inputfield100.GetComponent<InputField>();
        input[5] = inputfield101.GetComponent<InputField>();
        input[6] = inputfield110.GetComponent<InputField>();
        input[7] = inputfield111.GetComponent<InputField>();

        input[0].text = "0";
        input[1].text = "0";
        input[2].text = "1";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "1";
        input[6].text = "1";
        input[7].text = "0";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
            input[1].text == "0" &&
            input[2].text == "0" &&
            input[3].text == "1" &&
            input[4].text == "0" &&
            input[5].text == "0" &&
            input[6].text == "1" &&
            input[7].text == "1" &&
            message.text == "That's right!")
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
        SceneManager.LoadScene("Scenes/Prelab1");
    }
}