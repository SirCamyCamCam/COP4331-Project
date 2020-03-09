using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExcavatorButton : MonoBehaviour {

    public void SpawnButton()
    {
        AntManager.main.AddAntsToSpawn(Ant.AntType.EXCAVATOR);
    }
}
