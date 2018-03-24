using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierBehavior : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    GameObject prevGameobject = null;
    SpriteRenderer prevSprite = null; 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator OnTriggerEnter2D(Collider2D col)
    {

        float alpha = 0;
        SpriteRenderer sprite = null;
        GameObject infoObject = null;
        if (col.gameObject.GetComponent<LogicNode>()) yield break;
        infoObject = col.gameObject;
        Collider2D magnifierCollider = this.gameObject.GetComponent<Collider2D>();
        Collider2D infoObjectCollider = infoObject.GetComponent<Collider2D>();

        Debug.Log("Mouse action on magnifying glass");
        if (infoObject.GetComponent<NANDGate>())
        {
            sprite = GameObject.Find("74LS00Info").GetComponent<SpriteRenderer>();
        }
        else if (infoObject.GetComponent<INVGate>())
        {
            sprite = GameObject.Find("74LS04Info").GetComponent<SpriteRenderer>();
        }
        else if (infoObject.GetComponent<ANDGate>())
        {
            sprite = GameObject.Find("74LS08Info").GetComponent<SpriteRenderer>();
        }
        else if (infoObject.GetComponent<ORGate>())
        {
            sprite = GameObject.Find("74LS32Info").GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.Log("Error in magnifier collision");
            yield break;
        }
        alpha = sprite.color.a;
        prevGameobject = infoObject;
        prevSprite = sprite; 
        while (alpha < 1)
        {
            sprite.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.00001F);
            alpha += (float)0.01;
            if(magnifierCollider.IsTouching(infoObjectCollider) == false)
            {
                OnTriggerEnter2D(infoObjectCollider);
                yield break; 
            }
        }
        sprite.color = new Color(1, 1, 1, 1);
    }

    IEnumerator OnTriggerExit2D(Collider2D col)
    {
        float alpha;
        SpriteRenderer sprite = null;
        GameObject infoObject = null;
        if (col.gameObject.GetComponent<LogicNode>()) yield break;
        infoObject = col.gameObject;
        Debug.Log("Mouse action on magnifying glass");
        if (infoObject.GetComponent<NANDGate>())
        {
            sprite = GameObject.Find("74LS00Info").GetComponent<SpriteRenderer>();
        }
        else if (infoObject.GetComponent<INVGate>())
        {
            sprite = GameObject.Find("74LS04Info").GetComponent<SpriteRenderer>();
        }
        else if (infoObject.GetComponent<ANDGate>())
        {
            sprite = GameObject.Find("74LS08Info").GetComponent<SpriteRenderer>();
        }
        else if (infoObject.GetComponent<ORGate>())
        {
            sprite = GameObject.Find("74LS32Info").GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.Log("Error in magnifier collision");
            yield break;
        }
        alpha = sprite.color.a;
        while(alpha > 0)
        {

            sprite.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.001F);
            alpha -= (float)0.01;

        }

        sprite.color = new Color(1, 1, 1, 0);
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


