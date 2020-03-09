using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnForagerButton : MonoBehaviour {

    public void SpawnButton()
    {
        AntManager.main.AddAntsToSpawn(Ant.AntType.FORAGER);
    }
}
