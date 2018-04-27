using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
/// <summary>
/// Wires are generated using the ‘w’ key on the keyboard, 
/// or can be accessed via the dropdown menu. After initializing 
/// them, a click on a Logic Node is listened for on a callback, 
/// after the first click, the Wire ‘Line’ is rendered to follow 
/// the mouse from the specified Logic Node. Every click after 
/// ward creates an inflection point for the wire, if the clicked 
/// point is not a Logic Node, in which another ‘Line’ is rendered
/// from that point to the mouse. Only when a Logic Node is clicked 
/// is the wire sequence is done executing within the Wire object’s 
/// Update function, and two new Logic Nodes are created at each
/// end of the wire. The wire follows a similar priority system
/// to that of the Protoboard by analyzing the colliding Logic Nodes
/// of both ends of the wire.
/// </summary>
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

    /// <summary>
    /// Two LogicNodes are created, and a Text is also created to
    /// show that a wire is being placed. 
    /// </summary>
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


    public void SetNodePositions(Vector3 firstNodePos, Vector3 secondNodePos)
    {
        activelyBeingPlaced = false;
        startNode.transform.position = firstNodePos;
        endNode.transform.position = secondNodePos;
        WireLine = this.gameObject.AddComponent<LineRenderer>();
        WireLine.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        WireLine.startWidth = (float)0.1;
        WireLine.endWidth = (float)0.1;
        WireLine.sortingLayerName = "ActiveDevices";
        WireLine.positionCount = 2;
        WireLine.numCornerVertices = 2;
        WireLine.SetPosition(0, firstNodePos);
        WireLine.SetPosition(1, secondNodePos);
#pragma warning disable CS0618 // Type or member is obsolete
        WireLine.SetColors(Color.yellow, Color.yellow);
#pragma warning restore CS0618 // Type or member is obsolete

    }



    /// <summary>
    /// Callback helper function that toggles line color
    /// when the user clicks on the wire
    /// </summary>
    public void ToggleLineColor()
    {
        Debug.Log("Toggling WIRE color");
        if(currentColor == Color.red)
        {
            currentColor = Color.green;
#pragma warning disable CS0618 // Type or member is obsolete
            WireLine.SetColors(Color.green, Color.green);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        else if(currentColor == Color.green)
        {
            currentColor = Color.yellow;
#pragma warning disable CS0618 // Type or member is obsolete
            WireLine.SetColors(Color.yellow, Color.yellow);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        else if(currentColor == Color.yellow)
        {
            currentColor = Color.black;
#pragma warning disable CS0618 // Type or member is obsolete
            WireLine.SetColors(Color.black, Color.black);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        else
        {
            currentColor = Color.red;
#pragma warning disable CS0618 // Type or member is obsolete
            WireLine.SetColors(Color.red, Color.red);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }


    /// <summary>
    /// Helper function that helps draw a line between
    /// the origin position, and the current mouse position
    /// </summary>
    /// <param name="currentMousePos"></param>
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
	
    /// <summary>
    /// After the Wire is set, with both ends connecting to a Logic Node,
    /// the LogicNode's positions are changed to go to each end of the
    /// wire.
    /// </summary>
    private void SetLogicNodePositions()
    {

        startNode.transform.position = new Vector3(wireInflectionObjects[0].transform.position.x, wireInflectionObjects[0].transform.position.y, 10);
        endNode.transform.position = new Vector3(wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position.x, 
            wireInflectionObjects[wireInflectionObjects.Count - 1].transform.position.y, 10);
    }


    /// <summary>
    /// Update loop that helps implement the sequence that the wire
    /// goes through to place the wire. Based on several conditions, the
    /// state of the wire is changed from starting to be placed, to changing
    /// line positions, to adding lines based on inflection points, to ending
    /// the line on a Logic Node to create the wire. After the sequence is
    /// over, the activeBeingPlaced variable is toggled false to render
    /// the Update loop unsuable.
    /// </summary>
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
                        this.gameObject.name += " " + hit.collider.gameObject.name;
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
#pragma warning disable CS0618 // Type or member is obsolete
                        WireLine.SetColors(Color.red, Color.red);
#pragma warning restore CS0618 // Type or member is obsolete

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
                    this.gameObject.name += "]";
                }




            }
            //Communicate to collided objects that the wire is being destroyed, then destroy the wire
            else if (Input.GetMouseButtonDown(1))
            {
                LogicNode startLogic = startNode.GetComponent<LogicNode>(); startLogic.SetLogicState((int)LOGIC.INVALID);
                LogicNode endLogic = endNode.GetComponent<LogicNode>(); endLogic.SetLogicState((int)LOGIC.INVALID);
                Destroy(this.gameObject);
            }
        }
	}

    void OnDestroy()
    {
        Debug.Log("DESTROYING " + this.gameObject.name);
    }

    public void ReactToLogic(GameObject LogicNode)
    {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Reacts to the Logic of the two ends of the wires and sets the overall
    /// Logic state of the wire based on a priority system that prioritizes
    /// LOGIC.LOW first, LOGIC.HIGH next, and finally LOGIC.INVALID. This
    /// method is only usuable after activeBeingPlaced is toggled false.
    /// </summary>
    /// <param name="LogicNode"></param>
    /// <param name="requestedState"></param>
    public void ReactToLogic(GameObject LogicNode, int requestedState)
    {
        if (activelyBeingPlaced)
        {
            return;
        }
        LogicNode startLogic = startNode.GetComponent<LogicNode>();
        LogicNode endLogic = endNode.GetComponent<LogicNode>();
        GameObject startCollision = startLogic.GetCollidingNode(); GameObject endCollision = endLogic.GetCollidingNode();
        if(startCollision == null || endCollision == null)
        {
            Debug.Log(this.gameObject.name + " ends found to have no collisions, returning.");
            return;
        }
        LogicNode startCollisionLogic = startCollision.GetComponent<LogicNode>();
        LogicNode endCollisionLogic = endCollision.GetComponent<LogicNode>();
        int priorityState = (int)LOGIC.INVALID;
        int startCollState = startCollisionLogic.GetLogicState();
        int endCollState = endCollisionLogic.GetLogicState();
        if(startCollState == (int)LOGIC.LOW || endCollState == (int)LOGIC.LOW)
        {
            priorityState = (int)LOGIC.LOW;
        }
        else if(startCollState == (int)LOGIC.HIGH || endCollState == (int)LOGIC.HIGH)
        {
            priorityState = (int)LOGIC.HIGH;
        }
        Debug.Log(this.gameObject.name +  " priority state found to be: " + priorityState);
        startLogic.SetLogicState(priorityState);
        endLogic.SetLogicState(priorityState);
        if (startCollState != priorityState)
        {
            Debug.Log(this.gameObject.name + " Requesting node " + startCollisionLogic.gameObject.name + " to change to " + priorityState);
            startCollisionLogic.RequestStateChange(priorityState);
        }
        if (endCollState != priorityState)
        {
            Debug.Log(this.gameObject.name +  " Requesting node " + endCollisionLogic.gameObject.name + " to change to " + priorityState);
            endCollisionLogic.RequestStateChange(priorityState);
        }
    }
}
