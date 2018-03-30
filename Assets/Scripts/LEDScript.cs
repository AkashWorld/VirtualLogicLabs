using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDScript : MonoBehaviour, LogicInterface{
    private GameObject DeviceGameObject;
    private GameObject snapIndicatorGameObj;
    private const string LOGIC_DEVICE_ID = "LED_";
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool SNAPPED = false; //Set to true if all Logic Nodes of this device is in collision with an external node
    private GameObject LEDNodeVCC, LEDNodeGnd;
    Sprite LEDOn; Sprite LEDOff;
    private bool LEDState = false;


    public GameObject GetLEDNodeVCC()
    {
        return LEDNodeVCC;
    }

    public GameObject GetLEDNodeGnd()
    {
        return LEDNodeGnd;
    }

    // Use this for initialization
    void Start () {
        DeviceGameObject = this.gameObject;

        LEDNodeVCC = new GameObject(LOGIC_DEVICE_ID + "VCC"); //logic node with the name leftlogicnode_{i}_0
        LEDNodeVCC.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        LEDNodeVCC.transform.localPosition = new Vector3(-1.8f, -.05f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        LEDNodeVCC.transform.localScale = new Vector3(.10F, .10F, 0);
        LEDNodeVCC.AddComponent<LogicNode>();


        LEDNodeGnd = new GameObject(LOGIC_DEVICE_ID + "GND"); //logic node with the name leftlogicnode_{i}_0
        LEDNodeGnd.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        LEDNodeGnd.transform.localPosition = new Vector3(-1.2f, .165f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        LEDNodeGnd.transform.localScale = new Vector3(.10F, .10F, 0);
        LEDNodeGnd.AddComponent<LogicNode>();

        LEDOn = Resources.Load<Sprite>("Sprites/LEDon");
        LEDOff = Resources.Load<Sprite>("Sprites/LEDoff");
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
    public void OnMouseUp()
    {
        CheckIfSnapped();
    }

    public bool isLEDON()
    {
        Debug.Log("LED IS " + LEDState);
        return LEDState;
    }

    public void ReactToLogic(GameObject LogicNode)
    {
    }

    public void ReactToLogic(GameObject LogicNode, int requestedState)
    {
        if (!SNAPPED)
        {
            Debug.Log("LED Is not snapped, cannot react to logic.");
            return;
        }
        LogicNode VCCLogic = LEDNodeVCC.GetComponent<LogicNode>(); LogicNode GNDLogic = LEDNodeGnd.GetComponent<LogicNode>();
        LogicNode VCCCollidingNode = VCCLogic.GetCollidingNode().GetComponent<LogicNode>();
        LogicNode GNDCollidingNode = GNDLogic.GetCollidingNode().GetComponent<LogicNode>();
        SpriteRenderer LEDSpriteRen = this.gameObject.GetComponent<SpriteRenderer>();
        if (GNDCollidingNode.GetLogicState() == (int)LOGIC.LOW && VCCCollidingNode.GetLogicState() == (int)LOGIC.HIGH)
        {
            LEDSpriteRen.sprite = LEDOn;
            LEDState = true;
        }
        else
        {
            LEDSpriteRen.sprite = LEDOff;
            LEDState = false;
        }
    }


}
