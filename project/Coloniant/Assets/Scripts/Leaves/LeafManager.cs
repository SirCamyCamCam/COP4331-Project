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

    [Header("Dependencies")]
    [SerializeField]
    private GameObject leafPrefab;

    #endregion

    #region Run-Time Fields

    private List<Leaf> leafList;
    private List<Leaf> selectedLeaves;
    private Dictionary<Waypoint, List<Leaf>> leavesAtLeafSites;
    private Dictionary<Waypoint, List<Leaf>> leavesAtFarmSites;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;

        leavesAtLeafSites = new Dictionary<Waypoint, List<Leaf>>();
        leavesAtFarmSites = new Dictionary<Waypoint, List<Leaf>>();
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
    }

    public void NewFarmWaypoint(Waypoint w)
    {
        if (w == null)
        {
            Debug.Log("Null Waypoint in 'newFarmWaypoint' in LeafManager");
        }

        List<Leaf> newList = new List<Leaf>();
        leavesAtFarmSites[w] = newList;
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
        }

        selectedLeaves.Add(l);
    }

    public void RemoveSelectedLeaf(Leaf l)
    {
        if (l == null)
        {
            Debug.Log("Null Leaf in 'RemoveSelectedLeaf' in LeafManager");
        }
        selectedLeaves.Remove(l);
    }

    public bool IsLeafSelected(Leaf l)
    {
        if (l == null)
        {
            Debug.Log("Null Leaf in 'IsLeafSelected' in LeafManager");
        }
        return selectedLeaves.Contains(l);
    }

    #endregion
}
