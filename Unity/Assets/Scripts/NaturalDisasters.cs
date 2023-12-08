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
    private float cooldownDuration = 100f; // 5 minutes cooldown
    private float heatwaveProbability = 0.02f; // Initial low probability
    private bool isHeatwaveActive = false;
    
    private void Update()
    {
        // Check if it's time for a heatwave and cooldown is over
        if (_time  >= cooldownDuration )
        {
            HandleHeatwaveProbability();
            // Check if a heatwave should occur based on probability
            if (Random.value < heatwaveProbability)
            {
                StartHeatwave();
                _time = 0;
            }
        }
        else _time += Time.deltaTime;
        if (isHeatwaveActive)
        {
            _time += Time.deltaTime;
            _fireTime += Time.deltaTime;
            // Start a fire at a random structure position every 5 seconds
            if (_fireTime >= 5f)
            {
                //TODO fix GetRandomPositionOfTypeCell function 
                Vector3Int? randomPosition = placementManager.placementGrid.GetRandomPositionOfTypeCell(typeof(StructureCell));
                if (randomPosition != null)
                {
                    GameObject firePrefab = Instantiate(fire, (Vector3Int)randomPosition,Quaternion.identity);
                    Destroy(firePrefab, 30f);
                }
                _fireTime = 0;
            }
            // Check if the heatwave need to be stopped
            if(_time >= 30f)
            {
                StopHeatwave();
                _time = 0;
                _fireTime = 0;
                isHeatwaveActive = false;
            }
        }
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
}