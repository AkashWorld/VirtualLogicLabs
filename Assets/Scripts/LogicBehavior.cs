using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBehavior : MonoBehaviour {
    enum LOGIC { HIGH = 1, LOW = 0, INVALID = -1}
    private GameObject logic_node;
    private string logic_id;
    private int logic_state;
    LogicInterface OwningDevice;
    private GameObject CollisionNode;
	// Use this for initialization
	void Start () {
        logic_state = (int)LOGIC.INVALID;
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "LOGIC_NODE")
        {
            CollisionNode = coll.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == CollisionNode)
        {
            CollisionNode = null;
        }
    }

    public GameObject getCollidingNode()
    {
        if (CollisionNode != null)
        {
            return CollisionNode;
        }
        return null;
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse action on node: " + getLogicId());
        OwningDevice.ReactToLogic(logic_node);
    }


    public void setLogicNode(GameObject logic_node)
    {
        this.logic_node = logic_node;
        logic_node.tag = "LOGIC_NODE";
    }
    public GameObject getLogicNode()
    {
        return this.logic_node;
    }


    public void setOwningDevice(LogicInterface deviceInterface)
    {
        this.OwningDevice = deviceInterface;
    }


    public void setSpriteLogicColor(int state)
    {
        SpriteRenderer spriteRenderer = getLogicNode().GetComponent<SpriteRenderer>() as SpriteRenderer;
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
    public int setLogicState(int state)
    {
        if((state == (int)LOGIC.HIGH || state == (int)LOGIC.LOW || state == (int)LOGIC.INVALID) && logic_id != null)
        {
            if(state == 0)
            {
                Debug.Log("Node: " + logic_id + " set to logic state: LOW");
            }
            else if(state == 1)
            {
                Debug.Log("Node: " + logic_id + " set to logic state: HIGH");
            }
            this.logic_state = state;
            setSpriteLogicColor(this.logic_state);
            return logic_state;
        }
        else
        {
            Debug.Log("Error setting logic state.");
            if(logic_id == null)
            {
                Debug.Log("Logic ID is not set!");
            }
            Debug.Log("State change requested to: " + state);
        }
        return -100; //error
    }


    public int getLogicState()
    {
        return this.logic_state;
    }
    public void setLogicId(string logic_id)
    {
        this.logic_id = logic_id;
    }
    public string getLogicId()
    {
        return this.logic_id;
    }

}
