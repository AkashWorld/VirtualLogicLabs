using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBehavior : MonoBehaviour {
    enum LOGIC { HIGH = 1, LOW = 0, INVALID = -1}
    private string logic_id;
    private int logic_state;
    private GameObject logic_node;
	// Use this for initialization
	void Start () {
        logic_state = (int)LOGIC.LOW;
	}
	
	// Update is called once per frame
	void Update () {

    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Mouse down on Logic Node " + this.getLogicId());
        if (this.getLogicState() == (int)LOGIC.LOW)
        {
            Debug.Log("Changing Logic State to High");
            this.setLogicState((int)LOGIC.HIGH);
            SpriteRenderer spriteRenderer = getLogicNode().GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.material.color = new Color(1, 0, 0);
        }
        else
        {
            Debug.Log("Changing Logic State to Low");
            this.setLogicState((int)LOGIC.LOW);
            SpriteRenderer spriteRenderer = getLogicNode().GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.material.color = new Color(0, 1, 0);
        }
    }

    public void setLogicNode(GameObject logic_node)
    {
        this.logic_node = logic_node;
    }
    public GameObject getLogicNode()
    {
        return this.logic_node;
    }


    //Sets logic state of this particular component. 
    //logic_id MUST be set before this method is called
    //Accepted values are LOGIC.HIGH(int = 1) and LOGIC.LOW(int = 2)
    int setLogicState(int state)
    {
        if((state == (int)LOGIC.HIGH || state == (int)LOGIC.LOW) && logic_id != null)
        {
            Debug.Log("Node: " + logic_id + "set to logic state: " + state);
            this.logic_state = state;
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
    int getLogicState()
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
