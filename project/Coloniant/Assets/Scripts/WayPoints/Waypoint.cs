// --------------------------------------------------------------
// Coloniant - WaypointManager                          2/23/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    
    #region Inspector Fields

    [Header("Dependencies")]
    [SerializeField]
    private GameObject attachedGameObject;

    [Header("Settings")]
    [SerializeField]
    private int maxAntsNum;

    #endregion

    #region Runt-Time Fields

    private WaypointManager.WaypointType type;
    private List<Waypoint> connectedWaypoints;
    private WaypointManager.Level level;

    #endregion

    #region Monobehavior

    private void Awake()
    {
        connectedWaypoints = new List<Waypoint>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    /*void Update () {
		
	}*/

    #endregion

    #region Public methods

    // Sets the types for the waypoint and connected waypoints
    public void SetUpWaypointTypes(WaypointManager.WaypointType typeToSet, WaypointManager.Level levelToSet, List<Waypoint> connected)
    {
        type = typeToSet;
        level = levelToSet;
        connectedWaypoints = connected;
    }

    // Adds a new waypoint
    public void AddAConnectedWaypoint(Waypoint newWaypoint)
    {
        connectedWaypoints.Add(newWaypoint);
    }

    #endregion

    #region Private Methods

    #endregion
}