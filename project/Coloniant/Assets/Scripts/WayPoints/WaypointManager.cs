// --------------------------------------------------------------
// Coloniant - WaypointManager                          2/23/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {

    #region Inner Classes

    private class WaypointBridge : Object
    {
        public Waypoint waypoint1;
        public Waypoint waypoint2;

        public WaypointBridge(Waypoint firstWaypoint, Waypoint secondWaypoint)
        {
            waypoint1 = firstWaypoint;
            waypoint2 = secondWaypoint;
        }
    }

    #endregion

    #region Enum

    public enum WaypointType
    {
        NURSERY_SITE,
        TRASH_SITE,
        TRASH,
        FARM_SITE,
        LEAF_SITE,
        ENTRANCE,
        TRANSITION
    }

    public enum Level
    {
        ABOVE_GROUND,
        UNDER_GROUND
    }

    #endregion

    #region Static Fields

    public static WaypointManager main;

    #endregion

    #region Inspector Fields

    [Header("Starter Waypoint Locations")]
    [SerializeField]
    private GameObject startNurseryPosition;
    [SerializeField]
    private GameObject startTrashPosition;
    [SerializeField]
    private GameObject startFarmPosition;
    [SerializeField]
    private GameObject startTransitionPosition;
    [SerializeField]
    private GameObject startEntrancePosition;
    [SerializeField]
    private GameObject startLeafPostion;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject waypointPrefab;

    [Header("Settings")]
    [SerializeField]
    private int maxAntsOnStart;

    #endregion

    #region Run-Time Fields

    private List<Waypoint> allWaypoints;
    private List<Waypoint> leafWaypoints;
    private List<Waypoint> nurseryWaypoints;
    private List<Waypoint> farmWaypoints;
    private List<Waypoint> trashSitesWaypoint;
    private List<Waypoint> trashWaypoint;
    private List<Waypoint> entranceWaypoints;
    private List<Waypoint> transitionWaypoints;
    private HashSet<WaypointBridge> bridgeSet;
    private Dictionary<WaypointBridge, int> maxAntsAllowedBetweenWaypoints;
    private Dictionary<WaypointBridge, int> curentAntsInBridges;
    private Dictionary<WaypointBridge, float> flowBetweenWaypoints;
    private Dictionary<Waypoint, int> idleAntAtWaypointCount;

    private float currentOverallFlow;
    private float flowUpdateTime;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;
        allWaypoints = new List<Waypoint>();
        maxAntsAllowedBetweenWaypoints = new Dictionary<WaypointBridge, int>();
        curentAntsInBridges = new Dictionary<WaypointBridge, int>();
        flowBetweenWaypoints = new Dictionary<WaypointBridge, float>();
        idleAntAtWaypointCount = new Dictionary<Waypoint, int>();
    }

    // Use this for initialization
    void Start () {
        // Spawn all the necesary waypoints to begin the game
        SpawnWaypoint(WaypointType.TRANSITION, Level.UNDER_GROUND, null, startTransitionPosition.transform.position);
        //SpawnWaypoint(WaypointType.FARM_SITE, Level.UNDER_GROUND, )

        currentOverallFlow = 1;
        flowUpdateTime = GameManager.main.FlowUpdateSeconds();
        StartCoroutine(waitToUpdateOverallFlow());
	}
	
	// Update is called once per frame
	/*void Update () {
        
	}*/

    #endregion

    #region Private Methods

    // Updates all the flow values
    private void UpdateFlowValues()
    {
        float totalFlow = 0;
        float curAnts = 0;
        float maxAnts = 0;
        foreach (WaypointBridge wpb in bridgeSet)
        {
            maxAnts = maxAntsAllowedBetweenWaypoints[wpb];
            curAnts = curentAntsInBridges[wpb];
            flowBetweenWaypoints[wpb] = 1 - (curAnts / maxAnts);
            totalFlow += 1 - (curAnts / maxAnts);
        }

        currentOverallFlow = totalFlow / (float)bridgeSet.Count;
    }

    #endregion

    #region Public Methods

    // Spawns a new Waypoint
    public bool SpawnWaypoint(
        WaypointManager.WaypointType waypointType, 
        WaypointManager.Level waypointLevel, 
        List<Waypoint> connectedWaypoints, 
        Vector3 spawnLocation)
    {
        // Spawn it
        GameObject newWaypointGameObject = Instantiate(waypointPrefab, spawnLocation, new Quaternion(0, 0, 0, 0));
        if (newWaypointGameObject == null)
        {
            return false;
        }

        // Get Waypoint object
        Waypoint newWaypointClass = newWaypointGameObject.GetComponent<Waypoint>();
        if (newWaypointClass == null)
        {
            return false;
        }

        newWaypointClass.SetUpWaypointTypes(waypointType, waypointLevel, connectedWaypoints);

        // Connect everything
        foreach (Waypoint w in connectedWaypoints)
        {
            w.AddAConnectedWaypoint(newWaypointClass);
            WaypointBridge newBridge = new WaypointBridge(w, newWaypointClass);
            if (newBridge == null)
            {
                return false;
            }
            bridgeSet.Add(newBridge);
            maxAntsAllowedBetweenWaypoints.Add(newBridge, maxAntsOnStart);
            flowBetweenWaypoints.Add(newBridge, 0);
        }

        switch(waypointType)
        {
            case WaypointType.FARM_SITE:
                farmWaypoints.Add(newWaypointClass);
                break;
            case WaypointType.ENTRANCE:
                entranceWaypoints.Add(newWaypointClass);
                break;
            case WaypointType.LEAF_SITE:
                leafWaypoints.Add(newWaypointClass);
                break;
            case WaypointType.NURSERY_SITE:
                nurseryWaypoints.Add(newWaypointClass);
                break;
            case WaypointType.TRANSITION:
                transitionWaypoints.Add(newWaypointClass);
                break;
            case WaypointType.TRASH:
                trashWaypoint.Add(newWaypointClass);
                break;
            case WaypointType.TRASH_SITE:
                trashSitesWaypoint.Add(newWaypointClass);
                break;
            default:
                return false;
        }

        allWaypoints.Add(newWaypointClass);

        return true;
    }

    public bool RemoveWaypoint(Waypoint waypointToRemove)
    {
        bool foundBridge = false;
        foreach (WaypointBridge wb in bridgeSet)
        {
            if (wb.waypoint1 == waypointToRemove || wb.waypoint2 == waypointToRemove)
            {
                bridgeSet.Remove(wb);
                foundBridge = true;
                // Add functionality to connect the other two waypoints
            }
        }

        if (foundBridge == false)
        {
            return false;
        }



        return true;
    }

    // Adds an ant to the current bridge for flow calcualtions
    public bool AddToAntCountInBridge(Waypoint currentWaypoint, Waypoint nextWaypoint)
    {
        foreach (WaypointBridge wb in bridgeSet)
        {
            if (wb.waypoint1 == currentWaypoint && wb.waypoint2 == nextWaypoint)
            {
                curentAntsInBridges[wb] += 1;
                return true;
            }
            else if (wb.waypoint1 == nextWaypoint && wb.waypoint2 == currentWaypoint)
            {
                curentAntsInBridges[wb] += 1;
                return true;
            }
        }
        return false;
    }

    // Removes an ant from the past bridge for flow calculation
    public bool RemoveAntFromBridgeCount(Waypoint currentWaypoint, Waypoint pastWaypoint)
    {
        foreach (WaypointBridge wb in bridgeSet)
        {
            if (wb.waypoint1 == currentWaypoint && wb.waypoint2 == pastWaypoint)
            {
                curentAntsInBridges[wb] -= 1;
                return true;
            }
            else if (wb.waypoint1 == pastWaypoint && wb.waypoint2 == currentWaypoint)
            {
                curentAntsInBridges[wb] -= 1;
                return true;
            }
        }
        return false;
    }

    #endregion

    #region Coroutines

    private IEnumerator waitToUpdateOverallFlow()
    {
        yield return new WaitForSeconds(flowUpdateTime);
        UpdateFlowValues();
        StartCoroutine(waitToUpdateOverallFlow());
    }

    #endregion
}
