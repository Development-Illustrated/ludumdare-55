using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    // Flicker a light source
    private  Light lightSource;
    public float minIntensity;
    public float maxIntensity;
    public float minFlickerTime;
    public float maxFlickerTime;
    
    private float flickerCounter;
    private float flickerTime;
    private float targetIntensity;

    void Awake()
    {
        lightSource = GetComponent<Light>();
    }

    void Start()
    {
        flickerCounter = 0;
        flickerTime = Random.Range(minFlickerTime, maxFlickerTime);
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        if (flickerCounter >= 0)
        {
            flickerCounter -= Time.deltaTime;
        }
        else
        {
            lightSource.intensity = targetIntensity;
            flickerCounter = flickerTime;
            flickerTime = Random.Range(minFlickerTime, maxFlickerTime);
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
