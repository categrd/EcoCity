using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
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
        wasteToEnergyPlantPrefab;
        
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

    public void PlaceTemporaryHouse(Vector3Int position)
    {
        int randomIndex = GetRandomWeightedIndex(houseWeights);
        placementManager.PlaceTemporaryStructureWithButton(position, housesPrefabs[randomIndex].prefab);
    }
    public void PlaceHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            ResidenceCell house = new ResidenceCell(BuildingType.House);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, housesPrefabs[randomIndex].prefab);
            placementManager.PlaceObjectOnTheMap(position, housesPrefabs[randomIndex].prefab, house);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
        
    }
    public void PlaceTemporaryHighDensityHouse(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, highDensityHousePrefab);
    }
    public void PlaceHighDensityHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            ResidenceCell highDensityHouse = new ResidenceCell(BuildingType.HighDensityHouse);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, highDensityHousePrefab, highDensityHouse);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryClinic(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, clinicPrefab);
    }
    public void PlaceClinic(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            SanityCell clinic = new SanityCell(BuildingType.Clinic);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, clinicPrefab);
            placementManager.PlaceObjectOnTheMap(position, clinicPrefab, clinic);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryHospital(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, hospitalPrefab);
    }
    public void PlaceHospital(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            SanityCell hospital = new SanityCell(BuildingType.Hospital);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, hospitalPrefab, hospital);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryRestaurant(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, restaurantPrefab);
    }
    public void PlaceRestaurant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EntertainmentCell restaurant = new EntertainmentCell(BuildingType.Restaurant);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, restaurantPrefab);
            placementManager.PlaceObjectOnTheMap(position, restaurantPrefab, restaurant);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryShop(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, shopPrefab);
    }
    public void PlaceShop(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EntertainmentCell shop = new EntertainmentCell(BuildingType.Shop);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, shopPrefab);
            placementManager.PlaceObjectOnTheMap(position, shopPrefab, shop);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryBar(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, barPrefab);
    }
    public void PlaceBar(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EntertainmentCell bar = new EntertainmentCell(BuildingType.Bar);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, barPrefab);
            placementManager.PlaceObjectOnTheMap(position, barPrefab, bar);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryCinema(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, cinemaPrefab);
    }
    public void PlaceCinema(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EntertainmentCell cinema = new EntertainmentCell(BuildingType.Cinema);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, cinemaPrefab);
            placementManager.PlaceObjectOnTheMap(position, cinemaPrefab, cinema);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryUniversity(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, universityPrefab);
    }
    public void PlaceUniversity(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            PublicServiceCell university = new PublicServiceCell(BuildingType.University);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, universityPrefab);
            placementManager.PlaceObjectOnTheMap(position, universityPrefab, university);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryFireStation(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, fireStationPrefab);
    }
    public void PlaceFireStation(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            PublicServiceCell fireStation = new PublicServiceCell(BuildingType.FireStation);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, fireStationPrefab);
            placementManager.PlaceObjectOnTheMap(position, fireStationPrefab, fireStation);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryPoliceStation(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, policeStationPrefab);
    }
    public void PlacePoliceStation(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            PublicServiceCell policeStation = new PublicServiceCell(BuildingType.PoliceStation);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, policeStationPrefab);
            placementManager.PlaceObjectOnTheMap(position, policeStationPrefab, policeStation);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryFactory(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, factoryPrefab);
    }
    public void PlaceFactory(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            IndustryCell factory = new IndustryCell(BuildingType.Factory);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, factoryPrefab);
            placementManager.PlaceObjectOnTheMap(position, factoryPrefab, factory);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryCrop(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, cropPrefab);
    }
    public void PlaceCrop(Vector3Int position)
    {
        if (CheckBigStructure(position, 2 , 2))
        {
            IndustryCell crop = new IndustryCell(BuildingType.Crop);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, cropPrefab);
            placementManager.PlaceObjectOnTheMap(position, cropPrefab, crop,2,2);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryLivestock(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, livestockPrefab);
    }
    public void PlaceLivestock(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            IndustryCell livestock = new IndustryCell(BuildingType.Livestock);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, livestockPrefab);
            placementManager.PlaceObjectOnTheMap(position, livestockPrefab, livestock);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryLandfill(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, landfillPrefab);
    }
    public void PlaceLandfill(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            GarbageDisposalCell landfill = new GarbageDisposalCell(BuildingType.Landfill);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, landfillPrefab);
            placementManager.PlaceObjectOnTheMap(position, landfillPrefab, landfill);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryIncinerationPlant(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, incinerationPlantPrefab);
    }
    public void PlaceIncinerationPlant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            GarbageDisposalCell incinerationPlant = new GarbageDisposalCell(BuildingType.IncinerationPlant);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, incinerationPlantPrefab);
            placementManager.PlaceObjectOnTheMap(position, incinerationPlantPrefab, incinerationPlant);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryWasteToEnergyPlant(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, wasteToEnergyPlantPrefab);
    }
    public void PlaceWasteToEnergyPlant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            GarbageDisposalCell wasteToEnergyPlant = new GarbageDisposalCell(BuildingType.WasteToEnergyPlant);
            placementManager.buildPermanent = true;
            placementManager.PlaceTemporaryStructureWithButton(position, wasteToEnergyPlantPrefab);
            placementManager.PlaceObjectOnTheMap(position, wasteToEnergyPlantPrefab, wasteToEnergyPlant);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporarySolarPanel(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, solarPanelPrefab);
    }
    public void PlaceSolarPanel(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProductionCell solarPanel = new EnergyProductionCell(BuildingType.SolarPanel);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, solarPanelPrefab, solarPanel);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryWindTurbine(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, windTurbinePrefab);
    }
    public void PlaceWindTurbine(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProductionCell windTurbine = new EnergyProductionCell(BuildingType.WindTurbine);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, windTurbinePrefab, windTurbine);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryCarbonPowerPlant(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, carbonPowerPlantPrefab);
    }
    public void PlaceCarbonPowerPlant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProductionCell carbonPowerPlant = new EnergyProductionCell(BuildingType.CarbonPowerPlant);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, carbonPowerPlantPrefab, carbonPowerPlant);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    public void PlaceTemporaryNuclearPlant(Vector3Int position)
    {
        placementManager.PlaceTemporaryStructureWithButton(position, nuclearPlantPrefab);
    }
    public void PlaceNuclearPlant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProductionCell nuclearPlant = new EnergyProductionCell(BuildingType.NuclearPlant);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, nuclearPlantPrefab, nuclearPlant);
            AudioPlayer.instance.PlayPlacementSound();
            gameState.UpdateGameVariablesWhenBuilding(position);
        }
    }
    

    internal void PlaceBigStructure(Vector3Int position)
    {
        int width = 2;
        int height = 2;
        if(CheckBigStructure(position, width , height))
        {
            StructureCell structureCell = new StructureCell();
            int randomIndex = GetRandomWeightedIndex(bigStructureWeights);
            placementManager.PlaceObjectOnTheMap(position, bigStructuresPrefabs[randomIndex].prefab, structureCell, width, height);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    private bool CheckBigStructure(Vector3Int position, int width, int height)
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

    public void PlaceSpecial(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            StructureCell structureCell = new StructureCell();
            int randomIndex = GetRandomWeightedIndex(specialWeights);
            placementManager.PlaceObjectOnTheMap(position, specialPrefabs[randomIndex].prefab, structureCell);
            AudioPlayer.instance.PlayPlacementSound();
        }
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
