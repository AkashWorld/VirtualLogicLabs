using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerTagScript : MonoBehaviour {
    GameObject collidingObject;
    private Vector3 screenPoint;
    private Vector3 offset;
    private string type;
    private bool SNAPPED = false;
    // Use this for initialization
    void Start () {
        type = GradingCONSTANTS.INPUT;
	}

    public bool isSnapped()
    {
        return SNAPPED;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(type == GradingCONSTANTS.INPUT) {
            if(collision.GetComponent<Switch>() != null)
            {
                collidingObject = collision.gameObject;
            }
        }
        else if(type == GradingCONSTANTS.OUTPUT){
            if (collision.GetComponent<LEDScript>() != null)
            {
                collidingObject = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == collidingObject)
        {
            collidingObject = null;
        }
    }


    public GameObject GetCollidingObject()
    {
        return collidingObject;
    }


    void OnMouseDown()
    {
        Debug.Log("Checker " + this.gameObject.name + " Mouse Down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;
    }


    public void OnMouseUp()
    {
        if(collidingObject == null)
        {
            SNAPPED = false;
            return;
        }
        Vector3 collidingPos = new Vector3(collidingObject.transform.position.x, collidingObject.transform.position.y, collidingObject.transform.position.z);
        collidingPos.x -= .2f; collidingPos.y += .4f;
        this.gameObject.transform.position = collidingPos;
        SNAPPED = true;
    }

 

    public string Type
    {
        get
        {
            return this.type;
        }

        set
        {
            this.type = value;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
