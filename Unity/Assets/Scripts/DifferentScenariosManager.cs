using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentScenariosManager : MonoBehaviour
{
    public GameState gameState;
    public PlacementManager placementManager;
    
    public void StartNewScenario(int scenario)
    {
        // reload current scene to have a new scenario
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        gameState.ResetGameState();
        placementManager.ResetGrid();
        gameState.SetParametersOfScenario(scenario);
        
        
        
    }
}
