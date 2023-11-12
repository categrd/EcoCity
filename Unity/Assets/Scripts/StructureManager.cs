using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
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
            Type structureClass = placementManager.GetTypeOfPosition(position);
            gameManager.UpdateGameVariablesWhenDestroying(position);
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
        }
    }
    public void PlaceHospital(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            SanityCell hospital = new SanityCell(BuildingType.Hospital);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, hospitalPrefab, hospital);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    public void PlaceHighDensityHouse(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            ResidenceCell highDensityHouse = new ResidenceCell(BuildingType.HighDensityHouse);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, highDensityHousePrefab, highDensityHouse);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    public void PlaceSolarPanel(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProduction solarPanel = new EnergyProduction(BuildingType.SolarPanel);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, solarPanelPrefab, solarPanel);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    public void PlaceWindTurbine(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProduction windTurbine = new EnergyProduction(BuildingType.WindTurbine);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, windTurbinePrefab, windTurbine);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    public void PlaceCarbonPowerPlant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProduction carbonPowerPlant = new EnergyProduction(BuildingType.CarbonPowerPlant);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, carbonPowerPlantPrefab, carbonPowerPlant);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    public void PlaceNuclearPlant(Vector3Int position)
    {
        if (CheckPositionBeforePlacement(position))
        {
            EnergyProduction nuclearPlant = new EnergyProduction(BuildingType.NuclearPlant);
            int randomIndex = GetRandomWeightedIndex(houseWeights);
            placementManager.PlaceObjectOnTheMap(position, nuclearPlantPrefab, nuclearPlant);
            AudioPlayer.instance.PlayPlacementSound();
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
