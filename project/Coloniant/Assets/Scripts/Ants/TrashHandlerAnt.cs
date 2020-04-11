// --------------------------------------------------------------
// Coloniant - TrashHandlerAnt                          2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandlerAnt : MonoBehaviour {

    #region Inspector Fields

    [SerializeField]
    private Ant ant;
    [SerializeField]
    private float idleWaitTime;
    [SerializeField]
    private float dropOffWaiTime;

    #endregion

    #region Run-Time Fields

    private Trash transportingTrash;
    private Waypoint trashConnectedWaypoint;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.TRASH_HANDLER;
    }

    private void Start()
    {
        StartCoroutine(WaitToFindTrash());
    }

    #endregion

    #region Public Methods

    public void DecideNextMove()
    {
        // Bring to trash site
        if (transportingTrash != null && transportingTrash.ReturnTrashState() == TrashManager.TrashState.WAITING)
        {
            FindDropOffSite();
        }
        // Wait to find more trash
        else if (transportingTrash != null && transportingTrash.ReturnTrashState() == TrashManager.TrashState.TRANSPORT)
        {
            DropOffTrash();
        }
    }

    #endregion

    #region Private Methods

    // Drops off a piece of trash
    private void DropOffTrash()
    {
        Waypoint site = ant.ReturnCurrentWaypoint().GetComponent<Waypoint>();
        // If we got there and it's alrady full find another one
        if (TrashManager.main.CheckTrashSiteFull(site) == true)
        {
            FindDropOffSite();
        }

        TrashManager.main.AddTrashToSite(site);
        transportingTrash.DeleteTrashPrefab();
        transportingTrash = null;
        trashConnectedWaypoint = null;
        StartCoroutine(WaitToFindTrash());
        ant.AssignAntState(Ant.AntState.IDLE);
    }

    // Finds a drop off site
    private void FindDropOffSite()
    {
        transportingTrash.MarkAsPickedUp();
        transportingTrash.DisableSprite();
        Waypoint targetSite = TrashManager.main.FindDropOffSite();

        if (targetSite == null)
        {
            StartCoroutine(WaitToFindDropOffSite());
            ant.AssignAntState(Ant.AntState.IDLE);
            return;
        }

        if (trashConnectedWaypoint != null)
        {
            List<GameObject> list = WaypointManager.main.SearchPathKnownTarget(trashConnectedWaypoint, targetSite);
            ant.AssignWaypointList(list);
        }
        else
        {
            List<GameObject> list = WaypointManager.main.SearchPathKnownTarget(ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(), targetSite);
            ant.AssignWaypointList(list);
        }
    }

    // Finds a new trash
    private void FindNewTrash()
    {
        transportingTrash = TrashManager.main.FindTrashToPickUp();

        if (transportingTrash == null)
        {
            StartCoroutine(WaitToFindTrash());
            return;
        }

        Waypoint target = TrashManager.main.ReturnTrashWaypoint(transportingTrash);
        TrashManager.main.MarkAsSelected(transportingTrash);  

        if (target == null)
        {
            Debug.Log("Target Null in FindNewTrash in TrashHandlerAnt");
            StartCoroutine(WaitToFindTrash());
            return;
        }

        trashConnectedWaypoint = target;
        List<GameObject> path = WaypointManager.main.SearchPathKnownTarget(
            ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(),
            target
            );

        path.Add(transportingTrash.gameObject);
        ant.AssignWaypointList(path);
    }

    #endregion

    #region Coroutines

    // Waits to find the next trash
    private IEnumerator WaitToFindTrash()
    {
        yield return new WaitForSeconds(idleWaitTime);
        FindNewTrash();
    }

    // Waits to find drop off Site
    private IEnumerator WaitToFindDropOffSite()
    {
        yield return new WaitForSeconds(dropOffWaiTime);
        FindDropOffSite();
    }

    #endregion
}
