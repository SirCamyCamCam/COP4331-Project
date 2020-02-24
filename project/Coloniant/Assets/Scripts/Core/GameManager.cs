// --------------------------------------------------------------
// Coloniant - GameManager                              2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region enum

    public enum CurrentView
    {
        SURFACE,
        UNDER_GROUND
    };

    #endregion

    #region Static fields

    public static GameManager main;

    #endregion

    #region Inspector Fields

    [Header("Dependencies")]
    [SerializeField]
    private SpriteRenderer surface;
    [SerializeField]
    private SpriteRenderer ground;

    [Header("Settings")]
    [SerializeField]
    private float flowUpdateSeconds;

#endregion

    #region Run-Time Fields

    [HideInInspector]
    public int masterVolume;
    [HideInInspector]
    public CurrentView currentView;

    #endregion


    #region Monobehaviors

    private void Awake()
    {
        currentView = CurrentView.UNDER_GROUND;
        main = this;
    }

    // Use this for initialization
    void Start () {
        surface.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        SwitchViews();
	}

    #endregion

    #region Public Methods

    // Returns the flow update seconds
    public float FlowUpdateSeconds()
    {
        return flowUpdateSeconds;
    }

    #endregion

    #region Private Methods

    private void SwitchViews()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentView == CurrentView.UNDER_GROUND)
            {
                currentView = CurrentView.SURFACE;
                AntManager.main.SwitchLevelView(AntManager.SceneView.ABOVE_GROUND);
                WaypointManager.main.SwitchWaypointLevel(WaypointManager.Level.ABOVE_GROUND);
                surface.enabled = true;
                ground.enabled = false;
            }
            else
            {
                currentView = CurrentView.UNDER_GROUND;
                AntManager.main.SwitchLevelView(AntManager.SceneView.UNDER_GROUND);
                WaypointManager.main.SwitchWaypointLevel(WaypointManager.Level.UNDER_GROUND);
                surface.enabled = false;
                ground.enabled = true;
            }
        }
    }

#endregion
}
