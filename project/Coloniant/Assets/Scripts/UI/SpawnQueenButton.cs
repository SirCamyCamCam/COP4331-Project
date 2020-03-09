using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQueenButton : MonoBehaviour {

    public void SpawnButton()
    {
        AntManager.main.AddAntsToSpawn(Ant.AntType.QUEEN);
    }
}
