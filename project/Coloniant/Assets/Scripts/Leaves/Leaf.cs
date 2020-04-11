// --------------------------------------------------------------
// Coloniant - Leaf                                     3/29/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour {

    #region Run-Time Fields

    private float decay;
    private float decayRate;
    private float decayMultiplyer;
    private int leafLife;
    private LeafManager.State leafState;
    private Coroutine decayCoroutine;

    #endregion

    #region Monobehaviors

    // Use this for initialization
    void Start () {
        ResetDecay();
        decayCoroutine = null;
	}

    #endregion

    #region Public Methods

    // Resets the decay
    public void ResetDecay()
    {
        decay = 0;
    }

    public void SetLeafState(LeafManager.State newState)
    {
        leafState = newState;
    }

    public void SetDecayRate(float rate)
    {
        decayRate = rate;
    }

    public void SetDecayMultiplyer(float rate)
    {
        decayMultiplyer = rate;
    }

    public void StartDecay()
    {
        if (decayCoroutine == null)
        {
            decayCoroutine = StartCoroutine(DecayLeaf());
        }
    }

    public void SetLeafLife(int life)
    {
        leafLife = life;
    }

    public void StartLeafLife()
    {
        StartCoroutine(LeafLifeTimer());
    }

    public LeafManager.State ReturnLeafState()
    {
        return leafState;
    }

    public float ReturnDecayLevel()
    {
        return decay;
    }

    #endregion

    #region Private Methods

    private void SetToTrash()
    {
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(-1, 1);

        Vector3 waypointLocation = LeafManager.main.ReturnWaypointLeafIsAt(this).transform.position;

        Vector3 locationToSpawn = new Vector3(
            waypointLocation.x + randomX, 
            waypointLocation.y + randomY, 
            waypointLocation.z
            );

        GameObject trashGameObject = Instantiate(
            TrashManager.main.TrashPrefab(),
            locationToSpawn,
            new Quaternion(0, 0, 0, 0),
            TrashManager.main.gameObject.transform
            );

        Trash trash = trashGameObject.GetComponent<Trash>();
        trash.SetLayer(Trash.Layer.UNDERGROUND);
        TrashManager.main.CreatedNewTrash(trash);
        TrashManager.main.AddTrashToWaypoints(trash, LeafManager.main.ReturnWaypointLeafIsAt(this));
        LeafManager.main.LeafDeath(this, LeafManager.main.ReturnWaypointLeafIsAt(this));
        Destroy(gameObject);
    }

    #endregion

    #region Coroutines

    private IEnumerator DecayLeaf()
    {
        yield return new WaitForSeconds(decayRate);
        decay += decayMultiplyer;

        if (decay >= 1)
        {
            SetToTrash();
        }

        decayCoroutine = null;
        StartDecay();
    }

    private IEnumerator LeafLifeTimer()
    {
        yield return new WaitForSeconds(leafLife);
        SetToTrash();
    }

    #endregion
}
