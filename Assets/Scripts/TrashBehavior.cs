using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour {

    GameObject hoverObject = null;
    int colliderNumber = 0; 
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<MagnifierBehavior>() == false)
        {
            colliderNumber++; 
            Debug.Log("Start hovering over trash");
            if (col.gameObject.GetComponent<LogicNode>()) hoverObject = col.gameObject.transform.parent.gameObject;
            else hoverObject = col.gameObject;
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
            sprite.color = new Color(1F, 1F, 0F); 
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        colliderNumber--;
        if (colliderNumber == 0)
        {
            Debug.Log("Stop hovering over trash");
            hoverObject = null;
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
            sprite.color = new Color(1F, 1F, 1F);
        }
        
    }

    
        // Use this for initialization
        void Start ()
        {
		
	    }
	
	// Update is called once per frame
	void Update ()
    {
		if (hoverObject!= null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Deleting" + hoverObject.name);
                Destroy(hoverObject);
                hoverObject = null; 
            }
        }
	}
}
