using System;
using UnityEngine;

public static class EventBus
{
    public static event Action OnVibratePressed;
    public static event Action OnSettingsPressed;

    public static void VibratePress()
    {
        OnVibratePressed.Invoke();
    }
    public static void SettingsPress()
    {
        OnSettingsPressed.Invoke();
    }
}
