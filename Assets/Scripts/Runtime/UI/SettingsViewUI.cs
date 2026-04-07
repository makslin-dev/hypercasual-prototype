using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsViewUI : MonoBehaviour
{
    [SerializeField] private Button _vibrateButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Image _tutorialImage;
    [SerializeField] private Image _vibrateOffFrame;
    [SerializeField] private Image _soundOffFrame;

    private bool _tutorialOpened;
    private VibrationManager _vibrationManager;
    private AudioManager _audioManager;
    [Inject]
    private void Construct(AudioManager audioManager, VibrationManager vibration)
    {
        _audioManager = audioManager;
        _vibrationManager = vibration;
    }
    private void OnEnable()
    {
        BindButtons();
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnbindButtons();
        UnsubscribeFromEvents();
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
    private void SubscribeToEvents()
    {
        _vibrationManager.OnVibrationSet += InitVibrateButton;
        _audioManager.OnSoundSet += InitSoundButton;
    }
    private void UnsubscribeFromEvents()
    {
        _vibrationManager.OnVibrationSet -= InitVibrateButton;
        _audioManager.OnSoundSet -= InitSoundButton;
    }
    private void VibratePress()
    {
        EventBus.VibratePress();
        _vibrateOffFrame.enabled = !_vibrateOffFrame.enabled;
    }
    private void InitVibrateButton(bool arg)
    {
        _vibrateOffFrame.enabled = !arg;
    }
    private void SoundPress()
    {
        EventBus.SoundPressed();
        print("pressed");
        _soundOffFrame.enabled = !_soundOffFrame.enabled;
    }
    private void InitSoundButton(bool arg)
    {
        _soundOffFrame.enabled = !arg;
    }
    private void TutorialPress()
    {
        _tutorialImage.enabled = true;
        _tutorialOpened = true;
        EventBus.TutorialPressed();
    }
    private void BackPress()
    {
        if (_tutorialOpened)
        {
            _tutorialImage.enabled = false;
            _tutorialOpened = false;
            EventBus.BackPress(BackCaller.Tutorial);
        }
        else
        {
            EventBus.BackPress(BackCaller.Settings);
        }
    }
}
