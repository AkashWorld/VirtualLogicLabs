using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///     The LED is an important movable device, and similar to the ‘chips’,
///     they can be snapped. The LED takes in two inputs by detecting collision 
///     on both of it’s ‘legs’. If the shorter leg’s collided Logic Node has
///     a state of logic low and the longer leg’s collided Logic Node has a 
///     state of logic high, then the LED has a state of being “On”. This also
///     means that the sprite of the LED is modified to show that it is emitting
///     a light source. In every other situation, the LED is in the state of 
///     being “Off” and has a sprite that reflects that. The importance of the
///     LED doesn’t only come from being a good debugging device for the user,
///     but also is important in a technical aspect as it is used to check the
///     input and output states of the overall circuit of the lab. This will be
///     further expanded on.
/// </summary>
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

    /// <summary>
    /// Sets all 2 nodes in the specified position in the Logic Chips
    /// and loads the sprites for LEDON and LEDOFF into memory
    /// </summary>
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


    /// <summary>
    /// Mouse down on GameObject activates the movement of the object
    /// to follow the mouse position
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("LED Mouse Down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }
    /// <summary>
    /// Callback that notifies the object that the mouse is being dragged
    /// on it. This is used to help 'move' the GameObject by calculating the offset
    /// from the previous mouse position. 
    /// </summary>
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;
    }
        // Update is called once per frame
    void Update () {
		
	}
    /// <summary>
    /// Checks if the ground node is connected to ground.
    /// This is checked whenever a new state change is requested to be reacted to.
    /// </summary>
    /// <returns>True or False boolean value on whether the Device is considered on</returns>
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
    /// <summary>
    /// Check if the device has all it's nodes is colliding with another set of Logic Nodes
    /// </summary>
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


    public bool isSnapped()
    {
        return SNAPPED;
    }



    /// <summary>
    /// Checks if the chip is snapped when the Mouse click is released to snap it
    /// into position.
    /// </summary>
    public void OnMouseUp()
    {
        CheckIfSnapped();
    }

    /// <summary>
    /// Checks if the LED is in the on state, meaning
    /// that the ground node is connected to a ground state,
    /// and the vcc node is connected to a logic high.
    /// </summary>
    /// <returns></returns>
    public bool isLEDON()
    {
        Debug.Log("LED IS " + LEDState);
        return LEDState;
    }


    public void ReactToLogic(GameObject LogicNode)
    {
    }


    /// <summary>
    /// If the chip is snapped, react to the input logics and set the outputs
    /// to the correct states. Otherwise, clear the chips.
    /// </summary>
    /// <param name="logicNode"></param>
    /// <param name="requestedState"></param>
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
