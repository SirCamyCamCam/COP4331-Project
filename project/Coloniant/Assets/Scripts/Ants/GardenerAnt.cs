// --------------------------------------------------------------
// Coloniant - GardenerAnt                              2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenerAnt : MonoBehaviour {

    #region InspectorFields

    [SerializeField]
    private Ant ant;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.GARDENER;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    #endregion
}
