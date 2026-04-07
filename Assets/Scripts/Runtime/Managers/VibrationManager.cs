using System;
using UnityEngine;
using Zenject;

public class VibrationManager : IInitializable, IDisposable
{
    private bool _canVibrate = true;
    public event Action<bool> OnVibrationSet;
    public void Initialize()
    {
        SubscribeToEvents();
        SetVibration();
    }
    private void SubscribeToEvents()
    {
        EventBus.OnNextBlockStart += VibratePhone;
        EventBus.OnVibratePressed += ToggleVibration;
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnNextBlockStart -= VibratePhone;
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

    public void Dispose()
    {
        UnsubscribeFromEvents();
    }

}
