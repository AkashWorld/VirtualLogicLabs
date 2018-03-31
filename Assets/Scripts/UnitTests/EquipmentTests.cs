using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EquipmentTests {
	[UnityTest]
	public IEnumerator LEDTest() {
        // Use the Assert class to test conditions.
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject testGnd = new GameObject("testGnd");
        LogicNode gndLogic = testGnd.AddComponent<LogicNode>();
        GameObject testVcc = new GameObject("testVcc");
        LogicNode vccLogic = testVcc.AddComponent<LogicNode>();
        GameObject LEDPrefab = Resources.Load<GameObject>("Prefabs/Lab/LEDChip");
        Assert.NotNull(LEDPrefab);
        GameObject newLED = GameObject.Instantiate<GameObject>(LEDPrefab);
        Assert.AreNotEqual(newLED, null);
        yield return new WaitForSecondsRealtime(1);
        LEDScript ledScript = newLED.GetComponent<LEDScript>();
        GameObject ledGnd = ledScript.GetLEDNodeGnd(); GameObject ledVcc = ledScript.GetLEDNodeVCC();
        Assert.AreNotEqual(ledGnd, null); Assert.AreNotEqual(ledVcc, null);
        testGnd.transform.position = ledGnd.transform.position; testVcc.transform.position = ledVcc.transform.position;
        yield return new WaitForSecondsRealtime(1);
        ledScript.OnMouseUp();
        yield return new WaitForSecondsRealtime(1);
        gndLogic.SetLogicState((int)LOGIC.INVALID); vccLogic.SetLogicState((int)LOGIC.INVALID);
        if (ledScript.isLEDON())
        {
            Assert.Fail();
        }
        
        gndLogic.SetLogicState((int)LOGIC.LOW); vccLogic.SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        if (!ledScript.isLEDON())
        {
            Assert.Fail();
        }
        yield break;
    }



    [UnityTest]
    public IEnumerator SwitchTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject topNode = new GameObject("topNode");
        LogicNode topLogic = topNode.AddComponent<LogicNode>();
        GameObject botNode = new GameObject("botNode");
        LogicNode botLogic = botNode.AddComponent<LogicNode>();
        GameObject middleNode = new GameObject("middleNode");
        LogicNode middleLogic = middleNode.AddComponent<LogicNode>();
        GameObject SwitchPrefab = Resources.Load<GameObject>("Prefabs/Lab/Switch");
        Assert.NotNull(SwitchPrefab);
        GameObject switchGO = GameObject.Instantiate<GameObject>(SwitchPrefab);
        Switch switchScript = switchGO.GetComponent<Switch>();
        yield return new WaitForSecondsRealtime(1);
        LogicNode switchTop = switchScript.GetTopNode().GetComponent<LogicNode>();
        LogicNode switchBot = switchScript.GetBotNode().GetComponent<LogicNode>();
        LogicNode switchMiddle = switchScript.GetMiddleNode().GetComponent<LogicNode>();
        topNode.transform.position = switchTop.gameObject.transform.position;
        botNode.transform.position = switchBot.gameObject.transform.position;
        middleNode.transform.position = switchMiddle.gameObject.transform.position;
        yield return new WaitForSecondsRealtime(1);
        switchScript.OnMouseUp();
        yield return new WaitForSecondsRealtime(1);
        topLogic.SetLogicState((int)LOGIC.HIGH);
        botLogic.SetLogicState((int)LOGIC.LOW);
        switchScript.ToggleSwitch(true);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual(switchMiddle.GetLogicState(), topLogic.GetLogicState());
        switchScript.ToggleSwitch(false);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual(switchMiddle.GetLogicState(), botLogic.GetLogicState());
        yield break;
    }

    [UnityTest]
    public IEnumerator PowerSupplyTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject PSPrefab = Resources.Load<GameObject>("Prefabs/Lab/PowerSupply");
        Assert.NotNull(PSPrefab);
        GameObject PSGO = GameObject.Instantiate<GameObject>(PSPrefab);
        PowerSupplyScript psScript = PSGO.GetComponent<PowerSupplyScript>();
        yield return new WaitForSecondsRealtime(1);
        GameObject vccNode = psScript.GetVccNode(); GameObject gndNode = psScript.GetGndNode();
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual(vccNode.GetComponent<LogicNode>().GetLogicState(), (int)LOGIC.HIGH);
        Assert.AreEqual(gndNode.GetComponent<LogicNode>().GetLogicState(), (int)LOGIC.LOW);
        vccNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
        gndNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual(vccNode.GetComponent<LogicNode>().GetLogicState(), (int)LOGIC.HIGH);
        Assert.AreEqual(gndNode.GetComponent<LogicNode>().GetLogicState(), (int)LOGIC.LOW);
        yield break;
    }


    [UnityTest]
    public IEnumerator NANDTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject NANDChip = Resources.Load<GameObject>("Prefabs/Lab/NANDChip");
        Assert.NotNull(NANDChip);
        GameObject NANDGO = GameObject.Instantiate<GameObject>(NANDChip);
        NANDGate NANDGate = NANDGO.GetComponent<NANDGate>();
        string device_id = NANDGate.LOGIC_DEVICE_ID;
        yield return new WaitForSecondsRealtime(1);
        Dictionary<string, GameObject> nandNodes = NANDGate.GetLogicDictionary();
        List<GameObject> otherNodes = new List<GameObject>();
        for(int i = 0; i < 14; i++)
        {
            GameObject newNode = new GameObject("OtherNODE_"+i);
            otherNodes.Add(newNode);
        }
        List<LogicNode> otherLogic = new List<LogicNode>();
        for (int i = 0; i < 14; i++)
        {
            otherLogic.Add(otherNodes[i].AddComponent<LogicNode>());
        }
        List<GameObject> nandNodeList = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject DeviceNode;
            if (nandNodes.TryGetValue(device_id + i, out DeviceNode))
            {
                nandNodeList.Add(DeviceNode);
                otherNodes[i].transform.position = nandNodeList[i].transform.position;
            }
            else { Assert.Fail(); }
        }
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!NANDGate.IsDeviceOn());
        otherLogic[6].SetLogicState((int)LOGIC.LOW);
        otherLogic[13].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!NANDGate.IsDeviceOn());
        NANDGate.SetSnapped(true);
        Assert.IsTrue(NANDGate.IsDeviceOn());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[1].SetLogicState((int)LOGIC.HIGH);
        otherLogic[3].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[9].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        otherLogic[11].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW,nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[1].SetLogicState((int)LOGIC.HIGH);
        otherLogic[3].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[9].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        otherLogic[11].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[1].SetLogicState((int)LOGIC.LOW);
        otherLogic[3].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[9].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        otherLogic[11].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[1].SetLogicState((int)LOGIC.LOW);
        otherLogic[3].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[9].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        otherLogic[11].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        yield break;
    }


    [UnityTest]
    public IEnumerator ANDTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject ANDCHIP = Resources.Load<GameObject>("Prefabs/Lab/ANDChip");
        Assert.NotNull(ANDCHIP);
        GameObject ANDGO = GameObject.Instantiate<GameObject>(ANDCHIP);
        ANDGate ANDGATE = ANDGO.GetComponent<ANDGate>();
        string device_id = ANDGate.LOGIC_DEVICE_ID;
        yield return new WaitForSecondsRealtime(1);
        Dictionary<string, GameObject> nandNodes = ANDGATE.GetLogicDictionary();
        List<GameObject> otherNodes = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject newNode = new GameObject("OtherNODE_" + i);
            otherNodes.Add(newNode);
        }
        List<LogicNode> otherLogic = new List<LogicNode>();
        for (int i = 0; i < 14; i++)
        {
            otherLogic.Add(otherNodes[i].AddComponent<LogicNode>());
        }
        List<GameObject> nandNodeList = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject DeviceNode;
            if (nandNodes.TryGetValue(device_id + i, out DeviceNode))
            {
                nandNodeList.Add(DeviceNode);
                otherNodes[i].transform.position = nandNodeList[i].transform.position;
            }
            else { Assert.Fail(); }
        }
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!ANDGATE.IsDeviceOn());
        otherLogic[6].SetLogicState((int)LOGIC.LOW);
        otherLogic[13].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!ANDGATE.IsDeviceOn());
        ANDGATE.SetSnapped(true);
        Assert.IsTrue(ANDGATE.IsDeviceOn());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[1].SetLogicState((int)LOGIC.HIGH);
        otherLogic[3].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[9].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        otherLogic[11].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[1].SetLogicState((int)LOGIC.HIGH);
        otherLogic[3].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[9].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        otherLogic[11].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[1].SetLogicState((int)LOGIC.LOW);
        otherLogic[3].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[9].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        otherLogic[11].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[1].SetLogicState((int)LOGIC.LOW);
        otherLogic[3].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[9].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        otherLogic[11].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        yield break;
    }

    [UnityTest]
    public IEnumerator ORTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject ORChip = Resources.Load<GameObject>("Prefabs/Lab/ORChip");
        Assert.NotNull(ORChip);
        GameObject ORGo = GameObject.Instantiate<GameObject>(ORChip);
        ORGate OrGate = ORGo.GetComponent<ORGate>();
        string device_id = ORGate.LOGIC_DEVICE_ID;
        yield return new WaitForSecondsRealtime(1);
        Dictionary<string, GameObject> nandNodes = OrGate.GetLogicDictionary();
        List<GameObject> otherNodes = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject newNode = new GameObject("OtherNODE_" + i);
            otherNodes.Add(newNode);
        }
        List<LogicNode> otherLogic = new List<LogicNode>();
        for (int i = 0; i < 14; i++)
        {
            otherLogic.Add(otherNodes[i].AddComponent<LogicNode>());
        }
        List<GameObject> nandNodeList = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject DeviceNode;
            if (nandNodes.TryGetValue(device_id + i, out DeviceNode))
            {
                nandNodeList.Add(DeviceNode);
                otherNodes[i].transform.position = nandNodeList[i].transform.position;
            }
            else { Assert.Fail(); }
        }
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!OrGate.IsDeviceOn());
        otherLogic[6].SetLogicState((int)LOGIC.LOW);
        otherLogic[13].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!OrGate.IsDeviceOn());
        OrGate.SetSnapped(true);
        Assert.IsTrue(OrGate.IsDeviceOn());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[1].SetLogicState((int)LOGIC.HIGH);
        otherLogic[3].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[9].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        otherLogic[11].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[1].SetLogicState((int)LOGIC.HIGH);
        otherLogic[3].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[9].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        otherLogic[11].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[1].SetLogicState((int)LOGIC.LOW);
        otherLogic[3].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[9].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        otherLogic[11].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[1].SetLogicState((int)LOGIC.LOW);
        otherLogic[3].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[9].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        otherLogic[11].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[2].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[10].GetComponent<LogicNode>().GetLogicState());

        yield break;
    }


    [UnityTest]
    public IEnumerator INVTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject INVChip = Resources.Load<GameObject>("Prefabs/Lab/INVChip");
        Assert.NotNull(INVChip);
        GameObject InvGO = GameObject.Instantiate<GameObject>(INVChip);
        INVGate InvGate = InvGO.GetComponent<INVGate>();
        string device_id = INVGate.LOGIC_DEVICE_ID;
        yield return new WaitForSecondsRealtime(1);
        Dictionary<string, GameObject> nandNodes = InvGate.GetLogicDictionary();
        List<GameObject> otherNodes = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject newNode = new GameObject("OtherNODE_" + i);
            otherNodes.Add(newNode);
        }
        List<LogicNode> otherLogic = new List<LogicNode>();
        for (int i = 0; i < 14; i++)
        {
            otherLogic.Add(otherNodes[i].AddComponent<LogicNode>());
        }
        List<GameObject> nandNodeList = new List<GameObject>();
        for (int i = 0; i < 14; i++)
        {
            GameObject DeviceNode;
            if (nandNodes.TryGetValue(device_id + i, out DeviceNode))
            {
                nandNodeList.Add(DeviceNode);
                otherNodes[i].transform.position = nandNodeList[i].transform.position;
            }
            else { Assert.Fail(); }
        }
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!InvGate.IsDeviceOn());
        otherLogic[6].SetLogicState((int)LOGIC.LOW);
        otherLogic[13].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.IsTrue(!InvGate.IsDeviceOn());
        InvGate.SetSnapped(true);
        Assert.IsTrue(InvGate.IsDeviceOn());

        otherLogic[0].SetLogicState((int)LOGIC.LOW);
        otherLogic[2].SetLogicState((int)LOGIC.LOW);
        otherLogic[4].SetLogicState((int)LOGIC.LOW);
        otherLogic[8].SetLogicState((int)LOGIC.LOW);
        otherLogic[10].SetLogicState((int)LOGIC.LOW);
        otherLogic[12].SetLogicState((int)LOGIC.LOW);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[1].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[3].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[9].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.HIGH, nandNodeList[11].GetComponent<LogicNode>().GetLogicState());

        otherLogic[0].SetLogicState((int)LOGIC.HIGH);
        otherLogic[2].SetLogicState((int)LOGIC.HIGH);
        otherLogic[4].SetLogicState((int)LOGIC.HIGH);
        otherLogic[8].SetLogicState((int)LOGIC.HIGH);
        otherLogic[10].SetLogicState((int)LOGIC.HIGH);
        otherLogic[12].SetLogicState((int)LOGIC.HIGH);
        yield return new WaitForSecondsRealtime(1);
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[1].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[3].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[5].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[7].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[9].GetComponent<LogicNode>().GetLogicState());
        Assert.AreEqual((int)LOGIC.LOW, nandNodeList[11].GetComponent<LogicNode>().GetLogicState());


        yield break;
    }


    [UnityTest]
    [Timeout(100000000)]
    public IEnumerator ProtoboardTest()
    {
        SetupScene();
        yield return new WaitForSecondsRealtime(1);
        GameObject Protoboard = GameObject.Find("Protoboard");
        Assert.IsNotNull(Protoboard);
        ProtoboardObject protoboardObject = Protoboard.GetComponent<ProtoboardObject>();
        GameObject testNode = new GameObject("TesterNode");
        LogicNode testLogic = testNode.AddComponent<LogicNode>();
        foreach(KeyValuePair<string,List<GameObject>> entry in protoboardObject.GetNodeDictionary())
        {
            List<GameObject> relatedNodes = entry.Value;
            foreach (GameObject protonode in relatedNodes)
            {
                testNode.transform.position = protonode.transform.position;
                testLogic.SetLogicState((int)LOGIC.HIGH);
                yield return new WaitForSeconds(.1f);
                foreach (GameObject protoboardNode in relatedNodes) //place node
                {
                    Assert.AreEqual(testLogic.GetLogicState(), protoboardNode.GetComponent<LogicNode>().GetLogicState());
                }
                testLogic.SetLogicState((int)LOGIC.LOW);
                yield return new WaitForSeconds(.1f);
                foreach (GameObject protoboardNode in relatedNodes) //place node
                {
                    Assert.AreEqual(testLogic.GetLogicState(), protoboardNode.GetComponent<LogicNode>().GetLogicState());
                }
                testLogic.SetLogicState((int)LOGIC.INVALID);
                yield return new WaitForSeconds(.1f);
                foreach (GameObject protoboardNode in relatedNodes) //place node
                {
                    Assert.AreEqual(testLogic.GetLogicState(), protoboardNode.GetComponent<LogicNode>().GetLogicState());
                }
            }
        }
        yield return new WaitForSecondsRealtime(1);
        yield break;
    }
    
    [UnityTest]
    public IEnumerator Magnifier()
    {
        SetupScene();
        yield return new WaitForSeconds(2);
        GameObject MagnifyingGlass = GameObject.Find("MagnifyingGlass");
        MagnifyingGlass.transform.position = new Vector3(0, 0, 0);

        //Test 74LS00 Collision On
        GameObject NANDChip = Resources.Load<GameObject>("Prefabs/Lab/NANDChip");
        GameObject NANDGO = GameObject.Instantiate<GameObject>(NANDChip);
        NANDGO.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3); 
        GameObject NANDInfo = GameObject.Find("74LS00Info");
        SpriteRenderer NANDInfoSprite = NANDInfo.GetComponent<SpriteRenderer>();
        Assert.AreEqual(NANDInfoSprite.color, new Color(1f, 1f, 1f, 1f));
       
        //Test 74LS04 Collision On
        GameObject INVChip = Resources.Load<GameObject>("Prefabs/Lab/INVChip");
        GameObject INVGO = GameObject.Instantiate<GameObject>(INVChip);
        INVGO.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3);
        GameObject INVInfo = GameObject.Find("74LS04Info");
        SpriteRenderer INVInfoSprite = INVInfo.GetComponent<SpriteRenderer>();
        Assert.AreEqual(INVInfoSprite.color, new Color(1f, 1f, 1f, 1f));

        //Test 74LS08 Collision On
        GameObject ANDChip = Resources.Load<GameObject>("Prefabs/Lab/ANDChip");
        GameObject ANDGO = GameObject.Instantiate<GameObject>(ANDChip);
        ANDGO.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3);
        GameObject ANDInfo = GameObject.Find("74LS08Info");
        SpriteRenderer ANDInfoSprite = ANDInfo.GetComponent<SpriteRenderer>();
        Assert.AreEqual(ANDInfoSprite.color, new Color(1f, 1f, 1f, 1f));

        //Test 74LS08 Collision On
        GameObject ORChip = Resources.Load<GameObject>("Prefabs/Lab/ORChip");
        GameObject ORGO = GameObject.Instantiate<GameObject>(ORChip);
        ORGO.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3);
        GameObject ORInfo = GameObject.Find("74LS32Info");
        SpriteRenderer ORInfoSprite = ORInfo.GetComponent<SpriteRenderer>();
        Assert.AreEqual(ORInfoSprite.color, new Color(1f, 1f, 1f, 1f));

        //Test 74LS00 Collision Off
        NANDGO.transform.position = new Vector3(5, 5, 0);
        yield return new WaitForSeconds(3);
        Assert.AreEqual(NANDInfoSprite.color, new Color(1f, 1f, 1f, 0f));

        //Test 74LS04 Collision Off
        INVGO.transform.position = new Vector3(5, 5, 0);
        yield return new WaitForSeconds(3);
        Assert.AreEqual(INVInfoSprite.color, new Color(1f, 1f, 1f, 0f));

        //Test 74LS08 Collision Off
        ANDGO.transform.position = new Vector3(5, 5, 0);
        yield return new WaitForSeconds(3);
        Assert.AreEqual(ANDInfoSprite.color, new Color(1f, 1f, 1f, 0f));

        //Test 74LS32 Collision Off
        ORGO.transform.position = new Vector3(5, 5, 0);
        yield return new WaitForSeconds(3);
        Assert.AreEqual(ORInfoSprite.color, new Color(1f, 1f, 1f, 0f));

        yield break; 
    }

    [UnityTest]
    public IEnumerator Trash()
    {
        SetupScene();
        yield return new WaitForSeconds(2);
        GameObject Trash = GameObject.Find("Trash");
        TrashBehavior trashBehavior = Trash.GetComponent<TrashBehavior>(); 
        Trash.transform.position = new Vector3(0, 0, 0);

        GameObject NANDChip = Resources.Load<GameObject>("Prefabs/Lab/NANDChip");
        GameObject NANDGO = GameObject.Instantiate<GameObject>(NANDChip);
        NANDGO.transform.position = new Vector3(0, 0, 0);
        Assert.AreEqual((bool)GameObject.Find("NANDChip(Clone)"), true);
        trashBehavior.test = true;
        yield return new WaitForSeconds(2);
        Assert.AreNotEqual(GameObject.Find("NANDChip(Clone)"), true); 
        yield break; 
    }


    private void SetupScene()
    {
        SceneManager.LoadScene("Scenes/SandboxLab");
    }
}
