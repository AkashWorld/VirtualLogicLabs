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
            SpriteRenderer sprite_renderer_0 = logicNode_0.AddComponent<SpriteRenderer>();
            sprite_renderer_0.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_0.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_0 = logicNode_0.AddComponent<LogicBehavior>() as LogicBehavior; //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_0.setLogicId(LEFT_NODE + "_LEFT"); //logic id that sets all the nodes on the left column of the LEFT section of the protoboard the same id
            logic_behavior_0.setLogicNode(logicNode_0);
            CircleCollider2D circle_collider_0 = logicNode_0.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_0.isTrigger = true;
            circle_collider_0.radius = .05F;

            GameObject logicNode_1 = new GameObject(LEFT_NODE + "_" + i + "_" + 1); //logic node with the name leftlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-1.960F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_1 = logicNode_1.AddComponent<SpriteRenderer>();
            sprite_renderer_1.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_1.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_1 = logicNode_1.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_1.setLogicId(LEFT_NODE + "_RIGHT"); //logic id that sets all the nodes on the right column of the LEFT section of the protoboard the same id
            logic_behavior_1.setLogicNode(logicNode_1);
            CircleCollider2D circle_collider_1 = logicNode_1.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_1.isTrigger = true;
            circle_collider_1.radius = .05F;

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
            SpriteRenderer sprite_renderer_0 = logicNode_0.AddComponent<SpriteRenderer>();
            sprite_renderer_0.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_0.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_0 = logicNode_0.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_0.setLogicId(RIGHT_NODE + "_LEFT"); //logic id that sets all the nodes on the right column of the RIGHT section of the protoboard the same id
            logic_behavior_0.setLogicNode(logicNode_0);
            CircleCollider2D circle_collider_0 = logicNode_0.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_0.isTrigger = true;
            circle_collider_0.radius = .05F;

            GameObject logicNode_1 = new GameObject(RIGHT_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(1.960F, 2.275F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_1 = logicNode_1.AddComponent<SpriteRenderer>();
            sprite_renderer_1.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_1.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_1 = logicNode_1.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_1.setLogicId(RIGHT_NODE + "_RIGHT"); //logic id that sets all the nodes on the right column of the RIGHT section of the protoboard the same id
            logic_behavior_1.setLogicNode(logicNode_1);
            CircleCollider2D circle_collider_1 = logicNode_1.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_1.isTrigger = true;
            circle_collider_1.radius = .05F;

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
            SpriteRenderer sprite_renderer_0 = logicNode_0.AddComponent<SpriteRenderer>();
            sprite_renderer_0.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_0.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_0 = logicNode_0.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_0.setLogicId(MIDDLE_L_NODE + "_" +  i); //logic id that sets all the nodes on the same row the same logic ID on the middle left section
            logic_behavior_0.setLogicNode(logicNode_0);
            CircleCollider2D circle_collider_0 = logicNode_0.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_0.isTrigger = true;
            circle_collider_0.radius = .05F;


            GameObject logicNode_1 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(-.92F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_1 = logicNode_1.AddComponent<SpriteRenderer>();
            sprite_renderer_1.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_1.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_1 = logicNode_1.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_1.setLogicId(MIDDLE_L_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle left section
            logic_behavior_1.setLogicNode(logicNode_1);
            CircleCollider2D circle_collider_1 = logicNode_1.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_1.isTrigger = true;
            circle_collider_1.radius = .05F;

            GameObject logicNode_2 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 2); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(-.715F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_2 = logicNode_2.AddComponent<SpriteRenderer>();
            sprite_renderer_2.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_2.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_2 = logicNode_2.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_2.setLogicId(MIDDLE_L_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle left section
            logic_behavior_2.setLogicNode(logicNode_2);
            CircleCollider2D circle_collider_2 = logicNode_2.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_2.isTrigger = true;
            circle_collider_2.radius = .05F;

            GameObject logicNode_3 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 3); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(-.51F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_3 = logicNode_3.AddComponent<SpriteRenderer>();
            sprite_renderer_3.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_3.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_3 = logicNode_3.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_3.setLogicId(MIDDLE_L_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle left section
            logic_behavior_3.setLogicNode(logicNode_3);
            CircleCollider2D circle_collider_3 = logicNode_3.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_3.isTrigger = true;
            circle_collider_3.radius = .05F;

            GameObject logicNode_4 = new GameObject(MIDDLE_L_NODE + "_" + i + "_" + 4); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(-.305F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_4 = logicNode_4.AddComponent<SpriteRenderer>();
            sprite_renderer_4.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_4.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_4 = logicNode_4.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_4.setLogicId(MIDDLE_L_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle left section
            logic_behavior_4.setLogicNode(logicNode_4);
            CircleCollider2D circle_collider_4 = logicNode_4.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_4.isTrigger = true;
            circle_collider_4.radius = .05F;

            vertical_offset = vertical_offset - .2070F;
        }


        vertical_offset = 0;
        for (int i = 0; i < 27; i++)
        {
            GameObject logicNode_0 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 0); //logic node with the name rightlogicnode_{i}_0
            logicNode_0.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode_0.transform.localPosition = new Vector3(1.125F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_0.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_0 = logicNode_0.AddComponent<SpriteRenderer>();
            sprite_renderer_0.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_0.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_0 = logicNode_0.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_0.setLogicId(MIDDLE_R_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle right section
            logic_behavior_0.setLogicNode(logicNode_0);
            CircleCollider2D circle_collider_0 = logicNode_0.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_0.isTrigger = true;
            circle_collider_0.radius = .05F;

            GameObject logicNode_1 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 1); //logic node with the name rightlogicnode_{i}_1
            logicNode_1.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_1.transform.localPosition = new Vector3(.92F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_1.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_1 = logicNode_1.AddComponent<SpriteRenderer>();
            sprite_renderer_1.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_1.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_1 = logicNode_1.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_1.setLogicId(MIDDLE_R_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle right section
            logic_behavior_1.setLogicNode(logicNode_1);
            CircleCollider2D circle_collider_1 = logicNode_1.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_1.isTrigger = true;
            circle_collider_1.radius = .05F;

            GameObject logicNode_2 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 2); //logic node with the name rightlogicnode_{i}_2
            logicNode_2.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_2.transform.localPosition = new Vector3(.715F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_2.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_2 = logicNode_2.AddComponent<SpriteRenderer>();
            sprite_renderer_2.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_2.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_2 = logicNode_2.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_2.setLogicId(MIDDLE_R_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle right section
            logic_behavior_2.setLogicNode(logicNode_2);
            CircleCollider2D circle_collider_2 = logicNode_2.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_2.isTrigger = true;
            circle_collider_2.radius = .05F;

            GameObject logicNode_3 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 3); //logic node with the name rightlogicnode_{i}_3
            logicNode_3.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_3.transform.localPosition = new Vector3(.51F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_3.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_3 = logicNode_3.AddComponent<SpriteRenderer>();
            sprite_renderer_3.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_3.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_3 = logicNode_3.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_3.setLogicId(MIDDLE_R_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle right section
            logic_behavior_3.setLogicNode(logicNode_3);
            CircleCollider2D circle_collider_3 = logicNode_3.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_3.isTrigger = true;
            circle_collider_3.radius = .05F;

            GameObject logicNode_4 = new GameObject(MIDDLE_R_NODE + "_" + i + "_" + 4); //logic node with the name rightlogicnode_{i}_4
            logicNode_4.transform.parent = protoboard.transform; //sets the Protoboard game object as logicNode_1's parent
            logicNode_4.transform.localPosition = new Vector3(.305F, 2.69F + vertical_offset, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode_4.transform.localScale = new Vector3(.10F, .10F, 0);
            SpriteRenderer sprite_renderer_4 = logicNode_4.AddComponent<SpriteRenderer>();
            sprite_renderer_4.sprite = Resources.Load<Sprite>("logicCircle");
            sprite_renderer_4.sortingLayerName = "Logic";
            LogicBehavior logic_behavior_4 = logicNode_4.AddComponent<LogicBehavior>(); //Adds the LogicBehavior.cs component to this gameobject to control logic behavior
            logic_behavior_4.setLogicId(MIDDLE_R_NODE + "_" + i); //logic id that sets all the nodes on the same row the same logic ID on the middle right section
            logic_behavior_4.setLogicNode(logicNode_4);
            CircleCollider2D circle_collider_4 = logicNode_4.AddComponent<CircleCollider2D>() as CircleCollider2D;
            circle_collider_4.isTrigger = true;
            circle_collider_4.radius = .05F;

            vertical_offset = vertical_offset - .2070F;
        }
    }


    private void OnMouseDown()
    {
        Debug.Log("MouseDown detected");
        distance = Vector3.Distance(protoboard.transform.position, Camera.main.transform.position);
        dragging = true;
        Debug.Log("Distance found to be: " + distance);
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUp detected");
        dragging = false;
    }


    // Update is called once per frame
    void Update () {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            protoboard.transform.position = rayPoint;
        }

	}
}
