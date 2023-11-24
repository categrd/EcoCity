﻿using System;
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
    
    
    public Action OnRoadPlacement, OnHousePlacement, OnClinicPlacement, OnHospitalPlacement, OnSolarPanelPlacement, OnWindTurbinePlacement, OnCarbonPowerPlantPlacement, 
        OnNuclearPlantPlacement, OnHighDensityHousePlacement, OnShopPlacement, OnRestaurantPlacement, OnBarPlacement, OnCinemaPlacement, OnUniversityPlacement, 
        OnFireStationPlacement, OnPoliceStationPlacement, OnFactoryPlacement, OnCropPlacement, OnLivestockPlacement, OnLandfillPlacement, OnIncinerationPlantPlacement, 
        OnWasteToEnergyPlantPlacement, OnShowMenu, OnShowStats, OnShowEnvironmentStats;

    public Action OnDestroyStructure;

    public Button showMenu;
    public Button showStats;

    public Button showEnvironmentStats;
    
    public Button destroyStructureButton;
    
    public Button placeRoadButton;
    // Healthcare
    public Button placeClinicButton;
    public Button placeHospitalButton;

    // Energy
    public Button placeSolarPanelButton;
    public Button placeWindTurbineButton;
    public Button placeCarbonPowerPlantButton;
    public Button placeNuclearPlantButton;

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

    // Waste Management
    public Button placeLandfillButton;
    public Button placeIncinerationPlantButton;
    public Button placeWasteToEnergyPlantButton;
    
    public Text populationCapacityText;
    
    public Text employmentText;
    public Text jobsOccupiedText;
    public Text criminalsCoveredText;
    public Text patientsCoveredText;
    public Slider employmentSlider;
    public Slider jobsOccupiedSlider;
    public Slider criminalsCoveredSlider;
    public Slider patientsCoveredSlider;

    public Color outlineColor;
    List<Button> buttonList;
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
        buttonList = new List<Button> {
            destroyStructureButton,
            placeRoadButton,
            placeHouseButton,
            placeClinicButton,
            placePoliceStationButton,
            placeCropButton,
            showMenu,
            showStats,
            showEnvironmentStats,
            placeCinemaButton,
            /*
            placeHospitalButton,
            placeUniversityButton,
            placeFireStationButton,
            placeRestaurantButton,
            placeSolarPanelButton,
            placeWindTurbineButton,
            placeCarbonPowerPlantButton,
            placeNuclearPlantButton,
            placeHighDensityHouseButton,
            placeShopButton,
            placeBarButton,
            
            placeFactoryButton,
            placeLivestockButton,
            placeLandfillButton,
            placeIncinerationPlantButton,
            placeWasteToEnergyPlantButton
            */
        };
        
        // Add click listeners for all the buttons
        AddButtonClickListener(destroyStructureButton, () => { OnDestroyStructure?.Invoke(); });
        AddButtonClickListener(placeRoadButton, () => { OnRoadPlacement?.Invoke(); });
        AddButtonClickListener(placeHouseButton, () => { OnHousePlacement?.Invoke(); });
        AddButtonClickListener(placeClinicButton, () => { OnClinicPlacement?.Invoke(); });
        AddButtonClickListener(placePoliceStationButton, () => { OnPoliceStationPlacement?.Invoke(); });
        AddButtonClickListener(placeCropButton, () => { OnCropPlacement?.Invoke(); });
        AddButtonClickListener(showMenu, () => { OnShowMenu?.Invoke(); });
        AddButtonClickListener(showStats, () => { OnShowStats?.Invoke(); });
        AddButtonClickListener(showEnvironmentStats, () => { OnShowEnvironmentStats?.Invoke(); });
        AddButtonClickListener(placeCinemaButton, () => { OnCinemaPlacement?.Invoke(); });
        /*
        AddButtonClickListener(placeHospitalButton, () => { OnHospitalPlacement?.Invoke(); });
        AddButtonClickListener(placeRestaurantButton, () => { OnRestaurantPlacement?.Invoke(); });
        AddButtonClickListener(placeUniversityButton, () => { OnUniversityPlacement?.Invoke(); });
        AddButtonClickListener(placeFireStationButton, () => { OnFireStationPlacement?.Invoke(); });
        
        AddButtonClickListener(placeSolarPanelButton, () => { OnSolarPanelPlacement?.Invoke(); });
        AddButtonClickListener(placeWindTurbineButton, () => { OnWindTurbinePlacement?.Invoke(); });
        AddButtonClickListener(placeCarbonPowerPlantButton, () => { OnCarbonPowerPlantPlacement?.Invoke(); });
        AddButtonClickListener(placeNuclearPlantButton, () => { OnNuclearPlantPlacement?.Invoke(); });
        AddButtonClickListener(placeHighDensityHouseButton, () => { OnHighDensityHousePlacement?.Invoke(); });
        AddButtonClickListener(placeShopButton, () => { OnShopPlacement?.Invoke(); });
        
        AddButtonClickListener(placeBarButton, () => { OnBarPlacement?.Invoke(); });
        AddButtonClickListener(placeFactoryButton, () => { OnFactoryPlacement?.Invoke(); });
        AddButtonClickListener(placeCropButton, () => { OnCropPlacement?.Invoke(); });
        AddButtonClickListener(placeLivestockButton, () => { OnLivestockPlacement?.Invoke(); });
        AddButtonClickListener(placeLandfillButton, () => { OnLandfillPlacement?.Invoke(); });
        AddButtonClickListener(placeIncinerationPlantButton, () => { OnIncinerationPlantPlacement?.Invoke(); });
        AddButtonClickListener(placeWasteToEnergyPlantButton, () => { OnWasteToEnergyPlantPlacement?.Invoke(); });
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
            button.GetComponent<Outline>().enabled = false;
        }
    }
    private void Update()
    {
        _currentMoney = gameState.currentMoney;
        populationText.text = "Population: " + gameState.totalPopulation;
        if (_currentMoney >= 1000000)
        {
            moneyText.text = "Money: $" + Math.Round(_currentMoney / 1000000,2) + "M";
        }
        if (_currentMoney >= 1000 && _currentMoney < 1000000)
        {
            moneyText.text = "Money: $" + Math.Round(_currentMoney / 1000,2) + "K";
        }
        if(_currentMoney < 1000)
        {
            moneyText.text = "Money: $" + Math.Round(_currentMoney,2);
        }

        employmentText.text = "Employment: " + Math.Round(gameState.GetEmploymentRatio() * 100, 2) + "%";
        employmentSlider.value = (float)gameState.GetEmploymentRatio();
        jobsOccupiedText.text = "Jobs Occupied: " + Math.Round(gameState.GetJobsOccupiedRatio()*100,2) + "%";
        jobsOccupiedSlider.value = (float)gameState.GetJobsOccupiedRatio();
        criminalsCoveredText.text = "Criminals Covered: " + Math.Round(gameState.GetCriminalsCoveredRatio() * 100, 2) + "%";
        criminalsCoveredSlider.value = (float)gameState.GetCriminalsCoveredRatio();
        patientsCoveredText.text = "Patients Covered: " + Math.Round(gameState.GetPatientsCoveredRatio()*100,2) + "%";
        patientsCoveredSlider.value = (float)gameState.GetPatientsCoveredRatio();
        populationCapacityText.text = "Population Capacity: " + gameState.PopulationCapacity;
    }
}
