using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierButton : MonoBehaviour {

    public void SpawnAnt()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.SOLDIER, 1);
    }
}
