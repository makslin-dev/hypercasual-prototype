using UnityEngine;
using static UnityEngine.InputSystem.InputSettings;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }
    [SerializeField] private Canvas[] _views;
   
    private void OnEnable()
    {
        GameManager.Instance.OnNextBlockStart += AddScore;
        EventBus.OnVibratePressed += ToggleVibrate;
        EventBus.OnSettingsPressed += ShowSettings;
    }
    private void Awake()
    {
        Instance = this;
        SwitchView(0);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnNextBlockStart -= AddScore;
        EventBus.OnVibratePressed -= ToggleVibrate;
        EventBus.OnSettingsPressed -= ShowSettings;
    }
    private void SwitchView(int id)
    {
        foreach (var view in _views)
        {
            view.enabled = false;
        }
        _views[id].enabled = true;
    }
    private void ShowSettings()
    {
        SwitchView(2);
    }
    private void AddScore()
    {

    }
    private void ToggleVibrate()
    {

    }
    private void ToggleSound()
    {

    }
    private void ShowTutorial()
    {

    }
}
