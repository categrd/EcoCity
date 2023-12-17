using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public Research research;
    public UIController uiController;
    public GameState gameState;
    private BuildingType _buildingType;
    public StructurePrefabWeighted[] housesPrefabs, specialPrefabs, bigStructuresPrefabs;
    public GameObject clinicPrefab,
        hospitalPrefab,
        highDensityHousePrefab,
        solarPanelPrefab,
        windTurbinePrefab,
        carbonPowerPlantPrefab,
        nuclearPlantPrefab,
        waterPlantPrefab,
        shopPrefab,
        restaurantPrefab,
        barPrefab,
        cinemaPrefab,
        universityPrefab,
        fireStationPrefab,
        policeStationPrefab,
        factoryPrefab,
        cropPrefab,
        livestockPrefab,
        landfillPrefab,
        incinerationPlantPrefab,
        wasteToEnergyPlantPrefab,
        bigParkPrefab;
    
    private GameObject _prefab;
    private Cell _structure;
    
    public PlacementManager placementManager;
    public GameManager gameManager;
    public RoadManager roadManager;
    

    private float[] houseWeights, specialWeights, bigStructureWeights;

    private void Start()
    {
        houseWeights = housesPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        specialWeights = specialPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
        bigStructureWeights = bigStructuresPrefabs.Select(prefabStats => prefabStats.weight).ToArray();
    }

    public void DestroyStructure(Vector3Int position)
    {
        if (!placementManager.CheckIfPositionIsFree(position))
        {
            gameState.UpdateGameVariablesWhenDestroying(position);
            placementManager.DestroyGameObjectAt(position);
            roadManager.FixRoadWhenDestroying(position);
        }
    }
    
    
    public void PlaceStructure(Vector3Int position, BuildingType buildingType,int structureRotation, bool temporaryPlacementMode)
    {
        var (structureWidth, structureHeight, cost) = Cell.GetAttributesForBuildingType(buildingType);
        
        if (CheckBigStructure(position, structureWidth, structureHeight ) )
        {
            switch (buildingType)
            {
                case BuildingType.House:
                    var house = new ResidenceCell(BuildingType.House);
                    house.BuildingType = BuildingType.House;
                    _structure = house;
                    int randomIndex = GetRandomWeightedIndex(houseWeights);
                    _prefab = housesPrefabs[randomIndex].prefab;
                    break;
                case BuildingType.HighDensityHouse:
                    var highDensityHouse = new ResidenceCell(BuildingType.HighDensityHouse);
                    highDensityHouse.BuildingType = BuildingType.HighDensityHouse;
                    _structure =  highDensityHouse;
                    _prefab = highDensityHousePrefab;
                    break;
                case BuildingType.Clinic:
                    var clinic = new SanityCell(BuildingType.Clinic);
                    clinic.BuildingType = BuildingType.Clinic;
                    _structure = clinic;
                    _prefab = clinicPrefab;
                    break;
                case BuildingType.Hospital:
                    var hospital = new SanityCell(BuildingType.Hospital);
                    hospital.BuildingType = BuildingType.Hospital;
                    _structure = hospital;
                    _prefab = hospitalPrefab;
                    break;
                case BuildingType.Restaurant:
                    var restaurant = new EntertainmentCell(BuildingType.Restaurant);
                    restaurant.BuildingType = BuildingType.Restaurant;
                    _structure = restaurant;
                    _prefab = restaurantPrefab;
                    break;
                case BuildingType.Shop:
                    var shop = new EntertainmentCell(BuildingType.Shop);
                    shop.BuildingType = BuildingType.Shop;
                    _structure = shop;
                    _prefab = shopPrefab;
                    break;
                case BuildingType.Bar:
                    var bar = new EntertainmentCell(BuildingType.Bar);
                    bar.BuildingType = BuildingType.Bar;
                    _structure = bar;
                    _prefab = barPrefab;
                    break;
                case BuildingType.Cinema:
                    var cinema = new EntertainmentCell(BuildingType.Cinema);
                    cinema.BuildingType = BuildingType.Cinema;
                    _structure = cinema;
                    _prefab = cinemaPrefab;
                    break;
                case BuildingType.University:
                    var university = new PublicServiceCell(BuildingType.University);
                    university.BuildingType = BuildingType.University;
                    _structure = university;
                    _prefab = universityPrefab;
                    break;
                case BuildingType.FireStation:
                    var fireStation = new PublicServiceCell(BuildingType.FireStation);
                    fireStation.BuildingType = BuildingType.FireStation;
                    _structure = fireStation;
                    _prefab = fireStationPrefab;
                    break;
                case BuildingType.PoliceStation:
                    var policeStation = new PublicServiceCell(BuildingType.PoliceStation);
                    policeStation.BuildingType = BuildingType.PoliceStation;
                    _structure = policeStation;
                    _prefab = policeStationPrefab;
                    break;
                case BuildingType.Factory:
                    var factory = new IndustryCell(BuildingType.Factory);
                    factory.BuildingType = BuildingType.Factory;
                    _structure = factory;
                    _prefab = factoryPrefab;
                    break;
                case BuildingType.Crop:
                    var crop = new IndustryCell(BuildingType.Crop);
                    crop.BuildingType = BuildingType.Crop;
                    _structure = crop;
                    _prefab = cropPrefab;
                    break;
                case BuildingType.Livestock:
                    var livestock = new IndustryCell(BuildingType.Livestock);
                    livestock.BuildingType = BuildingType.Livestock;
                    _structure = livestock;
                    _prefab = livestockPrefab;
                    break; 
                case BuildingType.Landfill:
                    var landfill = new GarbageDisposalCell(BuildingType.Landfill);
                    landfill.BuildingType = BuildingType.Landfill;
                    _structure = landfill;
                    _prefab = landfillPrefab;
                    break;
                case BuildingType.IncinerationPlant:
                    var incinerationPlant = new GarbageDisposalCell(BuildingType.IncinerationPlant);
                    incinerationPlant.BuildingType = BuildingType.IncinerationPlant;
                    _structure = incinerationPlant;
                    _prefab = incinerationPlantPrefab;
                    break;
                case BuildingType.WasteToEnergyPlant:
                    var wasteToEnergyPlant = new GarbageDisposalCell(BuildingType.WasteToEnergyPlant);
                    wasteToEnergyPlant.BuildingType = BuildingType.WasteToEnergyPlant;
                    _structure = wasteToEnergyPlant;
                    _prefab = wasteToEnergyPlantPrefab;
                    break;
                case BuildingType.SolarPanel:
                    var solarPanel = new EnergyProductionCell(BuildingType.SolarPanel);
                    solarPanel.BuildingType = BuildingType.SolarPanel;
                    solarPanel.EnergyProduced *= research.SolarPanelModifier;
                    _structure = solarPanel;
                    _prefab = solarPanelPrefab;
                    break;
                case BuildingType.WindTurbine:
                    var windTurbine = new EnergyProductionCell(BuildingType.WindTurbine);
                    windTurbine.BuildingType = BuildingType.WindTurbine;
                    windTurbine.EnergyProduced *= research.WindTurbineModifier;
                    _structure = windTurbine;  
                    _prefab = windTurbinePrefab;
                    break;
                case BuildingType.CarbonPowerPlant:
                    var carbonPowerPlant = new EnergyProductionCell(BuildingType.CarbonPowerPlant);
                    carbonPowerPlant.BuildingType = BuildingType.CarbonPowerPlant;
                    _structure = carbonPowerPlant;
                    _prefab = carbonPowerPlantPrefab;
                    break;
                case BuildingType.NuclearPlant:
                    var nuclearPlant = new EnergyProductionCell(BuildingType.NuclearPlant);
                    nuclearPlant.BuildingType = BuildingType.NuclearPlant;
                    _structure = nuclearPlant;
                    _prefab = nuclearPlantPrefab;
                    break;
                case BuildingType.WaterPlant:
                    var waterPlant = new EnergyProductionCell(BuildingType.WaterPlant);
                    waterPlant.BuildingType = BuildingType.WaterPlant;
                    _structure = waterPlant;
                    _prefab = waterPlantPrefab;
                    break;
                case BuildingType.BigPark:
                    var bigPark = new EntertainmentCell(BuildingType.BigPark);
                    bigPark.BuildingType = BuildingType.BigPark;
                    _structure = bigPark;
                    _prefab = bigParkPrefab;
                    break;
            }
            _structure.WasteProduction *= research.WasteProductionModifier;
            if(temporaryPlacementMode)
            {
                placementManager.PlaceTemporaryStructureWithButton(position, _prefab, buildingType);
            }
            else if (CheckIfEnoughMoney(cost))
            {
                placementManager.buildPermanent = true;
                placementManager.PlaceTemporaryStructureWithButton(position, _prefab, buildingType);
                _structure.Position = position;
                placementManager.PlaceObjectOnTheMap(position, _prefab, _structure, structureWidth, structureHeight);
                AudioPlayer.instance.PlayPlacementSound();
                gameState.UpdateGameVariablesWhenBuilding(position);
            }
            
        }
    }
    
    public bool CheckBigStructure(Vector3Int position, int width, int height)
    {
        bool nearRoad = false;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                
                if (DefaultCheck(newPosition)==false)
                {
                    return false;
                }
                if (nearRoad == false)
                {
                    nearRoad = RoadCheck(newPosition);
                }
            }
        }
        return nearRoad;
    }

    private int GetRandomWeightedIndex(float[] weights)
    {
        float sum = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            //0->weihg[0] weight[0]->weight[1]
            if(randomValue >= tempSum && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }
    private bool CheckIfEnoughMoney(int cost)
    {
        if (gameState.currentMoney >= cost)
        {
            return true;
        }
        uiController.ShowNotEnoughMoneyText();
        Debug.Log("Not enough money");
        return false;
    }
    public bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }

        if (RoadCheck(position) == false)
            return false;
        
        return true;
    }
    

    private bool RoadCheck(Vector3Int position)
    {
        RoadCell roadCell = new RoadCell();
        if (placementManager.GetNeighboursOfTypeFor<RoadCell>(position).Count <= 0)
        {
            Debug.Log("Must be placed near a road");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        return true;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0,1)]
    public float weight;
}
