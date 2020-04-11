// --------------------------------------------------------------
// Coloniant - Ant Manager                              2/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntManager : MonoBehaviour{

    #region Enum

    public enum SceneView
    {
        UNDER_GROUND,
        ABOVE_GROUND
    }

    #endregion

    #region Static Fields

    public static AntManager main;

    #endregion

    #region Inspector Fields

    [Header("Ant Settings")]
    public float spawnRate;
    [SerializeField]
    private float defaultAntSpeed;
    [SerializeField]
    private float defaultIdleNoise;
    [SerializeField]
    private float defaultRotationSpeed;
    [SerializeField]
    private float defaultIdleDistance;
    [SerializeField]
    private float defaultWalkingNoise;
    [SerializeField]
    private float defaultWalkingWaypointDistance;

    [Header("Ant Consumption Settings")]
    [SerializeField]
    private float queenAntConsumptionRate;
    [SerializeField]
    private float foragerAntConsumptionRate;
    [SerializeField]
    private float trashHandlerConsumptionRate;
    [SerializeField]
    private float soldierConsumptionRate;
    [SerializeField]
    private float gardenerConsumptionRate;
    [SerializeField]
    private float excavatorConsumptionRate;
    [SerializeField]
    private float defaultFoodProduction;
    [SerializeField]
    private float consumptionWaitTime;

    [Header("Dependencies")]
    [SerializeField]
    private GameObject queenPrefab;

    #endregion

    #region Run-Time Fields

    private int soliderAntCount = 0;
    private int queenAntCount = 0;
    private int trashHandlersAntCount = 0;
    private int excavatorsAntCount = 0;
    private int foragerAntCount = 0;
    private int gardenerAntCount = 0;
    private List<Ant> antList;
    [HideInInspector]
    public SceneView currentView;
    private List<Ant> queenAnts;
    private List<Ant> soliderAnts;
    private List<Ant> gardenerAnts;
    private List<Ant> excavatorAnts;
    private List<Ant> trashHandlerAnts;
    private List<Ant> foragerAnts;
    // Food
    private float currentFood;
    private float currentConsumption;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }

        antList = new List<Ant>();
        queenAnts = new List<Ant>();
        soliderAnts = new List<Ant>();
        gardenerAnts = new List<Ant>();
        excavatorAnts = new List<Ant>();
        trashHandlerAnts = new List<Ant>();
        foragerAnts = new List<Ant>();
    }

    private void Start()
    {
        currentFood = defaultFoodProduction;
        GameObject nursery = WaypointManager.main.ReturnNurseryGameObject();
        GameObject newAnt = Instantiate(queenPrefab, nursery.transform.position, new Quaternion(0, 0, 0, 0), transform);
        queenAnts.Add(newAnt.GetComponent<Ant>());
        queenAnts[0].GetComponent<QueenAnt>().SetNursery(nursery);
        queenAnts[0].gameObject.GetComponent<Ant>().AssignTargetWaypoint(nursery);
        AddAntsToSpawn(Ant.AntType.EXCAVATOR);
        AddAntsToSpawn(Ant.AntType.FORAGER);
        AddAntsToSpawn(Ant.AntType.GARDENER);
        AddAntsToSpawn(Ant.AntType.SOLDIER);
        AddAntsToSpawn(Ant.AntType.SOLDIER);
        AddAntsToSpawn(Ant.AntType.TRASH_HANDLER);
        UpdateFoodStatus();
    }

    #endregion

    #region Public Methods

    // Return the default ant speed
    public float DefaultAntSpeed()
    {
        return defaultAntSpeed;
    }

    // Return the default ant idle noice
    public float DefaultAntIdleNoise()
    {
        return defaultIdleNoise;
    }

    // Returns the default rotation speed
    public float DefaultRotationSpeed()
    {
        return defaultRotationSpeed;
    }
    
    // Returns the default idle distance
    public float DefaultIdleDistance()
    {
        return defaultIdleDistance;
    }

    // Returns the default walking noise
    public float DefaultWalkingNoise()
    {
        return defaultWalkingNoise;
    }

    // Returns the default walking waypoint distance to switch waypoints
    public float DefaultWalkingWaypointDistance()
    {
        return defaultWalkingWaypointDistance;
    }

    // Changes the current view
    public void SwitchLevelView(SceneView view)
    {
        currentView = view;
        foreach (Ant a in antList)
        {
            a.ChangeView(view);
        }
    }

    // Adds to solider count
    public void AddToSoldierCount(Ant antToAdd)
    {
        if (antToAdd == null)
        {
            Debug.LogError("Attempted to add a null solider ant!");
            return;
        }
        soliderAntCount++;
        antList.Add(antToAdd);
    }

    // Adds to queen count
    public void AddToQueenCount(Ant antToAdd)
    {
        if (antToAdd == null)
        {
            Debug.LogError("Attempted to add a null queen ant!");
            return;
        }
        queenAntCount++;
        antList.Add(antToAdd);
    }

    // Adds to trash count
    public void AddToTrashHandlerCount(Ant antToAdd)
    {
        if (antToAdd == null)
        {
            Debug.LogError("Attempted to add a null trash handler ant!");
            return;
        }
        trashHandlersAntCount++;
        antList.Add(antToAdd);
    }

    // Adds to Excavator Count
    public void AddToExcavatorCount(Ant antToAdd)
    {
        if (antToAdd == null)
        {
            Debug.LogError("Attempted to add a null excavator ant!");
            return;
        }
        excavatorsAntCount++;
        antList.Add(antToAdd);
    }

    // Adds to forager count
    public void AddToForagerCount(Ant antToAdd)
    {
        if (antToAdd == null)
        {
            Debug.LogError("Attempted to add a null forager ant!");
            return;
        }
        foragerAntCount++;
        antList.Add(antToAdd);
    }

    // Adds to gardener count
    public void AddToGardenerCount(Ant antToAdd)
    {
        if (antToAdd == null)
        {
            Debug.LogError("Attempted to add a null gardener ant!");
            return;
        }
        gardenerAntCount++;
        antList.Add(antToAdd);
    }

    // Removes from solider count
    public void RemoveFromSoldierCount(Ant antToRemove)
    {
        if (antToRemove == null)
        {
            Debug.LogError("Attemped to remove a null solder ant!");
            return;
        }
        antList.Remove(antToRemove);
        if (soliderAntCount > 0)
        {
            soliderAntCount--;
        }
        else
        {
            soliderAntCount = 0;
        }
    }

    // Removes from queen count
    public void RemoveFromQueenCount(Ant antToRemove)
    {
        if (antToRemove == null)
        {
            Debug.LogError("Attemped to remove a null queen ant!");
            return;
        }
        if (queenAntCount > 0)
        {
            queenAntCount--;
        }
        else
        {
            queenAntCount = 0;
        }
    }

    // Removes from trash handeler count
    public void RemoveFromTrashHandlerCount(Ant antToRemove)
    {
        if (antToRemove == null)
        {
            Debug.LogError("Attemped to remove a null trash handeler ant!");
            return;
        }
        if (trashHandlersAntCount > 0)
        {
            trashHandlersAntCount--;
        }
        else
        {
            trashHandlersAntCount = 0;
        }
    }

    // Removes from excavator count
    public void RemoveFromExcavatorCount(Ant antToRemove)
    {
        if (antToRemove == null)
        {
            Debug.LogError("Attemped to remove a null excavator ant!");
            return;
        }
        if (excavatorsAntCount > 0)
        {
            excavatorsAntCount--;
        }
        else
        {
            excavatorsAntCount = 0;
        }
    }

    // Removes from forager count
    public void RemoveFromForagerCount(Ant antToRemove)
    {
        if (antToRemove == null)
        {
            Debug.LogError("Attemped to remove a null forager ant!");
            return;
        }
        if (foragerAntCount > 0)
        {
            foragerAntCount--;
        }
        else
        {
            foragerAntCount = 0;
        }
    }

    // Removes from gardener count
    public void RemoveFromGardenerCount(Ant antToRemove)
    {
        if (antToRemove == null)
        {
            Debug.LogError("Attemped to remove a null gardener ant!");
            return;
        }
        if (gardenerAntCount > 0)
        {
            gardenerAntCount--;
        }
        else
        {
            gardenerAntCount = 0;
        }
    }

    // Returns Solider count
    public int GetSoliderCount()
    {
        return soliderAntCount;
    }

    // Returns Queen count
    public int GetQueenCount()
    {
        return queenAntCount;
    }

    // Returns gardener count
    public int GetGardenerCount()
    {
        return gardenerAntCount;
    }

    // Returns excavator count
    public int GetExcavatorCount()
    {
        return excavatorsAntCount;
    }

    // Returns forager count
    public int GetForagerCount()
    {
        return foragerAntCount;
    }
    
    // Returns Trash Handler count
    public int GetTrashHandlerCount()
    {
        return trashHandlersAntCount;
    }

    // Returns the total ant count
    public int GetTotalAntCount()
    {
        return trashHandlersAntCount + foragerAntCount + gardenerAntCount + excavatorsAntCount + queenAntCount + soliderAntCount;
    }

    // Adds an Ant to the spawn counter
    public void AddAntsToSpawn(Ant.AntType type)
    {
        int randomQueen = Random.Range(0, queenAntCount - 1);

        switch(type)
        {
            case Ant.AntType.EXCAVATOR:
                queenAnts[randomQueen].GetComponent<QueenAnt>().AddAntToSpawn(QueenAnt.Ants.EXCAVATOR, 1);
                break;
            case Ant.AntType.FORAGER:
                queenAnts[randomQueen].GetComponent<QueenAnt>().AddAntToSpawn(QueenAnt.Ants.FORAGER, 1);
                break;
            case Ant.AntType.GARDENER:
                queenAnts[randomQueen].GetComponent<QueenAnt>().AddAntToSpawn(QueenAnt.Ants.GARDENER, 1);
                break;
            case Ant.AntType.TRASH_HANDLER:
                queenAnts[randomQueen].GetComponent<QueenAnt>().AddAntToSpawn(QueenAnt.Ants.TRASH_HANDLER, 1);
                break;
            case Ant.AntType.QUEEN:
                queenAnts[randomQueen].GetComponent<QueenAnt>().AddAntToSpawn(QueenAnt.Ants.QUEEN, 1);
                break;
            case Ant.AntType.SOLDIER:
                queenAnts[randomQueen].GetComponent<QueenAnt>().AddAntToSpawn(QueenAnt.Ants.SOLIDER, 1);
                break;
        }
    }

    // Increases the current food supply
    public void AddToFood(float ammount)
    {
        if (ammount < 0)
        {
            return;
        }
        currentFood += ammount;
    }

    // Decreases the current food supply
    public void RemoveFromFood(float ammount)
    {
        if (ammount < 0)
        {
            return;
        }
        currentFood -= ammount;
    }

    // Returns the current food
    public float ReturnCurrentFoodProduction()
    {
        return currentFood;
    }

    // Returns the current food consumption
    public float ReturnCurrentFoodConsumption()
    {
        return currentConsumption;
    }

    public void RemoveAntFromList(Ant ant)
    {
        if (ant == null)
        {
            Debug.Log("Ant null in RemoveAntFromList in AntManager");
        }

        antList.Remove(ant);  

        switch (ant.antType)
        {
            case Ant.AntType.EXCAVATOR:
                excavatorAnts.Remove(ant);
                RemoveFromExcavatorCount(ant);
                break;
            case Ant.AntType.FORAGER:
                foragerAnts.Remove(ant);
                RemoveFromForagerCount(ant);
                break;
            case Ant.AntType.GARDENER:
                gardenerAnts.Remove(ant);
                RemoveFromGardenerCount(ant);
                break;
            case Ant.AntType.QUEEN:
                queenAnts.Remove(ant);
                RemoveFromQueenCount(ant);
                break;
            case Ant.AntType.SOLDIER:
                soliderAnts.Remove(ant);
                RemoveFromSoldierCount(ant);
                break;
            case Ant.AntType.TRASH_HANDLER:
                trashHandlerAnts.Remove(ant);
                RemoveFromTrashHandlerCount(ant);
                break;
        }
    }

    public void KillPercenatgeAnt(float percent)
    {
        int numToKill = (int)(GetTotalAntCount() * percent);

        for (int i = 0; i < numToKill; i++)
        {
            StarveRandomAnt();
        }
    }

    #endregion

    #region Private Methods

    private void UpdateFoodStatus()
    {
        float tempConsumption = 0;
        // Tally up consumption
        foreach (Ant a in antList)
        {
            switch (a.antType)
            {
                case Ant.AntType.EXCAVATOR:
                    tempConsumption += excavatorConsumptionRate;
                    break;
                case Ant.AntType.FORAGER:
                    tempConsumption += foragerAntConsumptionRate;
                    break;
                case Ant.AntType.GARDENER:
                    tempConsumption += gardenerConsumptionRate;
                    break;
                case Ant.AntType.QUEEN:
                    tempConsumption += queenAntConsumptionRate;
                    break;
                case Ant.AntType.SOLDIER:
                    tempConsumption += soldierConsumptionRate;
                    break;
                case Ant.AntType.TRASH_HANDLER:
                    tempConsumption += trashHandlerConsumptionRate;
                    break;
                default:
                    Debug.Log("No type found for tempConsumption tally!");
                    break;
            }
        }
        currentConsumption = tempConsumption;

        // Kill any ants if we're over the limit
        if (currentConsumption > currentFood)
        {
            StarveRandomAnt();
        }

        StartCoroutine(WaitToCountConsumption());
    }

    // Kills a random ant(queens excluded)
    private void StarveRandomAnt()
    {
        int randomNum = Random.Range(0, antList.Count - 1);

        // Don't kill 1 queen so we can always spawn more ants
        if (antList[randomNum].antType == Ant.AntType.QUEEN && GetQueenCount() == 1)
        {
            if (GetTotalAntCount() == 1)
            {
                return;
            }
            StarveRandomAnt();
        }
        else
        {
            antList[randomNum].Die();
        }
    }

    #endregion

    #region Coroutine

    private IEnumerator WaitToCountConsumption()
    {
        yield return new WaitForSeconds(consumptionWaitTime);
        UpdateFoodStatus();
    }

    #endregion
}
