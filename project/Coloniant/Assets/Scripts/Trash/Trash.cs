// --------------------------------------------------------------
// Coloniant - Trash                                    3/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    #region Enum

    public enum Layer
    {
        SURFACE,
        UNDERGROUND
    }

    #endregion

    #region Inspector-Fields

    [Header("Dependencies")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    [SerializeField]
    private float targetScale;
    [SerializeField]
    private float bounceHeight;
    [SerializeField]
    private float bounceRate;
    [SerializeField]
    private float scaleRate;

    #endregion

    #region Run-Time Fields

    private TrashManager.TrashState trashState;
    private Waypoint connectedWaypoint1;
    private Waypoint connectedWaypoint2;
    private Layer layer;
    private float originalHeight;
    private bool grow;
    private bool shrink;

    #endregion

    #region Monobehaviors

    // Use this for initialization
    void Start () {
        trashState = TrashManager.TrashState.WAITING;
        originalHeight = transform.position.y;
        transform.localScale = new Vector3(0, 0, 0);

        if (layer == Layer.SURFACE && GameManager.main.currentView == GameManager.CurrentView.UNDER_GROUND)
        {
            spriteRenderer.enabled = false;
        }
        else if (layer == Layer.UNDERGROUND && GameManager.main.currentView == GameManager.CurrentView.SURFACE)
        {
            spriteRenderer.enabled = false;
        }
	}

    private void Update()
    {
        transform.position = new Vector3
            (
            transform.position.x,
            originalHeight + (Mathf.Sin(Time.time * bounceRate) * bounceHeight),
            transform.position.z
            );

        if (transform.localScale.x < targetScale && grow == false)
        {
            transform.localScale = new Vector3
                (
                transform.localScale.x + scaleRate, 
                transform.localScale.y + scaleRate, 
                transform.localScale.z + scaleRate
                );
        }
        else if (transform.localScale.x >= targetScale && grow == false)
        {
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            grow = true;
        }

        if (shrink == true && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3
                (
                transform.localScale.x - scaleRate,
                transform.localScale.y - scaleRate,
                transform.localScale.z - scaleRate
                );
        }
        else if (shrink == true && transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(0, 0, 0);
            shrink = false;
        }
    }

    #endregion

    #region Public Methods

    public void MarkAsPickedUp()
    {
        trashState = TrashManager.TrashState.TRANSPORT;

        if (connectedWaypoint1 != null)
        {
            FlowManager.main.RemoveTrashFromRoad(connectedWaypoint1, connectedWaypoint2);
        }
    }

    public TrashManager.TrashState ReturnTrashState()
    {
        return trashState;
    }

    public void DeleteTrashPrefab()
    {
        Destroy(gameObject);
    }

    public void DisableSprite()
    {
        shrink = true;
    }

    public void SetLayer(Layer layer)
    {
        this.layer = layer;
    }

    public void SwitchLayer()
    {
        if (layer == Layer.SURFACE)
        {
            layer = Layer.UNDERGROUND;
        }
        else
        {
            layer = Layer.SURFACE;
        }

        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    public void AssignConnectedWaypoint(Waypoint w1, Waypoint w2)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint 1 null in AssignConnectedWaypoint in Trash");
            return;
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint 2 null in AssignConnectedWaypoint in Trash");
            return;
        }

        connectedWaypoint1 = w1;
        connectedWaypoint2 = w2;

        FlowManager.main.AddTrashToRoad(connectedWaypoint1, connectedWaypoint2);
    }

    #endregion
}
