using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueenButton : MonoBehaviour {

    public void SpawnAnt()
    {
        var InputGameObject = GameObject.Find("Spawn Queen - InputField");
        var stringval = InputGameObject.GetComponent<TMPro.TMP_InputField>();
        var value = Convert.ToInt32(stringval.text);
        if (value > 0){
            for (int i = 0; i < value; i++){
                AntManager.main.AddAntsToSpawn(Ant.AntType.QUEEN);
            }
        }
    }
}
