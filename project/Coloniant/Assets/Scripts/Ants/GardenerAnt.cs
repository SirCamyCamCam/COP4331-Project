// --------------------------------------------------------------
// Coloniant - GardenerAnt                              2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenerAnt : MonoBehaviour {

    #region InspectorFields

    [SerializeField]
    private Ant ant;
    [SerializeField]
    private float idleWaitTime;
    [SerializeField]
    private float decayNeeded;

    #endregion

    #region Run-Time Fields

    private Leaf leafToChange;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        ant.antType = Ant.AntType.GARDENER;
    }

    private void Start()
    {
        StartCoroutine(waitToFindNextTarget());
    }

    #endregion

    #region Public Methods

    public void DecideNextMove()
    {
        if (leafToChange != null && leafToChange.ReturnLeafState() == LeafManager.State.WAIT)
        {
            leafToChange.SetLeafState(LeafManager.State.FUNGUS);
            leafToChange.ResetDecay();
            leafToChange.StartLeafLife();
            LeafManager.main.RemoveSelectedLeaf(leafToChange);
            leafToChange = null;
            AntManager.main.AddToFood(LeafManager.main.ReturnLeafWeight());
        }
        else if (leafToChange != null)
        {
            leafToChange.ResetDecay();
            LeafManager.main.RemoveSelectedLeaf(leafToChange);
            leafToChange = null;
        }

        ant.AssignAntState(Ant.AntState.IDLE);
        StartCoroutine(waitToFindNextTarget());
    }

    #endregion

    #region Private Methods

    private void FindNewTarget()
    {
        int randomFarm = Random.Range(0, WaypointManager.main.ReturnFarmList().Count - 1);

        // First change new leaf if we can
        foreach (Leaf l in LeafManager.main.ReturnLeavesAtFarm(WaypointManager.main.ReturnFarmList()[randomFarm]))
        {
            if (l.ReturnLeafState() == LeafManager.State.WAIT && LeafManager.main.IsLeafSelected(l) == false)
            {
                leafToChange = l;
                LeafManager.main.AssignSelectedLeaf(l);

                List<GameObject> list = WaypointManager.main.SearchPathKnownTarget(
                    ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(),
                    WaypointManager.main.ReturnFarmList()[randomFarm]
                    );

                ant.AssignWaypointList(list);
                return;
            }
        }

        // Second check decays
        foreach (Leaf l in LeafManager.main.ReturnLeavesAtFarm(WaypointManager.main.ReturnFarmList()[randomFarm]))
        {
            if (l.ReturnDecayLevel() >= decayNeeded && LeafManager.main.IsLeafSelected(l) == false)
            {
                leafToChange = l;
                LeafManager.main.AssignSelectedLeaf(l);

                // Set Target Waypoint
                List<GameObject> list = WaypointManager.main.SearchPathKnownTarget(
                    ant.ReturnCurrentWaypoint().GetComponent<Waypoint>(),
                    WaypointManager.main.ReturnFarmList()[randomFarm]
                    );

                ant.AssignWaypointList(list);
                return;
            }
        }

        // Nothing to do, wait
        StartCoroutine(waitToFindNextTarget());
    }

    #endregion

    #region Coroutines

    private IEnumerator waitToFindNextTarget()
    {
        yield return new WaitForSeconds(idleWaitTime);
        FindNewTarget();
    }

    #endregion
}
