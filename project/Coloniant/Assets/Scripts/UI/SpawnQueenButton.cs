using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueenButton : MonoBehaviour {

    public void SpawnButton()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.QUEEN, 1);
    }
}
