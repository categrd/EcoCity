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
    public DifferentScenariosManager differentScenariosManager;
    public UIController uiController;
    private float _temperature;
    public float Temperature
    {
        get => _temperature;
        set => _temperature = value;
    }
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
    
    
    private float _totalArea;
    
    private float _totalVegetablesProduced;
    private int _totalVegetablesConsumed;
    private float _totalMeatProduced;
    private int _totalMeatConsumed;
    
    private float _acidRainModifier = 1f;
    // set acid rain modifier method
    public float AcidRainModifier
    {
        get => _acidRainModifier;
        set => _acidRainModifier = value;
    }
    private float _smokeModifier = 1f;
    // set smoke modifier method
    public float SmokeModifier
    {
        get => _smokeModifier;
        set => _smokeModifier = value;
    }
    
    private int numberOfUniversity = 0;
    // set university built method
    public int NumberOfUniversity
    {
        get => numberOfUniversity;
        set => numberOfUniversity = value;
    }
    
    private int _totalCriminalsCovered;
    private int _totalFiresCovered;

    private int _totalNumberOfJobs;
    private int _totalUnemployed;
    private int _totalEmployed;
    private float _health;

    

    private float _timeWithNegativeMoney = 0f;
    private float _time;
    private float _co2Emissions;
    public float Co2Emissions
    {
        get => _co2Emissions;
        set => _co2Emissions = value;
    }

    private float _co2Produced;
    public float Co2Produced
    {
        get => _co2Produced;
        set => _co2Produced = value;
    }
    
    private float airPollution;
    public float AirPollution
    {
        get => airPollution;
        set => airPollution = value;
    }
    private float airPollutionProduced;
    public float AirPollutionProduced
    {
        get => airPollutionProduced;
        set => airPollutionProduced = value;
    }
    

    private void Start()
    {
        currentMoney = 10000000f;
        populationCapacity = 0;
        _time = 0f;
        _totalNumberOfJobs= 0;
        _co2Emissions = 0f;
        airPollution = 0f;
        _temperature = 20f;
        _totalArea = 0f;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= 1)
        {
            UpdateGameVariables();
            HandleLooseConditions();
            HandleWinConditions();
            _time = 0;
        }
        UpdateFire();
        _totalArea = placementManager.UpdateTotalArea();

    }
    
    private void HandleLooseConditions()
    {
        if (currentMoney < 0)
        {
            _timeWithNegativeMoney++;
        }
        else
        {
            _timeWithNegativeMoney = 0;
        }
        if (_timeWithNegativeMoney >= 60)
        {
            uiController.ShowGameOverText();
            Invoke(nameof(PrepareToStartNewScenario), 5f);
            
        }
    }
    private void PrepareToStartNewScenario()
    {
        differentScenariosManager.StartNewScenario(0);
    }
    private void HandleWinConditions()
    {
        
        
    }

    private void UpdateFire()
    {
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
        UpdatePopulationHealth();
        UpdateEmployment();
        UpdateTemperature();
        UpdateAcidRainModifier();
        UpdateStructuresOnFire();
        UpdateAirPollution();
        UpdateCo2Emissions();
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
    private void UpdatePopulationHealth()
    {
        //take into account the temperature and the smog modifier and sum by a constant
        _health = 100 - (_temperature - 20) * 0.5f - airPollution * 0.1f + 0.5f;
        if (_health < 10)
        {
            _health = 10;
        }
        else if (_health > 100)
        {
            _health = 100;
        }
    }
    
    private void UpdateAirPollution()
    {
        // update air pollution based on the air pollution produced by the structures but normalizing it by the total area
        // and substract it by a constant
        if(_totalArea != 0)
        {
            airPollution += airPollutionProduced / _totalArea - 0.001f * _totalArea;
        }
        // limit it to a range: min = 0, max = 100
        if (airPollution < 0)
        {
            airPollution = 0;
        }
        else if (airPollution > 100)
        {
            airPollution = 100;
        }
        
    }
    public float GetAirPollution()
    {
        return airPollution;
    }

    private void UpdateCo2Emissions()
    {
        // update co2 emissions based on the co2 produced by the structures but normalizing it by the total area
        // and substract it by a constant
        if(_totalArea != 0)
        {
            _co2Emissions += _co2Produced / _totalArea - 0.001f * _totalArea;
        }
        // limit it to a range: min = 0, max = 100
        if (_co2Emissions < 0)
        {
            _co2Emissions = 0;
        }
        else if (_co2Emissions > 100)
        {
            _co2Emissions = 100;
        }
    }
    public float GetCo2Emissions()
    {
       return _co2Emissions;
    }

    private void UpdateTemperature()
    {
        // update temperature based on co2 emissions, increase it by a fraction of the co2 emissions and decrease it by a constant
        _temperature += _co2Emissions * 0.0001f - 0.01f;
        // limit the temperature range
        if (_temperature < 15)
        {
            _temperature = 15;
        }
        else if (_temperature > 40)
        {
            _temperature = 40;
        }
    }
    private void UpdateAcidRainModifier()
    {
        _acidRainModifier -= 0.0001f;
        // limit the acid rain modifier range
        if (_acidRainModifier < 0f)
        {
            _acidRainModifier = 0f;
        }
        else if (_acidRainModifier > 0.9f)
        {
            _acidRainModifier = 0.9f;
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
                + GetVegetablesDemandSatisfactionRatio() + ((1-GetEmploymentRatio()) * employmentWeight - 0.5f) + _totalBeauty * beautyWeight) / 8 - _co2Emissions * 0.01f - airPollution * 0.01f;
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
        if(_totalVegetablesProduced * (1-_acidRainModifier) > totalVegetablesDemanded)
            return 1;
        if(totalVegetablesDemanded!=0)
            return (float) _totalVegetablesProduced * (1-_acidRainModifier)/ totalVegetablesDemanded;
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
        return _totalIncome  * GetJobsOccupiedRatio() - _totalCosts *  + (_totalGoodsProduced - _totalGoodsConsumed) * 10 + 
               (_totalVegetablesProduced * Mathf.Pow(1 - _acidRainModifier, 0.25f) - _totalVegetablesConsumed) * 10 + (_totalMeatProduced - _totalMeatConsumed) * 10
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
       int numberOfPatients = GetNumberOfPatients();
       if (numberOfPatients == 0)
           return 0;
       if(numberOfPatients <= _totalPatientsCovered)
           return 1;
       return _totalPatientsCovered/ numberOfPatients;
    }
    private int GetNumberOfPatients()
    {
        // number of patient is a percentage of the population and is affected by the health of the population
        return (int) Math.Ceiling(totalPopulation * _health * 0.01f);
    }
    
    public void UpdateGameVariablesWhenDestroying(Vector3Int position)
    {
        Cell cell = placementManager.GetCellAtPosition(position);
        _co2Produced -= cell.Co2Production;
        airPollutionProduced -= cell.AirPollutionProduction;
        
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
            if (publicServiceCell.BuildingType == BuildingType.University)
            {
                numberOfUniversity --;
            }
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
        _co2Produced += cell.Co2Production;
        airPollutionProduced += cell.AirPollutionProduction;
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
            // if cell is a university, unlock researches
            if (publicServiceCell.BuildingType == BuildingType.University)
            {
                numberOfUniversity ++;
            }
            
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
    
    
    public void SetParametersOfScenario(int scenario)
    {
        if (scenario == 0)
        {
            
        }

        if (scenario == 1)
        {
            
        }
        
    }

    public void ResetGameState()
    {
        // reset all the variables
        currentMoney = 10000000f;
        populationCapacity = 0;
        _time = 0f;
        _totalNumberOfJobs= 0;
        _totalVegetablesProduced = 0;
        _totalMeatProduced = 0;
        _totalGoodsProduced = 0;
        _totalEnergyProduced = 0;
        _totalWasteProduced = 0;
        _totalBeauty = 0;
        _totalCostumersCapacity = 0;
        _totalPatientsCovered = 0;
        _totalCriminalsCovered = 0;
        _totalIncome = 0;
        _totalEnergyConsumed = 0;
        _totalEmployed = 0;
        _totalUnemployed = 0;
        _totalResidenceComfort = 0;
        _totalFiresCovered = 0;
        _totalCosts = 0;
        _totalGoodsConsumed = 0;
        _totalVegetablesConsumed = 0;
        _totalMeatConsumed = 0;
        _totalWasteProduced = 0;
        _totalWasteDisposed = 0;
        _co2Emissions = 100f;
        airPollution = 100f;
        _temperature = 20f;
        _acidRainModifier = 1f;
        _smokeModifier = 1f;
        numberOfUniversity = 0;
        _health = 100;
        
        
    }

}
