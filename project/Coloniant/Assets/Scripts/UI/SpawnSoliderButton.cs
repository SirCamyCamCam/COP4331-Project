using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoliderButton : MonoBehaviour {

    public void SpawnAnt()
    {
        AntManager.main.AddAntsToSpawn(Ant.AntType.SOLDIER);
    }
}
