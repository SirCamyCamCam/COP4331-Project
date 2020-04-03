// --------------------------------------------------------------
// Coloniant - Trash                                    3/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    #region Inspector-Fields

    [Header("Dependencies")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Run-Time Fields

    private TrashManager.TrashState trashState;

    #endregion

    #region Monobehaviors

    // Use this for initialization
    void Start () {
        trashState = TrashManager.TrashState.WAITING;
	}

    #endregion

    #region Public Methods

    public void MarkAsPickedUp()
    {
        trashState = TrashManager.TrashState.TRANSPORT;
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
        spriteRenderer.enabled = false;
    }

    #endregion
}
