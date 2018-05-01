using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputChecker : MonoBehaviour {
    public Button mainMenu;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W)) //start wiring sequence
        {
            GameObject wire = new GameObject("[Wire");
            wire.AddComponent<Wire>();
            wire.transform.parent = Camera.main.transform;
        }
	}


}
