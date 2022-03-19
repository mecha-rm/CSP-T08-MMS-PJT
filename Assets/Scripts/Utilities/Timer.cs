using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// timer class
public class Timer : MonoBehaviour
{
    // if 'true', this is a countdown timer.
    [Tooltip("If 'true', the timer counts down. If false, the timer counts up.")]
    public bool countdown = false;

    // if 'true', the timer is paused.
    [Tooltip("Runs the timer if not paused.")]
    public bool paused = false;

    // if 'true', the timer starts running the moment the script starts.
    [Tooltip("Runs the timer when this script's Start() function is called.")]
    public bool runOnStart = true;

    // the current time.
    [Tooltip("The current time in seconds.")]
    public float currentTime = 0.0F;

    // the start time for the timer.
    [Tooltip("The start of timer.")]
    public float startTime = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        // starts the timer.
        if (runOnStart)
            StartTimer();

    }

    // starts the timer (unpauses by default).
    public void StartTimer()
    {
        currentTime = startTime;
        paused = false;
    }

    // stops the timer.
    public void StopTimer()
    {
        currentTime = startTime; // reset time
        paused = true; // timer should not be running.
    }

    // if the timer paused?
    public bool IsPaused()
    {
        return paused;
    }

    // sets if the timer should be paused.
    public void SetPaused(bool pause)
    {
        paused = pause;
    }

    // pauses the timer.
    public void Pause()
    {
        SetPaused(true);
    }

    // unpauses the timer.
    public void Unpause()
    {
        SetPaused(true);
    }

    // gets the current time.
    public float GetCurrentTime()
    {
        return currentTime;
    }

    // gets the amount of time that has passed.
    // changing time settings while the timer is running makes this value inaccurate.
    public float GetElapsedTime()
    {
        if(countdown) // countdown timer
        {
            return startTime - currentTime;
        }
        else // count up timer
        {
            return currentTime - startTime;
        }
    }

    // checks if the timer is finished.
    // if a counting up timer this will always return false.
    public bool IsFinished()
    {
        // counting up to infinity, so it's never finished.
        if (!countdown)
            return false;

        // if time is negative or 0, it's done.
        return (currentTime <= 0);
    }

    // Update is called once per frame
    void Update()
    {
        // timer is not paused.
        if(!paused)
        {
            // time in seconds.
            // if countdowning down, subtract delta time.
            // if counting up, add delta time.
            currentTime += Time.deltaTime * ((countdown) ? -1 : 1);

            // keeping it at 0 if counting down.
            if (currentTime <= 0)
                currentTime = 0.0F;
        }

    }

    // returns the time as a string.
    public string TimerToString(bool showDecimal = true)
    {
        // 1 hour converted to seconds.
        const float HourToSec = 3600.0F;

        // 1 minute converted to seconds
        const float MinToSec = 60.0F;

        // GETTING TIMES //

        // time in seconds (timer is already in seconds.)
        float seconds = currentTime;

        // time in full hours
        float hours = Mathf.Floor(seconds / HourToSec); // 3600 seconds in an hour.
        seconds -= hours * HourToSec; // reduce amount of hours.

        // time in full minutes
        float minutes = Mathf.Floor(seconds / MinToSec); // 60 seconds in an hour.
        seconds -= minutes * MinToSec; // reduce amount of hours.


        // TIME STRING //
        // formatting the string.
        string timeString = 
            hours.ToString() + ":" + 
            minutes.ToString() + ":" + 
            (showDecimal ? seconds.ToString() : Mathf.Ceil(seconds).ToString());
        
        // return the time string.
        return timeString;
    }
}
