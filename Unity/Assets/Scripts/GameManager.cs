using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UIController uiController;

    public StructureManager structureManager;
    public PlacementManager placementManager;

    private void Start()
    {
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnClinicPlacement += ClinicPlacementHandler;
        /*
        uiController.OnHospitalPlacement += HospitalPlacementHandler;
        uiController.OnSolarPanelPlacement += SolarPanelPlacementHandler;
        uiController.OnWindTurbinePlacement += WindTurbinePlacementHandler;
        uiController.OnCarbonPowerPlantPlacement += CarbonPowerPlantPlacementHandler;
        uiController.OnNuclearPlantPlacement += NuclearPlantPlacementHandler;
        uiController.OnHighDensityHousePlacement += HighDensityHousePlacementHandler;

        uiController.OnShopPlacement += ShopPlacementHandler;
        uiController.OnRestaurantPlacement += RestaurantPlacementHandler;
        uiController.OnBarPlacement += BarPlacementHandler;
        uiController.OnCinemaPlacement += CinemaPlacementHandler;
        uiController.OnUniversityPlacement += UniversityPlacementHandler;
        uiController.OnFireStationPlacement += FireStationPlacementHandler;
        uiController.OnPoliceStationPlacement += PoliceStationPlacementHandler;
        uiController.OnFactoryPlacement += FactoryPlacementHandler;
        uiController.OnCropPlacement += CropPlacementHandler;
        uiController.OnLivestockPlacement += LivestockPlacementHandler;
        uiController.OnLandfillPlacement += LandfillPlacementHandler;
        uiController.OnIncinerationPlantPlacement += IncinerationPlantPlacementHandler;
        uiController.OnWasteToEnergyPlantPlacement += WasteToEnergyPlantPlacementHandler;
        */

        
        
    }
/*
    private void BigStructurePlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceBigStructure;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceSpecial;
    }
*/
    private void HousePlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseHover += structureManager.PlaceTemporaryHouse;
        inputManager.OnMouseClick -= structureManager.PlaceTemporaryHouse;
        inputManager.OnMouseClick += structureManager.PlaceHouse;
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
    }

private void RoadPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += roadManager.PlaceRoad;
    inputManager.OnMouseHold += roadManager.PlaceRoad;
    inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void ClinicPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryClinic;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryClinic;
    inputManager.OnMouseClick += structureManager.PlaceClinic;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void HospitalPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceHospital;
}

private void SolarPanelPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceSolarPanel;
}

private void WindTurbinePlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceWindTurbine;
}

private void CarbonPowerPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceCarbonPowerPlant;
}

private void NuclearPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceNuclearPlant;
}

private void HighDensityHousePlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceHighDensityHouse;
}
/*
private void ShopPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceShopPlacement;
}
private void RestaurantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceRestaurant;
}
private void BarPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceBar;
}
private void CinemaPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceCinema;
}
private void UniversityPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceUniversity;
}
private void FireStationPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceFireStation;
}
private void PoliceStationPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlacePoliceStation;
}


private void LandfillPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceLandfill;
}

private void IncinerationPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceIncinerationPlant;
}

private void WasteToEnergyPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceWasteToEnergyPlant;
}

private void FactoryPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceFactory;
}

private void CropPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceCrop;
}

private void LivestockPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseClick += structureManager.PlaceLivestock;
}
*/
    private void ClearInputActions()
    {
        placementManager.DestroyTemporaryStructure();
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
        inputManager.OnMouseHover = null;
        inputManager.OnPressingEsc = null;
    }
    private void ClearInputActionsAndButtonColor()
    {
        placementManager.DestroyTemporaryStructure();
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
        inputManager.OnMouseHover = null;
        inputManager.OnPressingEsc = null;
        uiController.ResetButtonColor();
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0, inputManager.CameraMovementVector.y));
    }
}
