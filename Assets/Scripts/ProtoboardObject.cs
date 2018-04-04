using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Protoboard acts as both an input and output device on all of it’s Logic Nodes. 
/// A crucial data structure for the Protoboard is the hash table, due to the way the 
/// data is structured, and the speed of the retrieval of data. As specific rows, and 
/// specific columns of Logic Nodes have the relationship of representing one Logic Nodes, 
/// they need to represented in a way where a list of Logic Nodes is retrieved for a 
/// specific column/row request. A Hash Table is the perfect data structure for this as 
/// a key can be assigned to every set of related nodes, and a List (Array) data structure
/// of Logic Node GameObjects can be assigned as the value for the key value pair. During 
/// the ReactToLogic() function, the relevant list of of Logic Nodes can be received by 
/// knowing the calling Logic Node’s key in a time complexity of O(1). As mentioned earlier, 
/// a priority system is used to update the list of Logic Node’s state as a set must all 
/// have the same state. All colliding Logic Node’s with the set are checked for their Logic
/// States, and based on a priority system, the set as a whole is assigned one logic state.
/// The priority system assigns the logic low first, logic high second, and assigns logic 
/// neutral last. The protoboard is an immovable device as for the protoboard to be clickable, 
/// it would need to have a Box Collider component for the mouse input callbacks to be registered. 
/// However, since the Logic Nodes contained inside it also have Colliders, the Unity engine 
/// has a difficult time distinguishing which GameObject is colliding with which other GameObject.
/// We decided to remove the movable functionality from the protoboard due to this.
/// </summary>
/// 
public class ProtoboardObject : MonoBehaviour, LogicInterface{
    ///
    ///Left/Right column: 20x2
    /// Middle Left/Right column: 27x5
    ///
    int currentProtoboard = 0;
    Vector3[] protoboardPositions = new[] { new Vector3(-5.017177F, -1.284522F, 20), new Vector3(-4.29216F, -1.284522F, 20F), new Vector3(-3.5671F, -1.284522F, 20F), new Vector3(-2.84212F, -1.284522F, 20F), new Vector3(-2.11714F, -1.284522F, 20F), new Vector3(-1.39217F, -1.284522F, 20F), new Vector3(-0.66716F, -1.284522F, 20F) };
    string[] protoboardNames = {"extendedProtoboard", "extendedProtoboard1" , "extendedProtoboard2" , "extendedProtoboard3" , "extendedProtoboard4" , "extendedProtoboard5" , "extendedProtoboard6" }; 
    Vector3 horizontalOffset = new Vector3(0.725285F,0,0); 
    GameObject protoboard;
    const string LEFT_NODE = "leftlogicnode";
    const string RIGHT_NODE = "rightlogicnode";
    const string MIDDLE_L_NODE = "m_leftnode";
    const string MIDDLE_R_NODE = "m_rightnode";
    const string MIDDLE_LL_NODE = "m_farleftnode";
    const string MIDDLE_RR_NODE = "m_farrightnode";
    Dictionary<string, List<GameObject>> LogicID_Node_Dict; //Dictionary containing lists of matching Logic IDs


    public Dictionary<string, List<GameObject>> GetNodeDictionary()
    {
        return this.LogicID_Node_Dict;
    }
    
    /// <summary>
    /// Initializes the nodes on the protoboard and positions them. Adds them to the 
    /// protoboard's Dictionary data structure.
    /// </summary>
    void Start () {

        protoboard = this.gameObject;
        LogicID_Node_Dict = new Dictionary<string, List<GameObject>>(); //a dictionary (HASH TABLE) of the logic ID and GameObject pairs
        float vertical_offset = 0; //this variable dictate the offset in the Y axis of the protoboard when populating the logic nodes

        LogicID_Node_Dict.Add(LEFT_NODE + "_LEFT", new List<GameObject>());
        LogicID_Node_Dict.Add(LEFT_NODE + "_RIGHT", new List<GameObject>());
        //Populate leftmost column
        for (int i = 0; i < 20; i++)
        {
            //xx ·····  ·····  ··  
            GameObject logicNode_0 = new GameObject(LEFT_NODE + "_LEFT"); //logic node with the name leftlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(-3.41F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_0.AddComponent<LogicNode>();
            List<GameObject> leftnodeleftlist;
            if(LogicID_Node_Dict.TryGetValue(LEFT_NODE + "_LEFT", out leftnodeleftlist)) {
                leftnodeleftlist.Add(logicNode_0);
            }
            

            GameObject logicNode_1 = new GameObject(LEFT_NODE + "_RIGHT"); //logic node with the name leftlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-3.195F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_1.AddComponent<LogicNode>();
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
            GameObject logicNode_0 = new GameObject(RIGHT_NODE + "_LEFT"); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(3.195F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            LogicNode logicNode = logicNode_0.AddComponent<LogicNode>();
            

            List<GameObject> rightnodeleftlist;
            if (LogicID_Node_Dict.TryGetValue(RIGHT_NODE + "_LEFT", out rightnodeleftlist))
            {
                rightnodeleftlist.Add(logicNode_0);
            }

            GameObject logicNode_1 = new GameObject(RIGHT_NODE + "_RIGHT"); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(3.41F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_1.AddComponent<LogicNode>();
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
            GameObject logicNode_0 = new GameObject(MIDDLE_L_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(-1.125F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_0.AddComponent<LogicNode>();


            GameObject logicNode_1 = new GameObject(MIDDLE_L_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-.92F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_1.AddComponent<LogicNode>();

            GameObject logicNode_2 = new GameObject(MIDDLE_L_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(-.715F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_2.AddComponent<LogicNode>();

            GameObject logicNode_3 = new GameObject(MIDDLE_L_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(-.51F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_3.AddComponent<LogicNode>();

            GameObject logicNode_4 = new GameObject(MIDDLE_L_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(-.305F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_4.AddComponent<LogicNode>();

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
            GameObject logicNode_0 = new GameObject(MIDDLE_R_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(1.125F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_0.AddComponent<LogicNode>();

            GameObject logicNode_1 = new GameObject(MIDDLE_R_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(.92F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_1.AddComponent<LogicNode>();

            GameObject logicNode_2 = new GameObject(MIDDLE_R_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(.715F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_2.AddComponent<LogicNode>();

            GameObject logicNode_3 = new GameObject(MIDDLE_R_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(.51F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_3.AddComponent<LogicNode>();

            GameObject logicNode_4 = new GameObject(MIDDLE_R_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(.305F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_4.AddComponent<LogicNode>();

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



        vertical_offset = 0;
        for (int i = 0; i < 27; i++)
        {
            //··  ·····  xxxxx  ··
            GameObject logicNode_0 = new GameObject(MIDDLE_RR_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(2.58f, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_0.AddComponent<LogicNode>();

            GameObject logicNode_1 = new GameObject(MIDDLE_RR_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(2.375F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_1.AddComponent<LogicNode>();

            GameObject logicNode_2 = new GameObject(MIDDLE_RR_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(2.17F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_2.AddComponent<LogicNode>();

            GameObject logicNode_3 = new GameObject(MIDDLE_RR_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(1.965F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_3.AddComponent<LogicNode>();

            GameObject logicNode_4 = new GameObject(MIDDLE_RR_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(1.76F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_4.AddComponent<LogicNode>();

            LogicID_Node_Dict.Add(MIDDLE_RR_NODE + "_" + i, new List<GameObject>());
            List<GameObject> middle_row_list;
            if (LogicID_Node_Dict.TryGetValue(MIDDLE_RR_NODE + "_" + i, out middle_row_list))
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
            GameObject logicNode_0 = new GameObject(MIDDLE_LL_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(-2.58f, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_0.AddComponent<LogicNode>();

            GameObject logicNode_1 = new GameObject(MIDDLE_LL_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-2.375F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_1.AddComponent<LogicNode>();

            GameObject logicNode_2 = new GameObject(MIDDLE_LL_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(-2.17F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_2.AddComponent<LogicNode>();

            GameObject logicNode_3 = new GameObject(MIDDLE_LL_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(-1.965F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_3.AddComponent<LogicNode>();

            GameObject logicNode_4 = new GameObject(MIDDLE_LL_NODE + "_" + i); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(-1.76F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode_4.AddComponent<LogicNode>();

            LogicID_Node_Dict.Add(MIDDLE_LL_NODE + "_" + i, new List<GameObject>());
            List<GameObject> middle_row_list;
            if (LogicID_Node_Dict.TryGetValue(MIDDLE_LL_NODE + "_" + i, out middle_row_list))
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


    public List<GameObject> GetGameObjectByID(string ID)
    {
        List<GameObject> LogicNodeList;
        if (LogicID_Node_Dict.TryGetValue(ID, out LogicNodeList))
        {
            return LogicNodeList;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Method for debugging a set of Logic Nodes to different states
    /// by the programmer.
    /// </summary>
    /// <param name="logicNode">Logic Node that is conatained in the set that is being toggled.</param>
    private void ToggleNodeSet(GameObject logicNode)
    {
        LogicNode logicBehaviorScript = logicNode.GetComponent<LogicNode>();
        string logicID = logicBehaviorScript.gameObject.name;
        int state = logicBehaviorScript.GetLogicState();
        if (state == (int)LOGIC.INVALID)
        {
            Debug.Log("Logic INVALID is found, toggling to Logic Low");
            state = (int)LOGIC.LOW;
        }
        else if (state == (int)LOGIC.LOW)
        {
            Debug.Log("Logic LOW is found, Toggling to Logic HIGH");
            state = (int)LOGIC.HIGH;
        }
        else if (state == (int)LOGIC.HIGH)
        {
            Debug.Log("Logic HIGH is found, Toggling to Logic LOW");
            state = (int)LOGIC.INVALID;
        }
        //Get the list of GameObjects (LogicNodes) that have the same ID as the input logicNode
        List<GameObject> LogicNodeList;
        if (LogicID_Node_Dict.TryGetValue(logicID, out LogicNodeList))
        {
            //iterate through the list of logicNode
            foreach (GameObject node in LogicNodeList)
            {
                //if the itearting node is not the input logicNode, then set the node's state
                //to the input logicNode's state
                LogicNode logicScript = node.GetComponent<LogicNode>();
                logicScript.SetLogicState(state);

            }
        }
    }




    /// <summary>
    ///Interface method from LogicInterface.cs that allows the protoboard to react to any changes to its logic nodes.
    ///This method is called from OnMouseUp() function, so it regulates mouse toggles
    /// </summary>
    /// <param name="logicNode">
    /// The logic node that calls the ReactToLogic method
    /// </param>
    public void ReactToLogic(GameObject logicNode)
    {
  
        Debug.Log("Protoboard: reacting to node interaction.");
        ToggleNodeSet(logicNode);

    }

    /// <summary>
    ///Interface method from LogicInterface.cs that allows the protoboard to react to any changes to its logic nodes.
    ///This method is called from OnMouseUp() function, so it regulates mouse toggles
    /// </summary>
    /// <param name="logicNode"></param>
    /// <param name="requestedState"></param>
    /// The logic node that calls the ReactToLogic method and the requestedState
    public void ReactToLogic(GameObject logicNode, int requestedState)
    {
        Debug.Log("Protoboard node reacting: " + logicNode.gameObject.name);
        LogicNode logicBehavior = logicNode.GetComponent<LogicNode>();
        string logicID = logicBehavior.gameObject.name;
        //Get the list of GameObjects (LogicNodes) that have the same ID as the input logicNode
        List<GameObject> LogicNodeList;
        if (LogicID_Node_Dict.TryGetValue(logicID, out LogicNodeList))
        {
            int priorityState = (int)LOGIC.INVALID;
            //iterate through the list of logicNode and find the state that needs to be set for the entire set
            foreach (GameObject node in LogicNodeList)
            {
                LogicNode logicScript = node.GetComponent<LogicNode>();
                GameObject collidedNode = logicScript.GetCollidingNode();
                if(collidedNode != null)
                {
                    LogicNode collidingScript = collidedNode.GetComponent<LogicNode>();
                    //HIGH Logic State is only allowed to be the set's state if the set's state is INVALID
                    //or HIGH already
                    if(collidingScript.GetLogicState() == (int)LOGIC.HIGH 
                        && priorityState == (int)LOGIC.INVALID)
                    {
                        priorityState = (int)LOGIC.HIGH;
                    }
                    //LOW always gets priortiy
                    else if(collidingScript.GetLogicState() == (int)LOGIC.LOW)
                    {
                        priorityState = (int)LOGIC.LOW;
                    }
                    Debug.Log("Priority state found to be " + priorityState + " from Collided node "
                        + collidedNode.gameObject.name + " in Protoboard Node " + logicNode.gameObject.name 
                        + " from device " + collidedNode.gameObject.name);
                }
            }
            Debug.Log("PROTOBOARD Setting Logic Set " + logicID + " to state " + priorityState);

            foreach (GameObject node in LogicNodeList)
            {
                LogicNode logicScript = node.GetComponent<LogicNode>();
                logicScript.SetLogicState(priorityState);
            }
        }
    }

    
    void Update () {

    }

    public void IncrementProtoboard()
    {
        if (currentProtoboard >= 6) return; 
        currentProtoboard++; 
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>(); 
        sprite.sprite = Resources.Load<Sprite>("Sprites/Protoboards/" + protoboardNames[currentProtoboard]);
        this.gameObject.transform.position = protoboardPositions[currentProtoboard];
        List<string> keyList = new List<string>(this.LogicID_Node_Dict.Keys);
        foreach (string keyString in keyList)
        {
            foreach (GameObject child in LogicID_Node_Dict[keyString])
            {
                child.transform.localPosition = child.transform.localPosition - horizontalOffset;
            }
        }
        foreach(GameObject child in LogicID_Node_Dict[RIGHT_NODE + "_RIGHT"])
        {
            child.transform.localPosition = child.transform.localPosition + 2*horizontalOffset;
        }
        foreach (GameObject child in LogicID_Node_Dict[RIGHT_NODE + "_LEFT"])
        {
            child.transform.localPosition = child.transform.localPosition + 2*horizontalOffset;
        }
        for (int i = 0; i < 27; i++)
        {
            LogicID_Node_Dict.Add("extranode_" + i + "_" + currentProtoboard, new List<GameObject>());
            List<GameObject> extra_list;
            foreach (GameObject child in LogicID_Node_Dict[MIDDLE_RR_NODE + "_" + i])
            {
                GameObject clone = Instantiate(child, this.gameObject.transform);
                clone.name = "extranode_" + i + "_" + currentProtoboard;
                clone.transform.localPosition = clone.transform.localPosition + 2 * horizontalOffset * currentProtoboard;
                if (LogicID_Node_Dict.TryGetValue("extranode_" + i + "_" + currentProtoboard, out extra_list))
                {
                    extra_list.Add(clone);
                }
            }
        }
        return; 
    }

    public void DecrementProtoboard()
    {
        if (currentProtoboard <= 0) return;
        currentProtoboard--;
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = Resources.Load<Sprite>("Sprites/Protoboards/" + protoboardNames[currentProtoboard]);
        this.gameObject.transform.position = protoboardPositions[currentProtoboard];
        List<string> keyList = new List<string>(this.LogicID_Node_Dict.Keys);
        foreach (string keyString in keyList)
        {
            foreach (GameObject child in LogicID_Node_Dict[keyString])
            {
                child.transform.localPosition = child.transform.localPosition + horizontalOffset; 
            }
        }
        foreach (GameObject child in LogicID_Node_Dict[RIGHT_NODE + "_RIGHT"])
        {
            child.transform.localPosition = child.transform.localPosition - 2*horizontalOffset;
        }
        foreach (GameObject child in LogicID_Node_Dict[RIGHT_NODE + "_LEFT"])
        {
            child.transform.localPosition = child.transform.localPosition - 2*horizontalOffset;
        }
        for (int i = 0; i < 27; i++)
        {
            foreach (GameObject child in LogicID_Node_Dict["extranode_" + i + "_" + (currentProtoboard+1)])
            {
                Destroy(child); 
            }
            LogicID_Node_Dict.Remove("extranode_" + i + "_" + (currentProtoboard + 1)); 
        }
        return;
    }

}
