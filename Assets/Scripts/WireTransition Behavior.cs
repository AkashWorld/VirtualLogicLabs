using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTransitionBehavior : MonoBehaviour
{

    Vector3 startPosition;

    public void setStartPosition(Vector3 start)
    {
        startPosition = start;
    }

    // Use this for initialization
    void Start()
    {
        LineRenderer line = this.gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        line.startWidth = (float)0.1;
        line.endWidth = (float)0.1;
        line.sortingLayerName = "ActiveDevices";
        if (GameObject.Find("green_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
        {
            line.startColor = new Color(0, 1, 0);
            line.endColor = new Color(0, 1, 0);
        }
        if (GameObject.Find("red_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
        {
            line.startColor = new Color(1, 0, 0);
            line.endColor = new Color(1, 0, 0);
        }
        if (GameObject.Find("black_wire_button").GetComponent<WireButtonBehavior>().buttonOn)
        {
            line.startColor = new Color(0, 0, 0);
            line.endColor = new Color(0, 0, 0);
        }
        line.SetPosition(0, startPosition);
        line.SetPosition(1, new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        LineRenderer line = this.gameObject.GetComponent<LineRenderer>();
        line.SetPosition(1, worldPoint);
    }

}
