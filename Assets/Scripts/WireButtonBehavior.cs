using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireButtonBehavior : MonoBehaviour {

    public bool buttonOn = false;

    private void OnMouseUp()
    {
        string button_name = transform.name;

        if (buttonOn == true)
        {
            Debug.Log(button_name + " clicked off");
            buttonOn = false;
        }
        else if (buttonOn == false)
        {
            Debug.Log(button_name + " clicked on");
            buttonOn = true;
        }
        if (button_name == "red_wire_button")
        {
            GameObject.Find("green_wire_button").GetComponent<WireButtonBehavior>().buttonOn = false;
            GameObject.Find("black_wire_button").GetComponent<WireButtonBehavior>().buttonOn = false;
        }

        if (button_name == "green_wire_button")
        {
            GameObject.Find("red_wire_button").GetComponent<WireButtonBehavior>().buttonOn = false;
            GameObject.Find("black_wire_button").GetComponent<WireButtonBehavior>().buttonOn = false;
        }

        if (button_name == "black_wire_button")
        {
            GameObject.Find("green_wire_button").GetComponent<WireButtonBehavior>().buttonOn = false;
            GameObject.Find("red_wire_button").GetComponent<WireButtonBehavior>().buttonOn = false;
        }
    }


    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
