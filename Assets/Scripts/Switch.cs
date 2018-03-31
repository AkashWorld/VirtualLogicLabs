using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The switch is another important movable device, similar to the LED. 
/// It contains three Logic Nodes, two of which are inputs, and one of which is an output. 
/// It can be toggled up or down, and will prioritize the output to reflect the top, 
/// or bottom Logic Node input. Similar to the LED, they play an important role 
/// when analyzing the built in circuit as they can be designated as an overall 
/// input to the system.
/// </summary>
public class Switch : MonoBehaviour, LogicInterface {
	GameObject DeviceGameObject;
	private const string LOGIC_DEVICE_ID = "SPDT_SWITCH";
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool SNAPPED = false; //Set to true if all Logic Nodes of this device is in collision with an external node
    private GameObject topNode; //input
    private GameObject middleNode; //output
    private GameObject bottomNode; //input
    private bool SwitchUp = false;
    private LogicManager logicManager;



	/// <summary>
    ///  Place Logic Nodes on the switch GameObject on the top, middle, and bottom
    /// </summary>
	void Start () {
        logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
		DeviceGameObject = this.gameObject;

        topNode = new GameObject(LOGIC_DEVICE_ID + "TOP"); //logic node with the name leftlogicnode_{i}_0
        topNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        topNode.transform.localPosition = new Vector3(.009f, .2025f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        topNode.transform.localScale = new Vector3(.10F, .10F, 0);
        topNode.AddComponent<LogicNode>();


        middleNode = new GameObject(LOGIC_DEVICE_ID + "MIDDLE"); //logic node with the name leftlogicnode_{i}_0
        middleNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        middleNode.transform.localPosition = new Vector3(.009f, -.01f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        middleNode.transform.localScale = new Vector3(.10F, .10F, 0);
        middleNode.AddComponent<LogicNode>();


        bottomNode = new GameObject(LOGIC_DEVICE_ID + "BOTTOM"); //logic node with the name leftlogicnode_{i}_0
        bottomNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        bottomNode.transform.localPosition = new Vector3(.009f, -0.2225f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        bottomNode.transform.localScale = new Vector3(.10F, .10F, 0);
        bottomNode.AddComponent<LogicNode>();
    }

    /// <summary>
    /// Method to facilitate dragging the object
    /// </summary>
	void OnMouseDown()
	{
		Debug.Log("SWITCH Mouse Down");
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}
    /// <summary>
    /// Method to facilitate dragging the object
    /// </summary>
    void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		transform.position = curPosition;
	}

    /// <summary>
    /// Checks if all nodes are connected to another set of nodes
    /// </summary>
    /// <returns>Boolean value that indicates whether the device is SNAPPED or Not</returns>
    private bool CheckIfSnapped()
    {

        //Check if all nodes with the chip is colliding with another logic node;
        if (middleNode.GetComponent<LogicNode>().GetCollidingNode() == null || bottomNode.GetComponent<LogicNode>().GetCollidingNode() == null
            || topNode.GetComponent<LogicNode>().GetCollidingNode() == null)
        {
            Debug.Log("LED Not Snapped");
            SNAPPED = false;
        }
        else
        {
            Debug.Log("LED SNAPPED!");
            LogicNode middleNodeBehavior = middleNode.GetComponent<LogicNode>();
            GameObject CollidingNode = middleNodeBehavior.GetCollidingNode();
            Vector3 collidingNodePos = CollidingNode.transform.position;
            Vector3 offsetPosition = new Vector3(collidingNodePos.x - .009f, collidingNodePos.y + .01f , collidingNodePos.z);
            DeviceGameObject.transform.position = offsetPosition;
            SNAPPED = true;
        }

        return SNAPPED;
    }

    /// <summary>
    /// Method that detects if the Mouse is over the switch. It contains a method that
    /// checks if the user is Right clicking on the object to toggle the state of the switch.
    /// </summary>
    private void OnMouseOver()
    {
        //Right click trigger that switches the state of the switch, from UP to Down
        if (Input.GetMouseButtonDown(1))
        {
            if (SNAPPED && SwitchUp == false)
            {
                SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
                spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchUP");
                SwitchUp = true;
                this.ReactToLogic(this.gameObject, (int)LOGIC.INVALID);
            }
            else if (SNAPPED)
            {
                SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
                spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchDOWN");
                SwitchUp = false;
                this.ReactToLogic(this.gameObject, (int)LOGIC.INVALID);
            }
            else
            {
                this.ClearIO();
            }
            this.logicManager.ResetAllLogic();
        }
    }


    /// <summary>
    /// Method that switches the Sprite and state of the switch from top biased, to bottom biased
    /// </summary>
    /// <param name="toggleUp"></param>
    public void ToggleSwitch(bool toggleUp)
    {
        if (toggleUp && SNAPPED && SwitchUp == false)
        {
            SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
            spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchUP");
            SwitchUp = true;
            this.ReactToLogic(this.gameObject, (int)LOGIC.INVALID);
        }
        else if (!toggleUp && SNAPPED)
        {
            SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
            spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchDOWN");
            SwitchUp = false;
            this.ReactToLogic(this.gameObject, (int)LOGIC.INVALID);
        }
    }



    public void OnMouseUp()
    {
        CheckIfSnapped();
    }



    public bool isSnapped()
    {
        return SNAPPED;
    }



    // Update is called once per frame
    void Update () {
		
	}

    public void ReactToLogic(GameObject LogicNode)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Clears all of the node to LOGIC.INVALID of the Switch
    /// </summary>
    private void ClearIO()
    {
        topNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
        middleNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
        bottomNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
    }

    /// <summary>
    /// Method that reacts to the Logic Node's callback to handle it's Logic State
    /// </summary>
    /// <param name="LogicNode">LogicNode GameObject that called the method</param>
    /// <param name="requestedState">The requestedState that the Logic Node asked the device
    /// to change to.</param>
    public void ReactToLogic(GameObject LogicNode, int requestedState)
    {
        if (!SNAPPED)
        {
            Debug.Log("Switch cannot react to LOGIC, not SNAPPED.");
            return;
        }
        LogicNode topLogic = topNode.GetComponent<LogicNode>();
        LogicNode middleLogic = middleNode.GetComponent<LogicNode>();
        LogicNode bottomLogic = bottomNode.GetComponent<LogicNode>();
        LogicNode collidingTopLogic = topLogic.GetCollidingNode().GetComponent<LogicNode>();
        LogicNode collidingBottomLogic = bottomLogic.GetCollidingNode().GetComponent<LogicNode>();
        LogicNode collidingMiddleLogic = middleLogic.GetCollidingNode().GetComponent<LogicNode>();
        if (SwitchUp)
        {
            middleLogic.SetLogicState(collidingTopLogic.GetLogicState());
            collidingMiddleLogic.RequestStateChange(collidingTopLogic.GetLogicState());
        }
        else if (!SwitchUp)
        {
            middleLogic.SetLogicState(collidingBottomLogic.GetLogicState());
            collidingMiddleLogic.RequestStateChange(collidingTopLogic.GetLogicState());
        }

    }

    public GameObject GetTopNode()
    {
        return topNode;
    }
    public GameObject GetMiddleNode()
    {
        return middleNode;
    }
    public GameObject GetBotNode()
    {
        return bottomNode;
    }
}
