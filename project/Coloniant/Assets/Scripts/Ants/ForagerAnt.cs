// --------------------------------------------------------------
// Coloniant - ForagerAnt                               2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForagerAnt : MonoBehaviour {

    #region Inspector Fields

    [SerializeField]
    private Ant ant;
    [SerializeField]
    private float idleWaitTime;

    #endregion

    #region Run-Time Fields

    private Leaf transportingLeaf;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.FORAGER;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(waitToGoToLeafSite());
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    #endregion

    #region Public Methods

    public void DecideNextMove()
    {
        if (ant.ReturnCurrentWaypoint().GetComponent<Waypoint>().ReturnWaypointType() == WaypointManager.WaypointType.LEAF_SITE)
        {
            AtLeafSite();
        }
        else if (ant.ReturnCurrentWaypoint().GetComponent<Waypoint>().ReturnWaypointType() == WaypointManager.WaypointType.FARM_SITE)
        {
            AtFarmSite();
        }
    }

    #endregion

    #region Private Methods

    private void AtLeafSite()
    {
        transportingLeaf = LeafManager.main.CollectedALeaf(ant.ReturnCurrentWaypoint().GetComponent<Waypoint>());

        if (transportingLeaf == null)
        {
            // PANIC!! NO MORE LEAVES AT SITE FIND ANOTHER
        }

        ant.AssignAntState(Ant.AntState.IDLE);
        StartCoroutine(waitToGoToFarmSite());
    }

    private void AtFarmSite()
    {
        LeafManager.main.AddLeafToFarm(ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(), transportingLeaf);
        transportingLeaf = null;

        ant.AssignAntState(Ant.AntState.IDLE);
        StartCoroutine(waitToGoToLeafSite());
    }

    private void GoToLeafSite()
    {
        List<GameObject> waypointList = WaypointManager.main.SearchPathUnknownTarget(
            ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(),
            WaypointManager.WaypointType.LEAF_SITE
            );

        ant.AssignWaypointList(waypointList);
    }

    private void GoToFarmSite()
    {
        List<GameObject> waypointList = WaypointManager.main.SearchPathUnknownTarget(
            ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(),
            WaypointManager.WaypointType.FARM_SITE
            );

        ant.AssignWaypointList(waypointList);
    }


    #endregion

    #region Coroutine

    private IEnumerator waitToGoToLeafSite()
    {
        yield return new WaitForSeconds(idleWaitTime);
        GoToLeafSite();
    }

    private IEnumerator waitToGoToFarmSite()
    {
        yield return new WaitForSeconds(idleWaitTime);
        GoToFarmSite();
    }

#endregion
}
