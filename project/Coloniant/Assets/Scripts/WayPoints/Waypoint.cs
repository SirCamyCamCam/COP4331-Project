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
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Runt-Time Fields

    private WaypointManager.WaypointType type;
    [HideInInspector]
    public List<Waypoint> connectedWaypoints;
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
        switch (type)
        {
            case WaypointManager.WaypointType.FARM_SITE:
                spriteRenderer.color = Color.green;
                break;
            case WaypointManager.WaypointType.ENTRANCE:
                spriteRenderer.color = Color.black;
                break;
            case WaypointManager.WaypointType.LEAF_SITE:
                spriteRenderer.color = Color.cyan;
                break;
            case WaypointManager.WaypointType.NURSERY_SITE:
                spriteRenderer.color = Color.red;
                break;
            case WaypointManager.WaypointType.TRANSITION:
                spriteRenderer.color = Color.gray;
                break;
            case WaypointManager.WaypointType.TRASH:
                spriteRenderer.color = Color.yellow;
                break;
            case WaypointManager.WaypointType.TRASH_SITE:
                spriteRenderer.color = Color.blue;
                break;
            case WaypointManager.WaypointType.EXIT:
                spriteRenderer.color = Color.black;
                break;
            default:
                Debug.LogError("No type found for waypoint!");
                break;
        }

        if (GameManager.main.currentView == GameManager.CurrentView.SURFACE && level == WaypointManager.Level.UNDER_GROUND)
        {
            spriteRenderer.enabled = false;
        }
        else if (GameManager.main.currentView == GameManager.CurrentView.UNDER_GROUND && level == WaypointManager.Level.ABOVE_GROUND)
        {
            spriteRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    /*void Update () {
		
	}*/

    #endregion

    #region Public methods

    // Sets the types for the waypoint and connected waypoints
    public void SetUpWaypointTypes(WaypointManager.WaypointType typeToSet, WaypointManager.Level levelToSet)
    {
        type = typeToSet;
        level = levelToSet;
    }

    // Adds a new waypoint
    public void AddAConnectedWaypoint(Waypoint newWaypoint)
    {
        connectedWaypoints.Add(newWaypoint);
    }

    // Removes a waypoint from connected
    public void RemoveAConnectedWaypoint(Waypoint removedWaypoint)
    {
        connectedWaypoints.Remove(removedWaypoint);
    }

    // Returns the current level
    public WaypointManager.Level CurrentLevel()
    {
        return level;
    }

    // Switches the current level
    public void SwitchLevel(WaypointManager.Level changeLevel)
    {
        if (level == WaypointManager.Level.ABOVE_GROUND && changeLevel == WaypointManager.Level.ABOVE_GROUND)
        {
            spriteRenderer.enabled = true;
        }
        else if (level == WaypointManager.Level.ABOVE_GROUND && changeLevel == WaypointManager.Level.UNDER_GROUND)
        {
            spriteRenderer.enabled = false;
        }
        else if (level == WaypointManager.Level.UNDER_GROUND && changeLevel == WaypointManager.Level.UNDER_GROUND)
        {
            spriteRenderer.enabled = true;
        }
        else if (level == WaypointManager.Level.UNDER_GROUND && changeLevel == WaypointManager.Level.ABOVE_GROUND)
        {
            spriteRenderer.enabled = false;
        }
    }

    // Returns the type
    public WaypointManager.WaypointType ReturnWaypointType()
    {
        return type;
    }

    // Returns the gameobject
    public GameObject ReturnWaypointGameObject()
    {
        return attachedGameObject;
    }

    #endregion

    #region Private Methods

    #endregion
}