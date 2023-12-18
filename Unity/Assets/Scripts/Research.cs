using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Research : MonoBehaviour
{
    public PlacementManager placementManager;
    public NaturalDisasters naturalDisasters;
    public GameState gameState;
    public UIController uiController;
    
    public Button RenewableEnergyButton;
    public Button GreenConstructionButton;
    public Button NaturalDisastersButton;
    public Button IndustryButton;
    public Button AdvancedResearchButton;
    public Button ResearchEfficiencyButton;
    
    public Button WindEnergyButton;
    public Button SolarEnergyButton;
    public Button GreenMaterialsButton;
    public Button ReduceWasteButton;
    public Button ReduceCostButton;
    public Button SmogEventButton;
    public Button HeatwaveEventButton;
    public Button AcidRainEventButton;
    public Button FactorButton;
    public Button CropButton;
    public Button LivestockButton;
    public Button SpeedResearchButton;
    public Button UnlockResearchButton;
    public Button AdvanceCaptureCO2Button;
    public Button GeoengineeringSolutionsButton;
    
    
    public GameObject RenewableEnergyPanel;
    public GameObject GreenConstructionPanel;
    public GameObject NaturalDisastersPanel;
    public GameObject IndustryPanel;
    public GameObject AdvancedResearchPanel;
    public GameObject ResearchEfficiencyPanel;

    
    public Text TimeText;
    public Slider TimeSlider;
    
    private bool windEnergyUnlocked = false;
    private bool solarEnergyUnlocked = false;
    private bool greenMaterialsUnlocked = false;
    private bool reduceWasteUnlocked = false;
    private bool reduceCostUnlocked = false;
    private bool smogEventUnlocked = false;
    private bool heatwaveEventUnlocked = false;
    private bool acidRainEventUnlocked = false;
    private bool factorUnlocked = false;
    private bool cropUnlocked = false;
    private bool livestockUnlocked = false;
    private bool speedResearchUnlocked = false;
    private bool unlockResearchUnlocked = false;
    private bool advanceCaptureCO2Unlocked = false;
    private bool geoengineeringSolutionsUnlocked = false;
    
    
    
    
    private float windTurbineModifier = 1.0f;
    public float WindTurbineModifier => windTurbineModifier;
    private float solarPanelModifier = 1.0f;
    public float SolarPanelModifier => solarPanelModifier;
    private float wasteProductionModifier = 1.0f;
    public float WasteProductionModifier => wasteProductionModifier;
    
    private float factoryModifier = 1.0f;
    public float FactoryModifier => factoryModifier;
    private float vegetableModifier = 1.0f;
    public float VegetableModifier => vegetableModifier;
    private float meatModifier = 1.0f;
    public float MeatModifier => meatModifier;
    private float greenMaterialsModifier = 1.0f;
    public float GreenMaterialsModifier => greenMaterialsModifier;
    
    private float costModifier = 1.0f;
    public float CostModifier => costModifier;
    private float speedUpModifier = 1.0f;
    private bool advancedResearchUnlocked = false;
    private bool currentlyResearching = false;
    private float timeOfCurrentResearch = 0.0f;
    private float _timeResearch;
    private List<Button> _mainButtons;

    private void Start()
    {
        // add all buttons to a list
        _mainButtons = new List<Button>();
        _mainButtons.Add(RenewableEnergyButton);
        _mainButtons.Add(GreenConstructionButton);
        _mainButtons.Add(NaturalDisastersButton);
        _mainButtons.Add(IndustryButton);
        _mainButtons.Add(AdvancedResearchButton);
        _mainButtons.Add(ResearchEfficiencyButton);
    }
    private void Update()
    {
        // if research is currently happening, we increase time of a variable Time by Time.deltaTime and update the text
        // by having it the percentage of the research done and change the slider value
        if (currentlyResearching)
        {
            _timeResearch += Time.deltaTime;
            TimeText.text = (_timeResearch / timeOfCurrentResearch * 100).ToString("F2") + "%";
            TimeSlider.value = _timeResearch / timeOfCurrentResearch;
            if(_timeResearch >= timeOfCurrentResearch)
            {
                currentlyResearching = false;
                _timeResearch = 0.0f;
                uiController.ShowResearchFinishedText();
            }
        }
        else
        {
            TimeText.text = "0%";
            _timeResearch = 0.0f;
        }
        
    }
    // when a button is clicked, we activate the panel of the button and deactivate all other panels
    // and we set the outline active for the button and inactive for all other buttons
    public void OnButtonRenewableEnergyButton()
    {
        SetOutlineActive(RenewableEnergyButton);
        SetAllOtherOutlineInactive(RenewableEnergyButton);
        ActivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }

    public void OnButtonGreenConstructionButton()
    {
        SetOutlineActive(GreenConstructionButton);
        SetAllOtherOutlineInactive(GreenConstructionButton);
        ActivatePanel(GreenConstructionPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonNaturalDisastersButton()
    {
        SetOutlineActive(NaturalDisastersButton);
        SetAllOtherOutlineInactive(NaturalDisastersButton);
        ActivatePanel(NaturalDisastersPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonIndustryButton()
    {
        SetOutlineActive(IndustryButton);
        SetAllOtherOutlineInactive(IndustryButton);
        ActivatePanel(IndustryPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonAdvancedResearchButton()
    {
        SetOutlineActive(AdvancedResearchButton);
        SetAllOtherOutlineInactive(AdvancedResearchButton);
        ActivatePanel(AdvancedResearchPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonResearchEfficiencyButton()
    {
        SetOutlineActive(ResearchEfficiencyButton);
        SetAllOtherOutlineInactive(ResearchEfficiencyButton);
        ActivatePanel(ResearchEfficiencyPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
    }
    
    private void ActivatePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    private void DeactivatePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
    private void SetOutlineActive(Button button)
    {
        // set activate the outline
        button.GetComponent<Outline>().enabled = true;
    }
    private void SetAllOtherOutlineInactive(Button button)
    {
        // set all other outlines inactive
        foreach (Button mainButton in _mainButtons)
        {
            if (mainButton != button)
            {
                mainButton.GetComponent<Outline>().enabled = false;
            }
        }
    }
    public void OnClickWindEnergyButton()
    {
        if(currentlyResearching || windEnergyUnlocked)
            return;
        float windResearchTime = 30.0f;
        timeOfCurrentResearch = windResearchTime;
        
        Invoke(nameof(OnFinishWindEnergyResearch), windResearchTime);
        currentlyResearching = true;
        ChangeColourOfButton(WindEnergyButton, windResearchTime * speedUpModifier);
        
    }
    private void OnFinishWindEnergyResearch()
    {
        windTurbineModifier = 1.5f;
        List<Vector3Int?> windTurbinePositions = placementManager.GetAllPositionOfTypeCellSatisfying(
            typeof(EnergyProductionCell),
            (cell) => placementManager.CheckIfCellIsOfBuildingType(cell, BuildingType.WindTurbine));
        foreach (Vector3Int? windTurbinePosition in windTurbinePositions)
        {
            if (windTurbinePosition != null)
            {
                EnergyProductionCell windTurbineCell = (EnergyProductionCell) placementManager.GetCellAtPosition((Vector3Int) windTurbinePosition);
                windTurbineCell.EnergyProduced *= windTurbineModifier;
            }
        }
        windEnergyUnlocked = true;
    }
    public void OnClickSolarEnergyButton()
    {
        if(currentlyResearching || solarEnergyUnlocked)
            return;
        float solarResearchTime = 30.0f;
        timeOfCurrentResearch = solarResearchTime;
        Invoke(nameof(OnFinishSolarEnergyResearch), solarResearchTime * speedUpModifier );
        currentlyResearching = true;
        ChangeColourOfButton(SolarEnergyButton, solarResearchTime * speedUpModifier);
        
    }
    private void OnFinishSolarEnergyResearch()
    {
        solarPanelModifier = 1.5f;
        List<Vector3Int?> solarPanelPositions = placementManager.GetAllPositionOfTypeCellSatisfying(
            typeof(EnergyProductionCell),
            (cell) => placementManager.CheckIfCellIsOfBuildingType(cell, BuildingType.SolarPanel));
        foreach (Vector3Int? solarPanelPosition in solarPanelPositions)
        {
            if (solarPanelPosition != null)
            {
                EnergyProductionCell solarPanelCell = (EnergyProductionCell) placementManager.GetCellAtPosition((Vector3Int) solarPanelPosition);
                solarPanelCell.EnergyProduced *= solarPanelModifier;
            }
        }
        solarEnergyUnlocked = true;
    }
    public void OnClickGreenMaterialsButton()
    {
        if(currentlyResearching || greenMaterialsUnlocked)
            return;
        float greenMaterialsResearchTime = 30.0f;
        timeOfCurrentResearch = greenMaterialsResearchTime;
        Invoke(nameof(OnFinishGreenMaterialsResearch), greenMaterialsResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(GreenMaterialsButton, greenMaterialsResearchTime * speedUpModifier);
    }
    private void OnFinishGreenMaterialsResearch()
    {
        greenMaterialsModifier = 0.9f;
        List<Vector3Int?> structureCellPositions = placementManager.GetAllPositionOfTypeCellSatisfying(typeof(StructureCell));
        foreach (Vector3Int? structureCellPosition in structureCellPositions)
        {
            if (structureCellPosition != null)
            {
                StructureCell structureCell = (StructureCell) placementManager.GetCellAtPosition((Vector3Int) structureCellPosition);
                structureCell.Co2Production *= greenMaterialsModifier;
            }
        }
        greenMaterialsUnlocked = true;
    }
    public void OnClickReduceWasteButton()
    {
        if(currentlyResearching || reduceWasteUnlocked)
            return;
        float reduceWasteResearchTime = 30.0f;
        timeOfCurrentResearch = reduceWasteResearchTime;
        Invoke(nameof(OnFinishReduceWasteResearch), reduceWasteResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(ReduceWasteButton, reduceWasteResearchTime * speedUpModifier);
        
    }
    private void OnFinishReduceWasteResearch()
    {
        wasteProductionModifier = 0.8f;
        List<Vector3Int?> structureCellPositions = placementManager.GetAllPositionOfTypeCellSatisfying(typeof(StructureCell));
        foreach (Vector3Int? structureCellPosition in structureCellPositions)
        {
            if (structureCellPosition != null)
            {
                StructureCell structureCell = (StructureCell) placementManager.GetCellAtPosition((Vector3Int) structureCellPosition);
                structureCell.WasteProduction *= wasteProductionModifier;
            }
        }
        reduceWasteUnlocked = true;
    }
    public void OnClickReduceCostButton()
    {
        if(currentlyResearching || reduceCostUnlocked)
            return;
        float reduceCostResearchTime = 30.0f;
        timeOfCurrentResearch = reduceCostResearchTime;
        Invoke(nameof(OnFinishReduceCostResearch), reduceCostResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(ReduceCostButton, reduceCostResearchTime * speedUpModifier);
        
    }
    private void OnFinishReduceCostResearch()
    {
        costModifier = 0.9f;
        List<Vector3Int?> structureCellPositions = placementManager.GetAllPositionOfTypeCellSatisfying(typeof(StructureCell));
        foreach (Vector3Int? structureCellPosition in structureCellPositions)
        {
            if (structureCellPosition != null)
            {
                StructureCell structureCell = (StructureCell) placementManager.GetCellAtPosition((Vector3Int) structureCellPosition);
                structureCell.Cost *= costModifier;
            }
        }
        reduceCostUnlocked = true;
    }
    public void OnSmogEventButton()
    {
        if(currentlyResearching || smogEventUnlocked)
            return;
        float smogEventTime = 30.0f;
        timeOfCurrentResearch = smogEventTime;
        Invoke(nameof(OnFinishSmogEvent), smogEventTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(SmogEventButton, smogEventTime * speedUpModifier);
        
    }
    private void OnFinishSmogEvent()
    {
        naturalDisasters.ResearchSmogModifier = 0.5f;
        smogEventUnlocked = true;
    }
    public void OnHeatwaveEventButton()
    {
        if(currentlyResearching || heatwaveEventUnlocked)
            return;
        float heatwaveEventTime = 30.0f;
        timeOfCurrentResearch = heatwaveEventTime;
        Invoke(nameof(OnFinishHeatwaveEvent), heatwaveEventTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(HeatwaveEventButton, heatwaveEventTime * speedUpModifier);
        
    }
    private void OnFinishHeatwaveEvent()
    {
        naturalDisasters.ResearchHeatwaveModifier = 0.5f;
        heatwaveEventUnlocked = true;
    }
    public void OnAcidRainEventButton()
    {
        if(currentlyResearching || acidRainEventUnlocked)
            return;
        float acidRainEventTime = 30.0f;
        timeOfCurrentResearch = acidRainEventTime;
        Invoke(nameof(OnFinishAcidRainEvent), acidRainEventTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(AcidRainEventButton, acidRainEventTime * speedUpModifier);
        
    }
    private void OnFinishAcidRainEvent()
    {
        acidRainEventUnlocked = true;
        naturalDisasters.ResearchAcidRainModifier = 0.5f;
        
    }
    public void OnClickFactorButton()
    {
        if(currentlyResearching || factorUnlocked)
            return;
        float factorResearchTime = 30.0f;
        timeOfCurrentResearch = factorResearchTime;
        Invoke(nameof(OnFinishFactorResearch), factorResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(FactorButton, factorResearchTime * speedUpModifier);
        
    }
    private void OnFinishFactorResearch()
    {
        factoryModifier = 0.5f;
        List<Vector3Int?> factoryPositions = placementManager.GetAllPositionOfTypeCellSatisfying(
            typeof(IndustryCell),
            (cell) => placementManager.CheckIfCellIsOfBuildingType(cell, BuildingType.Factory));
        foreach (Vector3Int? factoryPosition in factoryPositions)
        {
            if (factoryPosition != null)
            {
                IndustryCell factoryCell = (IndustryCell) placementManager.GetCellAtPosition((Vector3Int) factoryPosition);
                factoryCell.Co2Production *= factoryModifier;
                factoryCell.AirPollutionProduction *= factoryModifier;
                
            }
        }
        factorUnlocked = true;
    }
    public void OnClickCropButton()
    {
        if(currentlyResearching || cropUnlocked)
            return;
        float cropResearchTime = 30.0f;
        timeOfCurrentResearch = cropResearchTime;
        Invoke(nameof(OnFinishCropResearch), cropResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(CropButton, cropResearchTime * speedUpModifier);
        
    }
    private void OnFinishCropResearch()
    {
        vegetableModifier = 1.5f;
        List<Vector3Int?> cropPositions = placementManager.GetAllPositionOfTypeCellSatisfying(
            typeof(IndustryCell),
            (cell) => placementManager.CheckIfCellIsOfBuildingType(cell, BuildingType.Crop));
        foreach (Vector3Int? cropPosition in cropPositions)
        {
            if (cropPosition != null)
            {
                IndustryCell cropCell = (IndustryCell) placementManager.GetCellAtPosition((Vector3Int) cropPosition);
                cropCell.VegetablesProduced *= vegetableModifier;
            }
        }
        cropUnlocked = true;
        
    }
    public void OnClickLivestockButton()
    {
        if(currentlyResearching || livestockUnlocked)
            return;
        float livestockResearchTime = 30.0f;
        timeOfCurrentResearch = livestockResearchTime;
        Invoke(nameof(OnFinishLivestockResearch), livestockResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(LivestockButton, livestockResearchTime * speedUpModifier);
    }
    private void OnFinishLivestockResearch()
    {
        meatModifier = 1.5f;
        List<Vector3Int?> livestockPositions = placementManager.GetAllPositionOfTypeCellSatisfying(
            typeof(IndustryCell),
            (cell) => placementManager.CheckIfCellIsOfBuildingType(cell, BuildingType.Livestock));
        foreach (Vector3Int? livestockPosition in livestockPositions)
        {
            if (livestockPosition != null)
            {
                IndustryCell livestockCell = (IndustryCell) placementManager.GetCellAtPosition((Vector3Int) livestockPosition);
                livestockCell.MeatProduced *= meatModifier;
            }
        }
        livestockUnlocked = true;
        
    }
    public void OnSpeedResearchButton()
    {
        if(currentlyResearching || speedResearchUnlocked)
            return;
        float onSpeedResearchTime = 30f;
        timeOfCurrentResearch = onSpeedResearchTime;
        Invoke(nameof(OnFinishSpeedResearch), onSpeedResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(SpeedResearchButton, onSpeedResearchTime * speedUpModifier);
        
    }
    private void OnFinishSpeedResearch()
    {
        speedUpModifier = 0.75f;
        speedResearchUnlocked = true;
    }
    public void OnUnlockResearchButton()
    {
        if(currentlyResearching || unlockResearchUnlocked)
            return;
        float onUnlockResearchTime = 30f;
        timeOfCurrentResearch = onUnlockResearchTime;
        Invoke(nameof(OnFinishUnlockResearch), onUnlockResearchTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(UnlockResearchButton, onUnlockResearchTime * speedUpModifier);
        
    }
    private void OnFinishUnlockResearch()
    {
        advancedResearchUnlocked = true;
        unlockResearchUnlocked = true;
    }
    public void OnAdvanceCaptureCO2Button()
    {
        if(currentlyResearching || !advancedResearchUnlocked || advanceCaptureCO2Unlocked)
            return;
        float onAdvanceCaptureCO2Time = 30f;
        timeOfCurrentResearch = onAdvanceCaptureCO2Time;
        Invoke(nameof(OnFinishAdvanceCaptureCO2), onAdvanceCaptureCO2Time * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(AdvanceCaptureCO2Button, onAdvanceCaptureCO2Time * speedUpModifier);
    }
    private void OnFinishAdvanceCaptureCO2()
    {
        advanceCaptureCO2Unlocked = true;
        // reduce co2 by percentage of it
        gameState.Co2Emissions -= gameState.Co2Emissions * 0.2f;
    }
    public void OnGeoengineeringSolutionsButton()
    {
        if(currentlyResearching || !advancedResearchUnlocked || geoengineeringSolutionsUnlocked)
            return;
        float onGeoengineeringSolutionsTime = 30f;
        timeOfCurrentResearch = onGeoengineeringSolutionsTime;
        Invoke(nameof(OnFinishGeoengineeringSolutions), onGeoengineeringSolutionsTime * speedUpModifier);
        currentlyResearching = true;
        ChangeColourOfButton(GeoengineeringSolutionsButton, onGeoengineeringSolutionsTime * speedUpModifier);
        
    }
    private void OnFinishGeoengineeringSolutions()
    {
        geoengineeringSolutionsUnlocked = true;
        // reduce temperature by percentage of it
        gameState.Temperature -= gameState.Temperature * 0.2f;
    }
    private void ChangeColourOfButton(Button button, float time)
    {
        ColorBlock colorBlock = button.colors;

        // Set both normalColor and highlightedColor to yellow
        colorBlock.normalColor = Color.yellow;
        colorBlock.highlightedColor = Color.yellow;

        button.colors = colorBlock;

        // Use coroutine to change color of button to green after a certain amount of time
        StartCoroutine(ChangeColorOfButtonCoroutine(button, time));
    }

    private IEnumerator ChangeColorOfButtonCoroutine(Button button, float time)
    {
        yield return new WaitForSeconds(time);

        ColorBlock colorBlock = button.colors;

        // Set both normalColor and highlightedColor to green
        colorBlock.normalColor = Color.green;
        colorBlock.highlightedColor = Color.green;

        button.colors = colorBlock;
    }



    
}

    

