using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NANDGate : MonoBehaviour, LogicInterface {
    private Dictionary<string, GameObject> logic_dictionary = new Dictionary<string, GameObject>(); //Contains all the gameobject nodes for the 74LS400 chip.+
    private GameObject nandGameObject;
    private const string LOGIC_DEVICE_ID = "74LS400_NODE_";
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
    void Start () {
        nandGameObject = GameObject.Find("74LS400");
        //Loop that places Logic Nodes on the 74LS400 chip
        float horizontal_pos = -.12f; //set up for left side of the chip
        float vertical_pos = .58f; //top of the chip
        for(int i = 0; i < 14; i++)
        {
            GameObject logicNode = new GameObject(LOGIC_DEVICE_ID + i); //logic node with the name leftlogicnode_{i}_0
            logicNode.transform.parent = nandGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode.transform.localPosition = new Vector3(horizontal_pos, vertical_pos + i * (-.208f), 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode.transform.localScale = new Vector3(.10F, .10F, 0);
            setNodeProperties(logicNode, LOGIC_DEVICE_ID + i);
            logic_dictionary.Add(LOGIC_DEVICE_ID + i, logicNode);
            if(i == 6) //when the left side is complete
            {
                vertical_pos = .58f + 7*(.208f); //go back to the top
                horizontal_pos = horizontal_pos + .38f; //change the horizontal position to the right side

            }
        }
	}


    void OnMouseDown()
    {
        Debug.Log("74LS400 Mouse Down");
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
        Debug.Log("74LS400 Mouse Up");
        //On release of mouse, clamp the hcip to the nodes
        GameObject node_1;
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 0, out node_1))
        {
            LogicBehavior logicNodeScript = node_1.GetComponent<LogicBehavior>();
            GameObject collidingNode = logicNodeScript.getCollidingNode();
            if (collidingNode != null)
            {
                Debug.Log("Colling Node position: " + collidingNode.transform.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ReactToLogic(GameObject logicNode)
    {

    }
}
