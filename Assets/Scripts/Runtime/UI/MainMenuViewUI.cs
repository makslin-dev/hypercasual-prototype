using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewUI : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;

    private void OnEnable()
    {
        BindButtons();
    }
    private void OnDisable()
    {
        UnbindButtons();
    }
    private void BindButtons()
    {
        _settingsButton.onClick.AddListener(SettingsPress);
    }
    private void UnbindButtons()
    {
        _settingsButton.onClick.RemoveListener(SettingsPress);
    }
    private void SettingsPress()
    {
        EventBus.SettingsPress();
    }
}
