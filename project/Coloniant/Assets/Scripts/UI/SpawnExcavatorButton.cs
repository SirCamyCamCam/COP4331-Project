using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExcavatorButton : MonoBehaviour {

    public void SpawnButton()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.EXCAVATOR, 1);
    }
}
