// --------------------------------------------------------------
// Coloniant - ForagerAnt                               2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForagerAnt : MonoBehaviour {

    #region Inspector Fields

    [SerializeField]
    private Ant ant;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.FORAGER;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    #endregion

    #region Public Methods

    public void PickUpLeaf()
    {

    }

    public void DropOffLeaf()
    {

    }

    #endregion
}
