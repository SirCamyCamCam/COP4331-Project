// --------------------------------------------------------------
// Coloniant - SpawnAntsTest                           2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAntsTest : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        // System load test
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.EXCAVATOR, 500);
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.FORAGER, 500);
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.GARDENER, 500);
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.QUEEN, 500);
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.SOLIDER, 500);
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.TRASH_HANDLER, 500);
    }
}
