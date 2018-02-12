using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBehavior : MonoBehaviour {

    string logic_id;
    int logic_state = -1;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void setLogicId(string id)
    {
        this.logic_id = id;
    }

    string getLogicId()
    {
        return this.logic_id;
    }
    int setLogicState(int state)
    {
        if(state == 1 || state == 0 || state == -1)
        {
            this.logic_state = state;
            return logic_state;
        }
        return -100; //error
    }
    int getLogicState()
    {
        return this.logic_state;
    }

}
