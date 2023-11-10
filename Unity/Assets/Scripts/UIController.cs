using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnClinicPlacement, OnHospitalPlacement, OnSolarPanelPlacement, OnWindTurbinePlacement, OnCarbonPowerPlantPlacement, 
        OnNuclearPlantPlacement, OnHighDensityHousePlacement, OnShopPlacement, OnRestaurantPlacement, OnBarPlacement, OnCinemaPlacement, OnUniversityPlacement, 
        OnFireStationPlacement, OnPoliceStationPlacement, OnFactoryPlacement, OnCropPlacement, OnLivestockPlacement, OnLandfillPlacement, OnIncinerationPlantPlacement, 
        OnWasteToEnergyPlantPlacement;
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

    public Color outlineColor;
    List<Button> buttonList;
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
            placeRoadButton,
            placeHouseButton,
            placeClinicButton,
            /*
            placeHospitalButton,
            placeSolarPanelButton,
            placeWindTurbineButton,
            placeCarbonPowerPlantButton,
            placeNuclearPlantButton,
            placeHighDensityHouseButton,
            placeShopButton,
            placeRestaurantButton,
            placeBarButton,
            placeCinemaButton,
            placeUniversityButton,
            placeFireStationButton,
            placePoliceStationButton,
            placeFactoryButton,
            placeCropButton,
            placeLivestockButton,
            placeLandfillButton,
            placeIncinerationPlantButton,
            placeWasteToEnergyPlantButton
            */
        };


        // Add click listeners for all the buttons
AddButtonClickListener(placeRoadButton, () => { OnRoadPlacement?.Invoke(); });
AddButtonClickListener(placeHouseButton, () => { OnHousePlacement?.Invoke(); });
AddButtonClickListener(placeClinicButton, () => { OnClinicPlacement?.Invoke(); });
/*
AddButtonClickListener(placeHospitalButton, () => { OnHospitalPlacement?.Invoke(); });
AddButtonClickListener(placeSolarPanelButton, () => { OnSolarPanelPlacement?.Invoke(); });
AddButtonClickListener(placeWindTurbineButton, () => { OnWindTurbinePlacement?.Invoke(); });
AddButtonClickListener(placeCarbonPowerPlantButton, () => { OnCarbonPowerPlantPlacement?.Invoke(); });
AddButtonClickListener(placeNuclearPlantButton, () => { OnNuclearPlantPlacement?.Invoke(); });
AddButtonClickListener(placeHighDensityHouseButton, () => { OnHighDensityHousePlacement?.Invoke(); });
AddButtonClickListener(placeShopButton, () => { OnShopPlacement?.Invoke(); });
AddButtonClickListener(placeRestaurantButton, () => { OnRestaurantPlacement?.Invoke(); });
AddButtonClickListener(placeBarButton, () => { OnBarPlacement?.Invoke(); });
AddButtonClickListener(placeCinemaButton, () => { OnCinemaPlacement?.Invoke(); });
AddButtonClickListener(placeUniversityButton, () => { OnUniversityPlacement?.Invoke(); });
AddButtonClickListener(placeFireStationButton, () => { OnFireStationPlacement?.Invoke(); });
AddButtonClickListener(placePoliceStationButton, () => { OnPoliceStationPlacement?.Invoke(); });
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
}
