using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDScript : MonoBehaviour, LogicInterface{
    private GameObject DeviceGameObject;
    private GameObject snapIndicatorGameObj;
    private const string LOGIC_DEVICE_ID = "LED_";
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool SNAPPED = false; //Set to true if all Logic Nodes of this device is in collision with an external node
    private GameObject LEDNodeVCC, LEDNodeGnd;

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
        DeviceGameObject = this.gameObject;

        LEDNodeVCC = new GameObject(LOGIC_DEVICE_ID + "VCC"); //logic node with the name leftlogicnode_{i}_0
        LEDNodeVCC.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        LEDNodeVCC.transform.localPosition = new Vector3(-1.8f, -.05f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        LEDNodeVCC.transform.localScale = new Vector3(.10F, .10F, 0);
        setNodeProperties(LEDNodeVCC, LOGIC_DEVICE_ID + "VCC");


        LEDNodeGnd = new GameObject(LOGIC_DEVICE_ID + "GND"); //logic node with the name leftlogicnode_{i}_0
        LEDNodeGnd.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        LEDNodeGnd.transform.localPosition = new Vector3(-1.2f, .165f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        LEDNodeGnd.transform.localScale = new Vector3(.10F, .10F, 0);
        setNodeProperties(LEDNodeGnd, LOGIC_DEVICE_ID + "GND");

    }


    void OnMouseDown()
    {
        Debug.Log("LED Mouse Down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;
    }
        // Update is called once per frame
    void Update () {
		
	}

    private bool IsDeviceOn()
    {
        if(LEDNodeGnd.GetComponent<LogicNode>().GetLogicState() == (int)LOGIC.LOW 
            && LEDNodeVCC.GetComponent<LogicNode>().GetCollidingNode() != null)
        {
            Debug.Log("LED Is Operable");
            return true;
        }
        return false;
    }

    private void CheckIfSnapped()
    {

        //Check if all nodes with the chip is colliding with another logic node;
        if(LEDNodeGnd.GetComponent<LogicNode>().GetCollidingNode() == null || LEDNodeVCC.GetComponent<LogicNode>().GetCollidingNode() == null)
        {
            Debug.Log("LED Not Snapped");
            SNAPPED = false;
        }
        else
        {
            Debug.Log("LED SNAPPED!");
            LogicNode GndNode = LEDNodeGnd.GetComponent<LogicNode>();
            GameObject CollidingNode = GndNode.GetCollidingNode();
            Vector3 collidingNodePos = CollidingNode.transform.position;
            Vector3 offsetPosition = new Vector3(collidingNodePos.x + 1.2f, collidingNodePos.y - .165f, collidingNodePos.z);
            DeviceGameObject.transform.position = offsetPosition;
            SNAPPED = true;
        }

       
    }
    void OnMouseUp()
    {
        CheckIfSnapped();
    }

    private void LEDIO()
    {

    }

    public void ReactToLogic(GameObject LogicNode)
    {
    }

    public void ReactToLogic(GameObject LogicNode, int requestedState)
    {
        if(LogicNode == LEDNodeGnd)
        {
            LEDNodeGnd.GetComponent<LogicNode>().SetLogicState(requestedState);
        }
        else if (LogicNode == LEDNodeVCC)
        {
            LEDNodeVCC.GetComponent<LogicNode>().SetLogicState(requestedState);
        }
        LogicNode logicGND = LEDNodeGnd.GetComponent<LogicNode>(); LogicNode logicVCC = LEDNodeVCC.GetComponent<LogicNode>();
        SpriteRenderer LEDSpriteRen = this.gameObject.GetComponent<SpriteRenderer>();
        if (logicGND.GetLogicState() == (int)LOGIC.LOW && logicVCC.GetLogicState() == (int)LOGIC.HIGH)
        {
            LEDSpriteRen.sprite = Resources.Load<Sprite>("LEDon");
        }
        else
        {
            LEDSpriteRen.sprite = Resources.Load<Sprite>("LEDoff");
        }
    }
}
