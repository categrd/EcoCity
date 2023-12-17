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
    
    public float windTurbineModifier = 1.0f;
    public float solarPanelModifier = 1.0f;
    
   

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
        windTurbineModifier = 1.5f;
        
        
    }
    public void OnClickSolarEnergyButton()
    {
        solarPanelModifier = 1.5f;
        
    }
    public void OnClickGreenMaterialsButton()
    {
        
    }
    public void OnClickReduceWasteButton()
    {
        
    }
    public void OnClickReduceCostButton()
    {
        
    }
    public void OnSmogEventButton()
    {
        
    }
    public void OnHeatwaveEventButton()
    {
        
    }
    public void OnAcidRainEventButton()
    {
        
    }
    public void OnClickFactorButton()
    {
        
    }
    public void OnClickCropButton()
    {
        
    }
    public void OnClickLivestockButton()
    {
        
    }
    public void OnSpeedResearchButton()
    {
        
    }
    public void OnUnlockResearchButton()
    {
        
    }
    public void OnAdvanceCaptureCO2Button()
    {
        
    }
    public void OnGeoengineeringSolutionsButton()
    {
        
    }
}

    

