// --------------------------------------------------------------
// Coloniant - Ant                                      2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {

    #region enum

    private enum AntState
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
    private GameObject[] waypointPath;
    private int currentWaypoint;
    private float currentSpeed;
    private int foodConsumptionRate;
    // Idle Actions
    private float xDirection;
    private float yDirection;
    private float idleNoise;
    private float rotationSpeed;
    private float idleDistance;
    private bool isReturningToWaypoint;

    #endregion

    #region Monobehaviors

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
        antState = AntState.IDLE;
        StartCoroutine(waitToKillAnt());
        xDirection = 500000;
        yDirection = 500000;
        currentSpeed = AntManager.main.DefaultAntSpeed();
        idleNoise = AntManager.main.DefaultAntIdleNoise();
        rotationSpeed = AntManager.main.DefaultRotationSpeed();
        idleDistance = AntManager.main.DefaultIdleDistance();
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
	}

    #endregion

    #region Public Methods

    // Used to assign the current target
    public void AssignTargetWaypoint(GameObject target)
    {
        targetWaypoint = target;
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

    #endregion

    #region Private Methods

    // Called when the Ant is idle
    private void IdleAnt()
    {
        if (Vector2.Distance(antGameObject.transform.position, targetWaypoint.transform.position) > idleDistance && isReturningToWaypoint == false)
        {
            isReturningToWaypoint = true;
        }
        else if (Vector2.Distance(antGameObject.transform.position, targetWaypoint.transform.position) < idleDistance - 1 && isReturningToWaypoint == true)
        {
            isReturningToWaypoint = false;
        }

        xDirection += 1 * Random.value;
        yDirection += 1 * Random.value;

        if (xDirection == int.MaxValue || yDirection == int.MaxValue)
        {
            xDirection = 0;
            yDirection = 0;
        }

        if (isReturningToWaypoint == false)
        {
            float randomVal = Mathf.PerlinNoise(
                xDirection * idleNoise,
                yDirection * idleNoise);
            float angle = Mathf.Lerp(-10, 10, randomVal);
            antGameObject.transform.eulerAngles = new Vector3(0,0, antGameObject.transform.eulerAngles.z + angle);
        }
        else
        {
            Vector3 direction = (targetWaypoint.transform.position - antGameObject.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.back);
            antGameObject.transform.rotation = Quaternion.Slerp(antGameObject.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            antGameObject.transform.eulerAngles = new Vector3(0, 0, antGameObject.transform.eulerAngles.z);

        }
        antGameObject.transform.position +=
            antGameObject.transform.up
            * Time.deltaTime
            * currentSpeed;
    }

    // Kills the ant
    private void Die()
    {
        Destroy(gameObject);
    }

    // Finds the next Waypoint in the path
    private void FindNextWayPoint()
    {

    }

    // Finds the waypoint which we need to go to for it's task
    private void FindNextWayPointTask()
    {

    }

    #endregion

    #region Coroutines

    // Waits the specified seconds to kill an ant
    private IEnumerator waitToKillAnt()
    {
        yield return new WaitForSeconds(lifeSeconds);
        Die();
    }

    #endregion
}
