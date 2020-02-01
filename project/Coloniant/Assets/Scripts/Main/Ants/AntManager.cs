using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AntManager {

    private static int soliderAntCount = 0;
    private static int queenAntCount = 0;
    private static int trashHandlersAntCount = 0;
    private static int excavatorsAntCount = 0;
    private static int foragerAntCount = 0;
    private static int gardenerAntCount = 0;

    #region Add and Remove Ants

    public static void AddToSoldierCount()
    {
        soliderAntCount++;
    }

    public static void AddToQueenCount()
    {
        queenAntCount++;
    }

    public static void AddToTrashHandlerCount()
    {
        trashHandlersAntCount++;
    }

    public static void AddToExcavatorCount()
    {
        excavatorsAntCount++;
    }

    public static void AddToForagerCount()
    {
        foragerAntCount++;
    }

    public static void AddToGardenerCount()
    {
        gardenerAntCount++;
    }

    public static void RemoveFromSoldierCount()
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

    public static void RemoveFromQueenCount()
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

    public static void RemoveFromTrashHandlerCount()
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

    public static void RemoveFromExcavatorCount()
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

    public static void RemoveFromForagerCount()
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

    public static void RemoveFromGardenerCount()
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

    #endregion
}
