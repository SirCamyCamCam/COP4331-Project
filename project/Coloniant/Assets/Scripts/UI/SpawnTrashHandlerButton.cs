using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrashHandlerButton : MonoBehaviour {

    public void SpawnButton()
    {
        AntManager.main.AddAntsToSpawn(Ant.AntType.TRASH_HANDLER);
    }
}
