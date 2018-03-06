using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NANDGate : MonoBehaviour, LogicInterface {
    private Dictionary<string, GameObject> logic_dictionary = new Dictionary<string, GameObject>(); //Contains all the gameobject nodes for the 74LS400 chip.+
    private GameObject nandGameObject;
    private const string LOGIC_DEVICE_ID = "74LS00_NAND_NODE_";
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool SNAPPED = false;
    private void setNodeProperties(GameObject logicNode, string logicNodeID)
    {
        LogicBehavior logic_behavior = logicNode.AddComponent<LogicBehavior>() as LogicBehavior; //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
        logic_behavior.setLogicId(logicNodeID); //logic id that sets all the nodes on the left column of the LEFT section of the protoboard the same id
        logic_behavior.setLogicNode(logicNode);
        logic_behavior.setOwningDevice(this);
        SpriteRenderer sprite_renderer = logicNode.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
        sprite_renderer.sprite = Resources.Load<Sprite>("logicCircle");
        sprite_renderer.sortingLayerName = "Logic";
        BoxCollider2D box_collider = logicNode.AddComponent<BoxCollider2D>();
        box_collider.size = new Vector2(1f, 1f);
        box_collider.isTrigger = true;
        Rigidbody2D rigidbody = logicNode.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;

    }

    // Use this for initialization
    void Start () {
        nandGameObject = GameObject.Find("74LS00");
        //Loop that places Logic Nodes on the 74LS400 chip
        float horizontal_pos = -.205f; //set up for left side of the chip
        float vertical_pos = .58f; //top of the chip
        float vertical_direct = -.208f;
        for(int i = 0; i < 14; i++)
        {
            GameObject logicNode = new GameObject(LOGIC_DEVICE_ID + i); //logic node with the name leftlogicnode_{i}_0
            logicNode.transform.parent = nandGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode.transform.localPosition = new Vector3(horizontal_pos, vertical_pos + i * (vertical_direct), 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode, LOGIC_DEVICE_ID + i);
            logic_dictionary.Add(LOGIC_DEVICE_ID + i, logicNode);
            if(i == 6) //when the left side is complete
            {
                vertical_pos = vertical_pos + (13 * vertical_direct);
                vertical_direct = .208f;
                horizontal_pos = horizontal_pos + .532f; //change the horizontal position to the right side

            }
        }
	}


    void OnMouseDown()
    {
        Debug.Log("74LS00 Mouse Down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;

    }

    //method that handles input output with collisions
    private void chipIO()
    {
        GameObject logic_0, logic_1, logic_2, logic_3, logic_4, logic_5, logic_6,
        logic_7,logic_8, logic_9, logic_10, logic_11, logic_12, logic_13;

        //GND
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 6, out logic_6))
        {
            LogicBehavior logic_behavior = logic_6.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if(collided_state != (int)LogicBehavior.LOGIC.LOW)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS00 Ground Input not set to LOW");
                return; //invalid
            }
        }
        //VCC
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 13, out logic_13))
        {
            LogicBehavior logic_behavior = logic_13.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if(collided_state != (int)LogicBehavior.LOGIC.HIGH)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS00 VCC Input not set to HIGH");
                return;
            }
        }
        //NAND INPUT 1
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 0, out logic_0))
        {
            LogicBehavior logic_behavior = logic_0.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if(collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 0 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND INPUT 1
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 1, out logic_1))
        {
            LogicBehavior logic_behavior = logic_1.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 1 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND INPUT 1
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 3, out logic_3))
        {
            LogicBehavior logic_behavior = logic_3.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 3 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND INPUT 1
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 4, out logic_4))
        {
            LogicBehavior logic_behavior = logic_4.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 4 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND ------OUTPUT------- 1
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 5, out logic_5))
        {
            LogicBehavior logic_behavior = logic_5.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            LogicBehavior lb_0, lb_1, lb_3, lb_4; //LogicBehavior references
            lb_0 = logic_0.GetComponent<LogicBehavior>(); lb_1 = logic_1.GetComponent<LogicBehavior>();
            lb_3 = logic_3.GetComponent<LogicBehavior>(); lb_4 = logic_4.GetComponent<LogicBehavior>();
            int low = (int)LogicBehavior.LOGIC.LOW;
            int invalid = (int)LogicBehavior.LOGIC.INVALID;
            if (lb_0.getLogicState() == low && lb_1.getLogicState() == low && lb_3.getLogicState() == low && lb_4.getLogicState() == low)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.HIGH);
            }
            else if (lb_0.getLogicState() != invalid && lb_1.getLogicState() != invalid && lb_3.getLogicState() != invalid && lb_4.getLogicState() != invalid)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.LOW);
            }
            else
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
            }
        }
        //NAND INPUT 2
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 8, out logic_8))
        {
            LogicBehavior logic_behavior = logic_8.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 8 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND INPUT 2
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 9, out logic_9))
        {
            LogicBehavior logic_behavior = logic_9.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 9 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND INPUT 2
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 11, out logic_11))
        {
            LogicBehavior logic_behavior = logic_11.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 11 input is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND INPUT 2
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 12, out logic_12))
        {
            LogicBehavior logic_behavior = logic_12.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            if (collided_state == (int)LogicBehavior.LOGIC.INVALID)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
                Debug.Log("NAND 74LS400 input 12 is invalid.");
            }
            else
            {
                logic_behavior.setLogicState(collided_state);
            }
        }
        //NAND ------OUTPUT----- 2
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 7, out logic_7))
        {
            LogicBehavior logic_behavior = logic_7.GetComponent<LogicBehavior>();
            GameObject collided_node = logic_behavior.getCollidingNode();
            LogicBehavior collided_behavior = collided_node.GetComponent<LogicBehavior>();
            int collided_state = collided_behavior.getLogicState();
            LogicBehavior lb_8, lb_9, lb_11, lb_12; //LogicBehavior references
            lb_8 = logic_8.GetComponent<LogicBehavior>(); lb_9 = logic_9.GetComponent<LogicBehavior>();
            lb_11 = logic_11.GetComponent<LogicBehavior>(); lb_12 = logic_12.GetComponent<LogicBehavior>();
            int low = (int)LogicBehavior.LOGIC.LOW;
            int invalid = (int)LogicBehavior.LOGIC.INVALID;
            if(lb_8.getLogicState() == low && lb_9.getLogicState() == low && lb_11.getLogicState() == low && lb_12.getLogicState() == low)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.HIGH);
            }
            else if(lb_8.getLogicState() != invalid && lb_9.getLogicState() != invalid && lb_11.getLogicState() != invalid && lb_12.getLogicState() != invalid)
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.LOW);
            }
            else
            {
                logic_behavior.setLogicState((int)LogicBehavior.LOGIC.INVALID);
            }
        }
    }
 
        void OnMouseUp()
        {
            Debug.Log("74LS00 Mouse Up");

            //Check if all nodes with the chip is colliding with another logic node;
            foreach (KeyValuePair<string, GameObject> entry in logic_dictionary)
            {
                GameObject logic_node = entry.Value;
                LogicBehavior logic_behavior = logic_node.GetComponent<LogicBehavior>();
                if(logic_behavior.getCollidingNode() == null)
                    {
                    SNAPPED = false;
                    Debug.Log("Snap not set.");
                    return;
                    }
            }
            //On release of mouse, SNAP the chip to the position
            GameObject node_left;
            //get both top left and top right logic nodes on the chip to check if they collided with any other logic nodes
            if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 0, out node_left))
            {
                LogicBehavior logicNodeScript_l = node_left.GetComponent<LogicBehavior>();
                GameObject collidingNodeLeft = logicNodeScript_l.getCollidingNode();
                Debug.Log("74LS00 SNAPPED!");
                Debug.Log("Colliding Node " + collidingNodeLeft.name + " position: " + collidingNodeLeft.transform.position);
                Vector3 collidingNodePos = collidingNodeLeft.transform.position;
                Vector3 offsetPosition = new Vector3(collidingNodePos.x + .245f, collidingNodePos.y - .58f, collidingNodePos.z);
                nandGameObject.transform.position = offsetPosition;
                SNAPPED = true;
            }


        }
    

    // Update is called once per frame
    void Update () {
        //Check if chip is snapped to protoboard, and then updates logic
        if (SNAPPED)
        {
            chipIO();
        }
	}

    public void ReactToLogic(GameObject logicNode)
    {

    }
}
