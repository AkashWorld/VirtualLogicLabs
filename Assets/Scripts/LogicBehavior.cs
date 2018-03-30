using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
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
	
	// Update is called once per frame
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

    //Sets logic state of this particular component. 
    //logic_id MUST be set before this method is called
    //Accepted values are LOGIC.HIGH(int = 10) and LOGIC.LOW(int = -10)
    //DOES NOT set '{RecentStateChange}' that gets checked in the update() method
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


    //Sets logic state of this particular component. 
    //logic_id MUST be set before this method is called
    //Accepted values are LOGIC.HIGH(int = 10) and LOGIC.LOW(int = -10)
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
