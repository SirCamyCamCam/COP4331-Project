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

    public enum AntLevel
    {
        UNDER_GROUND,
        ABOVE_GROUND
    };

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

    #endregion

    #region Run-Time fields

    private AntState antState;
    private AntLevel antLevel;
    [HideInInspector]
    public AntType antType;
    private GameObject previousWaypoint;
    private GameObject nextWaypoint;
    private GameObject targetWaypoint;
    private GameObject[] waypointPath;
    private int currentWaypoint;
    private int currentSpeed;
    private int foodConsumptionRate;

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

        antLevel = AntLevel.UNDER_GROUND;
        ChangeView(AntManager.main.currentView);
        antState = AntState.IDLE;
        StartCoroutine(waitToKillAnt());
	}

    // No update for effeciency
    // Update is called once per frame
    /*void Update () {
		
	}*/

    #endregion

    #region Public Methods

    // Switches between above ground a below ground
    public void ChangeView(AntManager.SceneView view)
    {
        if (view == AntManager.SceneView.ABOVE_GROUND && antLevel == AntLevel.UNDER_GROUND)
        {
            antSpriteRenderer.enabled = false;
        }
        else if (view == AntManager.SceneView.ABOVE_GROUND && antLevel == AntLevel.ABOVE_GROUND)
        {
            antSpriteRenderer.enabled = true;
        }
        else if (view == AntManager.SceneView.UNDER_GROUND && antLevel == AntLevel.UNDER_GROUND)
        {
            antSpriteRenderer.enabled = true;
        }
        else if (view == AntManager.SceneView.UNDER_GROUND && antLevel == AntLevel.ABOVE_GROUND)
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
