// --------------------------------------------------------------
// Coloniant - EasterEgg                                3/08/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {

    #region Static Fields

    public static EasterEgg main;

    #endregion

    #region Inspector Fields

    [SerializeField]
    private GameObject[] easterEggGameObjects;

    #endregion

    #region Run-Time Fields

    private bool s1;
    private bool e1;
    private bool n1;
    private bool d1;
    private bool n2;
    private bool u;
    private bool d2;
    private bool e2;
    private bool s2;
    [HideInInspector]
    public bool itSASecret;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        CheckKeyInputs();
	}

    #endregion

    #region Private Methods

    private void CheckKeyInputs()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.S) && s1 == false)
            {
                s1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && s1 == true && e1 == false)
            {
                e1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.N) && e1 == true && n1 == false)
            {
                n1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && n1 == true && d1 == false)
            {
                d1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.N) && d1 == true && n2 == false)
            {
                n2 = true;
            }
            else if (Input.GetKeyDown(KeyCode.U) && n2 == true && u == false)
            {
                u = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && u == true && d2 == false)
            {
                d2 = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && d2 == true && e2 == false)
            {
                e2 = true;
            }
            else if (Input.GetKeyDown(KeyCode.S) && e2 == true && s2 == false)
            {
                s2 = true;
                MakeTheMagicHappen();
                itSASecret = true;
            }
            else
            {
                s1 = false;
                e1 = false;
                n1 = false;
                d1 = false;
                n2 = false;
                u = false;
                d2 = false;
                e2 = false;
                s2 = false;
            }
        }
    }

    private void MakeTheMagicHappen()
    {
        foreach (Ant a in AntManager.main.antList)
        {
            int randomNum = Mathf.RoundToInt(Random.Range(0, easterEggGameObjects.Length - 1));
            if (randomNum < 0)
            {
                randomNum = 0;
            }
            else if (randomNum > easterEggGameObjects.Length - 1)
            {
                randomNum = easterEggGameObjects.Length - 1;
            }
            List<GameObject> list = new List<GameObject>();
            list.Add(easterEggGameObjects[randomNum]);
            a.AssignWaypointList(list);
            a.ChangeAntLevel(AntManager.SceneView.UNDER_GROUND);
            a.ChangeView(AntManager.SceneView.UNDER_GROUND);
            //a.ChangeTurningRadius(f);
            a.ChangeIdleDistance(5f);
            
        }
    }

    #endregion
}
