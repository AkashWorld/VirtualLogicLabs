using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoboardObject : MonoBehaviour, LogicInterface{
    /**
     * Left/Right column: 20x2
     * Middle Left/Right column: 27x5
     **/
    enum LOGIC { HIGH = 1, LOW = 0, INVALID = -1 }
    GameObject protoboard;
    const string LEFT_NODE = "leftlogicnode";
    const string RIGHT_NODE = "rightlogicnode";
    const string MIDDLE_L_NODE = "m_leftnode";
    const string MIDDLE_R_NODE = "m_rightnode";
    Dictionary<string, List<GameObject>> LogicID_Node_Dict; //Dictionary containing lists of matching Logic IDs


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
        box_collider.size = new Vector2(1f,1f);
        box_collider.isTrigger = true;
        Rigidbody2D rigidbody = logicNode.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    
    }


	// Use this for initialization
	void Start () {
        protoboard = GameObject.Find("Protoboard");
        LogicID_Node_Dict = new Dictionary<string, List<GameObject>>(); //a dictionary (HASH TABLE) of the logic ID and GameObject pairs
        float vertical_offset = 0; //this variable dictate the offset in the Y axis of the protoboard when populating the logic nodes

        LogicID_Node_Dict.Add(LEFT_NODE + "_LEFT", new List<GameObject>());
        LogicID_Node_Dict.Add(LEFT_NODE + "_RIGHT", new List<GameObject>());
        //Populate leftmost column
        for (int i = 0; i < 20; i++)
        {
            //xx ·····  ·····  ··  
            GameObject logicNode_0 = new GameObject(LEFT_NODE + "_" + i + "_" + 0); //logic node with the name leftlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(-1.745F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_0, LEFT_NODE + "_LEFT");
            List<GameObject> leftnodeleftlist;
            if(LogicID_Node_Dict.TryGetValue(LEFT_NODE + "_LEFT", out leftnodeleftlist)) {
                leftnodeleftlist.Add(logicNode_0);
            }
            

            GameObject logicNode_1 = new GameObject(LEFT_NODE + "_" + i + "_" + 1); //logic node with the name leftlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-1.960F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_1, LEFT_NODE + "_RIGHT");
            List<GameObject> leftnoderightlist;
            if (LogicID_Node_Dict.TryGetValue(LEFT_NODE + "_RIGHT", out leftnoderightlist))
            {
                leftnoderightlist.Add(logicNode_1);
            }

            vertical_offset = vertical_offset - .21F;
            if (i == 4 || i == 9 || i == 14) //At these intervals, there are bigger gaps that need to be accounted for
            {
                vertical_offset = vertical_offset - .185F;
            }
        }


        LogicID_Node_Dict.Add(RIGHT_NODE + "_LEFT", new List<GameObject>());
        LogicID_Node_Dict.Add(RIGHT_NODE + "_RIGHT", new List<GameObject>());
        vertical_offset = 0;
        //Populate rightmost column
        for (int i = 0; i < 20; i++)
        {
            //·· ·····  ·····  xx
            GameObject logicNode_0 = new GameObject(RIGHT_NODE + "_" + i + "_" + 0); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(1.745F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_0, RIGHT_NODE + "_LEFT");
            List<GameObject> rightnodeleftlist;
            if (LogicID_Node_Dict.TryGetValue(RIGHT_NODE + "_LEFT", out rightnodeleftlist))
            {
                rightnodeleftlist.Add(logicNode_0);
            }

            GameObject logicNode_1 = new GameObject(RIGHT_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(1.960F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_1, RIGHT_NODE + "_RIGHT");
            List<GameObject> rightnoderightlist;
            if (LogicID_Node_Dict.TryGetValue(RIGHT_NODE + "_RIGHT", out rightnoderightlist))
            {
                rightnoderightlist.Add(logicNode_1);
            }

            vertical_offset = vertical_offset - .21F;
            if (i == 4 || i == 9 || i == 14) //At these intervals, there are bigger gaps that need to be accounted for
            {
                vertical_offset = vertical_offset - .185F;
            }
        }

        vertical_offset = 0;
        for (int i = 0; i < 27; i++)
        {
            //·· xxxxx ·····  ··
            GameObject logicNode_0 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 0); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(-1.125F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_0, MIDDLE_L_NODE + "_" + i); 


            GameObject logicNode_1 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-.92F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_1, MIDDLE_L_NODE + "_" + i); 

            GameObject logicNode_2 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 2); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(-.715F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_2, MIDDLE_L_NODE + "_" + i); 

            GameObject logicNode_3 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 3); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(-.51F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_3, MIDDLE_L_NODE + "_" + i); 

            GameObject logicNode_4 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 4); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(-.305F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_4, MIDDLE_L_NODE + "_" + i);

            LogicID_Node_Dict.Add(MIDDLE_L_NODE + "_" + i, new List<GameObject>());
            List<GameObject> middle_row_list;
            if (LogicID_Node_Dict.TryGetValue(MIDDLE_L_NODE + "_" + i, out middle_row_list))
            {
                middle_row_list.Add(logicNode_0);
                middle_row_list.Add(logicNode_1);
                middle_row_list.Add(logicNode_2);
                middle_row_list.Add(logicNode_3);
                middle_row_list.Add(logicNode_4);
            }

                vertical_offset = vertical_offset - .2070F;
        }


        vertical_offset = 0;
        for (int i = 0; i < 27; i++)
        {
            //··  ·····  xxxxx  ··
            GameObject logicNode_0 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 0); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(1.125F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_0, MIDDLE_R_NODE + "_" + i); 

            GameObject logicNode_1 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(.92F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_1, MIDDLE_R_NODE + "_" + i); 

            GameObject logicNode_2 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 2); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(.715F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_2, MIDDLE_R_NODE + "_" + i); 

            GameObject logicNode_3 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 3); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(.51F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_3, MIDDLE_R_NODE + "_" + i);

            GameObject logicNode_4 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 4); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(.305F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_4, MIDDLE_R_NODE + "_" + i);

            LogicID_Node_Dict.Add(MIDDLE_R_NODE + "_" + i, new List<GameObject>());
            List<GameObject> middle_row_list;
            if (LogicID_Node_Dict.TryGetValue(MIDDLE_R_NODE + "_" + i, out middle_row_list))
            {
                middle_row_list.Add(logicNode_0);
                middle_row_list.Add(logicNode_1);
                middle_row_list.Add(logicNode_2);
                middle_row_list.Add(logicNode_3);
                middle_row_list.Add(logicNode_4);
            }

            vertical_offset = vertical_offset - .2070F;
        }
    }

    //Interface method from LogicInterface.cs that allows the protoboard to react to any changes to its logic nodes.
    public void ReactToLogic(GameObject logicNode)
    {
        Debug.Log("Protoboard: reacting to node interaction.");
        LogicBehavior logicBehaviorScript = logicNode.GetComponent<LogicBehavior>();
        string logicID = logicBehaviorScript.getLogicId();
        int state = logicBehaviorScript.getLogicState();
        if(state == (int)LOGIC.INVALID)
        {
            Debug.Log("Logic INVALID is found, toggling to Logic Low");
            state = (int)LOGIC.LOW;
        }
        else if(state == (int)LOGIC.LOW)
        {
            Debug.Log("Logic LOW is found, Toggling to Logic HIGH");
            state = (int)LOGIC.HIGH;
        }
        else if(state == (int)LOGIC.HIGH)
        {
            Debug.Log("Logic HIGH is found, Toggling to Logic LOW");
            state = (int)LOGIC.INVALID;
        }
        //Get the list of GameObjects (LogicNodes) that have the same ID as the input logicNode
        List<GameObject> LogicNodeList;
        if (LogicID_Node_Dict.TryGetValue(logicID, out LogicNodeList))
        {
            //iterate through the list of logicNode
            foreach(GameObject node in LogicNodeList)
            {
                //if the itearting node is not the input logicNode, then set the node's state
                //to the input logicNode's state
                LogicBehavior logicScript = node.GetComponent<LogicBehavior>();
                logicScript.setLogicState(state);

            }
        }

    }



    void Update () {


    }
}
