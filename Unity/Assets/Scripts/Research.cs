using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Slider = UnityEngine.UIElements.Slider;

public class Research : MonoBehaviour
{
    public PlacementManager placementManager;
    
    public Button RenewableEnergyButton;
    public Button GreenConstructionButton;
    public Button NaturalDisastersButton;
    public Button IndustryButton;
    public Button AdvancedResearchButton;
    public Button ResearchEfficiencyButton;
    
    public GameObject RenewableEnergyPanel;
    public GameObject GreenConstructionPanel;
    public GameObject NaturalDisastersPanel;
    public GameObject IndustryPanel;
    public GameObject AdvancedResearchPanel;
    public GameObject ResearchEfficiencyPanel;

    
    public Text TimeText;
    public GameObject TimeSlider;
    
    
    
    private float windTurbineModifier = 1.0f;
    public float WindTurbineModifier => windTurbineModifier;
    private float solarPanelModifier = 1.0f;
    public float SolarPanelModifier => solarPanelModifier;
    private float wasteProductionModifier = 1.0f;
    public float WasteProductionModifier => wasteProductionModifier;
    private float vegetableModifier = 1.0f;
    public float VegetableModifier => vegetableModifier;
    private float meatModifier = 1.0f;
    public float MeatModifier => meatModifier;
        
    private float costModifier = 1.0f;
    public float CostModifier => costModifier;
    private float speedUpModifier = 1.0f;
    private bool advancedResearchUnlocked = false;
    private bool currentlyResearching = false;
    private float timeOfCurrentResearch = 0.0f;
    private float _timeResearch;

    private void Start()
    {
        
        
    }
    private void Update()
    {
        // if research is currently happening, we increase time of a variable Time by Time.deltaTime and update the text
        // by having it the percentage of the research done and change the slider value
        if (currentlyResearching)
        {
            _timeResearch += Time.deltaTime;
            TimeText.text = (_timeResearch / timeOfCurrentResearch * 100).ToString("F2") + "%";
            if(_timeResearch >= timeOfCurrentResearch)
            {
                currentlyResearching = false;
                _timeResearch = 0.0f;
            }
        }
        else
        {
            TimeText.text = "0%";
            _timeResearch = 0.0f;
        }
        
    }

    public void OnButtonRenewableEnergyButton()
    {
        ActivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }

    public void OnButtonGreenConstructionButton()
    {
        ActivatePanel(GreenConstructionPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonNaturalDisastersButton()
    {
        ActivatePanel(NaturalDisastersPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonIndustryButton()
    {
        ActivatePanel(IndustryPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(AdvancedResearchPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonAdvancedResearchButton()
    {
        ActivatePanel(AdvancedResearchPanel);
        DeactivatePanel(RenewableEnergyPanel);
        DeactivatePanel(GreenConstructionPanel);
        DeactivatePanel(NaturalDisastersPanel);
        DeactivatePanel(IndustryPanel);
        DeactivatePanel(ResearchEfficiencyPanel);
    }
    
    public void OnButtonResearchEfficiencyButton()
    {
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
    public void OnClickWindEnergyButton()
    {
        if(currentlyResearching)
            return;
        float windResearchTime = 30.0f;
        timeOfCurrentResearch = windResearchTime;
        
        Invoke(nameof(OnFinishWindEnergyResearch), windResearchTime);
        currentlyResearching = true;
        
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
    }
    public void OnClickSolarEnergyButton()
    {
        if(currentlyResearching)
            return;
        float solarResearchTime = 30.0f;
        timeOfCurrentResearch = solarResearchTime;
        Invoke(nameof(OnFinishSolarEnergyResearch), solarResearchTime * speedUpModifier );
        currentlyResearching = true;
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
    }
    public void OnClickGreenMaterialsButton()
    {
        if(currentlyResearching)
            return;
        float greenMaterialsResearchTime = 30.0f;
        timeOfCurrentResearch = greenMaterialsResearchTime;
        Invoke(nameof(OnFinishGreenMaterialsResearch), greenMaterialsResearchTime * speedUpModifier);
        currentlyResearching = true;
    }
    private void OnFinishGreenMaterialsResearch()
    {
        
    }
    public void OnClickReduceWasteButton()
    {
        if(currentlyResearching)
            return;
        float reduceWasteResearchTime = 30.0f;
        timeOfCurrentResearch = reduceWasteResearchTime;
        Invoke(nameof(OnFinishReduceWasteResearch), reduceWasteResearchTime * speedUpModifier);
        currentlyResearching = true;
        
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
    }
    public void OnClickReduceCostButton()
    {
        if(currentlyResearching)
            return;
        float reduceCostResearchTime = 30.0f;
        timeOfCurrentResearch = reduceCostResearchTime;
        Invoke(nameof(OnFinishReduceCostResearch), reduceCostResearchTime * speedUpModifier);
        currentlyResearching = true;
        
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
    }
    public void OnSmogEventButton()
    {
        if(currentlyResearching)
            return;
        float smogEventTime = 30.0f;
        timeOfCurrentResearch = smogEventTime;
        Invoke(nameof(OnFinishSmogEvent), smogEventTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishSmogEvent()
    {
        
    }
    public void OnHeatwaveEventButton()
    {
        if(currentlyResearching)
            return;
        float heatwaveEventTime = 30.0f;
        timeOfCurrentResearch = heatwaveEventTime;
        Invoke(nameof(OnFinishHeatwaveEvent), heatwaveEventTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishHeatwaveEvent()
    {
        
    }
    public void OnAcidRainEventButton()
    {
        if(currentlyResearching)
            return;
        float acidRainEventTime = 30.0f;
        timeOfCurrentResearch = acidRainEventTime;
        Invoke(nameof(OnFinishAcidRainEvent), acidRainEventTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishAcidRainEvent()
    {
        
    }
    public void OnClickFactorButton()
    {
        if(currentlyResearching)
            return;
        float factorResearchTime = 30.0f;
        timeOfCurrentResearch = factorResearchTime;
        Invoke(nameof(OnFinishFactorResearch), factorResearchTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishFactorResearch()
    {
        
    }
    public void OnClickCropButton()
    {
        if(currentlyResearching)
            return;
        float cropResearchTime = 30.0f;
        timeOfCurrentResearch = cropResearchTime;
        Invoke(nameof(OnFinishCropResearch), cropResearchTime * speedUpModifier);
        currentlyResearching = true;
        
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
        
        
    }
    public void OnClickLivestockButton()
    {
        if(currentlyResearching)
            return;
        float livestockResearchTime = 30.0f;
        timeOfCurrentResearch = livestockResearchTime;
        Invoke(nameof(OnFinishLivestockResearch), livestockResearchTime * speedUpModifier);
        currentlyResearching = true;
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
        
    }
    public void OnSpeedResearchButton()
    {
        if(currentlyResearching)
            return;
        float onSpeedResearchTime = 30f;
        timeOfCurrentResearch = onSpeedResearchTime;
        Invoke(nameof(OnFinishSpeedResearch), onSpeedResearchTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishSpeedResearch()
    {
        speedUpModifier = 0.75f;
        
    }
    public void OnUnlockResearchButton()
    {
        if(currentlyResearching)
            return;
        float onUnlockResearchTime = 30f;
        timeOfCurrentResearch = onUnlockResearchTime;
        Invoke(nameof(OnFinishUnlockResearch), onUnlockResearchTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishUnlockResearch()
    {
        advancedResearchUnlocked = true;
    }
    public void OnAdvanceCaptureCO2Button()
    {
        if(currentlyResearching || !advancedResearchUnlocked)
            return;
        float onAdvanceCaptureCO2Time = 30f;
        timeOfCurrentResearch = onAdvanceCaptureCO2Time;
        Invoke(nameof(OnFinishAdvanceCaptureCO2), onAdvanceCaptureCO2Time * speedUpModifier);
        currentlyResearching = true;
    }
    private void OnFinishAdvanceCaptureCO2()
    {
        
    }
    public void OnGeoengineeringSolutionsButton()
    {
        if(currentlyResearching || !advancedResearchUnlocked)
            return;
        float onGeoengineeringSolutionsTime = 30f;
        timeOfCurrentResearch = onGeoengineeringSolutionsTime;
        Invoke(nameof(OnFinishGeoengineeringSolutions), onGeoengineeringSolutionsTime * speedUpModifier);
        currentlyResearching = true;
        
    }
    private void OnFinishGeoengineeringSolutions()
    {
        
    }
}

    

