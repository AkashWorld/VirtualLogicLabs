using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Wire : MonoBehaviour, LogicInterface {
    private GameObject startNode, endNode;
    private GameObject DeviceGameObject;
    private const string LOGIC_DEVICE_ID = "WIRE_";
    private List<GameObject> wireInflectionObjects;
    private bool activelyBeingPlaced;
    private LineRenderer WireLine = null;
    private Color currentColor = Color.red;
    private int currentState;
    private GameObject placingText;

    // Use this for initialization
    void Start () {
        currentState = (int)LOGIC.INVALID;
        wireInflectionObjects = new List<GameObject>();
        DeviceGameObject = this.gameObject;

        startNode = new GameObject(LOGIC_DEVICE_ID + "START"); //logic node with the name leftlogicnode_{i}_0
        startNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        startNode.transform.localScale = new Vector3(.10F, .10F, 0);
        startNode.AddComponent<LogicNode>();

        endNode = new GameObject(LOGIC_DEVICE_ID + "END"); //logic node with the name leftlogicnode_{i}_0
        endNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        endNode.transform.localScale = new Vector3(.10F, .10F, 0);
        endNode.AddComponent<LogicNode>();

        placingText = new GameObject("PlacingText");
        placingText.transform.parent = this.gameObject.transform;
        SpriteRenderer textSprRend = placingText.AddComponent<SpriteRenderer>();
        textSprRend.sprite = Resources.Load<Sprite>("Sprites/PlacingWire");
        textSprRend.sortingLayerName = "FrontLayer";
        placingText.transform.position = new Vector3(-7.5f, -4.7f, 10);
        activelyBeingPlaced = true;

    }

    public void ToggleLineColor()
    {
        Debug.Log("Toggling WIRE color");
        if(currentColor == Color.red)
        {
            currentColor = Color.green;
            WireLine.SetColors(Color.green, Color.green);
        }
        else if(currentColor == Color.green)
        {
            currentColor = Color.yellow;
            WireLine.SetColors(Color.yellow, Color.yellow);
        }
        else if(currentColor == Color.yellow)
        {
            currentColor = Color.black;
            WireLine.SetColors(Color.black, Color.black);
        }
        else
        {
            currentColor = Color.red;
            WireLine.SetColors(Color.red, Color.red);
        }
    }


    //GO = GameObject
    private void DrawLineBetweenGO(Vector3 currentMousePos)
    {
        if (WireLine == null)
        {
            return;
        }
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        WireLine.positionCount = wireInflectionObjects.Count + 1;
        Debug.Log("DrawLineBetweenGO: Setting Line Renderer in Position Count - " + wireInflectionObjects.Count);
        WireLine.SetPosition(wireInflectionObjects.Count, new Vector3(worldPoint.x,worldPoint.y,10));
    }
	
    private void SetLogicNodePositions()
    {

        startNode.transform.position = new Vector3(wireInflectionObjects[0].transform.position.x, wireInflectionObjects[0].transform.position.y, 10);
        endNode.transform.position = new Vector3(wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position.x, 
            wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position.y, 10);

    }



	// Update is called once per frame
	void Update () {
        if (activelyBeingPlaced)
        {
            DrawLineBetweenGO(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                bool isClickedLogicNode = false;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    if(hit.collider.transform.tag == "LOGIC_NODE")
                    {
                        Debug.Log("Wire: Clicked on logic node!");
                        isClickedLogicNode = true;
                    }
                }
                Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(wireInflectionObjects.Count == 0) //starting position isn't chosen yet
                {
                    if (isClickedLogicNode)
                    { //create StartingLogicNode
                        Debug.Log("Wire: Starting sequence.");

                        GameObject wireInflectionPoint = new GameObject("WIRE_POINT_" + wireInflectionObjects.Count);
                        wireInflectionPoint.AddComponent<WireInflection>();
                        wireInflectionPoint.transform.parent = DeviceGameObject.transform;
                        wireInflectionPoint.transform.position = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, 10);
                        wireInflectionObjects.Add(wireInflectionPoint);

                        WireLine = this.gameObject.AddComponent<LineRenderer>();
                        WireLine.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
                        WireLine.startWidth = (float)0.1;
                        WireLine.endWidth = (float)0.1;
                        WireLine.sortingLayerName = "ActiveDevices";
                        WireLine.positionCount = wireInflectionObjects.Count;
                        WireLine.numCornerVertices = wireInflectionObjects.Count;
                        WireLine.SetPosition(wireInflectionObjects.Count - 1, wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position);
                        WireLine.SetColors(Color.red, Color.red);

                    }
                }
                else if(!isClickedLogicNode) 
                {
                    Debug.Log("WIRE: Middle point in sequence.");
                    //create linerenderer, dont set end logic node
                    GameObject wireInflectionPoint = new GameObject("WIRE_POINT_" + wireInflectionObjects.Count);
                    wireInflectionPoint.AddComponent<WireInflection>();
                    wireInflectionPoint.transform.parent = DeviceGameObject.transform;
                    wireInflectionPoint.transform.position = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, 10);
                    wireInflectionObjects.Add(wireInflectionPoint);
                    WireLine.positionCount = wireInflectionObjects.Count;
                    WireLine.numCornerVertices = wireInflectionObjects.Count;
                    WireLine.SetPosition(wireInflectionObjects.Count - 1, wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position);

                    //SetRotation for capsule collider
                    GameObject previousInflectionNode = wireInflectionObjects[wireInflectionObjects.Count - 2];
                    var pos = Camera.main.WorldToScreenPoint(previousInflectionNode.transform.position);
                    var dir = Input.mousePosition - pos;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                    previousInflectionNode.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    CapsuleCollider2D capCol = previousInflectionNode.AddComponent<CapsuleCollider2D>();
                    float distanceBetweenGOs = Vector3.Distance(previousInflectionNode.transform.position, wireInflectionPoint.transform.position);
                    capCol.size = new Vector2(.1f, distanceBetweenGOs);
                    capCol.offset = new Vector2(0f, -(distanceBetweenGOs / 2));
                }
                else if (isClickedLogicNode)
                {
                    //create linerenderer, set end logic node
                    Debug.Log("WIRE: Ending sequence.");

                    GameObject wireInflectionPoint = new GameObject("WIRE_POINT_" + wireInflectionObjects.Count);
                    wireInflectionPoint.AddComponent<WireInflection>();
                    wireInflectionPoint.transform.parent = DeviceGameObject.transform;
                    wireInflectionPoint.transform.position = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, 10);
                    wireInflectionObjects.Add(wireInflectionPoint);
                    WireLine.positionCount = wireInflectionObjects.Count;
                    WireLine.numCornerVertices = wireInflectionObjects.Count;
                    WireLine.SetPosition(wireInflectionObjects.Count - 1, wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position);


                    //SetRotation for capsule collider
                    GameObject previousInflectionNode = wireInflectionObjects[wireInflectionObjects.Count - 2];
                    var pos = Camera.main.WorldToScreenPoint(previousInflectionNode.transform.position);
                    var dir = Input.mousePosition - pos;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                    previousInflectionNode.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    CapsuleCollider2D capCol = previousInflectionNode.AddComponent<CapsuleCollider2D>();
                    float distanceBetweenGOs = Vector3.Distance(previousInflectionNode.transform.position, wireInflectionPoint.transform.position);
                    capCol.size = new Vector2(.1f, distanceBetweenGOs);
                    capCol.offset = new Vector2(0f, -(distanceBetweenGOs / 2));

                    //End wire sequence
                    activelyBeingPlaced = false;
                    Destroy(placingText);
                    SetLogicNodePositions();
                    
                }




            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(this.gameObject);
            }
        }
	}


    public void ReactToLogic(GameObject LogicNode)
    {
        throw new System.NotImplementedException();
    }

    public void ReactToLogic(GameObject LogicNode, int requestedState)
    {
        if (activelyBeingPlaced)
        {
            return;
        }
        Debug.Log("WIRE: Reacting To Logic on Node: " + LogicNode.name);
        Debug.Log("WIRE: Attempting to request state change to: " + requestedState);
        LogicNode otherEnd = null; //the other end from the node that this is being called; 
        LogicNode currentEnd = null; //the currentNode that this is being called from
        LogicNode startNodeLogic = startNode.GetComponent<LogicNode>();
        LogicNode endNodeLogic = endNode.GetComponent<LogicNode>();
        if(LogicNode == startNode)
        {
            currentEnd = startNodeLogic;
            otherEnd = endNodeLogic;
        }
        else if(LogicNode == endNode)
        {
            currentEnd = endNodeLogic;
            otherEnd = startNodeLogic;
        }
        LogicNode collidingNodeCurrent = currentEnd.GetCollidingNode().GetComponent<LogicNode>(); //Node colliding with the other end of the wire
        LogicNode collidingNodeEnd = otherEnd.GetCollidingNode().GetComponent<LogicNode>(); //Node colliding with the other end of the wire
        currentEnd.SetLogicState(requestedState);
        otherEnd.SetLogicState(requestedState);
        collidingNodeEnd.RequestStateChange(requestedState); //request other device to change to logic
        if (collidingNodeEnd.GetLogicState() != requestedState) //other device node cannot change
        {
            requestedState = collidingNodeEnd.GetLogicState();
            currentEnd.SetLogicState(requestedState);
            otherEnd.SetLogicState(requestedState);
            //collidingNodeCurrent.RequestStateChange(requestedState); //request current device to calibrate to other device's logic
        }
        else //requested state change is successful
        {
            currentEnd.SetLogicState(requestedState);
            otherEnd.SetLogicState(requestedState);
        }
 
        
        
    }
}
