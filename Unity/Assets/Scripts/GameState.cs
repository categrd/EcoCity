using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameState : MonoBehaviour
{

    public List<StructureCell> structuresOnFire = new List<StructureCell>();
    public List<StructureCell> structuresOnFireToDestroy = new List<StructureCell>();
    public PlacementManager placementManager;
    public PopulationManager populationManager;
    public TransportManager transportManager;
    public float currentMoney;
    private int _totalIncome;
    private int _totalCosts;    
    public int populationCapacity;
    public int PopulationCapacity => populationCapacity;    
    
    public int totalPopulation;
    
    private float _totalResidenceComfort;
    
    private float _totalBeauty;
    
    private int _totalPatientsCovered;
    
    private int _totalCostumersCapacity;
    
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

    private bool[] scientificProgress;

    
    private float _time;
    public float co2Emissions;
    public float airPollution;

    private void Start()
    {
        currentMoney = 10000000f;
        populationCapacity = 0;
        _time = 0f;
        _totalNumberOfJobs= 0;
        scientificProgress = new bool[20];
        //try high value of co2 emissions for testing
        co2Emissions = 100f;
        // try high value of air pollution for testing
        airPollution = 100f;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= 1)
        {
            UpdateGameVariables();
            _time = 0;
        }

        foreach (var structureCell in structuresOnFire)
        {
            if (structureCell.IsOnFire && structureCell.IsFireTruckOnTheWay == false)
            {
                List<Vector3Int> neighboursRoads =
                    placementManager.GetNeighboursOfTypeFor<RoadCell>(structureCell.Position);
                // if there's at least a road next to the person, we continue with the code
                if (neighboursRoads.Count > 0)
                {
                    Vector3Int targetPosition = neighboursRoads[0];
                    // get random position of a fire station by checking if it's a fire station (using buildingType) and if it's not busy
                    Vector3Int? fireStationPosition = placementManager.GetRandomPositionOfTypeCellSatisfying(
                        typeof(PublicServiceCell),
                        (cell) => placementManager.CheckIfCellIsOfBuildingType(cell, BuildingType.FireStation));
                    // send fire truck to fire
                    
                    if (fireStationPosition != null)
                    {
                        PublicServiceCell fireStationCell =
                            (PublicServiceCell)placementManager.GetCellAtPosition((Vector3Int)fireStationPosition);
                        if (fireStationCell.FireTrucks > 0 && fireStationCell.EmployeeList.Count > 0)
                        {
                            foreach(Person person in fireStationCell.EmployeeList)
                            {
                                if (person.isPersonFree)
                                {
                                    Person fireman = person;
                                    fireman.personPrefab = null;
                                    fireman.carPrefab = null;
                                    fireman.busyTime = 0;
                                    fireman.isPersonFree = true;
                                    transportManager.SendFireTruckToFire(fireman,
                                            (Vector3Int)fireStationPosition,
                                        targetPosition, structureCell, transportManager.fireTruckPrefab);
                                    fireStationCell.FireTrucks--;
                                    structureCell.IsFireTruckOnTheWay = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void UpdateGameVariables()
    {
        UpdateTotalPopulation();
        UpdateEmployment();
        UpdateStructuresOnFire();
        populationManager.FindJob();
        currentMoney += GetEarnings();
        populationManager.HandlePeopleMovement();
        
    }
    private void UpdateStructuresOnFire()
    {
        foreach (var structureCell in structuresOnFire)
        {
            if (structureCell.IsOnFire)
            {
                structureCell.TimeOnFire ++;
                if (structureCell.TimeOnFire >= 60f)
                {
                    structureCell.IsOnFire = false;
                    structureCell.TimeOnFire = 0;
                    structuresOnFireToDestroy.Add(structureCell);
                    
                }
            }
        }
        foreach (var structureCell in structuresOnFireToDestroy)
        {
            Destroy(structureCell.FirePrefab);
            placementManager.DestroyGameObjectAt(structureCell.Position);
            structuresOnFire.Remove(structureCell);
        }
    }
    
    private void UpdateTotalPopulation()
    {
        if(totalPopulation < populationCapacity)
        {   
            int peopleToCreate = Math.Min(GetPopulationChange(), populationCapacity - totalPopulation);
            totalPopulation += peopleToCreate;
            if(GetPopulationChange() >= 0)
            {
                for (int i = 0; i < peopleToCreate; i++)
                {
                    populationManager.CreateNewPerson();
                }
            }
            
            else
            {
                for (int i = 0; i < -GetPopulationChange(); i++)
                {
                    populationManager.RemoveRandomPerson();
                }
            }
        }
        if( totalPopulation > populationCapacity)
        {
            totalPopulation = populationCapacity;
            Debug.Log("Population is over capacity");
            
            for(int i = 0; i < totalPopulation - populationCapacity; i++)
            {
                populationManager.RemoveRandomPerson();
            }
            
        } 
    }

    private void UpdateEmployment()
    {
        _totalUnemployed = populationManager.joblessPeople.Count;
        _totalEmployed = totalPopulation - _totalUnemployed;
    }
    public float GetEmploymentRatio()
    {
        if(totalPopulation!=0)
            return (float) _totalEmployed / totalPopulation;
        return 0;
    }
    private float GetQualityOfLife()
    { 
        var employmentWeight = 6f;
        var beautyWeight = 0.5f;
        return (_totalResidenceComfort + (GetCriminalsCoveredRatio() - 0.1f)+ (GetPatientsCoveredRatio() - 0.1f) + GetGoodsDemandSatisfactionRatio() + GetMeatDemandSatisfactionRatio()
                + GetVegetablesDemandSatisfactionRatio() + ((1-GetEmploymentRatio()) * employmentWeight - 0.5f) + _totalBeauty * beautyWeight) / 8;
    }

    private int GetPopulationChange()
    {
        return (int) Math.Ceiling(populationCapacity * GetQualityOfLife() * 0.01f);
    }
    
    
    public float GetGoodsDemandSatisfactionRatio()
    {   var totalGoodsDemanded = totalPopulation * 5 / 100;
        if(_totalGoodsProduced > totalGoodsDemanded)
            return 1;
        if(totalGoodsDemanded!=0)
            return (float) _totalGoodsProduced / totalGoodsDemanded;
        return 0;
    }
    public float GetVegetablesDemandSatisfactionRatio()
    {
        var totalVegetablesDemanded = totalPopulation * 10 / 100;
        if(_totalVegetablesProduced > totalVegetablesDemanded)
            return 1;
        if(totalVegetablesDemanded!=0)
            return (float) _totalVegetablesProduced / totalVegetablesDemanded;
        return 0;
    }
    public float GetMeatDemandSatisfactionRatio()
    {
        var totalMeatDemanded = totalPopulation * 5 / 100;
        if(_totalMeatProduced > totalMeatDemanded)
            return 1;
        if(totalMeatDemanded!=0)
            return (float) _totalMeatProduced / totalMeatDemanded;
        return 0;
    }
    private float GetEarnings()
    {
        return _totalIncome  * GetJobsOccupiedRatio() - _totalCosts + (_totalGoodsProduced - _totalGoodsConsumed) * 10 + 
               (_totalVegetablesProduced - _totalVegetablesConsumed) * 10 + (_totalMeatProduced - _totalMeatConsumed) * 10
               + _totalEnergyProduced - _totalEnergyConsumed;
    }
    public float GetJobsOccupiedRatio()
    {
        if(_totalNumberOfJobs!=0) 
            return (float) _totalEmployed / _totalNumberOfJobs;
        return 0;
    }
    public float GetCriminalsCoveredRatio()
    {
        int numberOfCriminals = totalPopulation * 5 / 100;
        if (numberOfCriminals == 0)
            return 0;
        if(numberOfCriminals <= _totalCriminalsCovered)
            return 1;
        return _totalCriminalsCovered/ numberOfCriminals;
    }

    public float GetPatientsCoveredRatio()
    {
       int numberOfPatients = totalPopulation * 15 / 100;
       if (numberOfPatients == 0)
           return 0;
       if(numberOfPatients <= _totalPatientsCovered)
           return 1;
       return _totalPatientsCovered/ numberOfPatients;
    }
    
    public void UpdateGameVariablesWhenDestroying(Vector3Int position)
    {
        Cell cell = placementManager.GetCellAtPosition(position);
        
        if (cell is StructureCell)
        {
            if(cell.GetType() != typeof(ResidenceCell))
            {
                populationManager.RemovePeopleJob(position);
            }
        }
        if (cell is ResidenceCell residenceCell)
        {
            //Update variables relative to ResidenceCell
            populationCapacity -= residenceCell.NumberOfResidentsCapacity;
            _totalIncome -= residenceCell.IncomeGenerated;
            _totalCosts -= residenceCell.MaintenanceCost;
           _totalEnergyConsumed -= residenceCell.EnergyConsumption;
           _totalBeauty -= residenceCell.Beauty;
           _totalResidenceComfort -= residenceCell.ComfortLevel;
           _totalWasteProduced -= residenceCell.WasteProduction;
           //Remove people from residence
           populationManager.RemovePeopleAt((Vector3Int)position);
        }
        if (cell is SanityCell sanityCell)
        {
            //Update variables relative to SanityCell
            _totalIncome -= sanityCell.IncomeGenerated;
            _totalCosts -= sanityCell.MaintenanceCost;
            _totalEnergyConsumed -= sanityCell.EnergyConsumption;
            _totalWasteProduced -= sanityCell.WasteProduction;
            _totalNumberOfJobs -= sanityCell.NumberOfEmployeesCapacity;
            _totalPatientsCovered -= sanityCell.PatientCapacity;
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
            _totalNumberOfJobs -= energyProductionCell.NumberOfEmployeesCapacity;
        }
        if (cell is EntertainmentCell entertainmentCell)
        {
            //Update variables relative to EntertainmentCell
            _totalIncome -= entertainmentCell.IncomeGenerated;
            _totalCosts -= entertainmentCell.MaintenanceCost;
            _totalEnergyConsumed -= entertainmentCell.EnergyConsumption;
            _totalBeauty -= entertainmentCell.Beauty;
            _totalWasteProduced -= entertainmentCell.WasteProduction;
            _totalNumberOfJobs -= entertainmentCell.NumberOfEmployeesCapacity;
            _totalCostumersCapacity -= entertainmentCell.CostumersCapacity;
        }
        if (cell is IndustryCell industryCell)
        {
            //Update variables relative to IndustryCell
            _totalIncome -= industryCell.IncomeGenerated;
            _totalCosts -= industryCell.MaintenanceCost;
            _totalEnergyConsumed -= industryCell.EnergyConsumption;
            _totalBeauty -= industryCell.Beauty;
            _totalWasteProduced -= industryCell.WasteProduction;
            _totalNumberOfJobs -= industryCell.NumberOfEmployeesCapacity;
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
            _totalNumberOfJobs -= publicServiceCell.NumberOfEmployeesCapacity;
            _totalCriminalsCovered -= publicServiceCell.CriminalsCovered;
        }
        if (cell is GarbageDisposalCell garbageDisposalCell)
        {
            //Update variables relative to GarbageDisposalCell
            _totalCosts -= garbageDisposalCell.MaintenanceCost;
            _totalEnergyConsumed -= garbageDisposalCell.EnergyConsumption;
            _totalWasteDisposed -= garbageDisposalCell.GarbageDisposed;
            _totalNumberOfJobs -= garbageDisposalCell.NumberOfEmployeesCapacity;
            _totalBeauty -= garbageDisposalCell.Beauty;
            
        }
    }
    public void UpdateGameVariablesWhenBuilding(Vector3Int position)
    {
        Cell cell = placementManager.GetCellAtPosition(position);
        if (cell is ResidenceCell residenceCell)
        {
            //Update variables relative to ResidenceCell
            populationCapacity += residenceCell.NumberOfResidentsCapacity;
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
            _totalNumberOfJobs += sanityCell.NumberOfEmployeesCapacity;
            _totalPatientsCovered += sanityCell.PatientCapacity;
            
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
            _totalNumberOfJobs += energyProductionCell.NumberOfEmployeesCapacity;
            
        }

        if (cell is EntertainmentCell entertainmentCell)
        {
            //Update variables relative to EntertainmentCell
            _totalIncome += entertainmentCell.IncomeGenerated;
            _totalCosts += entertainmentCell.MaintenanceCost;
            _totalEnergyConsumed += entertainmentCell.EnergyConsumption;
            _totalBeauty += entertainmentCell.Beauty;
            _totalWasteProduced += entertainmentCell.WasteProduction;
            _totalNumberOfJobs += entertainmentCell.NumberOfEmployeesCapacity;
            _totalCostumersCapacity += entertainmentCell.CostumersCapacity;
            
        }
        if (cell is IndustryCell industryCell)
        {
            //Update variables relative to IndustryCell
            _totalIncome += industryCell.IncomeGenerated;
            _totalCosts += industryCell.MaintenanceCost;
            _totalEnergyConsumed += industryCell.EnergyConsumption;
            _totalBeauty += industryCell.Beauty;
            _totalWasteProduced += industryCell.WasteProduction;
            _totalNumberOfJobs += industryCell.NumberOfEmployeesCapacity;
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
            _totalNumberOfJobs += publicServiceCell.NumberOfEmployeesCapacity;
            _totalCriminalsCovered += publicServiceCell.CriminalsCovered;
            
        }
        if (cell is GarbageDisposalCell garbageDisposalCell)
        {
            //Update variables relative to GarbageDisposalCell
            _totalCosts += garbageDisposalCell.MaintenanceCost;
            _totalEnergyConsumed += garbageDisposalCell.EnergyConsumption;
            _totalWasteDisposed += garbageDisposalCell.GarbageDisposed;
            _totalNumberOfJobs += garbageDisposalCell.NumberOfEmployeesCapacity;
            _totalBeauty += garbageDisposalCell.Beauty;
        }
        currentMoney -= cell.Cost;
    }

    public int GetScientificProgressLevel()
    {
        int numberOfScientificProgress = 0;
        foreach (bool progress in scientificProgress)
        {
            if (progress)
            {
                numberOfScientificProgress++;
            }
        }
        return numberOfScientificProgress;
    }
    
    public float GetScientificProgressRatio()
    {
        int numberOfScientificProgress = 0;
        foreach (bool progress in scientificProgress)
        {
            if (progress)
            {
                numberOfScientificProgress++;
            }
        }
        return (float) numberOfScientificProgress / scientificProgress.Length;
    }
    
    public bool upgradeScientificProgress()
    {
        try
        {
            scientificProgress[Array.IndexOf(scientificProgress, false)] = true;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
       
    }

}
