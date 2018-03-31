using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class IntegrationTesting{
    [UnityTest]
    public IEnumerator Lab1IntegrationTesting()
    {
        SceneManager.LoadScene("Scenes/Lab1Tester");
        yield return new WaitForSecondsRealtime(1);

        GameObject powerSupplyGO = GameObject.Find("PowerSupply");
        Assert.IsNotNull(powerSupplyGO);
        PowerSupplyScript powerSupply = powerSupplyGO.GetComponent<PowerSupplyScript>();
        GameObject ProtoboardGO = GameObject.Find("Protoboard");
        Assert.IsNotNull(ProtoboardGO);
        ProtoboardObject protoboardObj = ProtoboardGO.GetComponent<ProtoboardObject>();
        Dictionary<string, List<GameObject>> protoboardNodes = protoboardObj.GetNodeDictionary();
        GameObject ANDchip = GameObject.Find("ANDChip"); Assert.IsNotNull(ANDchip);
        ANDGate Andscript = ANDchip.GetComponent<ANDGate>();
        GameObject ORchip = GameObject.Find("ORChip"); Assert.IsNotNull(ORchip);
        ORGate Orscript = ORchip.GetComponent<ORGate>();
        GameObject switch1 = GameObject.Find("Switch"); Assert.IsNotNull(switch1);
        Switch switch1script = switch1.GetComponent<Switch>();
        GameObject switch2 = GameObject.Find("Switch (1)"); Assert.IsNotNull(switch2);
        Switch switch2script = switch2.GetComponent<Switch>();
        GameObject switch3 = GameObject.Find("Switch (2)"); Assert.IsNotNull(switch3);
        Switch switch3script = switch3.GetComponent<Switch>();
        GameObject led = GameObject.Find("LEDChip"); Assert.IsNotNull(led);
        LEDScript ledScript = led.GetComponent<LEDScript>();
        GameObject InputA = GameObject.Find("InputA"); Assert.IsNotNull(InputA);
        CheckerTagScript aScript = InputA.GetComponent<CheckerTagScript>(); 
        GameObject InputB = GameObject.Find("InputB"); Assert.IsNotNull(InputB);
        CheckerTagScript bScript = InputA.GetComponent<CheckerTagScript>();
        GameObject InputC = GameObject.Find("InputC"); Assert.IsNotNull(InputC);
        CheckerTagScript cScript = InputA.GetComponent<CheckerTagScript>();
        GameObject OutputF = GameObject.Find("OutputF"); Assert.IsNotNull(OutputF);
        CheckerTagScript fScript = InputA.GetComponent<CheckerTagScript>();
        yield return new WaitForSecondsRealtime(.1f);
        Andscript.OnMouseUp(); Orscript.OnMouseUp(); switch1script.OnMouseUp();
        switch2script.OnMouseUp(); switch3script.OnMouseUp();
        aScript.OnMouseUp(); bScript.OnMouseUp(); cScript.OnMouseUp(); fScript.OnMouseUp();
        ledScript.OnMouseUp();
        yield return new WaitForSecondsRealtime(.1f);
        Assert.IsTrue(Andscript.isSnapped()); Assert.IsTrue(Orscript.isSnapped());
        Assert.IsTrue(ledScript.isSnapped()); Assert.IsTrue(switch1script.isSnapped());
        Assert.IsTrue(switch2script.isSnapped()); Assert.IsTrue(switch3script.isSnapped());
        Assert.IsTrue(aScript.isSnapped()); Assert.IsTrue(bScript.isSnapped());
        Assert.IsTrue(cScript.isSnapped()); Assert.IsTrue(fScript.isSnapped());

        Wire wire1 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1);
        List<GameObject> leftList;
        protoboardNodes.TryGetValue("leftlogicnode_LEFT", out leftList);
        Assert.IsNotNull(leftList);
        GameObject topLeftNode = leftList[0];
        yield return null; yield return null; yield return null; yield return null;
        wire1.SetNodePositions(powerSupply.GetVccNode().transform.position, topLeftNode.transform.position);

        Wire wire2 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire2);
        List<GameObject> rightList;
        protoboardNodes.TryGetValue("leftlogicnode_RIGHT", out rightList);
        Assert.IsNotNull(rightList);
        GameObject topRightNode = rightList[0];
        yield return null; yield return null; yield return null; yield return null;
        wire2.SetNodePositions(powerSupply.GetGndNode().transform.position, topRightNode.transform.position);


        Wire wire1switch1 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farleftnodetoplist;
        protoboardNodes.TryGetValue("m_farleftnode_0", out farleftnodetoplist);
        Assert.IsNotNull(rightList);
        GameObject farleftnodetop = farleftnodetoplist[0];
        yield return null; yield return null; yield return null; yield return null;
        wire1switch1.SetNodePositions(leftList[1].transform.position, farleftnodetop.transform.position);

        Wire wire2switch1 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farleftnodetoplist2;
        protoboardNodes.TryGetValue("m_farleftnode_2", out farleftnodetoplist2);
        Assert.IsNotNull(rightList);
        GameObject farleftnodetop2 = farleftnodetoplist2[0];
        yield return null; yield return null; yield return null; yield return null;
        wire2switch1.SetNodePositions(rightList[1].transform.position, farleftnodetop2.transform.position);



        Wire wire1switch2 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farleftnodetoplist4;
        protoboardNodes.TryGetValue("m_farleftnode_4", out farleftnodetoplist4);
        Assert.IsNotNull(rightList);
        GameObject farleftnodetop4 = farleftnodetoplist4[0];
        yield return null; yield return null; yield return null; yield return null;
        wire1switch2.SetNodePositions(leftList[2].transform.position, farleftnodetop4.transform.position);

        Wire wire2switch2 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farleftnodetoplist6;
        protoboardNodes.TryGetValue("m_farleftnode_6", out farleftnodetoplist6);
        Assert.IsNotNull(rightList);
        GameObject farleftnodetop6 = farleftnodetoplist6[0];
        yield return null; yield return null; yield return null; yield return null;
        wire2switch2.SetNodePositions(rightList[2].transform.position, farleftnodetop6.transform.position);

        Wire wire1switch3 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farleftnodetoplist8;
        protoboardNodes.TryGetValue("m_farleftnode_8", out farleftnodetoplist8);
        Assert.IsNotNull(rightList);
        GameObject farleftnodetop8 = farleftnodetoplist8[0];
        yield return null; yield return null; yield return null; yield return null;
        wire1switch3.SetNodePositions(leftList[2].transform.position, farleftnodetop8.transform.position);

        Wire wire2switch3 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farleftnodetoplist10;
        protoboardNodes.TryGetValue("m_farleftnode_10", out farleftnodetoplist10);
        Assert.IsNotNull(rightList);
        GameObject farleftnodetop10 = farleftnodetoplist10[0];
        yield return null; yield return null; yield return null; yield return null;
        wire2switch3.SetNodePositions(rightList[2].transform.position, farleftnodetop10.transform.position);


        //ledground
        Wire wire2ledgnd = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1switch1);
        List<GameObject> farfarrightnodelist;
        protoboardNodes.TryGetValue("rightlogicnode_LEFT", out farfarrightnodelist);
        Assert.IsNotNull(rightList);
        GameObject farfarrightnode = farfarrightnodelist[0];
        yield return null; yield return null; yield return null; yield return null;
        wire2switch3.SetNodePositions(rightList[3].transform.position, farfarrightnode.transform.position);

        //Setupchips
        Wire wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        List<GameObject> node1list, node2list;
        protoboardNodes.TryGetValue("m_farleftnode_1", out node1list);
        protoboardNodes.TryGetValue("m_leftnode_0", out node2list);
        Assert.IsNotNull(node1list); Assert.IsNotNull(node2list);
        GameObject node1, node2;
        node1 = node1list[0]; node2 = node2list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        List<GameObject> node3list, node4list;
        protoboardNodes.TryGetValue("m_farleftnode_9", out node3list);
        protoboardNodes.TryGetValue("m_leftnode_1", out node4list);
        Assert.IsNotNull(node1list); Assert.IsNotNull(node2list);
        node1 = node3list[0]; node2 = node4list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        List<GameObject> node5list, node6list;
        protoboardNodes.TryGetValue("m_leftnode_2", out node5list);
        protoboardNodes.TryGetValue("m_leftnode_10", out node6list);
        Assert.IsNotNull(node1list); Assert.IsNotNull(node2list);
        node1 = node5list[0]; node2 = node6list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        List<GameObject> node7list, node8list;
        protoboardNodes.TryGetValue("m_farleftnode_5", out node7list);
        protoboardNodes.TryGetValue("m_leftnode_11", out node8list);
        Assert.IsNotNull(node1list); Assert.IsNotNull(node2list);
        node1 = node7list[0]; node2 = node8list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        List<GameObject> node9list, node10list;
        protoboardNodes.TryGetValue("m_farrightnode_21", out node9list);
        protoboardNodes.TryGetValue("m_leftnode_12", out node10list);
        Assert.IsNotNull(node1list); Assert.IsNotNull(node2list);
        node1 = node9list[0]; node2 = node10list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);



        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        protoboardNodes.TryGetValue("m_leftnode_6", out node2list);
        Assert.IsNotNull(node2list);
        node1 = rightList[5]; node2 = node2list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        protoboardNodes.TryGetValue("m_leftnode_16", out node2list);
        Assert.IsNotNull(rightList);
        node1 = rightList[6]; node2 = node2list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        protoboardNodes.TryGetValue("m_rightnode_0", out node2list);
        Assert.IsNotNull(rightList);
        node1 = leftList[6]; node2 = node2list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        wire = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire);
        protoboardNodes.TryGetValue("m_rightnode_10", out node2list);
        Assert.IsNotNull(rightList);
        node1 = leftList[7]; node2 = node2list[0];
        yield return null; yield return null; yield return null; yield return null;
        wire.SetNodePositions(node1.transform.position, node2.transform.position);

        

        yield return new WaitForSecondsRealtime(10);
        yield break;
    }






}
