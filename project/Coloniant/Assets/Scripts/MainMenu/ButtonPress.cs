using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class ButtonPress : MonoBehaviour {

	public bool isStart;

	public bool isOption; 

	public bool isQuit; 

	public bool isOptionE; 

	public GameObject start; 

	public GameObject options; 

	public void Start(){
		start.SetActive(true);
		options.SetActive(false); 
	}

	public void PlayPressed(){
		SceneManager.LoadScene("Main",LoadSceneMode.Single);
	}

	public void OptionsPressed()
	{
		start.SetActive(false);
		options.SetActive(true); 	
	}

	public void ExitPrssed()
	{
		Application.Quit();  
	}

	public void OptionsExotPressed()
	{
		start.SetActive(true);
		options.SetActive(false);
	}	

} 




