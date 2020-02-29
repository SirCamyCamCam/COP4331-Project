using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoliderButton : MonoBehaviour {

    public void SpawnAnt()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.SOLIDER, 1);
    }
}
