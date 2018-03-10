using UnityEngine;
public enum LOGIC { HIGH = 1, LOW = 0, INVALID = -1 }
public enum SOURCE { EQUIPMENT = 10, MOUSE = 11}
public interface LogicInterface
{
    void ReactToLogic(GameObject LogicNode);
    void ReactToLogic(GameObject LogicNode, int requestedState);
}