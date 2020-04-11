// --------------------------------------------------------------
// Coloniant - Flow Manager                             4/11/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour {

    #region Static Fields

    public static FlowManager main;

    #endregion

    #region Inner Classes

    private class Road
    {
        private Waypoint waypoint1;
        private Waypoint waypoint2;
        private LineRenderer lineRenderer;
        private int maxAnts;
        private int currentAnts;
        private int trash;
        private float antSpeed;

        public Road(Waypoint waypoint1, Waypoint waypoint2, LineRenderer lineRenderer, int maxAnts, float antSpeed)
        {
            this.waypoint1 = waypoint1;
            this.waypoint2 = waypoint2;
            this.maxAnts = maxAnts;
            this.lineRenderer = lineRenderer;
            this.antSpeed = antSpeed;
            trash = 0;
            currentAnts = 0;
        }

        public void AddToMaxAnts(int num)
        {
            if (num < 0)
            {
                return;
            }
            maxAnts += num;
        }
        
        public void RemoveFromMaxAnts(int num)
        {
            if (num < 0)
            {
                return;
            }
            maxAnts -= num;
        }

        public void AddAnAnt()
        {
            currentAnts += 1;
        }

        public void RemoveAnAnt()
        {
            currentAnts -= 1;
        }

        public void AddToTrash()
        {
            trash += 1;
        }

        public void RemoveFromTrash()
        {
            trash -= 1;
        }

        public List<Waypoint> ReturnWaypointList()
        {
            List<Waypoint> l = new List<Waypoint>();
            l.Add(waypoint1);
            l.Add(waypoint2);
            return l;
        }

        public Waypoint ReturnWaypoint1()
        {
            return waypoint1;
        }

        public Waypoint ReturnWaypoint2()
        {
            return waypoint2;
        }

        public LineRenderer ReturnLinerenderer()
        {
            return lineRenderer;
        }

        public float ReturnAntSpeed()
        {
            return antSpeed;
        }

        public int ReturnMaxAnts()
        {
            return maxAnts;
        }

        public int ReturnCurrentAnts()
        {
            return currentAnts;
        }

        public int ReturnTrashCount()
        {
            return trash;
        }

        public void AssignAntSpeed(float speed)
        {
            antSpeed = speed;
        }
    }

    #endregion

    #region Inspector Fields

    [Header("Settings")]
    [SerializeField]
    private int defaultMaxAnts;
    [SerializeField]
    private int upgradeAnts;
    [SerializeField]
    private float trashWeight;

    #endregion

    #region Run-Time Fields

    private List<Road> roadList;
    private Dictionary<Waypoint, List<Road>> waypointKeys;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;
    }

    // Use this for initialization
    void Start ()
    {
        roadList = new List<Road>();
        waypointKeys = new Dictionary<Waypoint, List<Road>>();
	}

    private void Update()
    {
        UpdateFlow();
    }

    #endregion

    #region Public Methods

    public void NewWaypoint(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Waypoint null in NewWaypoint in FlowManager");
            return;
        }
        List<Road> newList = new List<Road>();
        waypointKeys.Add(w, newList);
    }

    public void NewRoad(Waypoint w1, Waypoint w2, LineRenderer l)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint 1 null in NewRoad in FlowManager");
            return;
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint 2 null in NewRoad in FlowManager");
            return;
        }

        Road newRoad = new Road(w1, w2, l, defaultMaxAnts, AntManager.main.DefaultAntSpeed());

        roadList.Add(newRoad);

        if (waypointKeys.ContainsKey(w1) == false)
        {
            NewWaypoint(w1);
        }
        if (waypointKeys.ContainsKey(w2) == false)
        {
            NewWaypoint(w2);
        }

        waypointKeys[w1].Add(newRoad);
        waypointKeys[w2].Add(newRoad);
    }

    public void AddAntToRoad(Waypoint w1, Waypoint w2)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint1 null in AddAntToRoad in FlowManager");
            return;
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint2 null in AddAntToRoad in FlowManager");
            return;
        }

        List<Road> roadListToCheck = waypointKeys[w1];
        Road roadToUse = null;

        foreach (Road r in roadListToCheck)
        {
            if (r.ReturnWaypoint1() == w2)
            {
                roadToUse = r;
                break;
            }
            else if (r.ReturnWaypoint2() == w2)
            {
                roadToUse = r;
                break;
            }
        }

        if (roadToUse == null)
        {
            Debug.Log("No road found in AddAntToRoad in FlowManager");
            return;
        }

        roadToUse.AddAnAnt();
    }

    public void RemoveAntFromRoad(Waypoint w1, Waypoint w2)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint1 null in RemoveAntFromRoad in FlowManager");
            return;
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint2 null in RemoveAntFromRoad in FlowManager");
            return;
        }

        List<Road> roadListToCheck = waypointKeys[w1];
        Road roadToUse = null;

        foreach (Road r in roadListToCheck)
        {
            if (r.ReturnWaypoint1() == w2)
            {
                roadToUse = r;
                break;
            }
            else if (r.ReturnWaypoint2() == w2)
            {
                roadToUse = r;
                break;
            }
        }

        if (roadToUse == null)
        {
            Debug.Log("No road found in RemoveAntFromRoad in FlowManager");
            return;
        }

        roadToUse.RemoveAnAnt();
    }

    public void AddTrashToRoad(Waypoint w1, Waypoint w2)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint1 null in AddTrashToRoad in FlowManager");
            return;
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint2 null in AddTrashToRoad in FlowManager");
            return;
        }

        List<Road> roadListToCheck = waypointKeys[w1];
        Road roadToUse = null;

        foreach (Road r in roadListToCheck)
        {
            if (r.ReturnWaypoint1() == w2)
            {
                roadToUse = r;
                break;
            }
            else if (r.ReturnWaypoint2() == w2)
            {
                roadToUse = r;
                break;
            }
        }

        if (roadToUse == null)
        {
            Debug.Log("No road found in AddTrashToRoad in FlowManager");
            return;
        }

        roadToUse.AddToTrash();
    }

    public void RemoveTrashFromRoad(Waypoint w1, Waypoint w2)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint1 null in RemoveTrashFromRoad in FlowManager");
            return;
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint2 null in RemoveTrashFromRoad in FlowManager");
            return;
        }

        List<Road> roadListToCheck = waypointKeys[w1];
        Road roadToUse = null;

        foreach (Road r in roadListToCheck)
        {
            if (r.ReturnWaypoint1() == w2)
            {
                roadToUse = r;
                break;
            }
            else if (r.ReturnWaypoint2() == w2)
            {
                roadToUse = r;
                break;
            }
        }

        if (roadToUse == null)
        {
            Debug.Log("No road found in RemoveTrashFromRoad in FlowManager");
            return;
        }

        roadToUse.RemoveFromTrash();
    }

    public float ReturnTotalFlow()
    {
        int totalCapacity = 0;
        int totalAnts = 0;
        foreach (Road r in roadList)
        {
            totalAnts += r.ReturnCurrentAnts();
            totalCapacity += r.ReturnMaxAnts();
        }

        float flow = totalAnts / totalCapacity;

        if (flow > 1)
        {
            flow = 1;
        }

        return flow;
    }

    public float ReturnAntSpeed(Waypoint w1, Waypoint w2)
    {
        if (w1 == null)
        {
            Debug.Log("Waypoint1 null in ReturnAntSpeed in FlowManager");
            return AntManager.main.DefaultAntSpeed();
        }
        if (w2 == null)
        {
            Debug.Log("Waypoint2 null in ReturnAntSpeed in FlowManager");
            return AntManager.main.DefaultAntSpeed();
        }

        List<Road> roadListToCheck = waypointKeys[w1];
        Road roadToUse = null;

        foreach (Road r in roadListToCheck)
        {
            if (r.ReturnWaypoint1() == w2)
            {
                roadToUse = r;
                break;
            }
            else if (r.ReturnWaypoint2() == w2)
            {
                roadToUse = r;
                break;
            }
        }

        if (roadToUse == null)
        {
            Debug.Log("No road found in RemoveTrashFromRoad in FlowManager");
            return AntManager.main.DefaultAntSpeed();
        }

        return roadToUse.ReturnAntSpeed();
    }

    #endregion

    #region Private Methods

    private void UpdateFlow()
    {
        foreach (Road r in roadList)
        {
            float flow = (float)r.ReturnCurrentAnts() / (float)r.ReturnMaxAnts();
            flow += r.ReturnTrashCount() * trashWeight;

            if (flow > 1)
            {
                flow = 1;
            }

            Color c = new Color(flow, 1 - flow, 0, 1);
            r.ReturnLinerenderer().startColor = c;
            r.ReturnLinerenderer().endColor = c;

            float speed = 1.0f - flow;
            if (speed < 0.1f)
            {
                speed = 0.1f;
            }

            r.AssignAntSpeed(AntManager.main.DefaultAntSpeed() * speed);
        }
    }

    #endregion
}
