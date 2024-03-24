using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Performs a callback after a countdown timer has elapsed.
/// The countdown can be enabled or disabled externally.
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float _countdownTime = 1.0f;

    [SerializeField]
    private bool countdownOn = false;

    [SerializeField]
    private UnityEvent _callback;

    [SerializeField]
    private UnityEvent[] _progressCallbacks;

    private float _countdownTimer;

    public bool CountdownOn
    {
        get => countdownOn;

        set
        {
            Debug.Log("Start Value: " + value);

            if (value)
            {
                countdownOn = value;
                _countdownTimer = _countdownTime;
                Debug.Log("Starting Countdown for: " + _countdownTimer + "s");

            }
        }
    }

    // is called by Unity when ever a value in the inspector is changed
    private void OnValidate()
    {
        CountdownOn = countdownOn;
    }

    private void Awake()
    {
        Assert.IsTrue(_countdownTime >= 0, "Countdown Time must be positive.");
    }

    private void Update()
    {
        if (!countdownOn || _countdownTimer < 0)
        {
            return;
        }

        _countdownTimer -= Time.deltaTime;

        if (_countdownTimer > 0f)
        {
            var timePerInterval = _countdownTime / (_progressCallbacks.Length - 1);
            var currentIndex = Mathf.RoundToInt(_countdownTimer * timePerInterval);

            Debug.Log("current index " + currentIndex);
            _progressCallbacks[currentIndex].Invoke();
        }

        if (_countdownTimer < 0f)
        {
            Debug.Log("Finished");
            _countdownTimer = -1f;
            _callback.Invoke();
            countdownOn = false;
            return;
        }
    }
}
