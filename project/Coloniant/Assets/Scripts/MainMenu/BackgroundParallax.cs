// --------------------------------------------------------------
// Coloniant - Background Parallax                      4/12/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    #region Inspector Fields

    [SerializeField]
    private Vector2 movement;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private Transform backgroundTransform;

    #endregion

    #region Run-Time Fields

    private Vector2 originalPosition;

    #endregion

    #region Monobehaviors

    // Use this for initialization
    void Start () {
        originalPosition = backgroundTransform.transform.position;
        StartCoroutine(MoveBackground());
	}

    #endregion

    #region Private Methods

    private void CallMove()
    {
        StartCoroutine(MoveBackground());
    }

    #endregion

    #region Coroutine

    private IEnumerator MoveBackground()
    {
        backgroundTransform.position = originalPosition;
        float startTime = Time.time;
        Vector2 targetPos = new Vector2(originalPosition.x + movement.x, originalPosition.y + movement.y);

        while (Time.time - startTime <= moveTime)
        {
            backgroundTransform.transform.position = Vector2.Lerp(
                originalPosition,
                targetPos,
                (Time.time - startTime) / moveTime
                );

            yield return 1;
        }

        CallMove();
    }

    #endregion
}
