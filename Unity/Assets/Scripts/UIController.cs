using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    public GameState gameState;
    public GameObject statsPanel;
    //public GameObject environmentStatsPanel;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI moneyText;
    public Text notEnoughMoneyText;
    public Text buildUniversityText;
    public Text youWonText;
    public Text gameOverText;
    public Text researchFinishedText;

    public Action OnRoadPlacement, OnHousePlacement, OnClinicPlacement, OnHospitalPlacement, OnSolarPanelPlacement, OnWindTurbinePlacement, OnCarbonPowerPlantPlacement,
        OnNuclearPlantPlacement, OnWaterPlantPlacement, OnHighDensityHousePlacement, OnShopPlacement, OnRestaurantPlacement, OnBarPlacement, OnCinemaPlacement, OnUniversityPlacement,
        OnFireStationPlacement, OnPoliceStationPlacement, OnFactoryPlacement, OnCropPlacement, OnLivestockPlacement, OnLandfillPlacement, OnIncinerationPlantPlacement,
        OnWasteToEnergyPlantPlacement, OnBigParkPlacement, OnShowMenu, OnShowStats, OnShowEnvironmentStats, OnDestroyStructure, OnPublicServiceMenu, OnEnergySourceMenu, OnWaterSourceMenu,
        OnWasteDisposalMenu, OnIndustryMenu, OnDecorationMenu, OnShopMenu, OnHouseMenu, OnShowScientificProgress, OnCloseScientificProgress, OnUpgradeScientificProgress;


    public Button showMenu;
    public Button showStats;
    public Button showEnvironmentStats;

    public Button showScientificProgress;
    public Slider particlePollutionSlider;
    public Text  particlePollutionText;
    public Slider carbonDioxideSlider;
    public Text carbonDioxideText;
   
    public Button closeScientificProgress;

    public Button destroyStructureButton;
    
    public Button placeRoadButton;
    public Button openHouseMenuButton;
    public Button openPublicServiceMenuButton;
    public Button openEnergySourceMenuButton;
    public Button openWaterSourceMenuButton;
    public Button openWasteDisposalMenuButton;
    public Button openIndustryMenuButton;
    public Button openDecorationMenuButton;
    public Button openShopMenuButton;


    public GameObject HousePanel;
    public GameObject PublicServicePanel;
    public GameObject EnergySourcePanel;
    public GameObject WaterSourcePanel;
    public GameObject WasteDisposalPanel;
    public GameObject IndustryPanel;
    public GameObject DecorationPanel;
    public GameObject ShopPanel;




    // Healthcare
    public Button placeClinicButton;
    public Button placeHospitalButton;

    // Energy
    public Button placeSolarPanelButton;
    public Button placeWindTurbineButton;
    public Button placeCarbonPowerPlantButton;
    public Button placeNuclearPlantButton;

    // Water sources
    public Button placeWaterPlantButton;

    // Residential
    public Button placeHouseButton;
    public Button placeHighDensityHouseButton;

    // Commercial
    public Button placeShopButton;
    public Button placeRestaurantButton;
    public Button placeBarButton;
    public Button placeCinemaButton;

    // Education and Emergency Services
    public Button placeUniversityButton;
    public Button placeFireStationButton;
    public Button placePoliceStationButton;

    // Industrial
    public Button placeFactoryButton;
    public Button placeCropButton;
    public Button placeLivestockButton;
    // Decorations
    public Button placeBigParkButton;

    // Waste Management
    public Button placeLandfillButton;
    public Button placeIncinerationPlantButton;

    public Text populationCapacityText;

    public Text employmentText;
    public Text jobsOccupiedText;
    public Text criminalsCoveredText;
    public Text patientsCoveredText;
    public Text energyRatioText;
    public Slider employmentSlider;
    public Slider jobsOccupiedSlider;
    public Slider criminalsCoveredSlider;
    public Slider patientsCoveredSlider;
    public Slider energyRatioSlider;

    public Color outlineColor;
    List<Button> buttonList;
    List<GameObject> buildingPanelList;
    private double _currentMoney;
    void AddButtonClickListener(Button button, UnityAction action)
    {   
        button.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(button);
            action.Invoke();
        });
    }

    private void Start()
    {
        buildingPanelList = new List<GameObject>
        {
            HousePanel,
            PublicServicePanel,
            EnergySourcePanel,
            WaterSourcePanel,
            WasteDisposalPanel,
            IndustryPanel,
            DecorationPanel,
            ShopPanel
        };

        buttonList = new List<Button> {
            destroyStructureButton,
            placeRoadButton,
            placeHouseButton,
            placeHighDensityHouseButton,
            placeClinicButton,
            placePoliceStationButton,
            placeCropButton,
            showMenu,
            showStats,
            showEnvironmentStats,
            placeCinemaButton,
            placeSolarPanelButton,
            placeFactoryButton,
            placeRoadButton,
            placeWindTurbineButton,
            placeShopButton,
            placeBarButton,
            placeHospitalButton,
            placeUniversityButton,
            placeBigParkButton,
            openHouseMenuButton,
            openPublicServiceMenuButton,
            openEnergySourceMenuButton,
            openWaterSourceMenuButton,
            openWasteDisposalMenuButton,
            openIndustryMenuButton,
            openDecorationMenuButton,
            openShopMenuButton,
            showScientificProgress,
            
            closeScientificProgress,
            placeFireStationButton,
            placeRestaurantButton,
            placeCarbonPowerPlantButton,
            placeNuclearPlantButton,
            placeWaterPlantButton,


            /*
            
        
            placeLivestockButton,
            placeLandfillButton,
            placeIncinerationPlantButton,
            */
        };

         // Add click listeners for all the buttons
        AddButtonClickListener(destroyStructureButton, () => { OnDestroyStructure?.Invoke(); });
        AddButtonClickListener(placeRoadButton, () => { OnRoadPlacement?.Invoke(); });
        AddButtonClickListener(placeHouseButton, () => { OnHousePlacement?.Invoke(); });
        AddButtonClickListener(placeHighDensityHouseButton, () => { OnHighDensityHousePlacement?.Invoke(); });
        AddButtonClickListener(placeClinicButton, () => { OnClinicPlacement?.Invoke(); });
        AddButtonClickListener(placePoliceStationButton, () => { OnPoliceStationPlacement?.Invoke(); });
        AddButtonClickListener(placeCropButton, () => { OnCropPlacement?.Invoke(); });
        AddButtonClickListener(showMenu, () => { OnShowMenu?.Invoke(); });
        AddButtonClickListener(showStats, () => { OnShowStats?.Invoke(); });
        AddButtonClickListener(showEnvironmentStats, () => { OnShowEnvironmentStats?.Invoke(); });
        AddButtonClickListener(showScientificProgress, () => { OnShowScientificProgress?.Invoke(); });
        AddButtonClickListener(closeScientificProgress, () => { OnCloseScientificProgress?.Invoke(); });
        AddButtonClickListener(placeCinemaButton, () => { OnCinemaPlacement?.Invoke(); });
        AddButtonClickListener(placeWindTurbineButton, () => { OnWindTurbinePlacement?.Invoke(); });
        AddButtonClickListener(placeFactoryButton, () => { OnFactoryPlacement?.Invoke(); });
        AddButtonClickListener(placeHospitalButton, () => { OnHospitalPlacement?.Invoke(); });
        AddButtonClickListener(placeShopButton, () => { OnShopPlacement?.Invoke(); });
        AddButtonClickListener(placeUniversityButton, () => { OnUniversityPlacement?.Invoke(); });
        AddButtonClickListener(placeBigParkButton, () => { OnBigParkPlacement?.Invoke(); });
        AddButtonClickListener(placeSolarPanelButton, () => { OnSolarPanelPlacement?.Invoke(); });

        AddButtonClickListener(openHouseMenuButton, () => { OnHouseMenu?.Invoke(); });
        AddButtonClickListener(openPublicServiceMenuButton, () => { OnPublicServiceMenu?.Invoke(); });
        AddButtonClickListener(openEnergySourceMenuButton, () => { OnEnergySourceMenu?.Invoke(); });
        AddButtonClickListener(openWaterSourceMenuButton, () => { OnWaterSourceMenu?.Invoke(); });
        AddButtonClickListener(openWasteDisposalMenuButton, () => { OnWasteDisposalMenu?.Invoke(); });
        AddButtonClickListener(openIndustryMenuButton, () => { OnIndustryMenu?.Invoke(); });
        AddButtonClickListener(openDecorationMenuButton, () => { OnDecorationMenu?.Invoke(); });
        AddButtonClickListener(openShopMenuButton, () => { OnShopMenu?.Invoke(); });
        AddButtonClickListener(placeBarButton, () => { OnBarPlacement?.Invoke(); });
        AddButtonClickListener(placeFireStationButton, () => { OnFireStationPlacement?.Invoke(); });
        AddButtonClickListener(placeRestaurantButton, () => { OnRestaurantPlacement?.Invoke(); });        
        AddButtonClickListener(placeCarbonPowerPlantButton, () => { OnCarbonPowerPlantPlacement?.Invoke(); });
        AddButtonClickListener(placeNuclearPlantButton, () => { OnNuclearPlantPlacement?.Invoke(); });
        AddButtonClickListener(placeWaterPlantButton, () => { OnWaterPlantPlacement?.Invoke(); });

        /*
        AddButtonClickListener(placeLivestockButton, () => { OnLivestockPlacement?.Invoke(); });
        AddButtonClickListener(placeLandfillButton, () => { OnLandfillPlacement?.Invoke(); });
        AddButtonClickListener(placeIncinerationPlantButton, () => { OnIncinerationPlantPlacement?.Invoke(); });
        */
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    public void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            if (button!= null)
                button.GetComponent<Outline>().enabled = false;
        }
    }

    public void ResetBuildingPanels()
    {
        foreach (GameObject panel in buildingPanelList)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }
    public void ShowNotEnoughMoneyText()
    {
        notEnoughMoneyText.gameObject.SetActive(true);

        // Schedule a function to hide the text after 5 seconds
        Invoke(nameof(HideNotEnoughMoneyText), 2f);
    }
    public void ShowBuildUniversityText()
    {
        buildUniversityText.gameObject.SetActive(true);

        // Schedule a function to hide the text after 5 seconds
        Invoke(nameof(HideBuildUniversityText), 2f);
    }
    private void HideBuildUniversityText()
    {
        buildUniversityText.gameObject.SetActive(false);
    }
    public void ShowYouWonText()
    {
        youWonText.gameObject.SetActive(true);

        // Schedule a function to hide the text after 5 seconds
        Invoke(nameof(HideYouWonText), 5f);
    }
    private void HideYouWonText()
    {
        youWonText.gameObject.SetActive(false);
    }
    public void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true);

        // Schedule a function to hide the text after 5 seconds
        Invoke(nameof(HideGameOverText), 5f);
    }
    private void HideGameOverText()
    {
        gameOverText.gameObject.SetActive(false);
    }

    private void HideNotEnoughMoneyText()
    {
        notEnoughMoneyText.gameObject.SetActive(false);
    }
    public void ShowResearchFinishedText()
    {
        researchFinishedText.gameObject.SetActive(true);

        // Schedule a function to hide the text after 5 seconds
        Invoke(nameof(HideResearchFinishedText), 5f);
    }
    private void HideResearchFinishedText()
    {
        researchFinishedText.gameObject.SetActive(false);
    }

    
    private void Update()
    {
        _currentMoney = gameState.currentMoney;
        populationText.text = "Population: " + gameState.totalPopulation;
        if (_currentMoney >= 1000000)
        {
            moneyText.text = "Money: $" + Math.Round(_currentMoney / 1000000, 2) + "M";
        }
        if (_currentMoney >= 1000 && _currentMoney < 1000000)
        {
            moneyText.text = "Money: $" + Math.Round(_currentMoney / 1000, 2) + "K";
        }
        if (_currentMoney < 1000)
        {
            moneyText.text = "Money: $" + Math.Round(_currentMoney, 2);
        }

        employmentText.text = "Employment: " + Math.Round(gameState.GetEmploymentRatio() * 100, 2) + "%";
        employmentSlider.value = (float)gameState.GetEmploymentRatio();
        jobsOccupiedText.text = "Jobs Occupied: " + Math.Round(gameState.GetJobsOccupiedRatio() * 100, 2) + "%";
        jobsOccupiedSlider.value = (float)gameState.GetJobsOccupiedRatio();
        criminalsCoveredText.text = "Criminals Covered: " + Math.Round(gameState.GetCriminalsCoveredRatio() * 100, 2) + "%";
        criminalsCoveredSlider.value = (float)gameState.GetCriminalsCoveredRatio();
        patientsCoveredText.text = "Patients Covered: " + Math.Round(gameState.GetPatientsCoveredRatio() * 100, 2) + "%";
        patientsCoveredSlider.value = (float)gameState.GetPatientsCoveredRatio();
        populationCapacityText.text = "Population Capacity: " + gameState.PopulationCapacity;
        particlePollutionSlider.value = (float)gameState.GetAirPollution()/100;
        particlePollutionText.text = "Particle Pollution: " + Math.Round(gameState.GetAirPollution(), 2) + "%";
        carbonDioxideSlider.value = (float)gameState.GetCo2Emissions()/100;
        carbonDioxideText.text = "Carbon Dioxide: " + Math.Round(gameState.GetCo2Emissions(), 2) + "%";
        energyRatioText.text = "Energy Ratio: " + Math.Round(gameState.GetEnergyRatio() * 100, 2) + "%";
        energyRatioSlider.value = (float)gameState.GetEnergyRatio();
    }
}
