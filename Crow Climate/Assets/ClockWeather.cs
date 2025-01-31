using System;
using UnityEngine;
using TMPro;  

public class ClockWeather : MonoBehaviour
{
    private float timeInSeconds = 0f;
    private int hours = 0;
    private int minutes = 0;

    // UI 
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI weatherText;

    // Weather
    private string[] weatherConditions = { "Sunny", "Rainy", "Cloudy", "Stormy", "Snowy" };
    private string currentWeather;

    // Update interval 
    public float timeSpeed = 1f;

    // Weather change interval
    public float weatherChangeInterval = 300f;  // Change weather every 5 minutes of game time

    private float weatherTimer = 0f;

    void Start()
    {
        ChangeWeather();
        ResetClock(); 
    }

    void Update()
    {
        // Update time
        timeInSeconds += Time.deltaTime * timeSpeed;

        // Calculate hours and minutes
        hours = Mathf.FloorToInt(timeInSeconds / 60) % 24;
        minutes = Mathf.FloorToInt(timeInSeconds) % 60;

        // Format 
        string timeString = string.Format("{0:D2}:{1:D2}", hours, minutes);

        // Update the clock 
        if (clockText != null)
        {
            clockText.text = timeString;
        }

        // Update the weather
        weatherTimer += Time.deltaTime * timeSpeed;
        if (weatherTimer >= weatherChangeInterval)
        {
            ChangeWeather();  
            weatherTimer = 0f;  
        }

        // Update the weather 
        if (weatherText != null)
        {
            weatherText.text = "Weather: " + currentWeather;
        }
    }

    // Change the weather randomly 
    private void ChangeWeather()
    {
        int randomIndex = UnityEngine.Random.Range(0, weatherConditions.Length);
        currentWeather = weatherConditions[randomIndex];
    }

    public void SetTime(int newHours, int newMinutes)
    {
        hours = newHours;
        minutes = newMinutes;
        timeInSeconds = newHours * 60 + newMinutes;
    }

    // Reset clock time
    public void ResetClock()
    {
        SetTime(6, 0);  // Default start time
    }
}
