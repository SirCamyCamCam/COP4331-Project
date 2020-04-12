//By: Amin Kavehzadeh
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PopUps : MonoBehaviour {
	public AntManager antmanager;
	public GameObject Panel; 
	public GameObject foodPanel; 
	public GameObject raidPanel; 
	public GameObject Exit; 
	public Text AvgConsumption;
	public Text numSoldiers;  



	private static float consumption = 0.00f; 
	private static float currfood= 0.00f;
	private static float currsoldiers = 0.00f;  
	private static float AVGf = 0.00f; 
	private bool Exitf= false; 
	private bool Startf = false; 





	// Use this for initialization
	void start () {
	}

	// Update is called once per frame
	void Update () {

		consumption = AntManager.main.ReturnCurrentFoodConsumption(); 
		currfood = AntManager.main.ReturnCurrentFoodProduction(); 
		currsoldiers = AntManager.main.GetSoliderCount(); 
		numSoldiers.text = currsoldiers.ToString();

		
		if(AntManager.main.GetTotalAntCount() == 7){Startf = true;}
		//food Popup
		if(consumption >= currfood)
		{
			Exit.SetActive(false);
			Panel.SetActive(true); 
			foodPanel.SetActive(true); 
			AVGf = (currfood/consumption); 
			AvgConsumption.text = AVGf.ToString("F2");
			Exitf = true;  

		}
		//Protection Popup
		if(ProtectionManager.main.ReturnCurrentPercentage() <= 0 && Startf == true)
		{
			Exit.SetActive(false);
			Panel.SetActive(true); 
			raidPanel.SetActive(true); 
			
			Exitf = true; 
		}




		//closing the Popup window
		if((AVGf >=0.90f && Exitf == true) || (ProtectionManager.main.ReturnCurrentPercentage()> 0 && Exitf == true))
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