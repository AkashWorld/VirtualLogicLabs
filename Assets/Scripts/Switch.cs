using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



	// Use this for initialization
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


	void OnMouseDown()
	{
		Debug.Log("SWITCH Mouse Down");
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		transform.position = curPosition;
	}

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

	// Update is called once per frame
	void Update () {
		
	}

    public void ReactToLogic(GameObject LogicNode)
    {
        throw new System.NotImplementedException();
    }


    private void ClearIO()
    {
        topNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
        middleNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
        bottomNode.GetComponent<LogicNode>().SetLogicState((int)LOGIC.INVALID);
    }


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
