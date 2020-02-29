using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnForagerButton : MonoBehaviour {

    public void SpawnButton()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.FORAGER, 1);
    }
}
