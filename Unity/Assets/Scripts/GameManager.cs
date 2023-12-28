using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

 public class GameManager : MonoBehaviour
{

    


    // Singleton stuff 
    static GameManager instance;
    
    void Awake(){
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    



    // Rest of Attributes and Methods
    public MainMenuManager mainMenuManager;
    public GameObject menuPanel;
    public GameObject statsPanel;
    public GameObject environmentStatsPanel;
    public GameObject scientificProgressPanel;


    public GameObject housePanel, decorationPanel, publicservicesPanel, industryPanel, shopPanel,
    watersourcePanel, energysourcePanel;   


    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public Dictionary<QuestionAndAnswer,BuildingType> checkUnlock = new Dictionary<QuestionAndAnswer,BuildingType>();
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
        uiController.OnShopMenu += () => BuildingPanelHandler(shopPanel);
        uiController.OnDecorationMenu += () => BuildingPanelHandler(decorationPanel);
        uiController.OnPublicServiceMenu += () => BuildingPanelHandler(publicservicesPanel);        
        
    }
    
    public void CreateDictionary(List<QuestionAndAnswer> qanda){
        checkUnlock.Add(qanda[0],BuildingType.CarbonPowerPlant);
        checkUnlock.Add(qanda[1],BuildingType.NuclearPlant);
        checkUnlock.Add(qanda[2],BuildingType.University);
        checkUnlock.Add(qanda[3],BuildingType.HighDensityHouse);
        checkUnlock.Add(qanda[4],BuildingType.Hospital);
        checkUnlock.Add(qanda[5],BuildingType.SolarPanel);
        checkUnlock.Add(qanda[6],BuildingType.WindTurbine);
        checkUnlock.Add(qanda[7],BuildingType.Landfill);
        checkUnlock.Add(qanda[8],BuildingType.Factory);
        checkUnlock.Add(qanda[9],BuildingType.Cinema);
        checkUnlock.Add(qanda[10],BuildingType.Restaurant);
    }



    
    
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
        if(IsUnlocked(buildingType))
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
        else
        {
            uiController.ShowBuildingBlockedText();
        }
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
    
    public QuestionAndAnswer getKeyfromValue(BuildingType value)
    {
        foreach (var k in checkUnlock.Keys)
        {
            if (checkUnlock[k] == value)
            {
                return k;
            }
        }
        return null;
    }

    public bool IsUnlocked(BuildingType buildingType)
    {  
        if(getKeyfromValue(buildingType) == null)
        {
            return true;
        }
        else{
            return getKeyfromValue(buildingType).isDone;  
        }
    } 



    private void Update()
    {
        Debug.Log(MainMenuManager.Instance.GetCluster().ToString());
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0, inputManager.CameraMovementVector.y));
        cameraMovement.ZoomCamera(inputManager.Zoom);
        cameraMovement.LimitZoomCamera();
    }
}
