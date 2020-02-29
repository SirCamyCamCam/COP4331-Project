// --------------------------------------------------------------
// Coloniant - QueenAnt                                 2/16/2020
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
        Soldier = 1,
        FORAGER = 2,
        GARDENER = 3,
        TRASH_HANDLER = 4,
        EXCAVATOR = 5
    }

    #endregion

    public static QueenAnt main;

    #region Inspector Fields

    [Header("Prefabs")]
    [SerializeField]
    private GameObject queenPrefab;
    [SerializeField]
    private GameObject SoldierPrefab;
    [SerializeField]
    private GameObject foragerPrefab;
    [SerializeField]
    private GameObject gardenerPrefab;
    [SerializeField]
    private GameObject trashHandlerPrefab;
    [SerializeField]
    private GameObject excavatorPrefab;

    [Header("Dependencies")]
    [SerializeField]
    private Ant ant;
    [SerializeField]
    private GameObject spawn;

    #endregion

    #region Run-Time Fields

    private int[] antsToSpawn;
    private float spawnWaitTime;
    private Coroutine spawnTimer;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.QUEEN;
        antsToSpawn = new int[6];

        main = this;
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

    // Adds an ant which should spawn
    public bool AddAntToSpawn(Ants type, int count)
    {
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
        Instantiate(queenPrefab, spawn.transform.position, new Quaternion(0,0,0,0));
    }

    // Spawns a forager ant
    private void SpawnForager()
    {
        Instantiate(foragerPrefab, spawn.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Spawns a gardener ant
    private void SpawnGardener()
    {
        Instantiate(gardenerPrefab, spawn.transform.position, new Quaternion(0, 0, 0, 0));
    }
    
    // Spawns a excavator
    private void SpawnExcavator()
    {
        Instantiate(excavatorPrefab, spawn.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Spawns a trash handeler
    private void SpawnTrashHandler()
    {
        Instantiate(trashHandlerPrefab, spawn.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Spawns a Soldier
    private void SpawnSoldier()
    {
        Instantiate(SoldierPrefab, spawn.transform.position, new Quaternion(0, 0, 0, 0));
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
                    case (int)Ants.Soldier:
                        SpawnSoldier();
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
    }

    #endregion
}
