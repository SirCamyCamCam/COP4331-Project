//By: Amin Kavehzadeh 
// Modified by: Cameron Carstens
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaypointSpawner : MonoBehaviour {

    #region Inspector Fields

    [Header("Cameron's Addition:")]
    [SerializeField]
    private GameObject selectingConncected;
    [SerializeField]
    private GameObject waypointOptionsPanel;
    [SerializeField]
    private GameObject spawnAntsPanel;
    [SerializeField]
    private GameObject startSpawnSequenceButton;
    [SerializeField]
    private GameObject selectSpawnLocation;
    [SerializeField]
    private GameObject tooClosePanel;
    [SerializeField]
    private Material lineMaterial;
    [SerializeField]
    private float lineWidth;

    [Space(20)]
    [Header("Amin's:")]
    [SerializeField]
    private WaypointManager manager;

    [SerializeField] private OBJFinder finder;

    List<Waypoint> transitionwaypoint = new List<Waypoint>();
    public GameObject text;

    #endregion

    #region Run-Time Fields

    private bool farmT = false; 
    private bool trashT = false; 
    private bool nurseT = false; 
    private bool leafT = false; 
    private bool transitionT= false; 

    //waypoint stage flags
    private bool pickconnected = false; 
    private bool pickspawnlocation = false;

    List<LineRenderer> lines;

    #endregion

    #region Monobehaviors

    private void Start()
    {
        waypointOptionsPanel.SetActive(false);
        selectingConncected.SetActive(false);
        tooClosePanel.SetActive(false);
        selectSpawnLocation.SetActive(false);
        lines = new List<LineRenderer>();
    }


    // Update is called once per frame
    void Update () {
     
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //for farming Waypoints 
            if (farmT == true )
            {
                DoingFarmWaypoint();
            }

            if (trashT == true )
            {
                DoingTrashWaypoint();
            }

            if (nurseT == true)
            {
                DoingNurseryWaypoint();
            }

            if (transitionT == true )
            {
                DoingTransitionWaypoint();
            }
        }

        if (lines.Count != 0)
        {
            foreach (LineRenderer l in lines)
            {
                l.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono));
            }
        }
    }

    #endregion

    #region Public Methods

    public List<Waypoint> Transitionadder()
    {
        Waypoint reference = new Waypoint(); 

        if(finder.Getgrab() != null)
        {
            reference = finder.Getgrab().GetComponent<Waypoint>(); 
            transitionwaypoint.Add(reference);

            LineRenderer l = new GameObject().AddComponent<LineRenderer>();
            l.material = lineMaterial;
            l.startWidth = lineWidth;
            l.endWidth = lineWidth;
            l.numCapVertices = 5;
            l.SetPosition(0, reference.GetComponent<Transform>().position);
            l.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono));
            l.sortingOrder = -1;
            lines.Add(l);

            //print("added!");
            return transitionwaypoint; 
        }
        else
        {
            //print("finder.Getgrab() == null");
            return null; 
        }
    }

    // Called when we choose to spawn farm waypoint
    public void FarmT()
    {
        farmT = true;
        waypointOptionsPanel.SetActive(false);
        selectingConncected.SetActive(true);
    }

    // Called when we choose to spawn trash waypoint
    public void TrashT()
    {
        trashT = true;
        waypointOptionsPanel.SetActive(false);
        selectingConncected.SetActive(true);
    }

    // Called when we choose to spawn nursery waypoint
    public void NurseT()
    {
        nurseT = true;
        waypointOptionsPanel.SetActive(false);
        selectingConncected.SetActive(true);
    }

    // Called when we choose to spawn transition waypoint
    public void TransitionT()
    {
        transitionT = true;
        waypointOptionsPanel.SetActive(false);
        selectingConncected.SetActive(true);
    }

    // Called when we finished selecting the waypoint we want to connect
    public void pickconnectedF()
    {
        pickconnected = false;
        pickspawnlocation = true;
        selectingConncected.SetActive(false);
        selectSpawnLocation.SetActive(true);
    }

    // Called when we want to canel spawning a waypoint
    public void CancelSpawningWaypoint()
    {
        farmT = false;
        trashT = false;
        nurseT = false;
        leafT = false;
        transitionT = false;
        waypointOptionsPanel.SetActive(false);
        startSpawnSequenceButton.SetActive(true);
        spawnAntsPanel.SetActive(true);
    }

    // Called when we want to start spawning waypoints
    public void ShowSpawnWaypointPanel()
    {
        spawnAntsPanel.SetActive(false);
        waypointOptionsPanel.SetActive(true);
        startSpawnSequenceButton.SetActive(false);
    }

    #endregion

    #region Private Methods

    private void DoingFarmWaypoint()
    {
        Vector3 worldview = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjust = new Vector3(worldview.x, worldview.y, -0.068f);
        bool tooClose = false;

        pickconnected = true;

        if (pickconnected == true)
        {
            Transitionadder();
        }

        if (pickspawnlocation == true)
        {
            foreach (Waypoint way in manager.allWaypoints)
            {
                if (Vector3.Distance(way.transform.position, adjust) < 10)
                {
                    tooClose = true;
                }
            }

            if (tooClose == false)
            {
                //print("im about to spawn"); 
                manager.SpawnWaypoint(WaypointManager.WaypointType.FARM_SITE, WaypointManager.Level.UNDER_GROUND, transitionwaypoint, adjust);
                transitionwaypoint.Clear();
                pickspawnlocation = false;
                text.SetActive(false);
                farmT = false;
                tooClosePanel.SetActive(false);
                selectSpawnLocation.SetActive(false);
                waypointOptionsPanel.SetActive(false);
                startSpawnSequenceButton.SetActive(true);
                spawnAntsPanel.SetActive(true);
                foreach (LineRenderer l in lines)
                {
                    Destroy(l);
                }
                lines.Clear();
            }
            else
            {
                //print("Maybe Try another Spot?");
                tooClosePanel.SetActive(true);
                tooClose = false;
            }
        }
    }

    private void DoingTransitionWaypoint()
    {
        Vector3 worldview = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjust = new Vector3(worldview.x, worldview.y, -0.068f);
        bool tooClose = false;

        pickconnected = true;

        if (pickconnected == true)
        {
            Transitionadder();
        }

        if (pickspawnlocation == true)
        {
            foreach (Waypoint way in manager.allWaypoints)
            {
                if (Vector3.Distance(way.transform.position, adjust) < 10)
                {
                    tooClose = true;
                }
            }

            if (tooClose == false)
            {
                //print("im about to spawn");
                manager.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, transitionwaypoint, adjust);
                transitionwaypoint.Clear();
                pickspawnlocation = false;
                text.SetActive(false);
                transitionT = false;
                tooClosePanel.SetActive(false);
                selectSpawnLocation.SetActive(false);
                waypointOptionsPanel.SetActive(false);
                startSpawnSequenceButton.SetActive(true);
                spawnAntsPanel.SetActive(true);
                foreach (LineRenderer l in lines)
                {
                    Destroy(l);
                }
                lines.Clear();
            }
            else
            {
                //print("Maybe Try another Spot?");
                tooClose = false;
                tooClosePanel.SetActive(true);
            }
        }
    }

    private void DoingNurseryWaypoint()
    {
        Vector3 worldview = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjust = new Vector3(worldview.x, worldview.y, -0.068f);
        bool tooClose = false;

        pickconnected = true;

        if (pickconnected == true)
        {
            Transitionadder();
        }

        if (pickspawnlocation == true)
        {
            foreach (Waypoint way in manager.allWaypoints)
            {
                if (Vector3.Distance(way.transform.position, adjust) < 10)
                {
                    tooClose = true;
                }
            }

            if (tooClose == false)
            {
                manager.SpawnWaypoint(WaypointManager.WaypointType.NURSERY_SITE, WaypointManager.Level.UNDER_GROUND, transitionwaypoint, adjust);
                transitionwaypoint.Clear();
                pickspawnlocation = false;
                text.SetActive(false);
                nurseT = false;
                tooClosePanel.SetActive(false);
                selectSpawnLocation.SetActive(false);
                waypointOptionsPanel.SetActive(false);
                startSpawnSequenceButton.SetActive(true);
                spawnAntsPanel.SetActive(true);
                foreach (LineRenderer l in lines)
                {
                    Destroy(l);
                }
                lines.Clear();
            }
            else
            {
                //print("Maybe Try another Spot?");
                tooClose = false;
                tooClosePanel.SetActive(true);
            }
        }
    }

    private void DoingTrashWaypoint()
    {
        Vector3 worldview = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        Vector3 adjust = new Vector3(worldview.x, worldview.y, -0.068f);
        bool tooClose = false;

        pickconnected = true;

        if (pickconnected == true)
        {
            Transitionadder();
        }

        if (pickspawnlocation == true)
        {
            foreach (Waypoint way in manager.allWaypoints)
            {
                if (Vector3.Distance(way.transform.position, adjust) < 10)
                {
                    tooClose = true;
                }
            }

            if (tooClose == false)
            {
                manager.SpawnWaypoint(WaypointManager.WaypointType.TRASH_SITE, WaypointManager.Level.UNDER_GROUND, transitionwaypoint, adjust);
                transitionwaypoint.Clear();
                pickspawnlocation = false;
                text.SetActive(false);
                trashT = false;
                tooClosePanel.SetActive(false);
                selectSpawnLocation.SetActive(false);
                waypointOptionsPanel.SetActive(false);
                startSpawnSequenceButton.SetActive(true);
                spawnAntsPanel.SetActive(true);
                foreach (LineRenderer l in lines)
                {
                    Destroy(l);
                }
                lines.Clear();
            }
            else
            {
                //print("Maybe Try another Spot?");
                tooClose = false;
                tooClosePanel.SetActive(true);
            }
        }
    }

    #endregion
}
