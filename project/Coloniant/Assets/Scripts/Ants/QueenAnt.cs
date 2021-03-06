﻿// --------------------------------------------------------------
// Coloniant - QueenAnt                                 2/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAnt : MonoBehaviour {

    #region enum

    public enum Ants
    {
        QUEEN = 0,
        SOLIDER = 1,
        FORAGER = 2,
        GARDENER = 3,
        TRASH_HANDLER = 4,
        EXCAVATOR = 5
    }

    #endregion

    //public static QueenAnt main;

    #region Inspector Fields

    [Header("Prefabs")]
    [SerializeField]
    private GameObject queenPrefab;
    [SerializeField]
    private GameObject soliderPrefab;
    [SerializeField]
    private GameObject foragerPrefab;
    [SerializeField]
    private GameObject gardenerPrefab;
    [SerializeField]
    private GameObject trashHandlerPrefab;
    [SerializeField]
    private GameObject excavatorPrefab;

    [Header("Dependencies")]
    public Ant ant;
    [SerializeField]
    private GameObject spawn;

    #endregion

    #region Run-Time Fields

    private int[] antsToSpawn;
    private float spawnWaitTime;
    private Coroutine spawnTimer;
    private GameObject nurery;

    //counts the number of Queens per nursery 
    private int QueenC = 0; 
    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.QUEEN;
        antsToSpawn = new int[6];
    }

    // Use this for initialization
    void Start () {
        spawnWaitTime = AntManager.main.spawnRate;
    }

    // Update is called once per frame
    /*void Update () {
		
	}*/

    #endregion

    #region Public Methods

    public void SetNursery(GameObject nur)
    {
        nurery = nur;
    }

    // Adds an ant which should spawn
    public bool AddAntToSpawn(Ants type, int count)
    {
        //Debug.Log("Called");
        if (count <= 0)
        {
            return false;
        }
        antsToSpawn[(int)type] += count;

        return SpawnAnts();
    }

    #endregion

    #region Private Methods

    // Calls to start spawning ants
    private bool SpawnAnts()
    {
        if (spawnTimer == null)
        {
            spawnTimer = StartCoroutine(spawnAntTimer());
        }
        else
        {
            return false;
        }
        return true;
    }

    // Spawns a queen ant
    private void SpawnQueen()
    {
        
        if(QueenC == 0){
        GameObject newAnt = Instantiate(queenPrefab, gameObject.transform.position, new Quaternion(0,0,0,0), AntManager.main.transform);
        newAnt.GetComponent<Ant>().AssignTargetWaypoint(nurery);
        QueenC++; 
        }
        else {return;}
        
    }

    // Spawns a forager ant
    private void SpawnForager()
    {
        GameObject newAnt = Instantiate(foragerPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0), AntManager.main.transform);
        newAnt.GetComponent<Ant>().AssignTargetWaypoint(nurery);
    }

    // Spawns a gardener ant
    private void SpawnGardener()
    {
        GameObject newAnt = Instantiate(gardenerPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0), AntManager.main.transform);
        newAnt.GetComponent<Ant>().AssignTargetWaypoint(nurery);
    }
    
    // Spawns a excavator
    private void SpawnExcavator()
    {
        GameObject newAnt = Instantiate(excavatorPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0), AntManager.main.transform);
        newAnt.GetComponent<Ant>().AssignTargetWaypoint(nurery);
    }

    // Spawns a trash handeler
    private void SpawnTrashHandler()
    {
        GameObject  newAnt = Instantiate(trashHandlerPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0), AntManager.main.transform);
        newAnt.GetComponent<Ant>().AssignTargetWaypoint(nurery);
    }

    // Spawns a solider
    private void SpawnSolider()
    {
        GameObject newAnt = Instantiate(soliderPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0), AntManager.main.transform);
        newAnt.GetComponent<Ant>().AssignTargetWaypoint(nurery);
    }

    #endregion

    #region Coroutines

    // Spawns ants at a set rate
    private IEnumerator spawnAntTimer()
    {
        yield return new WaitForSeconds(spawnWaitTime);

        // Checks to see if we have any more ants to spawn, if we do then exit and
        // create a new instance of the coroutine to check again
        for (int i = 0; i < 6; i++)
        {
            if (antsToSpawn[i] > 0)
            {
                switch(i)
                {
                    case (int)Ants.QUEEN:
                        SpawnQueen();
                        break;
                    case (int)Ants.SOLIDER:
                        SpawnSolider();
                        break;
                    case (int)Ants.FORAGER:
                        SpawnForager();
                        break;
                    case (int)Ants.GARDENER:
                        SpawnGardener();
                        break;
                    case (int)Ants.EXCAVATOR:
                        SpawnExcavator();
                        break;
                    case (int)Ants.TRASH_HANDLER:
                        SpawnTrashHandler();
                        break;
                }

                antsToSpawn[i]--;
                spawnTimer = null;
                SpawnAnts();
                yield break;
            }
        }
        spawnTimer = null;
    }

    #endregion
}
