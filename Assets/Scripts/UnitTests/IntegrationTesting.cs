using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class IntegrationTesting{
    [UnityTest]
    private IEnumerator Lab1IntegrationTesting()
    {
        SceneManager.LoadScene("Scenes/Lab1");
        yield return new WaitForSecondsRealtime(1);
        GameObject powerSupplyGO = GameObject.Find("PowerSupply");
        Assert.IsNotNull(powerSupplyGO);
        PowerSupplyScript powerSupply = powerSupplyGO.GetComponent<PowerSupplyScript>();
        GameObject ProtoboardGO = GameObject.Find("Protoboard");
        Assert.IsNotNull(ProtoboardGO);
        ProtoboardObject protoboardObj = ProtoboardGO.GetComponent<ProtoboardObject>();
        Wire wire1 = new GameObject("Wire").AddComponent<Wire>();
        Assert.IsNotNull(wire1);
        yield return new WaitForSecondsRealtime(1);

        yield break;
    }

}
