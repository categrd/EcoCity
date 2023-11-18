using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
    public PlacementManager placementManager;
    public float currentMoney;
    private int _totalIncome;
    private int _totalCosts;
    public int population;
    private float _totalResidenceComfort;
    private float _populationHappiness;
    private float _totalBeauty;
    
    private int _totalPatientCapacity;
    
    private float _totalEnergyProduced;
    private float _totalEnergyConsumed;
    
    private float _totalWasteProduced;
    private float _totalWasteDisposed;
    
    private int _totalGoodsProduced;
    private int _totalGoodsConsumed;
    
    private int _totalVegetablesProduced;
    private int _totalVegetablesConsumed;
    private int _totalMeatProduced;
    private int _totalMeatConsumed;
    
    private int _totalCriminalsCovered;
    private int _totalFiresCovered;

    private int _totalNumberOfJobs;
    private int _totalUnemployed;
    private int _totalEmployed;
    private float _employmentRatio;
    private float _jobOccupiedRatio;
    
    private float _earnings;
    
    private float _time;

    private void Start()
    {
        currentMoney = 10000000f;
        population = 0;
        _time = 0f;
        _totalNumberOfJobs= 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time >= 1)
        {
            UpdateGameVariables();
            _time = 0;
        }
    }

    private void UpdateGameVariables()
    {
        if (population >= _totalNumberOfJobs)
        {
            _totalEmployed = _totalNumberOfJobs;
            _totalUnemployed = population - _totalNumberOfJobs;
        }
        else
        {
            _totalEmployed = population;
            _totalUnemployed = 0;
        }
        if(_totalNumberOfJobs!=0) 
            _jobOccupiedRatio= (float) _totalEmployed / _totalNumberOfJobs;
        _earnings = (_totalIncome - _totalCosts) * _jobOccupiedRatio;
        currentMoney += _earnings;
    }
    public void UpdateGameVariablesWhenDestroying(Vector3Int position)
    {
        Cell cell = placementManager.GetCellAtPosition(position);
        if (cell is ResidenceCell residenceCell)
        {
            //Update variables relative to ResidenceCell
            population -= residenceCell.NumberOfResidents;
            _totalIncome -= residenceCell.IncomeGenerated;
            _totalCosts -= residenceCell.MaintenanceCost;
           _totalEnergyConsumed -= residenceCell.EnergyConsumption;
           _totalBeauty -= residenceCell.Beauty;
           _totalResidenceComfort -= residenceCell.ComfortLevel;
           _totalWasteProduced -= residenceCell.WasteProduction;
           
        }
        if (cell is SanityCell sanityCell)
        {
            //Update variables relative to SanityCell
            _totalIncome -= sanityCell.IncomeGenerated;
            _totalCosts -= sanityCell.MaintenanceCost;
            _totalEnergyConsumed -= sanityCell.EnergyConsumption;
            _totalWasteProduced -= sanityCell.WasteProduction;
            _totalNumberOfJobs -= sanityCell.NumberOfEmployees;
            _totalPatientCapacity -= sanityCell.PatientCapacity;
        }
        if (cell is RoadCell roadCell)
        {
            //Update variables relative to RoadCell
        }
        if (cell is EnergyProductionCell energyProductionCell)
        {
            //Update variables relative to EnergyProductionCell
            _totalCosts -= energyProductionCell.MaintenanceCost;
            _totalEnergyProduced -= energyProductionCell.EnergyProduced;
            _totalWasteProduced -= energyProductionCell.WasteProduction;
            _totalBeauty -= energyProductionCell.Beauty;
            _totalNumberOfJobs -= energyProductionCell.NumberOfEmployees;
        }
        if (cell is EntertainmentCell entertainmentCell)
        {
            //Update variables relative to EntertainmentCell
            _totalIncome -= entertainmentCell.IncomeGenerated;
            _totalCosts -= entertainmentCell.MaintenanceCost;
            _totalEnergyConsumed -= entertainmentCell.EnergyConsumption;
            _totalBeauty -= entertainmentCell.Beauty;
            _totalWasteProduced -= entertainmentCell.WasteProduction;
            _totalNumberOfJobs -= entertainmentCell.NumberOfEmployees;
            _populationHappiness -= entertainmentCell.Happiness;
        }
        if (cell is IndustryCell industryCell)
        {
            //Update variables relative to IndustryCell
            _totalIncome -= industryCell.IncomeGenerated;
            _totalCosts -= industryCell.MaintenanceCost;
            _totalEnergyConsumed -= industryCell.EnergyConsumption;
            _totalBeauty -= industryCell.Beauty;
            _totalWasteProduced -= industryCell.WasteProduction;
            _totalNumberOfJobs -= industryCell.NumberOfEmployees;
            _totalGoodsProduced -= industryCell.GoodsProduced;
            _totalMeatProduced -= industryCell.MeatProduced;
            _totalVegetablesProduced -= industryCell.VegetablesProduced;
        }
        if (cell is PublicServiceCell publicServiceCell)
        {
            //Update variables relative to PublicServiceCell
            _totalIncome -= publicServiceCell.IncomeGenerated;
            _totalCosts -= publicServiceCell.MaintenanceCost;
            _totalEnergyConsumed -= publicServiceCell.EnergyConsumption;
            _totalBeauty -= publicServiceCell.Beauty;
            _totalWasteProduced -= publicServiceCell.WasteProduction;
            _totalNumberOfJobs -= publicServiceCell.NumberOfEmployees;
            _totalCriminalsCovered -= publicServiceCell.CriminalsCovered;
        }
        if (cell is GarbageDisposalCell garbageDisposalCell)
        {
            //Update variables relative to GarbageDisposalCell
            _totalCosts -= garbageDisposalCell.MaintenanceCost;
            _totalEnergyConsumed -= garbageDisposalCell.EnergyConsumption;
            _totalWasteDisposed -= garbageDisposalCell.GarbageDisposed;
            _totalNumberOfJobs -= garbageDisposalCell.NumberOfEmployees;
            _totalBeauty -= garbageDisposalCell.Beauty;
            
        }
    }
    public void UpdateGameVariablesWhenBuilding(Vector3Int position)
    {
        Cell cell = placementManager.GetCellAtPosition(position);
        if (cell is ResidenceCell residenceCell)
        {
            //Update variables relative to ResidenceCell
            population += residenceCell.NumberOfResidents;
            _totalIncome += residenceCell.IncomeGenerated;
            _totalCosts += residenceCell.MaintenanceCost;
            _totalEnergyConsumed += residenceCell.EnergyConsumption;
            _totalBeauty += residenceCell.Beauty;
            _totalResidenceComfort += residenceCell.ComfortLevel;
            _totalWasteProduced += residenceCell.WasteProduction;
            
        }
        if (cell is SanityCell sanityCell)
        {
            //Update variables relative to SanityCell
            _totalIncome += sanityCell.IncomeGenerated;
            _totalCosts += sanityCell.MaintenanceCost;
            _totalEnergyConsumed += sanityCell.EnergyConsumption;
            _totalWasteProduced += sanityCell.WasteProduction;
            _totalNumberOfJobs += sanityCell.NumberOfEmployees;
            _totalPatientCapacity += sanityCell.PatientCapacity;
            
        }
        if (cell is RoadCell roadCell)
        {
            //Update variables relative to RoadCell
        }
        if (cell is EnergyProductionCell energyProductionCell)
        {
            //Update variables relative to EnergyProductionCell
            _totalCosts += energyProductionCell.MaintenanceCost;
            _totalEnergyProduced += energyProductionCell.EnergyProduced;
            _totalWasteProduced += energyProductionCell.WasteProduction;
            _totalBeauty += energyProductionCell.Beauty;
            _totalNumberOfJobs += energyProductionCell.NumberOfEmployees;
            
        }

        if (cell is EntertainmentCell entertainmentCell)
        {
            //Update variables relative to EntertainmentCell
            _totalIncome += entertainmentCell.IncomeGenerated;
            _totalCosts += entertainmentCell.MaintenanceCost;
            _totalEnergyConsumed += entertainmentCell.EnergyConsumption;
            _totalBeauty += entertainmentCell.Beauty;
            _totalWasteProduced += entertainmentCell.WasteProduction;
            _totalNumberOfJobs += entertainmentCell.NumberOfEmployees;
            _populationHappiness += entertainmentCell.Happiness;
            
        }
        if (cell is IndustryCell industryCell)
        {
            //Update variables relative to IndustryCell
            _totalIncome += industryCell.IncomeGenerated;
            _totalCosts += industryCell.MaintenanceCost;
            _totalEnergyConsumed += industryCell.EnergyConsumption;
            _totalBeauty += industryCell.Beauty;
            _totalWasteProduced += industryCell.WasteProduction;
            _totalNumberOfJobs += industryCell.NumberOfEmployees;
            _totalGoodsProduced += industryCell.GoodsProduced;
            _totalMeatProduced += industryCell.MeatProduced;
            _totalVegetablesProduced += industryCell.VegetablesProduced;
            
        }
        if (cell is PublicServiceCell publicServiceCell)
        {
            //Update variables relative to PublicServiceCell
            _totalIncome += publicServiceCell.IncomeGenerated;
            _totalCosts += publicServiceCell.MaintenanceCost;
            _totalEnergyConsumed += publicServiceCell.EnergyConsumption;
            _totalBeauty += publicServiceCell.Beauty;
            _totalWasteProduced += publicServiceCell.WasteProduction;
            _totalNumberOfJobs += publicServiceCell.NumberOfEmployees;
            _totalCriminalsCovered += publicServiceCell.CriminalsCovered;
            
        }
        if (cell is GarbageDisposalCell garbageDisposalCell)
        {
            //Update variables relative to GarbageDisposalCell
            _totalCosts += garbageDisposalCell.MaintenanceCost;
            _totalEnergyConsumed += garbageDisposalCell.EnergyConsumption;
            _totalWasteDisposed += garbageDisposalCell.GarbageDisposed;
            _totalNumberOfJobs += garbageDisposalCell.NumberOfEmployees;
            _totalBeauty += garbageDisposalCell.Beauty;
        }
        currentMoney -= cell.Cost;
    }
}
