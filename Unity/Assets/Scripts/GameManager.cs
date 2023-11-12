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

    public float zoomSpeed;

    private void Start()
    {
        uiController.OnDestroyStructure += DestroyStructureHandler;
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnClinicPlacement += ClinicPlacementHandler;
        uiController.OnPoliceStationPlacement += PoliceStationPlacementHandler;
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
    private void DestroyStructureHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.DestroyStructure;
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        
    }
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
        Debug.Log("Entered road placement handler");
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
    inputManager.OnMouseHover += structureManager.PlaceTemporaryHospital;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryHospital;
    inputManager.OnMouseClick += structureManager.PlaceHospital;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void SolarPanelPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporarySolarPanel;
    inputManager.OnMouseClick -= structureManager.PlaceTemporarySolarPanel;
    inputManager.OnMouseClick += structureManager.PlaceSolarPanel;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void WindTurbinePlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryWindTurbine;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryWindTurbine;
    inputManager.OnMouseClick += structureManager.PlaceWindTurbine;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void CarbonPowerPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryCarbonPowerPlant;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryCarbonPowerPlant;
    inputManager.OnMouseClick += structureManager.PlaceCarbonPowerPlant;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
    
}

private void NuclearPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryNuclearPlant;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryNuclearPlant;
    inputManager.OnMouseClick += structureManager.PlaceNuclearPlant;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void HighDensityHousePlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryHighDensityHouse;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryHighDensityHouse;
    inputManager.OnMouseClick += structureManager.PlaceHighDensityHouse;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void ShopPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryShop;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryShop;
    inputManager.OnMouseClick += structureManager.PlaceShop;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
    
}

private void RestaurantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryRestaurant;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryRestaurant;
    inputManager.OnMouseClick += structureManager.PlaceRestaurant;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void UniversityPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryUniversity;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryUniversity;
    inputManager.OnMouseClick += structureManager.PlaceUniversity;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}
private void FireStationPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryFireStation;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryFireStation;
    inputManager.OnMouseClick += structureManager.PlaceFireStation;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}
private void PoliceStationPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryPoliceStation;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryPoliceStation;
    inputManager.OnMouseClick += structureManager.PlacePoliceStation;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}


 private void BarPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryBar;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryBar;
    inputManager.OnMouseClick += structureManager.PlaceBar;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}
private void CinemaPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryCinema;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryCinema;
    inputManager.OnMouseClick += structureManager.PlaceCinema;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void LandfillPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryLandfill;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryLandfill;
    inputManager.OnMouseClick += structureManager.PlaceLandfill;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void IncinerationPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryIncinerationPlant;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryIncinerationPlant;
    inputManager.OnMouseClick += structureManager.PlaceIncinerationPlant;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void WasteToEnergyPlantPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryWasteToEnergyPlant;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryWasteToEnergyPlant;
    inputManager.OnMouseClick += structureManager.PlaceWasteToEnergyPlant;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
    
}

private void FactoryPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryFactory;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryFactory;
    inputManager.OnMouseClick += structureManager.PlaceFactory;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void CropPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryCrop;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryCrop;
    inputManager.OnMouseClick += structureManager.PlaceCrop;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

private void LivestockPlacementHandler()
{
    ClearInputActions();
    inputManager.OnMouseHover += structureManager.PlaceTemporaryLivestock;
    inputManager.OnMouseClick -= structureManager.PlaceTemporaryLivestock;
    inputManager.OnMouseClick += structureManager.PlaceLivestock;
    inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
}

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
        cameraMovement.ZoomCamera(inputManager.Zoom);

    }


}
