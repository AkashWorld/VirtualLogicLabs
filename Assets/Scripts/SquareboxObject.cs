using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareboxObject : MonoBehaviour {
    bool dragging = false;
    float distance;
    GameObject gameobj;
	// Use this for initialization
	void Start () {
		gameobj = GameObject.Find("ColliderTester");
    }


    private void OnMouseDown()
    {
        Debug.Log("MouseDown detected");
        distance = Vector3.Distance(gameobj.transform.position, Camera.main.transform.position);
        dragging = true;
    }

    private void OnMouseUp()
    {
        Debug.Log("MouseUp detected");
        dragging = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collsion detected with: " + collision.gameObject.tag);
    }

    // Update is called once per frame
    void Update () {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            gameobj.transform.position = rayPoint;
        }
    }
}
