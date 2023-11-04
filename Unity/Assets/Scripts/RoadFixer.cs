using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;
    Type roadCellType = typeof(RoadCell);

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int temporaryPosition)
    {
        //[right, up, left, down]
        var result = placementManager.GetNeighbourTypesFor(temporaryPosition);
        int roadCount = 0;

        roadCount = result.Count(type => type == roadCellType);
        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(placementManager, result, temporaryPosition);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(placementManager, result, temporaryPosition))
                return;
            CreateCorner(placementManager, result, temporaryPosition);
        }
        else if (roadCount == 3)
        {
            Create3Way(placementManager, result, temporaryPosition);
        }
        else
        {
            Create4Way(placementManager, result, temporaryPosition);
        }
    }

    private void Create4Way(PlacementManager placementManager, Type[] result, Vector3Int temporaryPosition)
    {
        placementManager.ModifyStructureModel(temporaryPosition, fourWay, Quaternion.identity);
    }

    //[left, up, right, down]
    private void Create3Way(PlacementManager placementManager, Type[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == roadCellType && result[2] == roadCellType && result[3] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.identity);
        }
        else if (result[2] == roadCellType && result[3] == roadCellType && result[0] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 90, 0));
        }
        else if (result[3] == roadCellType && result[0] == roadCellType && result[1] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 180, 0));
        }
        else if (result[0] == roadCellType && result[1] == roadCellType && result[2] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, threeWay, Quaternion.Euler(0, 270, 0));
        }
    }

    //[left, up, right, down]
    private void CreateCorner(PlacementManager placementManager, Type[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == roadCellType && result[2] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 90, 0));
        }
        else if (result[2] == roadCellType && result[3] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 180, 0));
        }
        else if (result[3] == roadCellType && result[0] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.Euler(0, 270, 0));
        }
        else if (result[0] == roadCellType && result[1] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, corner, Quaternion.identity);
        }
    }

    //[left, up, right, down]
    private bool CreateStraightRoad(PlacementManager placementManager, Type[] result, Vector3Int temporaryPosition)
    {
        if (result[0] == roadCellType && result[2] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.identity);
            return true;
        }
        else if (result[1] == roadCellType && result[3] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, roadStraight, Quaternion.Euler(0, 90, 0));
            return true;
        }
        return false;
    }

    //[left, up, right, down]
    private void CreateDeadEnd(PlacementManager placementManager, Type[] result, Vector3Int temporaryPosition)
    {
        if (result[1] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 270, 0));
        }
        else if (result[2] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.identity);
        }
        else if (result[3] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 90, 0));
        }
        else if (result[0] == roadCellType)
        {
            placementManager.ModifyStructureModel(temporaryPosition, deadEnd, Quaternion.Euler(0, 180, 0));
        }
    }
}
