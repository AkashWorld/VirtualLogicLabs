using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicNode : MonoBehaviour {
    private GameObject logic_node;
    private string logic_id;
    private int logic_state;
    LogicInterface OwningDevice;
    private GameObject collidingNode;
    private bool recentStateChange = false;
	// Use this for initialization
	void Start () {
        logic_state = (int)LOGIC.INVALID;
	}
	
	// Update is called once per frame
	void Update () {
        //check if node is colliding, and a recent state change is detected
        if(collidingNode != null && recentStateChange == true)
        {
            Debug.Log("Update() in Logic Node: " + this.logic_id);
            recentStateChange = false;
            LogicNode collidedBehavior = collidingNode.GetComponent<LogicNode>();
            collidedBehavior.RequestStateChange(this.GetLogicState());
        }
    }

    private void OnMouseUp()
    {
        if (GameObject.Find("wireTransition") == false)
        {
            Debug.Log("Mouse action on node: " + GetLogicId());
            OwningDevice.ReactToLogic(logic_node);
            if (GameObject.Find("green_wire_button").GetComponent<WireButtonBehavior>().buttonOn || GameObject.Find("black_wire_button").GetComponent<WireButtonBehavior>().buttonOn || GameObject.Find("red_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
            {
                GameObject wireTransition = new GameObject("wireTransition");
                wireTransition.transform.parent = this.logic_node.transform;
                WireTransitionBehavior wire_transition_behavior = wireTransition.AddComponent<WireTransitionBehavior>();
                wire_transition_behavior.setStartPosition(this.transform.position);
            }
        }
        else
        {
            GameObject wire = new GameObject("wire");
            LineRenderer line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            line.startWidth = (float)0.1;
            line.endWidth = (float)0.1;
            line.sortingLayerName = "ActiveDevices";
            if (GameObject.Find("green_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
            {
                line.startColor = new Color(0, 1, 0);
                line.endColor = new Color(0, 1, 0);
            }
            if (GameObject.Find("red_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
            {
                line.startColor = new Color(1, 0, 0);
                line.endColor = new Color(1, 0, 0);
            }
            if (GameObject.Find("black_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
            {
                line.startColor = new Color(0, 0, 0);
                line.endColor = new Color(0, 0, 0);
            }
            LineRenderer lines = GameObject.Find("wireTransition").GetComponent<LineRenderer>();
            line.SetPosition(0, lines.GetPosition(0));
            line.SetPosition(1, this.transform.position);
            Destroy(GameObject.Find("wireTransition"));
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "LOGIC_NODE")
        {
            collidingNode = coll.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == collidingNode)
        {
            LogicNode collided_logic_node = collision.gameObject.GetComponent<LogicNode>();
            if (collided_logic_node.GetLogicState() != (int)LOGIC.INVALID)
            {
                Debug.Log("Requesting state change to INVALID in Node " + collided_logic_node.GetLogicId());
                collided_logic_node.RequestStateChange((int)LOGIC.INVALID);
            }
            collidingNode = null;
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
    private void SetSpriteLogicColor(int state)
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
