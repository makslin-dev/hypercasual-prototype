using System;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance;

    private bool _canVibrate = true;
    public event Action<bool> OnVibrationSet;
    private void Awake()
    {
       Instance = this;
    }
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Start()
    {
        SetVibration();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    } 
    private void SubscribeToEvents()
    {
        GameManager.Instance.OnNextBlockStart += VibratePhone;
        EventBus.OnVibratePressed += ToggleVibration;
    }
    private void UnsubscribeFromEvents()
    {
        GameManager.Instance.OnNextBlockStart -= VibratePhone;
        EventBus.OnVibratePressed -= ToggleVibration;
    }
    private void SetVibration()
    {
        bool canVibrate = true;
        int vibrateIndex = PlayerPrefs.GetInt("VibrationEnabled", 1);
        if (vibrateIndex == 0)
        {
            canVibrate = false;
        }
        _canVibrate = canVibrate;
        print("Can vibrate" + _canVibrate);
        OnVibrationSet?.Invoke(canVibrate);
    }
    private void ToggleVibration()
    {
        _canVibrate = !_canVibrate;

        PlayerPrefs.SetInt("VibrationEnabled", _canVibrate ? 1 : 0);
        PlayerPrefs.Save();

        if (_canVibrate)
        {
            Handheld.Vibrate();
        }
    }

    private void VibratePhone()
    {
        if (_canVibrate)
        {
            Handheld.Vibrate();
        }
    }
}
