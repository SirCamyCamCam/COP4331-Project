// --------------------------------------------------------------
// Coloniant - LeafManager                              3/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafManager : MonoBehaviour {

    #region Enum

    public enum State
    {
        DEFAULT,
        TRASH,
        FUNGUS
    }

    #endregion

    #region Inspector Fields

    [Header("Settings")]
    [SerializeField]
    private float leafLife;
    [SerializeField]
    private float decayRate;

    #endregion

    #region Run-Time Fields

    private List<Leaf> leafList;

    #endregion

    #region Monobehaviors

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #endregion
}
