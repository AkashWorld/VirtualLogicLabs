using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyScript : MonoBehaviour, LogicInterface {
    enum LOGIC { HIGH = 1, LOW = 0, INVALID = -1 }
    GameObject gndNode;
    GameObject vccNode;
    GameObject powerSupply;
    public readonly string gndKey = "GROUNDNODE_KEY";
    public readonly string vccKey = "VCCNODE_KEY";

    private void setNodeProperties(GameObject logicNode, string logicNodeID)
    {
        LogicNode logic_behavior = logicNode.AddComponent<LogicNode>() as LogicNode; //Adds the LogicNode.cs component to this gameobject to control logic behavior
        logic_behavior.SetLogicId(logicNodeID); //logic id that sets all the nodes on the left column of the LEFT section of the protoboard the same id
        logic_behavior.SetLogicNode(logicNode);
        logic_behavior.SetOwningDevice(this);
        SpriteRenderer sprite_renderer = logicNode.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
        sprite_renderer.sprite = Resources.Load<Sprite>("logicCircle");
        sprite_renderer.sortingLayerName = "Logic";
        BoxCollider2D box_collider = logicNode.AddComponent<BoxCollider2D>();
        box_collider.size = new Vector2(1f, 1f);
        box_collider.isTrigger = true;
        Rigidbody2D rigidbody = logicNode.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

    }

    // Use this for initialization
    void Start () {
        powerSupply = GameObject.Find("PowerSupply");

        vccNode = new GameObject(vccKey); //logic node with the name leftlogicnode_{i}_0
        vccNode.transform.parent = powerSupply.transform; //sets the Protoboard game object as logicNode_0's parent
        vccNode.transform.localPosition = new Vector3(-.55f, -.48f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        vccNode.transform.localScale = new Vector3(.3F, .3F, 0);
        setNodeProperties(vccNode, vccKey);
        LogicNode vccBehavior = vccNode.GetComponent<LogicNode>();
        vccBehavior.SetLogicState((int)LOGIC.HIGH);

        gndNode = new GameObject(gndKey); //logic node with the name leftlogicnode_{i}_0
        gndNode.transform.parent = powerSupply.transform; //sets the Protoboard game object as logicNode_0's parent
        gndNode.transform.localPosition = new Vector3(.35f,-.48f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        gndNode.transform.localScale = new Vector3(.3F, .3f, 0);
        setNodeProperties(gndNode, gndKey);
        LogicNode gndBehavior = gndNode.GetComponent<LogicNode>();
        gndBehavior.SetLogicState((int)LOGIC.LOW);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReactToLogic(GameObject callingNode, int source)
    {
        throw new System.NotImplementedException();
    }

    public void ReactToLogic(GameObject LogicNode)
    {
        throw new System.NotImplementedException();
    }
}
