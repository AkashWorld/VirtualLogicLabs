using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORGate : MonoBehaviour, LogicInterface
{
    private Dictionary<string, GameObject> logic_dictionary = new Dictionary<string, GameObject>(); //Contains all the gameobject nodes for the 74LS400 chip.+
    private GameObject ORGameObject;
    private const string LOGIC_DEVICE_ID = "74LS32_OR_NODE_";
    private Vector3 screenPoint;
    private Vector3 offset;
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
    void Start()
    {
        ORGameObject = GameObject.Find("74LS32");
        //Loop that places Logic Nodes on the 74LS400 chip
        float horizontal_pos = -.205f; //set up for left side of the chip
        float vertical_pos = .58f; //top of the chip
        for (int i = 0; i < 14; i++)
        {
            GameObject logicNode = new GameObject(LOGIC_DEVICE_ID + i); //logic node with the name leftlogicnode_{i}_0
            logicNode.transform.parent = ORGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode.transform.localPosition = new Vector3(horizontal_pos, vertical_pos + i * (-.208f), 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode, LOGIC_DEVICE_ID + i);
            logic_dictionary.Add(LOGIC_DEVICE_ID + i, logicNode);
            if (i == 6) //when the left side is complete
            {
                vertical_pos = .58f + 7 * (.208f); //go back to the top
                horizontal_pos = horizontal_pos + .532f; //change the horizontal position to the right side

            }
        }
    }


    void OnMouseDown()
    {
        Debug.Log("74LS32 Mouse Down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;

    }
    void OnMouseUp()
    {
        Debug.Log("74LS32 Mouse Up");

        //Check if all nodes with the chip is colliding with another logic node;
        foreach (KeyValuePair<string, GameObject> entry in logic_dictionary)
        {
            GameObject logic_node = entry.Value;
            LogicBehavior logic_behavior = logic_node.GetComponent<LogicBehavior>();
            if (logic_behavior.getCollidingNode() == null)
            {
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
            Debug.Log("74LS32 SNAPPED!");
            Debug.Log("Colliding Node " + collidingNodeLeft.name + " position: " + collidingNodeLeft.transform.position);
            Vector3 collidingNodePos = collidingNodeLeft.transform.position;
            Vector3 offsetPosition = new Vector3(collidingNodePos.x + .245f, collidingNodePos.y - .58f, collidingNodePos.z);
            ORGameObject.transform.position = offsetPosition;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReactToLogic(GameObject logicNode)
    {

    }
}
