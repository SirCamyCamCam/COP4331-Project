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

    #endregion

    #region Private Methods


    #endregion
}
