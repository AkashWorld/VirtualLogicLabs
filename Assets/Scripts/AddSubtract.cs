using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSubtract : MonoBehaviour {


    private void OnMouseEnter()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 0); 
    }

    private void OnMouseExit()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1);
    }

    private void OnMouseUp()
    {

        ProtoboardObject proto = GameObject.Find("Protoboard").GetComponent<ProtoboardObject>(); 

        if (this.gameObject.name == "Add")
        {
            proto.IncrementProtoboard(); 
        }

        if (this.gameObject.name == "Subtract")
        {
            proto.DecrementProtoboard(); 
        }

    }

}
