
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingWaypointButton : MonoBehaviour {
	#region Inspector Fields

    [SerializeField]
   
   public Waypoint waypointlocation; 
    #endregion

    #region Monobehaviors
private bool check;			
	public bool OnbuttonPress()
	{	
		check = true; 
		return true; 
		
	}
	
void Update()
{
	if(Input.GetKeyDown(KeyCode.Mouse0) && check == true)
		{
			
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition,Camera.MonoOrStereoscopicEye.Mono);
			//Vector3 setz = new Vector3(worldPoint.x,worldPoint.y,worldPoint.z);

			List<Waypoint> newConnectWaypoint = new List<Waypoint>();
			
			newConnectWaypoint.Add(waypointlocation); 
			WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.FARM_SITE,WaypointManager.Level.ABOVE_GROUND,newConnectWaypoint,worldPoint); 
			
			check = false; 
		}
}
	#endregion
}
