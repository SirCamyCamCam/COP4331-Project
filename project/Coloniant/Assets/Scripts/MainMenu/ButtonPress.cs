//By: Amin Kavehzadeh
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ButtonPress : MonoBehaviour {

	// Use this for initialization

	public GameObject start;
	public GameObject option;
    [SerializeField]
    private FadeTransition fadeTransitionScript;
	

	void Start () {
		start.SetActive(true);
		option.SetActive(false); 

	}
	
	public void Playpressed(){
        fadeTransitionScript.FadeIn("Main");
	}

	public void OptionPressed(){
		start.SetActive(false);
		option.SetActive(true);
		
	}

	public void OptionsExotPressed(){
		start.SetActive(true);
		option.SetActive(false);
	}

	
	public void ExitPressd(){
		Application.Quit(); 
	}
}
