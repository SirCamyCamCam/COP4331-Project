// --------------------------------------------------------------
// Coloniant - LeafManager                              3/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafManager : MonoBehaviour {

    #region Static Fields

    public static LeafManager main;

    #endregion

    #region Enum

    public enum State
    {
        TRANSPORT,
        TRASH,
        FUNGUS,
        LEAF,
        WAIT
    }

    #endregion

    #region Inspector Fields

    [Header("Settings")]
    [SerializeField]
    private int leafLife;
    [SerializeField]
    private float decayRate;
    [SerializeField]
    private float decayMultiplyer;
    [SerializeField]
    private int defaultSpawnNum;
    [SerializeField]
    private float leafWeight;
    [SerializeField]
    private int farmSiteLeafCapacity;

    [Header("Dependencies")]
    [SerializeField]
    private GameObject leafPrefab;

    #endregion

    #region Run-Time Fields

    private List<Leaf> leafList;
    private List<Leaf> selectedLeaves;
    private Dictionary<Waypoint, List<Leaf>> leavesAtLeafSites;
    private Dictionary<Waypoint, List<Leaf>> leavesAtFarmSites;
    private Dictionary<Leaf, Waypoint> leavesAtFarmWaypoint;
    private Dictionary<Waypoint, int> farmSiteCapacities;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;

        leavesAtLeafSites = new Dictionary<Waypoint, List<Leaf>>();
        leavesAtFarmSites = new Dictionary<Waypoint, List<Leaf>>();
        leavesAtFarmWaypoint = new Dictionary<Leaf, Waypoint>();
        farmSiteCapacities = new Dictionary<Waypoint, int>();
        leafList = new List<Leaf>();
        selectedLeaves = new List<Leaf>();
    }

    #endregion

    #region Public Methods

    public Leaf CollectedALeaf(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint given to 'CollectedALeaf' in LeafManager");
            return null;
        }

        if (leavesAtLeafSites[w].Count == 0)
        {
            return null;
        }

        Leaf leaf = leavesAtLeafSites[w][0];
        leaf.SetLeafState(State.TRANSPORT);
        leavesAtLeafSites[w].Remove(leaf);
        return leaf;
    }

    public void AddLeafToFarm(Waypoint w, Leaf l)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint in 'AddLeafToFarm' in LeafManager");
        }

        if (l == null)
        {
            Debug.Log("Null Leaf in 'AddLeafToFarm' in LeafManager");
        }

        l.SetLeafState(State.WAIT);
        l.StartDecay();
        leavesAtFarmSites[w].Add(l);
        leavesAtFarmWaypoint.Add(l, w);
        farmSiteCapacities[w] += 1;
    }

    public void NewFarmWaypoint(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint in 'newFarmWaypoint' in LeafManager");
        }

        List<Leaf> newList = new List<Leaf>();
        leavesAtFarmSites[w] = newList;
        farmSiteCapacities.Add(w, 0);
    }

    public void NewLeafSite(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint in 'NewLeafSite' in LeafManager");
        }

        List<Leaf> newList = new List<Leaf>();
        for (int i = 0; i < defaultSpawnNum; i++)
        {
            Leaf newLeaf = new GameObject().AddComponent<Leaf>();
            newLeaf.transform.parent = transform;
            newLeaf.gameObject.name = "Leaf";
            newLeaf.SetLeafState(State.LEAF);
            newLeaf.SetDecayRate(decayRate);
            newLeaf.SetDecayMultiplyer(decayMultiplyer);
            newLeaf.SetLeafLife(leafLife);
            newList.Add(newLeaf);
            leafList.Add(newLeaf);
        }
        leavesAtLeafSites[w] = newList;
    }

    public List<Leaf> ReturnLeavesAtFarm(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint in 'ReturnLeavesAtFarm' in LeafManager");
        }

        return leavesAtFarmSites[w];
    }

    public List<Leaf> ReturnLeavesAtLeafSite(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint in 'ReturnLeavesAtLeafSite' in LeafManager");
        }

        return leavesAtLeafSites[w];
    }

    public void AssignSelectedLeaf(Leaf l)
    {
        if (l == null)
        {
            Debug.Log("Null Leaf in 'AssignSelectedLeaf' in LeafManager");
            return;
        }

        selectedLeaves.Add(l);
    }

    public void RemoveSelectedLeaf(Leaf l)
    {
        if (l == null)
        {
            Debug.Log("Null Leaf in 'RemoveSelectedLeaf' in LeafManager");
            return;
        }
        selectedLeaves.Remove(l);
    }

    public bool IsLeafSelected(Leaf l)
    {
        if (l == null)
        {
            Debug.Log("Null Leaf in 'IsLeafSelected' in LeafManager");
            return false;
        }
        return selectedLeaves.Contains(l);
    }

    public Waypoint ReturnWaypointLeafIsAt(Leaf leaf)
    {
        if (leaf == null)
        {
            Debug.Log("Null leaf in ReturnWaypointLeafIsAt in LeafManager");
            return null;
        }
        
        return leavesAtFarmWaypoint[leaf];
    }

    public void LeafDeath(Leaf l, Waypoint w)
    {
        if (l == null)
        {
            Debug.Log("Leaf is null at LeafDeath in LeafManager");
            return;
        }

        if (w == null)
        {
            Debug.Log("Waypoint is null at LeafDeath in LeafManager");
            return;
        }


        leavesAtFarmSites[w].Remove(l);
        leavesAtFarmWaypoint.Remove(l);
        AntManager.main.RemoveFromFood(leafWeight);
        farmSiteCapacities[w] -= 1;
    }

    public float ReturnLeafWeight()
    {
        return leafWeight;
    }

    public bool CheckFarmSiteFull(Waypoint w)
    {
        if (farmSiteCapacities[w] == farmSiteLeafCapacity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Waypoint FindFarmSite()
    {
        List<Waypoint> farmList = WaypointManager.main.ReturnFarmList();
        int[] check = new int[farmList.Count];
        bool sitesNotCheck = true;
        bool findAnotherRandom = true;
        int random = 0;
        Waypoint returnWaypoint = null;

        while (returnWaypoint == null)
        {
            sitesNotCheck = false;
            foreach (int i in check)
            {
                if (i == 0)
                {
                    sitesNotCheck = true;
                }
            }

            if (sitesNotCheck == false)
            {
                return null;
            }

            findAnotherRandom = true;
            while (findAnotherRandom == true)
            {
                random = Random.Range(0, farmList.Count - 1);
                if (check[random] == 0)
                {
                    findAnotherRandom = false;
                }
            }
            check[random] = 1;

            if (CheckFarmSiteFull(farmList[random]) == false)
            {
                return farmList[random];
            }
        }

        return null;
    }

    #endregion
}
