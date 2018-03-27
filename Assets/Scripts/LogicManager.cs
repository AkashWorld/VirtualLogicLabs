using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour {
    LinkedList<GameObject> ActiveLogicNodes;
	// Use this for initialization
	void Start () {
        ActiveLogicNodes = new LinkedList<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetAllLogic()
    {
        foreach(GameObject node in ActiveLogicNodes)
        {
            LogicNode logicNode = node.GetComponent<LogicNode>();
            logicNode.SetLogicStateWithoutNotification((int)LOGIC.INVALID);
        }
    }

    public void AddGameObject(GameObject newGameObject)
    {
        if (!ActiveLogicNodes.Contains(newGameObject))
        {
            ActiveLogicNodes.AddLast(newGameObject);
        }
    }

    public void AddGameObject(List<GameObject> list)
    {
        foreach(GameObject item in list)
        {
            if (!ActiveLogicNodes.Contains(item))
            {
                ActiveLogicNodes.AddLast(item);
            }
        }
    }

    public void RemoveGameObject(GameObject requestedRemovalNode)
    {
        ActiveLogicNodes.Remove(requestedRemovalNode);
    }

    public void RemoveGameObject(List<GameObject> removalList)
    {
        foreach(GameObject item in removalList)
        {
            ActiveLogicNodes.Remove(item);
        }
    }
}
