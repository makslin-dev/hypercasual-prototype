using UnityEngine;
using UnityEngine.UI;

public class SettingsViewUI : MonoBehaviour
{
    [SerializeField] private Button _vibrateButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _backButton;

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
        _vibrateButton.onClick.AddListener(VibratePress);
        _soundButton.onClick.AddListener(SoundPress);
        _tutorialButton.onClick.AddListener(TutorialPress);
        _backButton.onClick.AddListener(BackPress);

    }
    private void UnbindButtons()
    {
        _vibrateButton.onClick.RemoveListener(VibratePress);
        _soundButton.onClick.RemoveListener(SoundPress);
        _tutorialButton.onClick.RemoveListener(TutorialPress);
        _backButton.onClick.RemoveListener(BackPress);
    }
    private void VibratePress()
    {
        EventBus.VibratePress();
    }
    private void SoundPress()
    {

    }
    private void TutorialPress()
    {
    
    }
    private void BackPress()
    {

    }
}
