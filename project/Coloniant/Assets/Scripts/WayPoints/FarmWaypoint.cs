//By: Amin Kavehzadeh 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FarmWaypoint : MonoBehaviour {

	private WaypointManager manager;
	public GameObject farm;

	
	List<Waypoint> transitionwaypoint = new List<Waypoint>(); 

	private bool farmT = false; 
	private bool trashT = false; 
	private bool nurseT = false; 


	public void FarmT() { farmT = true;}  
	public void TrashT(){ trashT = true;}
	public void NurseT(){ nurseT = true;}


	
	public void Spawn(Vector3 pos)
	{
		Instantiate(farm).transform.position = pos; 
	}

 void Start () {
}
 
 // Update is called once per frame
 void Update () {
     
	 if(Input.GetKeyDown(KeyCode.Mouse0))
	 {
		 Vector3 worldview = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
	 	 Vector3 adjust = new Vector3(worldview.x,worldview.y,farm.transform.position.z); 

		if (farmT == true )
	 	{
			 
			 manager.SpawnWaypoint(WaypointManager.WaypointType.FARM_SITE,WaypointManager.Level.UNDER_GROUND,transitionwaypoint,adjust); 
		

			farmT = false; 
	 	}

		if (trashT == true )
	 	{
		
		Spawn(adjust); 
		trashT = false; 
		
	 	}
		if (nurseT == true )
	 	{
		
		Spawn(adjust); 
		nurseT = false; 
		
	 	}

	 }
	 

	     
}




}