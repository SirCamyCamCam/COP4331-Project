// --------------------------------------------------------------
// Coloniant - Fade Transition                          4/03/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour {

    #region Inspector Fields

    [Header("Dependencies")]
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Image backgroundImage;
    [Header("Settings")]
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private float waitToFadeInTime;

    #endregion

    #region Monohehaviors

    // Use this for initialization
    void Start () {
		if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            backgroundImage.enabled = false;
            backgroundImage.color = Color.clear;
            if (loadingText != null)
            {
                loadingText.enabled = false;
            }
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            StartCoroutine(FadeOutRoutine());
        }
	}

    #endregion

    #region Public Methods

    // Call to fade in
    public void FadeIn(string newScene)
    {
        StartCoroutine(FadeInRoutine(newScene));
    }

    #endregion

    #region Coroutines

    private IEnumerator FadeInRoutine(string newScene)
    {
        float startTime = Time.time;
        backgroundImage.enabled = true;
        Color originalPanelColor = backgroundImage.color;
        while (Time.time - startTime <= fadeTime)
        {
            backgroundImage.color = Color.Lerp(originalPanelColor, Color.black, (Time.time - startTime) / fadeTime);
            yield return 0;
        }

        backgroundImage.color = Color.black;
        SceneManager.LoadScene(newScene);
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(waitToFadeInTime);
        float startTime = Time.time;
        Color originalTextColor = loadingText.color;
        Color originalPanelColor = backgroundImage.color;
        while (Time.time - startTime <= fadeTime)
        {
            loadingText.color = Color.Lerp(originalTextColor, Color.clear, (Time.time - startTime) / fadeTime);
            backgroundImage.color = Color.Lerp(originalPanelColor, Color.clear, (Time.time - startTime) / fadeTime);
            yield return 0;
        }

        loadingText.color = Color.clear;
        backgroundImage.color = Color.clear;
        loadingText.enabled = false;
        backgroundImage.enabled = false;
    }

    #endregion
}
