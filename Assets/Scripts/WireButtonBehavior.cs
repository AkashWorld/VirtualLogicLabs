using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireButtonBehavior : MonoBehaviour {

    private void OnMouseUp()
    {
        string button_name = transform.name;
        if (transform.childCount == 0)
        {
            if (button_name == "red_wire_button")
            {
                Debug.Log("Red button clicked on");
                GameObject redWireOn = new GameObject("redWireOn");
                redWireOn.transform.parent = this.gameObject.transform;
            }
            else if (button_name == "green_wire_button")
            {
                Debug.Log("Green button clicked on");
                GameObject greenWireOn = new GameObject("greenWireOn");
                greenWireOn.transform.parent = this.gameObject.transform;
            }
            else if (button_name == "black_wire_button")
            {
                Debug.Log("Black button clicked on");
                GameObject blackWireOn = new GameObject("blackWireOn");
                blackWireOn.transform.parent = this.gameObject.transform;
            }
            else
            {
                Debug.Log("Error in clicking button");
            }
        }

        else
        {
            if (button_name == "red_wire_button")
            {
                Debug.Log("Red button clicked off");
                foreach (Transform children in transform)
                {
                    Destroy(children.gameObject);
                }
            }
            else if (button_name == "green_wire_button")
            {
                Debug.Log("Green button clicked off");
                foreach (Transform children in transform)
                {
                    Destroy(children.gameObject);
                }
            }
            else if (GameObject.Find("blackWireOn") != null)
            {
                Debug.Log("Black button clicked off");
                foreach (Transform children in transform)
                {
                    Destroy(children.gameObject);
                }
            }
            else
            {
                Debug.Log("Error in clicking button");
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
