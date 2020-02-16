// --------------------------------------------------------------
// Coloniant - Ant Manager                              2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {

    #region Inspector Fields


    #endregion

    #region Run-Time fields

    private int lifeSeconds;

    #endregion

    #region Monobehaviors

    // Use this for initialization
    void Start () {
        StartCoroutine(waitToKillAnt());
	}

    // No update for effeciency
    // Update is called once per frame
    /*void Update () {
		
	}*/

    #endregion

    #region Public Methods

    

    #endregion

    #region Private Methods

    // Kills the ant
    private void Die()
    {

    }

    #endregion

    #region Coroutines

    private IEnumerator waitToKillAnt()
    {
        yield return new WaitForSeconds(lifeSeconds);
        Die();
    }

    #endregion
}
