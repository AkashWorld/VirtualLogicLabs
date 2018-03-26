using UnityEngine;
public enum LOGIC { HIGH = 10, LOW = -10, INVALID = 0 }
public enum SOURCE { EQUIPMENT = 10, MOUSE = 11}
public interface LogicInterface
{
    void ReactToLogic(GameObject LogicNode);
    void ReactToLogic(GameObject LogicNode, int requestedState);
}