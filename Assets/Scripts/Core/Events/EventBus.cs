using System;
using UnityEngine;

public static class EventBus
{
    public static event Action OnVibratePressed;
    public static event Action OnSoundPressed;
    public static event Action OnSettingsPressed;
    public static event Action OnBackPressed;
    public static void VibratePress()
    {
        OnVibratePressed?.Invoke();
    }
    public static void SoundPressed()
    {
        OnSoundPressed?.Invoke();
    }
    public static void SettingsPress()
    {
        OnSettingsPressed?.Invoke();
    }
    public static void BackPress()
    {
        OnBackPressed?.Invoke();
    }
}
