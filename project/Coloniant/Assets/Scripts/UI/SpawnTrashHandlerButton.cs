using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrashHandlerButton : MonoBehaviour {

    public void SpawnButton()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.TRASH_HANDLER, 1);
    }
}
