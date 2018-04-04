using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  We represent a Logic Node graphically with a small circle that has
///  three different colors, Green for Logic High, Red for Logic Low, 
///  and White for Neutral. These states are kept by each Logic Node as 
///  an integer that is predefined statically. Outside of testing scenarios,
///  every single Logic Node is a child of a GameObject that implements an 
///  interface called Logic Device. We will expand upon this further down
///  in the document.
///  For each Logic Node, it is incredibly important to determine if it is 
///  positionally overlapping with another Logic Node, analogous to a digital 
///  logic component connecting to another digital logic component via physical
///  contact.This is only detected when a collider component in the shape of the
///  GameObject’s collision perimeter is added to the Logic Node GameObject on 
///  object instantiation. Whenever this overlap happens between two Logic Nodes, 
///  a collision is detected by the Unity engine, and a callback function called 
///  OnTriggerEnter() is called, which notifies the programmer to react to this 
///  collision.For Logic Nodes, we notify the object that a collision has been
///  detected recently and keep note of the Logic Node that collided.The Logic
///  Node object does not immediately react to any collisions as the user may
///  be actively moving the Logic Node’s position. Upon reaching the next Update loop,
///  responsibility of how to change the Logic State is given to the owning device 
///  of the Logic Node.
/// </summary>
public class LogicNode : MonoBehaviour {
    private GameObject logic_node;
    public int logic_state;
    LogicInterface OwningDevice;
    private GameObject collidingNode;
    private bool recentStateChange = false;
    private bool recentCollisionEnter = false;
    private bool recentCollisionExit = false;
    private LogicManager logicManager;
    private GameObject protoboard;

	/// <summary>
    /// Creates the initilization state for a normal Logic Node. Sets the scale of every
    /// dimension to 10% the normal sprite size. Sets the tag of the GameObject to "LOGIC_NODE",
    /// adds a Sprite, Box Collider, and RigidBody2D components.
    /// </summary>
	void Start () {
        logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        this.gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
        protoboard = GameObject.Find("Protoboard");
        logic_state = (int)LOGIC.INVALID;
        logic_node = this.gameObject;
        this.gameObject.tag = "LOGIC_NODE";
        if (this.gameObject.GetComponent<SpriteRenderer>() == null)
        {
            SpriteRenderer sprite_renderer = this.gameObject.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
            sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/logicCircle");
            sprite_renderer.sortingLayerName = "Logic";
        }
        BoxCollider2D box_collider = this.gameObject.AddComponent<BoxCollider2D>();
        box_collider.size = new Vector2(1f, 1f);
        box_collider.isTrigger = true;
        Rigidbody2D rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }
	
	/// <summary>
    /// Update loop that checks if the node recently exited collision. If it did, it resets
    /// the Logic of all LogicNodes in the system.
    /// It also checks if there is a colliding node, and a recent state change or recent collision enter.
    /// </summary>
	void Update () {
        if (recentCollisionExit)
        {
            recentCollisionExit = false;
            this.logicManager.ResetAllLogic();
        }

        //check if node is colliding, and a recent state change is detected
        if ((collidingNode != null) && (recentStateChange  || recentCollisionEnter))
        {
            Debug.Log("Update() in Logic Node: " + this.gameObject.name);
            LogicNode collidedBehavior = collidingNode.GetComponent<LogicNode>();
            collidedBehavior.RequestStateChange(this.GetLogicState());
            recentStateChange = false;
            recentCollisionEnter = false;
        }
    }

    /// <summary>
    /// Checks if the user right clicked, asks owning device to take care of the Logic.
    /// </summary>
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (this.gameObject.transform.parent != null)
            {
                OwningDevice = this.gameObject.transform.parent.GetComponent<LogicInterface>();
                OwningDevice.ReactToLogic(this.gameObject);
            }
        }
    }


    /// <summary>
    /// Checks if the Logic Node entered collision with another Logic Node
    /// Also adds the Logic Node itself to the Logic Manager's tracking of Active 
    /// Logic Nodes.
    /// </summary>
    /// <param name="coll"></param>
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "LOGIC_NODE")
        {
            if (this.gameObject.transform.parent == protoboard)
            {   //Special case for protoboard to add all related nodes to the logic manager.
                logicManager.AddGameObject(protoboard.GetComponent<ProtoboardObject>().GetGameObjectByID(this.gameObject.name));
            }
            else
            {
                logicManager.AddGameObject(this.gameObject);
            }
            Debug.Log("Collision detected between node [" + this.gameObject.name + "] and [" + coll.gameObject.name + "]");
            this.collidingNode = coll.gameObject;
            this.recentCollisionEnter = true;
        }

    }

    /// <summary>
    /// On collision exit, takes this Logic Node, and all related nodes (for Protoboard),
    /// takes itself out of the Logic Manager's tracking of Active Logic Nodes.
    /// Also requests the owning device to set the Logic Node to neutral
    /// Logic State.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == collidingNode)
        {
            this.collidingNode = null;
            if (this.gameObject.transform.parent == protoboard)
            {   //Special case for protoboard to add all related nodes to the logic manager.
                logicManager.RemoveGameObject(protoboard.GetComponent<ProtoboardObject>().GetGameObjectByID(this.gameObject.name));
            }
            else
            {
                logicManager.RemoveGameObject(this.gameObject);
            }
            LogicNode collided_logic_node = collision.gameObject.GetComponent<LogicNode>();
            Debug.Log("Collision EXIT between node [" + this.gameObject.name + "] and [" + collision.gameObject.name + "]");
            if (collided_logic_node.GetLogicState() != (int)LOGIC.INVALID)
            {
                collided_logic_node.RequestStateChange((int)LOGIC.INVALID);
                this.RequestStateChange((int)LOGIC.INVALID);
                this.recentCollisionExit = true;
            }
        }
    }
    


    public GameObject GetCollidingNode()
    {
        if (collidingNode != null)
        {
            return collidingNode;
        }
        return null;
    }

    public void RequestStateChange(int RequestedState)
    {
        if(this.gameObject.transform.parent != null)
        {
            OwningDevice = this.gameObject.transform.parent.GetComponent<LogicInterface>();
            OwningDevice.ReactToLogic(this.gameObject, RequestedState);
        }
    }
    
    public GameObject GetLogicNode()
    {
        return this.logic_node;
    }


    public void SetOwningDevice(LogicInterface deviceInterface)
    {
        this.OwningDevice = deviceInterface;
    }

    //sets the color of the circular logic node
    private void SetSpriteLogicColor()
    {
        int state = this.logic_state;
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        if(spriteRenderer == null)
        {
            spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/logicCircle");
            spriteRenderer.sortingLayerName = "Logic";
        }
        if (state == (int)LOGIC.INVALID)
        {
            Debug.Log("Setting Color to White");
            spriteRenderer.material.color = new Color(1, 1, 1);
        }
        else if(state == (int)LOGIC.HIGH)
        {
            Debug.Log("Setting color to green.");
            spriteRenderer.material.color = new Color(0, 1, 0);
        }
        else if(state == (int)LOGIC.LOW)
        {
            Debug.Log("Setting color to red.");
            spriteRenderer.material.color = new Color(1, 0, 0);
        }
    }
    /// <summary>
    ///Sets logic state of this particular component. 
    ///logic_id MUST be set before this method is called
    ///Accepted values are LOGIC.HIGH(int = 10) and LOGIC.LOW(int = -10)
    ///DOES NOT set '{RecentStateChange}' that gets checked in the update() method
    /// </summary>
    /// <param name="requestedState"></param>

    public void SetLogicStateWithoutNotification(int requestedState)
    {
        //if change is detected in state
        if (this.logic_state != requestedState)
        {
            //check if value of the requested state is valid
            if ((requestedState == (int)LOGIC.HIGH || requestedState == (int)LOGIC.LOW
                || requestedState == (int)LOGIC.INVALID))
            {
                Debug.Log("Setting Logic State of Node " + this.gameObject.name + " to " + requestedState);
                this.logic_state = requestedState;
                SetSpriteLogicColor();
            }
            else
            {
                Debug.Log("Error setting logic state. Invalid requested recieved.");
            }
        }
        return;
    }

    ///<summary>
    ///Sets logic state of this particular component. 
    ///logic_id MUST be set before this method is called
    ///Accepted values are LOGIC.HIGH(int = 10) and LOGIC.LOW(int = -10)</summary>
    public void SetLogicState(int requestedState)
    {
        //if change is detected in state
        if (this.logic_state != requestedState)
        {
            //check if value of the requested state is valid
            if ((requestedState == (int)LOGIC.HIGH || requestedState == (int)LOGIC.LOW 
                || requestedState == (int)LOGIC.INVALID))
            {
                Debug.Log("Setting Logic State of Node " + this.gameObject.name + " to " + requestedState);
                this.logic_state = requestedState;
                this.recentStateChange = true;
                SetSpriteLogicColor();
            }
            else
            {
                Debug.Log("Error setting logic state. Invalid requested recieved.");
            }
        }
        return; 
    }




    public int GetLogicState()
    {
        return this.logic_state;
    }
 


}
