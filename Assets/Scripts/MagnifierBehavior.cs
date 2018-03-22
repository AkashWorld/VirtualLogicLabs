using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierBehavior : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Mouse action on magnifying glass");
        if (col.gameObject.name == "74LS00")
        {
            SpriteRenderer sprite = GameObject.Find("download").GetComponent<SpriteRenderer>();
            sprite.enabled = true;
            SpriteRenderer sprite1 = GameObject.Find("InstructionBase").GetComponent<SpriteRenderer>();
            sprite1.enabled = true;

        }
        if (col.gameObject.name == "74LS04")
        {

        }
        if (col.gameObject.name == "74LS08")
        {

        }
        if (col.gameObject.name == "74LS00")
        {

        }
        if (col.gameObject.name == "74LS00")
        {

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "74LS00")
        {
            SpriteRenderer sprite = GameObject.Find("download").GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            SpriteRenderer sprite1 = GameObject.Find("InstructionBase").GetComponent<SpriteRenderer>();
            sprite1.enabled = false;
        }
        if (col.gameObject.name == "74LS04")
        {

        }
        if (col.gameObject.name == "74LS00")
        {

        }
        if (col.gameObject.name == "74LS00")
        {

        }
        if (col.gameObject.name == "74LS00")
        {

        }

    }

    void OnMouseDown()
    {
        Debug.Log("Magnifying Glass ButtonDown");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;
    }

}


