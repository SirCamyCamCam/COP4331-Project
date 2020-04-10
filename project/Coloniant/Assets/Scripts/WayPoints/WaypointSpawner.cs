//By: Amin Kavehzadeh 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaypointSpawner : MonoBehaviour {

    [SerializeField] private WaypointManager manager;
    
    [SerializeField] private OBJFinder finder; 
    
    List<Waypoint> transitionwaypoint = new List<Waypoint>(); 
    public GameObject text; 

    private bool farmT = false; 
    private bool trashT = false; 
    private bool nurseT = false; 

    //waypoint stage flags
    private bool pickconnected = false; 
    private bool pickspawnlocation = false;  

    

    public void FarmT() {farmT = true;text.SetActive(true);}  
    public void TrashT(){ trashT = true;text.SetActive(true);}
    public void NurseT(){ nurseT = true;text.SetActive(true);}
    public void pickconnectedF(){ pickconnected = false;pickspawnlocation = true;}



 // Update is called once per frame
 void Update () {
     
     if(Input.GetKeyDown(KeyCode.Mouse0))
     {
         Vector3 worldview = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
         Vector3 adjust = new Vector3(worldview.x,worldview.y,-0.068f); 
         bool tooClose = false; 

        //for farming Waypoints 
        if (farmT == true )
        {   
             pickconnected = true; 
             
             if (pickconnected == true){Transitionadder();}   
        
                if(pickspawnlocation == true)
                {
                    foreach(Waypoint way in manager.allWaypoints)
                    {
                        if(Vector3.Distance(way.transform.position,adjust)<10)
                        {
                            tooClose = true; 
                        }
                    }

                    if(tooClose == false)
                    {
                            print("im about to spawn"); 
                            manager.SpawnWaypoint(WaypointManager.WaypointType.FARM_SITE,WaypointManager.Level.UNDER_GROUND,transitionwaypoint,adjust); 
                            transitionwaypoint.Clear(); 
                            pickspawnlocation = false;
                            text.SetActive(false);
                            farmT = false; 
                    }else{print("Maybe Try another Spot?");tooClose = false;}
                   
                    
                }
            
        }

        if (trashT == true )
        {
         pickconnected = true; 
             
             if (pickconnected == true){Transitionadder();}   
        
                if(pickspawnlocation == true)
                {
                    foreach(Waypoint way in manager.allWaypoints)
                    {
                        if(Vector3.Distance(way.transform.position,adjust)<10)
                        {
                            tooClose = true; 
                        }
                    }

                    if(tooClose == false)
                    {
                    manager.SpawnWaypoint(WaypointManager.WaypointType.TRASH_SITE,WaypointManager.Level.UNDER_GROUND,transitionwaypoint,adjust); 
                    transitionwaypoint.Clear(); 
                    pickspawnlocation = false;
                    text.SetActive(false);
                    trashT = false; 
                    }else{print("Maybe Try another Spot?");tooClose = false;}
                   
                    
                }
        
        }

        

        if (nurseT == true )
        {
        
            pickconnected = true; 
             
             if (pickconnected == true){Transitionadder();}   
        
                if(pickspawnlocation == true)
                {
                    foreach(Waypoint way in manager.allWaypoints)
                    {
                        if(Vector3.Distance(way.transform.position,adjust)<10)
                        {
                            tooClose = true; 
                        }
                    }

                    if(tooClose == false)
                    {
                    manager.SpawnWaypoint(WaypointManager.WaypointType.NURSERY_SITE,WaypointManager.Level.UNDER_GROUND,transitionwaypoint,adjust); 
                    transitionwaypoint.Clear(); 
                    pickspawnlocation = false;
                    text.SetActive(false);
                    nurseT = false; 
                    }else{print("Maybe Try another Spot?");tooClose = false;}
                   
                    
                }
        
        }

     }
     
    
        
}

public List<Waypoint> Transitionadder()
{
     Waypoint reference = new Waypoint(); 


    if(finder.Getgrab() != null)
    {
        reference = finder.Getgrab().GetComponent<Waypoint>(); 
        transitionwaypoint.Add(reference); 
        print("added!");
        return transitionwaypoint; 
    }
    else
    {
        print("finder.Getgrab() == null");
        return null; 
    }
    

}


}
