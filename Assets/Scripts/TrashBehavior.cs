using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Facilitates deletion of GameObjects that represent
/// equipments in the Virtual Logic Lab.
/// </summary>
public class TrashBehavior : MonoBehaviour {
    int colliderNumber = 0;
    public bool test = false;
    GameObject hoverObject = null;
    /// <summary>
    /// Detects entering collision with another object to show the indicator
    /// for an active "Trash"
    /// </summary>
    /// <param name="col"></param>

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
    /// <summary>
    /// Detects exiting collision with another object to show the indicator
    /// for an inactive "Trash"
    /// </summary>
    /// <param name="col"></param>
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

	
	/// <summary>
    /// Destroys the GameObject is the mouse button is lifted up
    /// </summary>
	void Update ()
    {
		if (hoverObject!= null)
        {
            if (Input.GetMouseButtonUp(0) || test)
            {
                Debug.Log("Deleting" + hoverObject.name);
                Destroy(hoverObject);
                hoverObject = null; 
            }
        }
	}
}


