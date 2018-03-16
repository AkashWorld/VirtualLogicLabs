using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireInflection : MonoBehaviour {
    Wire parentWire;


  

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            parentWire.ToggleLineColor();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(parentWire.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        parentWire = this.gameObject.transform.parent.GetComponent<Wire>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
