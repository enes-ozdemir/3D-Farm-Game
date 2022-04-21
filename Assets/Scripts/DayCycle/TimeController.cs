using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [Header("Time")] [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private TextMeshProUGUI timeText;
    private DateTime _currentTime;


    [SerializeField] private Light sunLight;

    [SerializeField] private float sunRiseHour;
    private TimeSpan _sunRiseTime;

    [SerializeField] private float sunSetHour;
    private TimeSpan _sunSetTime;

    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;

    [SerializeField] private AnimationCurve lightChangeCurve;

    [SerializeField] private float maxSunIntensity;
    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonIntensity;

    [SerializeField] private Material sunRiseMaterial;
    [SerializeField] private Material sunUpMaterial;
    [SerializeField] private Material sunDownMaterial;
    [SerializeField] private Material nightMaterial;


    // Start is called before the first frame update
    void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        _sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        _sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (_currentTime.TimeOfDay > _sunRiseTime && _currentTime.TimeOfDay < _sunSetTime)
        {
            TimeSpan sunRiseToSunSetDuration = CalculateTimeDifference(_sunRiseTime, _sunSetTime);
            TimeSpan timeSinceSunRise = CalculateTimeDifference(_sunRiseTime, _currentTime.TimeOfDay);

            double percentage = timeSinceSunRise.TotalMinutes / sunRiseToSunSetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float) percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(_sunSetTime, _sunRiseTime);
            TimeSpan timeSinceSunSet = CalculateTimeDifference(_sunSetTime, _currentTime.TimeOfDay);

            double percentage = timeSinceSunSet.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float) percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        UpdateSkyBox();
    }

    private void UpdateSkyBox()
    {
        switch (_currentTime.Hour)
        {
            case 7:
                RenderSettings.skybox=sunRiseMaterial;
                break;
            case 12:
                RenderSettings.skybox=sunUpMaterial;
                break;
            case 17:
                RenderSettings.skybox=sunDownMaterial;
                break;
            case 22:
                RenderSettings.skybox=nightMaterial;
                break;
        }
        
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight =
            Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private void UpdateTimeOfDay()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null) timeText.text = _currentTime.ToString("HH:mm");
    }
}