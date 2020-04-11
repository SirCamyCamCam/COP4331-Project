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

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;
    }

    // Update is called once per frame
    void Update () {
        CheckPercent();
	}

    #endregion

    #region Public Methods

    public float ReturnCurrentPercentage()
    {
        return (AntManager.main.GetSoliderCount() / AntManager.main.GetTotalAntCount()) / (requieredSoldierPecentage);
    }

    #endregion

    #region Private Methods

    private void CheckPercent()
    {
        if (AntManager.main.GetSoliderCount() / AntManager.main.GetTotalAntCount() > requieredSoldierPecentage)
        {
            AntManager.main.KillPercenatgeAnt(percentThatGetKilled);
        }
    }

    #endregion
}
