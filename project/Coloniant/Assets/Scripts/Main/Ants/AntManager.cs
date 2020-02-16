// --------------------------------------------------------------
// Coloniant - Ant Manager                              2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntManager : MonoBehaviour{

    #region Static Fields

    public static AntManager main;

    #endregion

    #region Inspector Fields



    #endregion

    #region Run-Time Fields

    private int soliderAntCount = 0;
    private int queenAntCount = 0;
    private int trashHandlersAntCount = 0;
    private int excavatorsAntCount = 0;
    private int foragerAntCount = 0;
    private int gardenerAntCount = 0;

#endregion

    #region Public Methods

    // Adds to solider count
    public void AddToSoldierCount()
    {
        soliderAntCount++;
    }

    // Adds to queen count
    public void AddToQueenCount()
    {
        queenAntCount++;
    }

    // Adds to trash count
    public void AddToTrashHandlerCount()
    {
        trashHandlersAntCount++;
    }

    // Adds to Excavator Count
    public void AddToExcavatorCount()
    {
        excavatorsAntCount++;
    }

    // Adds to forager count
    public void AddToForagerCount()
    {
        foragerAntCount++;
    }

    // Adds to gardener count
    public void AddToGardenerCount()
    {
        gardenerAntCount++;
    }

    // Removes from solider count
    public void RemoveFromSoldierCount()
    {
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
    public void RemoveFromQueenCount()
    {
        if(queenAntCount > 0)
        {
            queenAntCount--;
        }
        else
        {
            queenAntCount = 0;
        }
    }

    // Removes from trash handeler count
    public void RemoveFromTrashHandlerCount()
    {
        if(trashHandlersAntCount > 0)
        {
            trashHandlersAntCount--;
        }
        else
        {
            trashHandlersAntCount = 0;
        }
    }

    // Removes from excavator count
    public void RemoveFromExcavatorCount()
    {
        if(excavatorsAntCount > 0)
        {
            excavatorsAntCount--;
        }
        else
        {
            excavatorsAntCount = 0;
        }
    }

    // Removes from forager count
    public void RemoveFromForagerCount()
    {
        if(foragerAntCount > 0)
        {
            foragerAntCount--;
        }
        else
        {
            foragerAntCount = 0;
        }
    }

    // Removes from gardener count
    public void RemoveFromGardenerCount()
    {
        if(gardenerAntCount > 0)
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

    #endregion

    #region Private Methods


    #endregion
}
