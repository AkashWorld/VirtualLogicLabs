using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, LogicInterface {
	GameObject DeviceGameObject;
	private const string LOGIC_DEVICE_ID = "ST_SWITCH";
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool SNAPPED = false; //Set to true if all Logic Nodes of this device is in collision with an external node
    private GameObject middleNode;
    private GameObject bottomNode;
    private bool SwitchOn = false;


	private void setNodeProperties(GameObject logicNode, string logicNodeID)
	{
		LogicNode logic_behavior = logicNode.AddComponent<LogicNode>() as LogicNode; //Adds the LogicNode.cs component to this gameobject to control logic behavior
		logic_behavior.SetLogicId(logicNodeID); //logic id that sets all the nodes on the left column of the LEFT section of the protoboard the same id
		logic_behavior.SetLogicNode(logicNode);
		logic_behavior.SetOwningDevice(this);
		SpriteRenderer sprite_renderer = logicNode.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
		sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/logicCircle");
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

        middleNode = new GameObject(LOGIC_DEVICE_ID + "MIDDLE"); //logic node with the name leftlogicnode_{i}_0
        middleNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        middleNode.transform.localPosition = new Vector3(.009f, -.01f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        middleNode.transform.localScale = new Vector3(.10F, .10F, 0);
        setNodeProperties(middleNode, LOGIC_DEVICE_ID + "MIDDLE");


        bottomNode = new GameObject(LOGIC_DEVICE_ID + "BOTTOM"); //logic node with the name leftlogicnode_{i}_0
        bottomNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        bottomNode.transform.localPosition = new Vector3(.009f, -0.2225f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        bottomNode.transform.localScale = new Vector3(.10F, .10F, 0);
        setNodeProperties(bottomNode, LOGIC_DEVICE_ID + "BOTTOM");
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

    private void CheckIfSnapped()
    {

        //Check if all nodes with the chip is colliding with another logic node;
        if (middleNode.GetComponent<LogicNode>().GetCollidingNode() == null || bottomNode.GetComponent<LogicNode>().GetCollidingNode() == null)
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


    }

    void OnMouseUp()
    {
        CheckIfSnapped();
        if(SNAPPED && SwitchOn == false)
        {
            SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
            spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchUP");
            SwitchOn = true;
        }
        else if(SNAPPED)
        {
            SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
            spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchDOWN");
            SwitchOn = false;
            LogicNode bottomNodeLogic = bottomNode.GetComponent<LogicNode>();
            bottomNodeLogic.SetLogicState((int)LOGIC.INVALID);
            bottomNodeLogic.GetCollidingNode().GetComponent<LogicNode>().RequestStateChange((int)LOGIC.INVALID);
        }
        else
        {
            SpriteRenderer spr_ren = DeviceGameObject.GetComponent<SpriteRenderer>();
            spr_ren.sprite = Resources.Load<Sprite>("Sprites/SwitchDOWN");
            SwitchOn = false;
        }

    }

	// Update is called once per frame
	void Update () {
		
	}

    public void ReactToLogic(GameObject LogicNode)
    {
        throw new System.NotImplementedException();
    }

    public void ReactToLogic(GameObject LogicNode, int requestedState)
    {
        if(SNAPPED && LogicNode == middleNode && SwitchOn)
        {
            int inputVoltage = requestedState;
            Debug.Log("Switch: detected input change " + requestedState);
            LogicNode middleNodeBehavior = middleNode.GetComponent<LogicNode>();
            middleNodeBehavior.SetLogicState(inputVoltage);
            LogicNode bottomNodeBehavior = bottomNode.GetComponent<LogicNode>();
            bottomNodeBehavior.SetLogicState(inputVoltage);
            LogicNode collidingNodeBehavior = bottomNodeBehavior.GetCollidingNode().GetComponent<LogicNode>();
            collidingNodeBehavior.RequestStateChange(inputVoltage);
        }
    }


}
