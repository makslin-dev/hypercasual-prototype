using System;
using UnityEditor.PackageManager;
using UnityEngine;

public static class EventBus
{
    public static event Action OnVibratePressed;
    public static event Action OnSoundPressed;
    public static event Action OnSettingsPressed;
    public static event Action OnTutorialPressed;
    public static event Action<BackCaller> OnBackPressed;
    public static event Action OnNextBlockStart;
    public static event Action OnGameStarted;
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
    public static void TutorialPressed()
    {
        OnTutorialPressed?.Invoke();
    }
    public static void BackPress(BackCaller caller)
    {
        OnBackPressed?.Invoke(caller);
    }
    public static void StartNextBlock()
    {
        OnNextBlockStart?.Invoke();
    }
    public static void StartGame()
    {
        OnGameStarted?.Invoke();
    }
}
