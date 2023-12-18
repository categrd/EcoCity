using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject statsPanel;
    public GameObject environmentStatsPanel;
    public GameObject scientificProgressPanel;


    public GameObject housePanel, decorationPanel, publicservicesPanel, industryPanel, shopPanel, wastedisposalPanel,
     watersourcePanel, energysourcePanel;   


    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UIController uiController;
    public StructureManager structureManager;
    public PlacementManager placementManager;
    
    public GameState gameState;

    public float zoomSpeed;

    private void Start()
    {
        uiController.OnDestroyStructure += DestroyStructureHandler;
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnCinemaPlacement += () => StructurePlacementHandler(BuildingType.Cinema);
        uiController.OnHousePlacement += () => StructurePlacementHandler(BuildingType.House);
        uiController.OnClinicPlacement += () => StructurePlacementHandler(BuildingType.Clinic);
        uiController.OnHospitalPlacement += () => StructurePlacementHandler(BuildingType.Hospital);
        uiController.OnSolarPanelPlacement += () => StructurePlacementHandler(BuildingType.SolarPanel);
        uiController.OnWindTurbinePlacement += () => StructurePlacementHandler(BuildingType.WindTurbine);
        uiController.OnCarbonPowerPlantPlacement += () => StructurePlacementHandler(BuildingType.CarbonPowerPlant);
        uiController.OnNuclearPlantPlacement += () => StructurePlacementHandler(BuildingType.NuclearPlant);
        uiController.OnHighDensityHousePlacement += () => StructurePlacementHandler(BuildingType.HighDensityHouse);
        uiController.OnShopPlacement += () => StructurePlacementHandler(BuildingType.Shop);
        uiController.OnRestaurantPlacement += () => StructurePlacementHandler(BuildingType.Restaurant);
        uiController.OnBarPlacement += () => StructurePlacementHandler(BuildingType.Bar);
        uiController.OnUniversityPlacement += () => StructurePlacementHandler(BuildingType.University);
        uiController.OnFireStationPlacement += () => StructurePlacementHandler(BuildingType.FireStation);
        uiController.OnPoliceStationPlacement += () => StructurePlacementHandler(BuildingType.PoliceStation);
        uiController.OnFactoryPlacement += () => StructurePlacementHandler(BuildingType.Factory);
        uiController.OnCropPlacement += () => StructurePlacementHandler(BuildingType.Crop);
        uiController.OnLivestockPlacement += () => StructurePlacementHandler(BuildingType.Livestock);
        uiController.OnLandfillPlacement += () => StructurePlacementHandler(BuildingType.Landfill);
        uiController.OnIncinerationPlantPlacement += () => StructurePlacementHandler(BuildingType.IncinerationPlant);
        uiController.OnWasteToEnergyPlantPlacement += () => StructurePlacementHandler(BuildingType.WasteToEnergyPlant);
        uiController.OnBigParkPlacement += () => StructurePlacementHandler(BuildingType.BigPark);
        uiController.OnWaterPlantPlacement += () => StructurePlacementHandler(BuildingType.WaterPlant);

        uiController.OnShowMenu += () => PanelHandler(menuPanel);
        uiController.OnShowStats += () => PanelHandler(statsPanel);
        uiController.OnShowEnvironmentStats += () => PanelHandler(environmentStatsPanel);
        uiController.OnShowScientificProgress += () => ScientificPanelHandler(scientificProgressPanel);
        uiController.OnCloseScientificProgress += () => PanelHandler(scientificProgressPanel); 

        uiController.OnHouseMenu += () => BuildingPanelHandler(housePanel);
        uiController.OnIndustryMenu += () => BuildingPanelHandler(industryPanel);
        uiController.OnWaterSourceMenu += () => BuildingPanelHandler(watersourcePanel);
        uiController.OnEnergySourceMenu += () => BuildingPanelHandler(energysourcePanel);
        uiController.OnWasteDisposalMenu += () => BuildingPanelHandler(wastedisposalPanel);
        uiController.OnShopMenu += () => BuildingPanelHandler(shopPanel);
        uiController.OnDecorationMenu += () => BuildingPanelHandler(decorationPanel);
        uiController.OnPublicServiceMenu += () => BuildingPanelHandler(publicservicesPanel);
        
    }
    
    /*
    private void ShowMenuHandler()
    {
        ClearInputActions();
        if (menuPanel.activeSelf)
        {
            ClearInputActionsAndButtonColor();    
        }

        ToggleMenu(menuPanel);
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        inputManager.OnPressingEsc += () => ToggleMenu(menuPanel);

    }
    private void ShowStatsHandler()
    {
        ClearInputActions();
        if (statsPanel.activeSelf)
        {
            ClearInputActionsAndButtonColor();    
        }

        ToggleMenu(statsPanel);
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        inputManager.OnPressingEsc += () => ToggleMenu(statsPanel);

    }

    private void ShowEnvironmentStatsHandler()
    {
        ClearInputActions();
        if (environmentStatsPanel.activeSelf)
        {
            ClearInputActionsAndButtonColor();    
        }

        ToggleMenu(environmentStatsPanel);
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        inputManager.OnPressingEsc += () => ToggleMenu(environmentStatsPanel);

    }
    */ 

    

    private void PanelHandler(GameObject panel)
    {
        ClearInputActions();
        if (panel.activeSelf)
        {
            ClearInputActionsAndButtonColor();    
        }

        ToggleMenu(panel);
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        inputManager.OnPressingEsc += () => ToggleMenu(panel);

    }
    private void ScientificPanelHandler(GameObject panel)
    {
        ClearInputActions();
        if (panel.activeSelf)
        {
            ClearInputActionsAndButtonColor();    
        }
        if(gameState.NumberOfUniversity > 0)
        {
            ToggleMenu(panel);
        }
        else
        {
            // show a message that the university is not built yet
            uiController.ShowBuildUniversityText();
            inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
            // on pressing esc set the menu inactive
            inputManager.OnPressingEsc += () => SetMenuInactive(panel);
        }
        
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        inputManager.OnPressingEsc += () => ToggleMenu(panel);

    }

    private void BuildingPanelHandler(GameObject panel)
    {
        ClearInputActions();
        if (panel.activeSelf)
        {
            uiController.ResetBuildingPanels();
            ClearInputActionsAndButtonColor();    
        }
        else
        {
            uiController.ResetBuildingPanels();
            ToggleMenu(panel);
        }
        
        
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        inputManager.OnPressingEsc += () => ToggleMenu(panel);
    }

    private void ToggleText(Text text){

    }
    private void ToggleMenu(GameObject panel)
    {
        panel.SetActive(!(panel.activeSelf));
    }
    private void SetMenuInactive(GameObject panel)
    {
        panel.SetActive(false);
    }
    
    private void DestroyStructureHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.DestroyStructure;
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
        
    }
    private void StructurePlacementHandler(BuildingType buildingType)
    {
        ClearInputActions();
        int structureRotation = 0;
        inputManager.OnMouseHover += (hoverPosition) =>
        {
            structureManager.PlaceStructure(hoverPosition, buildingType, structureRotation, true );
        };
        inputManager.OnMouseClick -= (hoverPosition) =>
        {
            structureManager.PlaceStructure(hoverPosition, buildingType,structureRotation, true );
        };
        inputManager.OnMouseClick += (hoverPosition) =>
        {
            structureManager.PlaceStructure(hoverPosition, buildingType,structureRotation, false );
        };
        inputManager.OnPressingEsc += ClearInputActionsAndButtonColor;
    }
    private void RoadPlacementHandler()
    {
        uiController.ResetBuildingPanels();
        ClearInputActions();
        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
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
        ClearInputActions();
        uiController.ResetButtonColor();
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0, inputManager.CameraMovementVector.y));
        cameraMovement.ZoomCamera(inputManager.Zoom);
        cameraMovement.LimitZoomCamera();
    }
}
