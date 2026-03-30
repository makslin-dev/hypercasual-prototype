using UnityEngine;
using static UnityEngine.InputSystem.InputSettings;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }
    [SerializeField] private Canvas[] _views;

    private bool _isVibrationEnabled = true;
    private void OnEnable()
    {
        GameManager.Instance.OnNextBlockStart += AddScore;
        EventBus.OnSettingsPressed += ShowSettings;
        EventBus.OnBackPressed += ShowMenuView;
    }
    private void Awake()
    {
        Instance = this;
        SwitchView(0);
        SetStartingSettings();
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextBlockStart -= AddScore;
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
    private void AddScore()
    {

    }
    private void ShowMenuView()
    {
        SwitchView(0);
    }
}
