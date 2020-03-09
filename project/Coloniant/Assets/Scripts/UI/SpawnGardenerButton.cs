using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGardenerButton : MonoBehaviour {

    public void SpawnButton()
    {
        AntManager.main.AddAntsToSpawn(Ant.AntType.GARDENER);
    }
}
