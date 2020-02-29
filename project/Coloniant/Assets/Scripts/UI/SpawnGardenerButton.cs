using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGardenerButton : MonoBehaviour {

    public void SpawnButton()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.GARDENER, 1);
    }
}
