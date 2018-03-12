using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour {

    public Dropdown dropDown;
    public List<string> equipmentNames;
	// Use this for initialization
	public void Start () {
        equipmentNames = new List<string>();
        equipmentNames.Add("Protoboard"); equipmentNames.Add("74LS00"); equipmentNames.Add("74LS04");
        equipmentNames.Add("74LS08"); equipmentNames.Add("74LS32"); equipmentNames.Add("LED");
        equipmentNames.Add("Power Supply");
        dropDown.ClearOptions();
        List<Dropdown.OptionData> equipmentListDD = new List<Dropdown.OptionData>();
        foreach(string equipName in equipmentNames)
        {
            var equipNameOptionData = new Dropdown.OptionData(equipName);
            equipmentListDD.Add(equipNameOptionData);
        }
        dropDown.AddOptions(equipmentListDD);
        GameObject label = dropDown.transform.Find("Label").gameObject;
        label.GetComponent<Text>().text = "Equipment";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
