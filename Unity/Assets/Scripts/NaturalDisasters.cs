using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HeatwaveManager : MonoBehaviour
{
    public GameState gameState;
    public PlacementManager placementManager;
    public GameObject fire;
    public Volume heatwaveVolume;
    private float _time;
    private float _fireTime;
    private float cooldownDuration = 1f; // 5 minutes cooldown
    private float heatwaveProbability = 0.02f; // Initial low probability
    private bool isHeatwaveActive = false;

    public GameObject acidRainPrefab;
    private GameObject _acidRain = null;
    private float _acidRainTime;
    private float acidRainProbability = 0.02f; // Initial low probability
    private bool isAcidRainActive = false;

    public GameObject smogPrefab;
    private GameObject _smog = null;
    private float _smogTime;
    private float smogProbability = 0.02f; // Initial low probability
    private bool isSmogActive = false;
    
    private void Update()
    {
        
        // Check if it's time for a heatwave and cooldown is over
        if (_time >= cooldownDuration)
        {
            HandleHeatwaveProbability();
            HandleAcidRainProbability();
            HandleSmogProbability();
            // Check if a heatwave should occur based on probability
            if (Random.value < heatwaveProbability && !isHeatwaveActive && !isAcidRainActive && !isSmogActive)
            {
                StartHeatwave();
                isHeatwaveActive = true;
                _time = 0;
            }

            // Check if a acidRain should occur based on probability
            if (Random.value < acidRainProbability && !isHeatwaveActive && !isAcidRainActive && !isSmogActive)
            {
                StartAcidRain();
                isAcidRainActive = true;
                _time = 0;
            }

            // Check if a smog should occur based on probability
            if (Random.value < smogProbability && !isHeatwaveActive && !isAcidRainActive && !isSmogActive)
            {
                StartSmog();
                isSmogActive = true;
                _time = 0;
            }

        }
        else _time += Time.deltaTime;
        if (isHeatwaveActive)
            HandleHeatwave();
        if (isAcidRainActive)
            HandleAcidRain();
        if (isSmogActive)
            HandleSmog();
    }

    private void StartHeatwave()
    {
        // Trigger your heatwave effects or events here
        Debug.Log("Heatwave Occurred!");
        if (heatwaveVolume != null)
        {
            ApplyHeatwaveSettings();
        }
        else
        {
            Debug.LogError("Heatwave Volume not assigned!");
        }
    }

    private void HandleHeatwave()
    {
        _time += Time.deltaTime;
        _fireTime += Time.deltaTime;
        // Start a fire at a random structure position every 5 seconds
        if (_fireTime >= 5f)
        {
            Vector3Int? randomPosition = placementManager.GetRandomPositionOfTypeCellSatisfying(typeof(StructureCell),
                (cell) => placementManager.IsStructureCellOnFire(cell));

            if (randomPosition != null)
            {
                Cell cell = placementManager.GetCellAtPosition((Vector3Int)randomPosition);
                if (cell is StructureCell structureCell)
                {
                    structureCell.IsOnFire = true;
                    structureCell.FirePrefab = Instantiate(fire, (Vector3Int)randomPosition, Quaternion.identity);
                    gameState.structuresOnFire.Add(structureCell);
                }
            }

            _fireTime = 0;
        }

        // Check if the heatwave need to be stopped
        if (_time >= 60f)
        {
            StopHeatwave();
            _time = 0;
            _fireTime = 0;
            isHeatwaveActive = false;
        }
    }

    private void StopHeatwave()
    {
        // Stop your heatwave effects or events here
        Debug.Log("Heatwave Stopped!");
        if (heatwaveVolume != null)
        {
            heatwaveVolume.enabled = false;
        }
        else
        {
            Debug.LogError("Heatwave Volume not assigned!");
        }
    }

    private void HandleHeatwaveProbability()
    {
        // Probability depends on current C02 emissions with a exponential function
        heatwaveProbability = Mathf.Pow(gameState.co2Emissions, 2) / 1000000;
    }

    // Start is called before the first frame update

    void ApplyHeatwaveSettings()
    {
        // Access Color Adjustments component from the Volume
        ColorAdjustments colorAdjustments;
        if (heatwaveVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            // Modify color grading settings for the heatwave
            colorAdjustments.colorFilter.value = new Color(1.0f, 0.5f, 0.0f, 1.0f); // Orange

            // Enable the volume to make the changes take effect
            heatwaveVolume.enabled = true;
        }
        else
        {
            Debug.LogError("Color Adjustments not found in the Heatwave Volume!");
        }
    }


    private void HandleAcidRainProbability()
    {
        // Probability depends on current C02 emissions with a exponential function
        acidRainProbability = Mathf.Pow(gameState.airPollution, 2) / 1000000;
    }

    private void StartAcidRain()
    {
        _acidRain = Instantiate(acidRainPrefab, new Vector3(6.17f, 5.59f, 5.72f), Quaternion.identity);
    }
    private void HandleAcidRain()
    {
        _acidRainTime += Time.deltaTime;
        if (_acidRainTime >= 60f)
        {
            StopAcidRain();
            _acidRainTime = 0;
            isAcidRainActive = false;
        }
        
    }
    private void StopAcidRain()
    {
        Destroy(_acidRain);
    }
    
    private void HandleSmogProbability()
    {
        // Probability depends on current C02 emissions with a exponential function
        smogProbability = Mathf.Pow(gameState.airPollution, 2) / 1000000;
    }

    private void StartSmog()
    {
        _smog = Instantiate(smogPrefab, new Vector3(6.009356f, 1.044306f, 6.765965f), Quaternion.identity);
    }
    private void HandleSmog()
    {
        _smogTime += Time.deltaTime;
        if (_smogTime >= 60f)
        {
            StopSmog();
            _smogTime = 0;
            isSmogActive = false;
        }
        
    }
    private void StopSmog()
    {
        Destroy(_smog);
    }
}
