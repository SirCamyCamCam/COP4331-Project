// --------------------------------------------------------------
// Coloniant - TrashManager                             3/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour {

    #region Static Fields

    public static TrashManager main;

    #endregion

    #region Enum

    public enum TrashState
    {
        WAITING,
        TRANSPORT
    }

    #endregion

    #region Inspector Fields

    [Header("Dependencies")]
    [SerializeField]
    private GameObject trashPrefab;
    [Header("Settings")]
    [SerializeField]
    private float trashWeight;
    [SerializeField]
    private float trashDeleteTime;

    #endregion

    #region Run-Time Fields

    private List<Trash> trashList;
    private List<Trash> selectedTrash;
    private List<Trash> waitingTrash;
    private List<Waypoint> trashSiteList;
    private Dictionary<Waypoint, float> trashCapacities;
    private Dictionary<Trash, Waypoint> trashWaitingWaypoints;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        main = this;

        trashList = new List<Trash>();
        selectedTrash = new List<Trash>();
        waitingTrash = new List<Trash>();
        trashSiteList = new List<Waypoint>();
        trashCapacities = new Dictionary<Waypoint, float>();
        trashWaitingWaypoints = new Dictionary<Trash, Waypoint>();
    }

    #endregion

    #region Public Methods

    // Marks a piece of trash as selected
    public void MarkAsSelected(Trash trash)
    {
        if (trash == null)
        {
            Debug.Log("Null trash in MarkAsSelected in TrashManager");
            return;
        }
        selectedTrash.Add(trash);
        waitingTrash.Remove(trash);
        trashWaitingWaypoints.Remove(trash);
    }

    // Returns whether a trash has been already selected
    public bool CheckIfTrashSelected(Trash trash)
    {
        if (trash == null)
        {
            Debug.Log("Invalid trash in CheckIfTrashSelected in TrashManager");
            return false;
        }
        return selectedTrash.Contains(trash);
    }

    // Adds trash to the site it is being dropped off at
    public void AddTrashToSite(Waypoint site)
    {
        if (site == null)
        {
            Debug.Log("Trash Waypoint Site does not exist in AddTrashToSite in TrashManager");
            return;
        }
        if (trashCapacities.ContainsKey(site) == false)
        {
            Debug.Log("Invalid trash site waypoint in AddTrashToSite in TrashManager");
            return;
        }

        trashCapacities[site] += trashWeight;
        StartCoroutine(DeleteTrash(site));
    }

    // Creates a new trash site with 0 filled capacity
    public void AddTrashWaypoint(Waypoint site)
    {
        if (site == null)
        {
            Debug.Log("Null site in AddTrashWaypoint in TrashManager");
            return;
        }

        trashSiteList.Add(site);
        trashCapacities.Add(site, 0);
    }

    // Returns whether a trash site has space
    public bool CheckTrashSiteFull(Waypoint site)
    {
        if (site == null)
        {
            Debug.Log("Null site in CheckTrashSiteFull in TrashManager");
            return false;
        }

        if (trashCapacities[site] < 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Adds a trash to the list of trash
    public void CreatedNewTrash(Trash trash)
    {
        if (trash == null)
        {
            Debug.Log("Trash null in CreatedNewTrash in TrashManager");
            return;
        }

        trashList.Add(trash);
        waitingTrash.Add(trash);
    }

    // Removes trash from all lists
    public void RemoveTrashFromAll(Trash trash)
    {
        if (trash == null)
        {
            Debug.Log("Trash null in RemoveTrash in TrashManager");
            return;
        }

        if (selectedTrash.Contains(trash))
        {
            selectedTrash.Remove(trash);
        }
        if (waitingTrash.Contains(trash))
        {
            waitingTrash.Remove(trash);
        }

        trashList.Remove(trash);
    }

    // Marks the nearest waypoint at which trash was dropped
    public void AddTrashToWaypoints(Trash trash, Waypoint waypoint)
    {
        if (trash == null)
        {
            Debug.Log("Trash null in AddTrashToWaypoints in TrashManager");
            return;
        }

        if (waypoint == null)
        {
            Debug.Log("Waypoint null in AddTrashToWaypoints in TrashManager");
            return;
        }

        trashWaitingWaypoints.Add(trash, waypoint);
    }

    // Returns the waypoint to which the ant must go before the trash
    public Waypoint ReturnTrashWaypoint(Trash trash)
    {
        if (trash == null)
        {
            Debug.Log("Trash is null in ReturnTrashWaypoint in TrashManager");
            return null;
        }

        return trashWaitingWaypoints[trash];
    }

    // Returns a piece of trash to pick up
    public Trash FindTrashToPickUp()
    {
        if (waitingTrash.Count == 0)
        {
            return null;
        }

        foreach (Trash t in waitingTrash)
        {
            if (selectedTrash.Contains(t) == false)
            {
                return t;
            }
        }

        return null;
    }

    // Returns a site to drop off the trash
    public Waypoint FindDropOffSite()
    {
        int[] check = new int[trashCapacities.Count];
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
                random = Random.Range(0, trashCapacities.Count - 1);
                if (check[random] == 0)
                {
                    findAnotherRandom = false;
                }
            }
            check[random] = 1;

            if (CheckTrashSiteFull(trashSiteList[random]) == false)
            {
                return trashSiteList[random];
            }
        }

        return null;
    }

    // Returns the trash prefab
    public GameObject TrashPrefab()
    {
        return trashPrefab;
    }

    public void SwitchLayers()
    {
        foreach (Trash t in trashList)
        {
            t.SwitchLayer();
        }
    }

    public float ReturnTotalTrashCapacity()
    {
        return trashSiteList.Count;
    }

    public float ReturnTotalCurrentTrash()
    {
        float count = 0;

        foreach(Waypoint w in trashSiteList)
        {
            count += trashCapacities[w];
        }

        count += (float)trashList.Count * trashWeight;

        return count;
    }

    #endregion

    #region Private Methods



    #endregion

    #region Coroutines

    private IEnumerator DeleteTrash(Waypoint w)
    {
        yield return new WaitForSeconds(trashDeleteTime);

        trashCapacities[w] -= trashWeight;
    }

    #endregion
}
