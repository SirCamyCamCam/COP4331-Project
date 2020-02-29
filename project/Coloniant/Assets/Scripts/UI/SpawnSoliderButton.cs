using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnAnt()
    {
        QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.Soldier, 1);
    }
}
