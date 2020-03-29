using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGardenerButton : MonoBehaviour {

    public void SpawnAnt()
    {
        var InputGameObject = GameObject.Find("Spawn Gardener - InputField");
        var stringval = InputGameObject.GetComponent<TMPro.TMP_InputField>();
        var value = Convert.ToInt32(stringval.text);
        if (value > 0){
            for (int i = 0; i < value; i++){
                AntManager.main.AddAntsToSpawn(Ant.AntType.GARDENER);
            }
        }
    }
}
