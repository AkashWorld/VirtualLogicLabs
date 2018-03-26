using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyScript : MonoBehaviour, LogicInterface {
    GameObject gndNode;
    GameObject vccNode;
    GameObject powerSupply;
    private string gndKey = "GROUNDNODE_KEY";
    private string vccKey = "VCCNODE_KEY";
    private Vector3 screenPoint;
    private Vector3 offset;


    // Use this for initialization
    void Start () {
        powerSupply = this.gameObject;

        vccNode = new GameObject(vccKey); //logic node with the name leftlogicnode_{i}_0
        vccNode.transform.parent = powerSupply.transform; //sets the Protoboard game object as logicNode_0's parent
        vccNode.transform.localPosition = new Vector3(-.55f, -.48f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        vccNode.transform.localScale = new Vector3(.3F, .3F, 0);
        vccNode.AddComponent<LogicNode>();


        gndNode = new GameObject(gndKey); //logic node with the name leftlogicnode_{i}_0
        gndNode.transform.parent = powerSupply.transform; //sets the Protoboard game object as logicNode_0's parent
        gndNode.transform.localPosition = new Vector3(.35f,-.48f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        gndNode.transform.localScale = new Vector3(.3F, .3f, 0);
        gndNode.AddComponent<LogicNode>();



    }

    // Update is called once per frame
    void Update () {
        vccNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.HIGH);
        gndNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.LOW);
    }

    public void ReactToLogic(GameObject callingNode, int source)
    {
        
    }

    public void ReactToLogic(GameObject LogicNode)
    {

    }

    public void TurnOffRelatedNodes(GameObject LogicNode)
    {
        Debug.Log("RESET on device " + this.gameObject.name + " , The requesting node is: " + LogicNode.gameObject.name);
        if (LogicNode == vccNode) { vccNode.GetComponent<LogicNode>().SetLogicStateWithoutNotification((int)(LOGIC.INVALID)); }
        else if (LogicNode == gndNode) { gndNode.GetComponent<LogicNode>().SetLogicStateWithoutNotification((int)(LOGIC.INVALID)); }
    }
}
