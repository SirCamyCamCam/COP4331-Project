// --------------------------------------------------------------
// Coloniant - SoliderAnt                               2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderAnt : MonoBehaviour {

    #region Inpesctor Fields

    [SerializeField]
    private Ant ant;
    [SerializeField]
    private float patrolWaitTime;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.SOLDIER;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(WaitToPatrol());
	}

    #endregion

    #region Public Methods

    public void DecideNextMove()
    {
        StartCoroutine(WaitToPatrol());
        ant.AssignAntState(Ant.AntState.IDLE);
        // No Repond for time sake
    }

    #endregion

    #region Private Methods

    private void Patrol()
    {
        Waypoint w = WaypointManager.main.ReturnRandomWaypoint();
        List<GameObject> path = WaypointManager.main.SearchPathKnownTarget(ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(), w);
        ant.AssignWaypointList(path);
    }

    private void Respond()
    {

    }

    #endregion

    #region Coroutines

    private IEnumerator WaitToPatrol()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        Patrol();
    }

    #endregion
}
