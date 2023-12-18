using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class NaturalDisasters : MonoBehaviour
{
    public GameState gameState;
    public PlacementManager placementManager;
    public GameObject fire;
    public Volume heatwaveVolume;
    private float _time;
    private float _fireTime;
    private float cooldownDuration = 20f; // 5 minutes cooldown
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
    private float researchSmogModifier = 1.0f;
    
    public float ResearchSmogModifier
    {
        get => researchSmogModifier;
        set => researchSmogModifier = value;
    }
    private float researchAcidRainModifier = 1.0f;
    public float ResearchAcidRainModifier
    {
        get => researchAcidRainModifier;
        set => researchAcidRainModifier = value;
    }
    private float researchHeatwaveModifier = 1.0f;
    public float ResearchHeatwaveModifier
    {
        get => researchHeatwaveModifier;
        set => researchHeatwaveModifier = value;
    }
    
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

    private void Start()
    {
        _fireTime = 20f;
    }

    private void HandleHeatwave()
    {
        _time += Time.deltaTime;
        _fireTime -=  Time.deltaTime;
        // Start a fire at a random structure position every 5 seconds
        if (_fireTime <= 10f * researchHeatwaveModifier)
        {
            Vector3Int? randomPosition = placementManager.GetRandomPositionOfTypeCellSatisfying(typeof(StructureCell),
                (cell) => placementManager.IsStructureCellNotOnFire(cell));

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
            _fireTime = 20;
        }
        // increase temperature
        gameState.Temperature += 0.1f * researchHeatwaveModifier*Time.deltaTime;

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
        heatwaveProbability = Mathf.Pow(gameState.Co2Emissions, 2) / 100000000;
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
        acidRainProbability = Mathf.Pow(gameState.AirPollution, 2) / 100000000;
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
        // apply consequences of acid rain
        gameState.AcidRainModifier += 0.01f * gameState.AirPollution * researchAcidRainModifier * Time.deltaTime;
        
    }
    private void StopAcidRain()
    {
        Destroy(_acidRain);
    }
    
    private void HandleSmogProbability()
    {
        // Probability depends on current C02 emissions with a exponential function
        smogProbability = Mathf.Pow(gameState.AirPollution, 2) / 100000000;
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
        // apply consequences of smog
        gameState.SmokeModifier += 0.01f * gameState.AirPollution * researchSmogModifier * Time.deltaTime;
    }
    private void StopSmog()
    {
        Destroy(_smog);
    }
}
