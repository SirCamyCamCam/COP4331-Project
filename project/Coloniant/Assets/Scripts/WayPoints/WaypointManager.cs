// --------------------------------------------------------------
// Coloniant - WaypointManager                          2/23/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaypointManager : MonoBehaviour {

    #region Inner Classes

    [System.Serializable]
    public class WaypointBridge
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
        EXIT,
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
    private GameObject startExitPostion;
    [SerializeField]
    private GameObject startLeafPostion;

    [Header("Line Renderer")]
    public Material lineMaterial;
    public float lineWidth;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject waypointPrefab;

    [Header("Settings")]
    [SerializeField]
    private int maxAntsOnStart;

    [Space(10)]
    [Header("Sprite Renderers")]
    [SerializeField]
    private Sprite farmingSprite;
    [SerializeField]
    private Sprite transitionSprite;
    [SerializeField]
    private Sprite trashSprite;
    [SerializeField]
    private Sprite nurserySprite;
    [SerializeField]
    private Sprite exitSprite;
    [SerializeField]
    private Sprite enterSprite;
    [SerializeField]
    private Sprite leafSprite;

    #endregion

    #region Run-Time Fields

    private List<Waypoint> allWaypoints;
    private List<Waypoint> leafWaypoints;
    private List<Waypoint> nurseryWaypoints;
    private List<Waypoint> farmWaypoints;
    private List<Waypoint> trashSitesWaypoint;
    private List<Waypoint> entranceWaypoints;
    private List<Waypoint> transitionWaypoints;
    private List<Waypoint> exitWaypoints;
    private HashSet<WaypointBridge> bridgeSet;
    private Dictionary<WaypointBridge, LineRenderer> waypointLines;
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
        waypointLines = new Dictionary<WaypointBridge, LineRenderer>();
        bridgeSet = new HashSet<WaypointBridge>();

        allWaypoints = new List<Waypoint>();
        leafWaypoints = new List<Waypoint>();
        nurseryWaypoints = new List<Waypoint>();
        farmWaypoints = new List<Waypoint>();
        trashSitesWaypoint = new List<Waypoint>();
        entranceWaypoints =  new List<Waypoint>();
        transitionWaypoints = new List<Waypoint>();
        exitWaypoints = new List<Waypoint>();
}

    // Use this for initialization
    void Start () {
        SpawnAllOriginalWaypoints();
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

    // Spawns all the original waypoints to start the game
    private void SpawnAllOriginalWaypoints()
    {
        List<Waypoint> transitionWaypoint = new List<Waypoint>();
        List<Waypoint> entranceWaypoint = new List<Waypoint>();
        List<Waypoint> exitWaypoint = new List<Waypoint>();
        // Spawn Transition point
        transitionWaypoint.Add(
            SpawnWaypoint(
                WaypointType.TRANSITION,
                Level.UNDER_GROUND, 
                transitionWaypoint,
                startTransitionPosition.transform.position
                )
            );
        // Spawn farm point
        SpawnWaypoint(
            WaypointType.FARM_SITE,
            Level.UNDER_GROUND,
            transitionWaypoint,
            startFarmPosition.transform.position
            );
        // Spawn exit point
        exitWaypoint.Add(
            SpawnWaypoint(
                WaypointType.EXIT,
                Level.UNDER_GROUND,
                transitionWaypoint, 
                startExitPostion.transform.position
                )
            );
        // Spawn nursery point
        SpawnWaypoint(
            WaypointType.NURSERY_SITE,
            Level.UNDER_GROUND,
            transitionWaypoint,
            startNurseryPosition.transform.position
            );
        // Spawn entrance point
        entranceWaypoint.Add(
            SpawnWaypoint(
                WaypointType.ENTRANCE,
                Level.ABOVE_GROUND,
                exitWaypoint,
                startEntrancePosition.transform.position
                )
            );
        // Spawn trash site
        SpawnWaypoint(
            WaypointType.TRASH_SITE,
            Level.UNDER_GROUND,
            transitionWaypoint,
            startTrashPosition.transform.position
            );
        // Spawn Leaf site
        SpawnWaypoint(
            WaypointType.LEAF_SITE,
            Level.ABOVE_GROUND,
            entranceWaypoint,
            startLeafPostion.transform.position
            );

        Destroy(startNurseryPosition.transform.parent.gameObject);
    }
    
    private List<GameObject> SearchWaypointPathRecursiveType(
        Waypoint currentWaypoint, 
        WaypointType waypointTypeToFind, 
        List<GameObject> list,
        List<Waypoint> visited)
    {
        // Check for base case
        if (currentWaypoint.ReturnWaypointType() == waypointTypeToFind)
        {
            return list;
        }

        // int i = 0;

        // check for other posibilties
        foreach (Waypoint w in currentWaypoint.connectedWaypoints)
        {
            //Debug.Log(i);
            //i++;
            if (visited.Contains(w) == false)
            {
                visited.Add(w);
                list.Add(w.ReturnWaypointGameObject());
                SearchWaypointPathRecursiveType(w, waypointTypeToFind, list, visited);

                if (list[list.Count - 1].GetComponent<Waypoint>().ReturnWaypointType() == waypointTypeToFind)
                {
                    return list;
                }

                list.Remove(w.ReturnWaypointGameObject());
            }
        }

        return list;
    }

    private List<GameObject> SearchWaypointPathRecursiveTarget(
        Waypoint currentWaypoint, 
        Waypoint targetWaypoint, 
        List<GameObject> list, 
        List<Waypoint> visited)
    {
        // Base Case
        if (currentWaypoint == targetWaypoint)
        {
            return list;
        }

        // check for other posibilties
        foreach (Waypoint w in currentWaypoint.connectedWaypoints)
        {
            //Debug.Log(i);
            //i++;
            if (visited.Contains(w) == false)
            {
                visited.Add(w);
                list.Add(w.ReturnWaypointGameObject());
                SearchWaypointPathRecursiveTarget(w, targetWaypoint, list, visited);

                if (list[list.Count - 1].GetComponent<Waypoint>() == targetWaypoint)
                {
                    return list;
                }

                list.Remove(w.ReturnWaypointGameObject());
            }
        }

        return list;
    }

    #endregion

    #region Public Methods

    // Spawns a new Waypoint
    public Waypoint SpawnWaypoint(
        WaypointManager.WaypointType waypointType, 
        WaypointManager.Level waypointLevel, 
        List<Waypoint> connectedWaypoints, 
        Vector3 spawnLocation)
    {
        // Spawn it
        GameObject newWaypointGameObject = Instantiate(waypointPrefab, spawnLocation, new Quaternion(0, 0, 0, 0), transform);
        if (newWaypointGameObject == null)
        {
            Debug.Log("Failed to spawn a " + waypointType);
            return null;
        }

        // Get Waypoint object
        Waypoint newWaypointClass = newWaypointGameObject.GetComponent<Waypoint>();
        if (newWaypointClass == null)
        {
            Debug.Log("Failed to get waypoint class");
            return null;
        }

        // Assign connected waypoints
        newWaypointClass.SetUpWaypointTypes(waypointType, waypointLevel);

        // Connect everything else
        if (connectedWaypoints.Count > 0)
        {
            foreach (Waypoint w in connectedWaypoints)
            {
                if (w == null)
                {
                    Debug.Log("Failed to get connected waypoints");
                    return null;
                }
                w.AddAConnectedWaypoint(newWaypointClass);
                newWaypointClass.AddAConnectedWaypoint(w);
                WaypointBridge newBridge = new WaypointBridge(w, newWaypointClass);
                if (newBridge == null)
                {
                    Debug.Log("Failed to create a new bridge for " + waypointType);
                    return null;
                }
                bridgeSet.Add(newBridge);
                curentAntsInBridges.Add(newBridge, 0);
                maxAntsAllowedBetweenWaypoints.Add(newBridge, maxAntsOnStart);
                flowBetweenWaypoints.Add(newBridge, 0);

                LineRenderer newLine = new GameObject().AddComponent<LineRenderer>();
                newLine.transform.parent = transform;
                newLine.gameObject.name = "LineRenderer";
                newLine.material = main.lineMaterial;
                newLine.startWidth = lineWidth;
                newLine.endWidth = lineWidth;
                newLine.numCapVertices = 5;
                newLine.SetPosition(0, w.GetComponent<Transform>().position);
                newLine.SetPosition(1, newWaypointGameObject.transform.position);
                newLine.sortingOrder = -1;
                if (waypointLevel == Level.ABOVE_GROUND && w.CurrentLevel() == Level.ABOVE_GROUND && GameManager.main.currentView == GameManager.CurrentView.UNDER_GROUND)
                {
                    newLine.enabled = false;
                }
                else if (waypointLevel == Level.UNDER_GROUND && w.CurrentLevel() == Level.UNDER_GROUND && GameManager.main.currentView == GameManager.CurrentView.SURFACE)
                {
                    newLine.enabled = false;
                }
                else if (waypointLevel == Level.UNDER_GROUND && w.CurrentLevel() == Level.ABOVE_GROUND)
                {
                    newLine.enabled = false;
                }
                else if (waypointLevel == Level.ABOVE_GROUND && w.CurrentLevel() == Level.UNDER_GROUND)
                {
                    newLine.enabled = false;
                }
                waypointLines.Add(newBridge, newLine);
            }
        }

        // Add to type list
        switch(waypointType)
        {
            case WaypointType.FARM_SITE:
                farmWaypoints.Add(newWaypointClass);
                LeafManager.main.NewFarmWaypoint(newWaypointClass);
                newWaypointClass.AssignSprite(farmingSprite);
                break;
            case WaypointType.ENTRANCE:
                entranceWaypoints.Add(newWaypointClass);
                newWaypointClass.AssignSprite(enterSprite);
                break;
            case WaypointType.LEAF_SITE:
                leafWaypoints.Add(newWaypointClass);
                LeafManager.main.NewLeafSite(newWaypointClass);
                newWaypointClass.AssignSprite(leafSprite);
                break;
            case WaypointType.NURSERY_SITE:
                nurseryWaypoints.Add(newWaypointClass);
                newWaypointClass.AssignSprite(nurserySprite);
                break;
            case WaypointType.TRANSITION:
                transitionWaypoints.Add(newWaypointClass);
                newWaypointClass.AssignSprite(transitionSprite);
                break;
            case WaypointType.TRASH_SITE:
                trashSitesWaypoint.Add(newWaypointClass);
                TrashManager.main.AddTrashWaypoint(newWaypointClass);
                newWaypointClass.AssignSprite(trashSprite);
                break;
            case WaypointType.EXIT:
                trashSitesWaypoint.Add(newWaypointClass);
                newWaypointClass.AssignSprite(exitSprite);
                break;
            default:
                Debug.Log("Failed to assign waypoint type");
                return null;
        }

        // Add to all
        allWaypoints.Add(newWaypointClass);

        return newWaypointClass;
    }

    public bool RemoveWaypoint(Waypoint waypointToRemove)
    {
        bool foundBridge = false;
        foreach (WaypointBridge wb in bridgeSet)
        {
            if (wb.waypoint1 == waypointToRemove)
            {
                // Delete from connected waypoints
                wb.waypoint2.RemoveAConnectedWaypoint(waypointToRemove);
                bridgeSet.Remove(wb);
                foundBridge = true;
                // Add functionality to connect the other two waypoints
            }
            else if(wb.waypoint2 == waypointToRemove)
            {
                // Delete from connected waypoints
                wb.waypoint1.RemoveAConnectedWaypoint(waypointToRemove);
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

    public void SwitchWaypointLevel(Level level)
    {
        foreach (Waypoint w in allWaypoints)
        {
            w.SwitchLevel(level);
        }
        foreach (WaypointBridge w in bridgeSet)
        {
            if (w.waypoint1.CurrentLevel() == level && w.waypoint2.CurrentLevel() == level)
            {
                waypointLines[w].enabled = true;
            }
            else
            {
                waypointLines[w].enabled = false;
            }
        }
    }

    public List<GameObject> SearchPathUnknownTarget(Waypoint initalWaypoint, WaypointType waypointTypeToFind)
    {
        List<GameObject> path = new List<GameObject>();
        List<Waypoint> visited = new List<Waypoint>();

        // BFS Style

        /*List<Waypoint> visited = new List<Waypoint>();
        Queue<Waypoint> q = new Queue<Waypoint>();

        q.Enqueue(initalWaypoint);
        visited.Add(initalWaypoint);

        while (q.Count != 0)
        {
            Waypoint waypoint = q.Dequeue();

            if (waypoint.ReturnWaypointType() == waypointTypeToFind)
            {
                path.Add(waypoint.ReturnWaypointGameObject());
                return path;
            }

            foreach (Waypoint w in waypoint.connectedWaypoints)
            {
                if (!visited.Contains(w))
                {
                    visited.Add(w);
                    q.Enqueue(w);
                }
            }
        }*/

        path.Add(initalWaypoint.ReturnWaypointGameObject());
        visited.Add(initalWaypoint);
        path = SearchWaypointPathRecursiveType(initalWaypoint, waypointTypeToFind, path, visited);

        return path;
    }

    public List<GameObject> SearchPathKnownTarget(Waypoint initalWaypoint, Waypoint targetWaypoint)
    {
        List<GameObject> path = new List<GameObject>();
        List<Waypoint> visited = new List<Waypoint>();

        path.Add(initalWaypoint.ReturnWaypointGameObject());
        visited.Add(initalWaypoint);

        path = SearchWaypointPathRecursiveTarget(initalWaypoint, targetWaypoint, path, visited);

        return path;
    }

    public GameObject ReturnNurseryGameObject()
    {
        return nurseryWaypoints[0].gameObject;
    }

    public List<Waypoint> ReturnFarmList()
    {
        return farmWaypoints;
    }

    public List<Waypoint> ReturnLeafList()
    {
        return leafWaypoints;
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
