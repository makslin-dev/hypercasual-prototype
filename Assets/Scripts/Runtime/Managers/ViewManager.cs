using System;
using UnityEngine;
using Zenject;

public class ViewManager : IInitializable, IDisposable
{
    private bool _isVibrationEnabled = true;

    private Canvas[] _views;
    [Inject]
    private void Construct(Canvas[] views)
    {
        _views = views;
    }

    public void Initialize()
    {
        SubscribeToEvents();
        InitViews();
    }
    private void SubscribeToEvents()
    {
        EventBus.OnSettingsPressed += ShowSettings;
        EventBus.OnBackPressed += ShowMenuView;
    }
    private void InitViews()
    {
        SwitchView(0);
        SetStartingSettings();
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnSettingsPressed -= ShowSettings;
        EventBus.OnBackPressed -= ShowMenuView;
    }
    private void SwitchView(int id)
    {
        foreach (var view in _views)
        {
            view.enabled = false;
        }
        _views[id].enabled = true;
    }
    private void SetStartingSettings()
    {
        int vibrationSave = PlayerPrefs.GetInt("VibrationEnabled");
        bool isVibrate = false;
        if (vibrationSave == 1)
        {
            isVibrate = true;
        }
        _isVibrationEnabled = isVibrate;
    }
    private void ShowSettings()
    {
        SwitchView(2);
    }
    private void ShowMenuView()
    {
        SwitchView(0);
    }
    public void Dispose()
    {
        UnsubscribeFromEvents();
    }
}
