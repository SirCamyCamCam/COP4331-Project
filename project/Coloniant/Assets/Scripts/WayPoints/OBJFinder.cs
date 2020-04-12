using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJFinder : MonoBehaviour {

    [SerializeField] RaycastHit hit; 
    [SerializeField] Ray ray;
    [SerializeField] Waypoint grab = null;
    
    
    
    // Update is called once per frame
    void Update () {
           
                    
        
    }

    public Waypoint Getgrab()
    {

         if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    if(hit.transform != null)
                    {
                        grab = hit.transform.gameObject.GetComponent<Waypoint>();
                    
                    }
                
                }
            }


         
        if(grab != null)
        {
            Waypoint temp = grab;
            grab = null; 
            return temp; 
        }
        else
        {
            return null; 
        }
    }
    

    
}
