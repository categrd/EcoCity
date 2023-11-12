using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public PlacementManager placementManager;
    
    public void UpdateGameVariablesWhenDestroying(Vector3Int position)
    {
        if (placementManager.CheckIfPositionIsOfType(position, typeof(ResidenceCell)))
        {
            //Update variables relative to ResidenceCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(SanityCell)))
        {
            //Update variables relative to SanityCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(RoadCell)))
        {
            //Update variables relative to RoadCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(EnergyProductionCell)))
        {
            //Update variables relative to EnergyProductionCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(EntertainmentCell)))
        {
            //Update variables relative to EntertainmentCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(IndustryCell)))
        {
            //Update variables relative to IndustryCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(PublicServiceCell)))
        {
            //Update variables relative to PublicServiceCell
        }
        if (placementManager.CheckIfPositionIsOfType(position, typeof(GarbageDisposalCell)))
        {
            //Update variables relative to GarbageDisposalCell
        }
    }
}
