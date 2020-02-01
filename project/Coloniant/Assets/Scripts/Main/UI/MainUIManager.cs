using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {

    // Food Stuff
    public Text foodProducionTextNumber;
    public Text foodConsumptionTextNumber;
    public Slider foodRatioSlider;
    public Transform foodDropDownButton;
    public GameObject foodPanel;
    private bool isFoodShowing;

    // Protection Stuff
    public Text protectionSoliderTextNumber;
    public Text protectionTotalTextNumber;
    public Slider protectionRatioSlider;
    public Transform protectionDropDownButton;
    public GameObject protectionPanel;
    private bool isProtectionShowing;

    // Trash Stuff
    public Text trashSpaceTextNumber;
    public Text trashTotalTextNumber;
    public Slider trashRatioSlider;
    public Transform trashDropDownButton;
    public GameObject trashPanel;
    private bool isTrashShowing;

    // Flow
    public Slider flowSlider;

    // Ants
    public Text totalAntsTextNumber;
    public Text totalSoldierTextNumber;
    public Text totalForagerTextNumber;
    public Text totalGardenersTextNumber;
    public Text totalTrashHandlersTextNumber;
    public Text totalExcavatorsTextNumber;
    public Text totalQueensTextNumber;
    public Transform totalAntsDropDownButton;
    public GameObject totalAntPanel;
    private bool isAntsShowing;

	// Use this for initialization
	void Start ()
    {
        totalAntPanel.SetActive(false);
        trashPanel.SetActive(false);
        protectionPanel.SetActive(false);
        foodPanel.SetActive(false);

        isFoodShowing = false;
        isTrashShowing = false;
        isAntsShowing = false;
        isProtectionShowing = false;

        foodDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        protectionDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        trashDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        totalAntsDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isFoodShowing == true)
        {
            // Update text values
        }

        if(isTrashShowing == true)
        {
            // Update text values
        }

        if(isAntsShowing == true)
        {
            // Update Text values
        }

        if(isProtectionShowing == true)
        {
            // Update text values
        }
	}

 #region Public 

    public void FoodButton()
    {
        if (isFoodShowing == false)
        {
            isFoodShowing = true;
            foodPanel.SetActive(true);
            foodDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            isFoodShowing = false;
            foodPanel.SetActive(false);
            foodDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void TrashButton()
    {
        if (isTrashShowing == false)
        {
            isTrashShowing = true;
            trashPanel.SetActive(true);
            trashDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            isTrashShowing = false;
            trashPanel.SetActive(false);
            trashDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void AntsButton()
    {
        if (isAntsShowing == false)
        {
            isAntsShowing = true;
            totalAntPanel.SetActive(true);
            totalAntsDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            isAntsShowing = false;
            totalAntPanel.SetActive(false);
            totalAntsDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void ProtectionButton()
    {
        if (isProtectionShowing == false)
        {
            isProtectionShowing = true;
            protectionPanel.SetActive(true);
            protectionDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            isProtectionShowing = false;
            protectionPanel.SetActive(false);
            protectionDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    #endregion
}
