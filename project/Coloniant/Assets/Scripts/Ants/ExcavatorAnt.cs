// --------------------------------------------------------------
// Coloniant - ExcavatorAnt                             2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorAnt : MonoBehaviour {

    #region Inspector Fields

    [SerializeField]
    private Ant ant;
    [SerializeField]
    private float expansionTime;
    [SerializeField]
    private float waitToFindTime;

    #endregion

    #region Run-Time Fields

    private FlowManager.Road road;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.EXCAVATOR;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(WaitToFindExpansion());
	}

    // Update is called once per frame
    /*void Update () {
		
	}*/

    #endregion

    #region Public Methods

    public void DecideNextMove()
    {
        if (road != null)
        {
            StartCoroutine(WaitToIncreaseSize());
            ant.AssignAntState(Ant.AntState.IDLE);
        }
        else
        {
            StartCoroutine(WaitToFindExpansion());
            ant.AssignAntState(Ant.AntState.IDLE);
        }
    }

    #endregion

    #region Private Methods

    private void IncreaseSize()
    {
        if (road == null)
        {
            Debug.Log("Road null in IncreaseSize in ExcavatorAnt");
            return;
        }
        road.AddToMaxAnts();
        FlowManager.main.RemoveFromSelectedRoads(road);
        road = null;
        StartCoroutine(WaitToFindExpansion());
        ant.AssignAntState(Ant.AntState.IDLE);
    }

    private void FindExpansion()
    {
        road = FlowManager.main.FindNeededExpansion();

        if (road == null)
        {
            StartCoroutine(WaitToFindExpansion());
            return;
        }

        List<GameObject> path1 = WaypointManager.main.SearchPathKnownTarget(ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(), road.ReturnWaypoint1());
        List<GameObject> path2 = WaypointManager.main.SearchPathKnownTarget(ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(), road.ReturnWaypoint2());

        if (path1.Count <= path2.Count)
        {
            ant.AssignWaypointList(path1);
        }
        else
        {
            ant.AssignWaypointList(path2);
        }
    }

    #endregion

    #region Coroutines

    private IEnumerator WaitToIncreaseSize()
    {
        yield return new WaitForSeconds(expansionTime);
        IncreaseSize();
    }

    private IEnumerator WaitToFindExpansion()
    {
        yield return new WaitForSeconds(waitToFindTime);
        FindExpansion();
    }

    #endregion
}
