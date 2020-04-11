// --------------------------------------------------------------
// Coloniant - Protection Manager                       4/11/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionManager : MonoBehaviour {

    #region Static Fields

    public static ProtectionManager main;

    #endregion

    #region Inspector Fields

    [Header("Settings")]
    [SerializeField]
    private float requieredSoldierPecentage;
    [SerializeField]
    private float percentThatGetKilled;

    #endregion

    #region Run-Time Fields

    private bool allowCheck;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        allowCheck = false;
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update () {
        if (allowCheck == true)
        {
            CheckPercent();
        }
	}

    #endregion

    #region Public Methods

    public float ReturnCurrentPercentage()
    {
        float percent = requieredSoldierPecentage / ((float)AntManager.main.GetSoliderCount() / (float)AntManager.main.GetTotalAntCount());

        if (percent < 0)
        {
            percent = 0;
        }

        if (percent > 1)
        {
            percent = 1;
        }

        percent = 1 - percent;

        return percent;
    }

    #endregion

    #region Private Methods

    private void CheckPercent()
    {
        Debug.Log((float)AntManager.main.GetSoliderCount() / (float)AntManager.main.GetTotalAntCount());
        if ((float)AntManager.main.GetSoliderCount() / (float)AntManager.main.GetTotalAntCount() < requieredSoldierPecentage)
        {
            AntManager.main.KillPercenatgeAnt(percentThatGetKilled);
        }
    }

    #endregion

    #region Corutines

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(1f);
        allowCheck = true;
    }

    #endregion
}
