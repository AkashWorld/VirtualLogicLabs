using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoboardObject : MonoBehaviour {
    /**
     * Left/Right column: 20x2
     * Middle Left/Right column: 27x5
     **/
    //ArrayList to hold references to the {Logic Node Game Objects} to reference.
    GameObject protoboard;
    const string LEFT_NODE = "leftlogicnode";
    const string RIGHT_NODE = "rightlogicnode";
    const string MIDDLE_L_NODE = "m_leftnode";
    const string MIDDLE_R_NODE = "m_rightnode";

    float distance;
    bool dragging = false;

    private void setNodeProperties(GameObject logicNode, string logicNodeID)
    {
        SpriteRenderer sprite_renderer = logicNode.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
        sprite_renderer.sprite = Resources.Load<Sprite>("logicCircle");
        sprite_renderer.sortingLayerName = "Logic";
        LogicBehavior logic_behavior = logicNode.AddComponent<LogicBehavior>() as LogicBehavior; //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
        logic_behavior.setLogicId(logicNodeID); //logic id that sets all the nodes on the left column of the LEFT section of the protoboard the same id
        logic_behavior.setLogicNode(logicNode);
        BoxCollider2D box_collider = logicNode.AddComponent<BoxCollider2D>();
        box_collider.size = new Vector2(.1f,.1f);
        box_collider.isTrigger = true;
        Rigidbody2D rigidbody = logicNode.AddComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    
    }


	// Use this for initialization
	void Start () {
        protoboard = GameObject.Find("Protoboard");

        float vertical_offset = 0; //this variable dictate the offset in the Y axis of the protoboard when populating the logic nodes

        //Populate leftmost column
        for(int i = 0; i < 20; i++)
        {
            GameObject logicNode_0 = new GameObject(LEFT_NODE + "_" + i + "_" + 0); //logic node with the name leftlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(-1.745F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_0, LEFT_NODE + "_LEFT");

            GameObject logicNode_1 = new GameObject(LEFT_NODE + "_" + i + "_" + 1); //logic node with the name leftlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-1.960F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_1, LEFT_NODE + "_RIGHT");


            vertical_offset = vertical_offset - .21F;
            if (i == 4 || i == 9 || i == 14) //At these intervals, there are bigger gaps that need to be accounted for
            {
                vertical_offset = vertical_offset - .185F;
            }
        }

        vertical_offset = 0;
        //Populate rightmost column
        for (int i = 0; i < 20; i++)
        {
            GameObject logicNode_0 = new GameObject(RIGHT_NODE + "_" + i + "_" + 0); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(1.745F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_0, RIGHT_NODE + "_LEFT"); 
 

            GameObject logicNode_1 = new GameObject(RIGHT_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(1.960F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode_1, RIGHT_NODE + "_RIGHT"); 

            vertical_offset = vertical_offset - .21F;
            if (i == 4 || i == 9 || i == 14) //At these intervals, there are bigger gaps that need to be accounted for
            {
                vertical_offset = vertical_offset - .185F;
            }
        }

        vertical_offset = 0;
        for (int i = 0; i < 27; i++)
        {
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

            vertical_offset = vertical_offset - .2070F;
        }


        vertical_offset = 0;
        for (int i = 0; i < 27; i++)
        {
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

            vertical_offset = vertical_offset - .2070F;
        }
    }
    /*

    private void OnMouseDown()
    {
        Debug.Log("MouseDown detected");
        distance = Vector3.Distance(protoboard.transform.position, Camera.main.transform.position);
        dragging = true;
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUp detected");
        dragging = false;
    }

    */
    // Update is called once per frame
    void Update () {
        /*
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            protoboard.transform.position = rayPoint;
        }*/

    }
}
