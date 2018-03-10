using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBehavior : MonoBehaviour {
    private GameObject logic_node;
    private string logic_id;
    private int logic_state;
    LogicInterface OwningDevice;
    private GameObject collisionNode;
    private bool recentStateChange = false;
	// Use this for initialization
	void Start () {
        logic_state = (int)LOGIC.INVALID;
	}
	
	// Update is called once per frame
	void Update () {
        //check if node is colliding, and a recent state change is detected
        if(collisionNode != null && recentStateChange == true)
        {
            Debug.Log("Update() in Logic Node: " + this.logic_id);
            recentStateChange = false;
            LogicBehavior collidedBehavior = collisionNode.GetComponent<LogicBehavior>();
            collidedBehavior.RequestStateChange(this.GetLogicState());
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "LOGIC_NODE")
        {
            collisionNode = coll.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == collisionNode)
        {
            collisionNode = null;
        }
    }

    public GameObject GetCollidingNode()
    {
        if (collisionNode != null)
        {
            return collisionNode;
        }
        return null;
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse action on node: " + GetLogicId());
        OwningDevice.ReactToLogic(logic_node);
    }

    public void RequestStateChange(int RequestedState)
    {
        this.OwningDevice.ReactToLogic(this.logic_node, RequestedState);
    }

    public void SetLogicNode(GameObject logic_node)
    {
        this.logic_node = logic_node;
        logic_node.tag = "LOGIC_NODE";
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
    public void SetSpriteLogicColor(int state)
    {
        SpriteRenderer spriteRenderer = GetLogicNode().GetComponent<SpriteRenderer>() as SpriteRenderer;
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
    //Accepted values are LOGIC.HIGH(int = 1) and LOGIC.LOW(int = 0)
    public void SetLogicState(int requesetedState)
    {
        int currentState = this.GetLogicState();
        //if change is detected in state
        if (currentState != requesetedState)
        {
            //check if value of the requested state is valid
            if ((requesetedState == (int)LOGIC.HIGH || requesetedState == (int)LOGIC.LOW 
                || requesetedState == (int)LOGIC.INVALID) && logic_id != null)
            {
                //debug statements
                if (requesetedState == (int)LOGIC.LOW)
                {
                    Debug.Log("Node: " + logic_id + " set to logic state: LOW");
                }
                else if (requesetedState == (int)LOGIC.HIGH)
                {
                    Debug.Log("Node: " + logic_id + " set to logic state: HIGH");
                }
                this.logic_state = requesetedState;
                this.recentStateChange = true;
                SetSpriteLogicColor(this.logic_state);
            }
            else
            {
                Debug.Log("Error setting logic state.");
                if (logic_id == null)
                {
                    Debug.Log("Logic ID is not set!");
                }
                Debug.Log("State change requested to: " + requesetedState);
            }
        }
        return; 
    }
    public int GetLogicState()
    {
        return this.logic_state;
    }
    public void SetLogicId(string logic_id)
    {
        this.logic_id = logic_id;
    }
    public string GetLogicId()
    {
        return this.logic_id;
    }

}
