// --------------------------------------------------------------
// Coloniant - Ant Manager                              2/16/2020
// Author(s): Cameron Carstens
// Contact: cameroncarstens@knights.ucf.edu
// --------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {

    #region Enum

    private enum ShowingPanel
    {
        FOOD,
        PROTECTION,
        ANTS,
        TRASH,
        NONE
    };

    #endregion

    #region Static

    public static MainUIManager main;

    #endregion

    #region Inspector Fields

    [Header("Food")]
    [SerializeField]
    private Text foodProducionTextNumber;
    [SerializeField]
    private Text foodConsumptionTextNumber;
    [SerializeField]
    private Slider foodRatioSlider;
    [SerializeField]
    private Transform foodDropDownButton;
    [SerializeField]
    private GameObject foodPanel;

    [Header("Protection")]
    [SerializeField]
    private Text protectionSoliderTextNumber;
    [SerializeField]
    private Text protectionTotalTextNumber;
    [SerializeField]
    private Slider protectionRatioSlider;
    [SerializeField]
    private Transform protectionDropDownButton;
    [SerializeField]
    private GameObject protectionPanel;

    [Header("Trash")]
    [SerializeField]
    private Text trashSpaceTextNumber;
    [SerializeField]
    private Text trashTotalTextNumber;
    [SerializeField]
    private Slider trashRatioSlider;
    [SerializeField]
    private Transform trashDropDownButton;
    [SerializeField]
    private GameObject trashPanel;

    [Header("Flow")]
    public Slider flowSlider;

    [Header("Ants")]
    [SerializeField]
    private Text totalAntsTextNumber;
    [SerializeField]
    private Text totalSoldierTextNumber;
    [SerializeField]
    private Text totalForagerTextNumber;
    [SerializeField]
    private Text totalGardenersTextNumber;
    [SerializeField]
    private Text totalTrashHandlersTextNumber;
    [SerializeField]
    private Text totalExcavatorsTextNumber;
    [SerializeField]
    private Text totalQueensTextNumber;
    [SerializeField]
    private Transform totalAntsDropDownButton;
    [SerializeField]
    private GameObject totalAntPanel;

    #endregion

    #region Run-Time Fields

    private ShowingPanel currentPanel;

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
    }

    // Use this for initialization
    void Start ()
    {
        currentPanel = ShowingPanel.NONE;

        totalAntPanel.SetActive(false);
        trashPanel.SetActive(false);
        protectionPanel.SetActive(false);
        foodPanel.SetActive(false);

        foodDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        protectionDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        trashDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        totalAntsDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentPanel)
        {
            case ShowingPanel.ANTS:
                UpdateAntPanel();
                break;
            case ShowingPanel.FOOD:
                UpdateFoodPanel();
                break;
            case ShowingPanel.PROTECTION:
                UpdateProtectionPanel();
                break;
            case ShowingPanel.TRASH:
                UpdateTrashPanel();
                break;
            default:
                currentPanel = ShowingPanel.NONE;
                break;
        }
	}

    #endregion

    #region Public Methods

    // Drop down button function for food
    public void FoodButton()
    {
        if (currentPanel == ShowingPanel.FOOD)
        {
            currentPanel = ShowingPanel.NONE;
            foodPanel.SetActive(false);
            foodDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            OnlyOnePanelAllowedToShow();
            currentPanel = ShowingPanel.FOOD;
            foodPanel.SetActive(true);
            foodDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    // Drop down button function for trash
    public void TrashButton()
    {
        if (currentPanel == ShowingPanel.TRASH)
        {
            currentPanel = ShowingPanel.NONE;
            trashPanel.SetActive(false);
            trashDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            OnlyOnePanelAllowedToShow();
            currentPanel = ShowingPanel.TRASH;
            trashPanel.SetActive(true);
            trashDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    // Drop down button function for ants
    public void AntsButton()
    {
        if (currentPanel == ShowingPanel.ANTS)
        {
            currentPanel = ShowingPanel.NONE;
            totalAntPanel.SetActive(false);
            totalAntsDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            OnlyOnePanelAllowedToShow();
            currentPanel = ShowingPanel.ANTS;
            totalAntPanel.SetActive(true);
            totalAntsDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    // Drop down button function for protection
    public void ProtectionButton()
    {
        if (currentPanel == ShowingPanel.PROTECTION)
        {
            currentPanel = ShowingPanel.NONE;
            protectionPanel.SetActive(false);
            protectionDropDownButton.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            OnlyOnePanelAllowedToShow();
            currentPanel = ShowingPanel.PROTECTION;
            protectionPanel.SetActive(true);
            protectionDropDownButton.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    #endregion

    #region Private Methods

    // Disables other panels if we open another one for performance sake
    private void OnlyOnePanelAllowedToShow()
    {
        if (currentPanel != ShowingPanel.NONE)
        {
            switch (currentPanel)
            {
                case ShowingPanel.ANTS:
                    AntsButton();
                    currentPanel = ShowingPanel.NONE;
                    break;
                case ShowingPanel.FOOD:
                    FoodButton();
                    currentPanel = ShowingPanel.NONE;
                    break;
                case ShowingPanel.PROTECTION:
                    ProtectionButton();
                    currentPanel = ShowingPanel.NONE;
                    break;
                case ShowingPanel.TRASH:
                    TrashButton();
                    currentPanel = ShowingPanel.NONE;
                    break;
                default:
                    currentPanel = ShowingPanel.NONE;
                    break;
            }
        }
    }

    // Updates all the values on the food panel when needed
    private void UpdateFoodPanel()
    {
        // To Do
    }

    // Updates all the values on the ant panel when needed
    private void UpdateAntPanel()
    {
        // To Do
    }

    // Updates all the values on the protection panel when needed
    private void UpdateProtectionPanel()
    {
        // To Do
    }

    // Updates all the values on the trash panel when needed
    private void UpdateTrashPanel()
    {
        // To Do
    }

    #endregion
}
