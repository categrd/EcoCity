using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;
    
    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

    public RoadFixer roadFixer;
    RoadCell roadCell = new RoadCell();
    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int position)
    {

        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            //Debug.Log("Position is not in bound");
            return;
        }

        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            //Debug.Log("Position is not free");
            return;
        }
        if (placementMode == false)
        {
            temporaryPlacementPositions.Clear();
            roadPositionsToRecheck.Clear();

            placementMode = true;
            startPosition = position;

            temporaryPlacementPositions.Add(position);
            placementManager.PlaceTemporaryStructure<RoadCell>(position, roadFixer.deadEnd);

        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();
            temporaryPlacementPositions.Clear();

            foreach (var positionsToFix in roadPositionsToRecheck)
            {
                roadFixer.FixRoadAtPosition(placementManager, positionsToFix);
            }

            roadPositionsToRecheck.Clear();

            temporaryPlacementPositions = placementManager.GetPathBetween(startPosition, position);

            foreach (var temporaryPosition in temporaryPlacementPositions)
            {
                if (placementManager.CheckIfPositionIsFree(temporaryPosition) == false)
                {
                    roadPositionsToRecheck.Add(temporaryPosition);
                    continue;
                }  
                placementManager.PlaceTemporaryStructure<RoadCell>(temporaryPosition, roadFixer.deadEnd);
            }
        }

        FixRoadPrefabs();

    }

    private void FixRoadPrefabs()
    {
        foreach (var temporaryPosition in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);
            var neighbours = placementManager.GetNeighboursOfTypeFor<RoadCell>(temporaryPosition);
            
            foreach (var roadposition in neighbours)
            {
                Debug.Log(roadposition);
                if (roadPositionsToRecheck.Contains(roadposition)==false)
                {
                    roadPositionsToRecheck.Add(roadposition);
                }
            }
        }
        foreach (var positionToFix in roadPositionsToRecheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, positionToFix);
        }
    }

    public void FinishPlacingRoad()
    {
        placementMode = false;
        placementManager.AddTemporaryStructuresToStructureDictionary();
        if (temporaryPlacementPositions.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        temporaryPlacementPositions.Clear();
        startPosition = Vector3Int.zero;
    }

    public void FixRoadWhenDestroying(Vector3Int position)
    {
        Vector3Int[] positionsToCheck = {
            position + new Vector3Int(1, 0, 0),
            position - new Vector3Int(1, 0, 0),
            position + new Vector3Int(0, 0, 1),
            position + new Vector3Int(0, 0, -1)
        };

        foreach (Vector3Int item in positionsToCheck)
        {
            if (placementManager.CheckIfPositionIsOfType(item, roadFixer.roadCellType))
            {
                roadFixer.FixRoadAtPosition(placementManager, item);
            }
        }
    }

}
