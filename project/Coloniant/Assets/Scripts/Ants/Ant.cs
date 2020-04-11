// --------------------------------------------------------------
// Coloniant - Ant                                      2/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {

    #region enum

    public enum AntState
    {
        WALKING,
        IDLE,
        DEAD,
        JOB
    }

    public enum AntType
    {
        GARDENER,
        QUEEN,
        SOLDIER,
        EXCAVATOR,
        TRASH_HANDLER,
        FORAGER
    };

    #endregion

    #region Inspector Fields

    [SerializeField]
    private SpriteRenderer antSpriteRenderer;
    [SerializeField]
    private int lifeSeconds;
    [SerializeField]
    private GameObject antGameObject;

    #endregion

    #region Run-Time fields

    // Basic
    private AntState antState;
    private AntManager.SceneView antLevel;
    [HideInInspector]
    public AntType antType;
    private GameObject previousWaypoint;
    private GameObject nextWaypoint;
    private GameObject targetWaypoint;
    private List<GameObject> waypointPath;
    private int currentWaypoint;
    private float currentSpeed;
    private int foodConsumptionRate;
    // Noise
    private float xDirection;
    private float yDirection;
    // Idle Actions
    private float idleNoise;
    private float rotationSpeed;
    private float idleDistance;
    private bool isReturningToWaypoint;
    // Walking Actions
    private float walkingNoise;
    private float walkingWaypointDistance;
    private Coroutine checkAntSpeed;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        antState = AntState.IDLE;
    }

    // Use this for initialization
    void Start ()
    {
        switch(antType)
        {
            case AntType.EXCAVATOR:
                AntManager.main.AddToExcavatorCount(this);
                break;
            case AntType.FORAGER:
                AntManager.main.AddToForagerCount(this);
                break;
            case AntType.QUEEN:
                AntManager.main.AddToQueenCount(this);
                break;
            case AntType.TRASH_HANDLER:
                AntManager.main.AddToTrashHandlerCount(this);
                break;
            case AntType.SOLDIER:
                AntManager.main.AddToSoldierCount(this);
                break;
            case AntType.GARDENER:
                AntManager.main.AddToGardenerCount(this);
                break;
            default:
                Debug.LogError("No defined ant type to add to manager!");
                break;
        }

        antLevel = AntManager.SceneView.UNDER_GROUND;
        ChangeView(AntManager.main.currentView);
        StartCoroutine(waitToKillAnt());
        xDirection = 500000;
        yDirection = 500000;
        currentSpeed = AntManager.main.DefaultAntSpeed();
        idleNoise = AntManager.main.DefaultAntIdleNoise();
        rotationSpeed = AntManager.main.DefaultRotationSpeed();
        idleDistance = AntManager.main.DefaultIdleDistance();
        walkingNoise = AntManager.main.DefaultWalkingNoise();
        walkingWaypointDistance = AntManager.main.DefaultWalkingWaypointDistance();
        isReturningToWaypoint = false;
	}

    // No update for effeciency
    // Update is called once per frame
    void Update () {
        if (targetWaypoint == null)
        {
            return;
        }

        if (antState == AntState.IDLE)
        {
            IdleAnt();
        }
        else if (antState == AntState.WALKING)
        {
            WalkingAnt();
            CheckWaypointDistance();
        }
	}

    #endregion

    #region Public Methods

    // Used to assign the current target
    public void AssignTargetWaypoint(GameObject target)
    {
        previousWaypoint = targetWaypoint;
        targetWaypoint = target;
        if (previousWaypoint != null && targetWaypoint.GetComponent<Waypoint>() != null && previousWaypoint.GetComponent<Waypoint>() != null)
        {
            FlowManager.main.AddAntToRoad(targetWaypoint.GetComponent<Waypoint>(), previousWaypoint.GetComponent<Waypoint>());
            CallCheckSpeed();
        }
    }

    // Switches between above ground a below ground
    public void ChangeView(AntManager.SceneView view)
    {
        if (view == AntManager.SceneView.ABOVE_GROUND && antLevel == AntManager.SceneView.UNDER_GROUND)
        {
            antSpriteRenderer.enabled = false;
        }
        else if (view == AntManager.SceneView.ABOVE_GROUND && antLevel == AntManager.SceneView.ABOVE_GROUND)
        {
            antSpriteRenderer.enabled = true;
        }
        else if (view == AntManager.SceneView.UNDER_GROUND && antLevel == AntManager.SceneView.UNDER_GROUND)
        {
            antSpriteRenderer.enabled = true;
        }
        else if (view == AntManager.SceneView.UNDER_GROUND && antLevel == AntManager.SceneView.ABOVE_GROUND)
        {
            antSpriteRenderer.enabled = false;
        }
        else
        {
            Debug.LogError("Invalid combination of scene and ant views in ants!");
        }
    }

    public void ChangeAntLevel(AntManager.SceneView view)
    {
        antLevel = view;
        ChangeView(AntManager.main.currentView);
    }

    public AntState ReturnAntState()
    {
        return antState;
    }

    public void AssignWaypointList(List<GameObject> list)
    {
        waypointPath = list;
        AssignAntState(AntState.WALKING);
        AssignTargetWaypoint(waypointPath[0]);
        currentWaypoint = 0;
    }

    public GameObject ReturnCurrentWaypoint()
    {
        return targetWaypoint;
    }

    public void AssignAntState(AntState setToState)
    {
        antState = setToState;
    }

    // Kills the ant
    public void Die()
    {
        AntManager.main.RemoveAntFromList(this);
        if (previousWaypoint == null && targetWaypoint.GetComponent<Waypoint>() == null)
        {
            Destroy(gameObject);
            return;
        }

        GameObject trashGameObject = Instantiate(
            TrashManager.main.TrashPrefab(), 
            gameObject.transform.position, 
            new Quaternion(0, 0, 0, 0), 
            TrashManager.main.gameObject.transform
            );

        Trash trash = trashGameObject.GetComponent<Trash>();
        if (antLevel == AntManager.SceneView.ABOVE_GROUND)
        {
            trash.SetLayer(Trash.Layer.SURFACE);
        }
        else
        {
            trash.SetLayer(Trash.Layer.UNDERGROUND);
        }
        TrashManager.main.CreatedNewTrash(trash);

        if (previousWaypoint == null)
        {
            TrashManager.main.AddTrashToWaypoints(trash, targetWaypoint.GetComponent<Waypoint>());
        }
        else
        {
            float lastDistance = Vector2.Distance(transform.position, previousWaypoint.transform.position);
            float targetDistance = Vector2.Distance(transform.position, targetWaypoint.transform.position);

            if (targetDistance < lastDistance)
            {
                TrashManager.main.AddTrashToWaypoints(trash, targetWaypoint.GetComponent<Waypoint>());
            }
            else
            {
                TrashManager.main.AddTrashToWaypoints(trash, previousWaypoint.GetComponent<Waypoint>());
            }

            FlowManager.main.AddTrashToRoad(targetWaypoint.GetComponent<Waypoint>(), previousWaypoint.GetComponent<Waypoint>());
        }
        
        Destroy(gameObject);
    }

    #endregion

    #region Private Methods

    // Called when the Ant is idle
    private void IdleAnt()
    {
        // Determine whether to turn around
        if (Vector2.Distance(antGameObject.transform.position, targetWaypoint.transform.position) > idleDistance && isReturningToWaypoint == false)
        {
            isReturningToWaypoint = true;
        }
        else if (Vector2.Distance(antGameObject.transform.position, targetWaypoint.transform.position) < idleDistance - 1 && isReturningToWaypoint == true)
        {
            isReturningToWaypoint = false;
        }

        // Noise stuff
        xDirection += 1 * Random.value;
        yDirection += 1 * Random.value;

        if (xDirection == int.MaxValue || yDirection == int.MaxValue)
        {
            xDirection = 0;
            yDirection = 0;
        }

        if (isReturningToWaypoint == false)
        {
            // Apply noise
            float randomVal = Mathf.PerlinNoise(
                xDirection * idleNoise,
                yDirection * idleNoise);
            float angle = Mathf.Lerp(-10, 10, randomVal);
            antGameObject.transform.eulerAngles = new Vector3(0,0, antGameObject.transform.eulerAngles.z + angle);
        }
        else
        {
            // Turn to waypoint
            Vector3 direction = (targetWaypoint.transform.position - antGameObject.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.back);
            antGameObject.transform.rotation = Quaternion.Slerp(antGameObject.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            antGameObject.transform.eulerAngles = new Vector3(0, 0, antGameObject.transform.eulerAngles.z);

        }
        // Go forward
        antGameObject.transform.position +=
            antGameObject.transform.up
            * Time.deltaTime
            * currentSpeed;
    }

    // Called when the Ant is walking
    private void WalkingAnt()
    {
        // Face direction of target waypoint
        Vector3 direction = (targetWaypoint.transform.position - antGameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.back);
        antGameObject.transform.rotation = Quaternion.Slerp(antGameObject.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        // Noise
        // Noise stuff
        xDirection += 1 * Random.value;
        yDirection += 1 * Random.value;

        if (xDirection == int.MaxValue || yDirection == int.MaxValue)
        {
            xDirection = 0;
            yDirection = 0;
        }

        float randomVal = Mathf.PerlinNoise(
                xDirection * walkingNoise,
                yDirection * walkingNoise);
        float angle = Mathf.Lerp(-2, 2, randomVal);
        antGameObject.transform.eulerAngles = new Vector3(0, 0, antGameObject.transform.eulerAngles.z + angle);

        antGameObject.transform.position +=
            antGameObject.transform.up
            * Time.deltaTime
            * currentSpeed;
    }

    // Checks the distance between the ant and the waypoint
    private void CheckWaypointDistance()
    {
        if (Vector2.Distance(antGameObject.transform.position, targetWaypoint.transform.position) < walkingWaypointDistance)
        {
            FindNextWayPoint();
        }
    }

    // Finds the next Waypoint in the path
    private void FindNextWayPoint()
    {
        FlowManager.main.RemoveAntFromRoad(targetWaypoint.GetComponent<Waypoint>(), previousWaypoint.GetComponent<Waypoint>());
        // If this is the last waypoint, find next task
        if (targetWaypoint == waypointPath[waypointPath.Count - 1])
        {
            FindNextWayPointTask();
            return;
        }

        // Assign target to the next waypoint
        currentWaypoint++;
        previousWaypoint = targetWaypoint;
        targetWaypoint = waypointPath[currentWaypoint];

        if (targetWaypoint.GetComponent<Waypoint>() == null)
        {
            return;
        }

        FlowManager.main.AddAntToRoad(targetWaypoint.GetComponent<Waypoint>(), previousWaypoint.GetComponent<Waypoint>());
        CallCheckSpeed();

        if (targetWaypoint.GetComponent<Waypoint>().CurrentLevel() == WaypointManager.Level.ABOVE_GROUND)
        {
            ChangeAntLevel(AntManager.SceneView.ABOVE_GROUND);
            if (GameManager.main.currentView == GameManager.CurrentView.SURFACE)
            {
                antSpriteRenderer.enabled = true;
            }
            else
            {
                antSpriteRenderer.enabled = false;
            }
        }
        else
        {
            ChangeAntLevel(AntManager.SceneView.UNDER_GROUND);
            if (GameManager.main.currentView == GameManager.CurrentView.UNDER_GROUND)
            {
                antSpriteRenderer.enabled = true;
            }
            else
            {
                antSpriteRenderer.enabled = false;
            }
        }
    }

    // Finds the waypoint which we need to go to for it's task
    private void FindNextWayPointTask()
    {
        switch (antType)
        {
            case AntType.FORAGER:
                gameObject.GetComponent<ForagerAnt>().DecideNextMove();
                break;
            case AntType.GARDENER:
                gameObject.GetComponent<GardenerAnt>().DecideNextMove();
                break;
            case AntType.TRASH_HANDLER:
                gameObject.GetComponent<TrashHandlerAnt>().DecideNextMove();
                break;
        }
    }

    private void CallCheckSpeed()
    {
        if (checkAntSpeed != null)
        {
            StopCoroutine(checkAntSpeed);
        }

        checkAntSpeed = StartCoroutine(CheckSpeedAntShouldGo());
    }

    #endregion

    #region Coroutines

    // Waits the specified seconds to kill an ant
    private IEnumerator waitToKillAnt()
    {
        yield return new WaitForSeconds(lifeSeconds);
        Die();
    }

    private IEnumerator CheckSpeedAntShouldGo()
    {
        yield return new WaitForSeconds(1.5f);

        if (previousWaypoint == null)
        {
            currentSpeed = AntManager.main.DefaultAntSpeed();
            yield break;
        }

        Waypoint w1 = targetWaypoint.GetComponent<Waypoint>();
        Waypoint w2 = previousWaypoint.GetComponent<Waypoint>();

        if (w1 == null)
        {
            yield break;
        }
        if (w2 == null)
        {
            yield break;
        }

        currentSpeed = FlowManager.main.ReturnAntSpeed(w1, w2);
        checkAntSpeed = null;
        CallCheckSpeed();
    }

    #endregion
}
