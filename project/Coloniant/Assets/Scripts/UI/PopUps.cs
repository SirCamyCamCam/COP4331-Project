//By: Amin Kavehzadeh
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PopUps : MonoBehaviour {
	public AntManager antmanager;
	public GameObject Panel; 
	public GameObject foodPanel; 
	public GameObject Exit; 
	public Text AvgConsumption; 



	private static float consumption = 0.00f; 
	private static float currfood= 0.00f; 
	private static float AVGf = 0.00f; 
	private bool Exitf= false; 





	// Use this for initialization
	void start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		consumption = antmanager.currentConsumption; 
		currfood = antmanager.currentFood; 

		
		if(consumption >= currfood)
		{
			Panel.SetActive(true); 
			foodPanel.SetActive(true); 
			AVGf = (consumption/currfood)-1; 
			AvgConsumption.text = AVGf.ToString();
			Exitf = true;  

		}

		if(consumption <= currfood && Exitf == true)
		{
			Exit.SetActive(true);
			Exitf = false; 
		}
		


	}

	public void closePopup()
	{
		Panel.SetActive(false); 
		foodPanel.SetActive(false); 

	}



}
