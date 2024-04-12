using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// Performs a callback after each step in a countdown.
/// The countdown can be enabled or disabled externally.
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    // Countdown time in seconds
    [SerializeField, Min(0), Tooltip("The total time for the countdown in seconds.")]
    private float _countdownTime = 1.0f;

    // Flag to control the countdown
    [SerializeField, Tooltip("Enable or disable the countdown.")]
    public bool countdownOn = false;

    // Array of callbacks to be invoked at each step of the countdown
    [SerializeField, Tooltip("The callbacks to be invoked at each step of the countdown.")]
    private UnityEvent[] _progressCallbacks;

    // Time per interval calculated based on the countdown time and the number of callbacks
    private float timePerInterval = 0f;

    // Current step number in the countdown
    private int currentStepNumber = 0;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Assert.IsTrue(_countdownTime >= 0, "Countdown Time must be positive.");
    }

    // OnValidate is called by Unity whenever a value in the inspector is changed
    private void OnValidate()
    {
        if (countdownOn)
        {
            StartCountDown();
        }
    }

    /// <summary>
    /// Starts the countdown.
    /// </summary>
    public void StartCountDown()
    {
        countdownOn = true;
        currentStepNumber = 0;
        timePerInterval = _countdownTime / (_progressCallbacks.Length - 1);
        Debug.Log("Starting Countdown for: " + _countdownTime + "s");
        StartCoroutine(RunTimeInterval());
    }

    /// <summary>
    /// Coroutine to run the time interval.
    /// </summary>
    IEnumerator RunTimeInterval()
    {
        if (currentStepNumber < _progressCallbacks.Length)
        {
            Debug.Log("Invoking element " + currentStepNumber);
            _progressCallbacks[currentStepNumber].Invoke();
            currentStepNumber++;
            yield return new WaitForSeconds(timePerInterval);
            StartCoroutine(RunTimeInterval());
        }
        else
        {
            Debug.Log("Timer Finished");
            countdownOn = false;
        }
    }
}