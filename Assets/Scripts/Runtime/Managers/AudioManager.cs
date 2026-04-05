using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("AudioComponents")]
    [SerializeField] private AudioMixer _gameMixer;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private GameSoundSO _gameSoundSO;
    [Header("Parameter Names")]
    [SerializeField] private string _sfxVolumeParam = "GameVolume";

    public event Action<bool> OnSoundSet;

    private bool _isSound = true;

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Start()
    {
        InitSound();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        EventBus.OnSoundPressed += ToggleSound;
        EventBus.OnNextBlockStart += PlayNextBlockSound;
        EventBus.OnBackPressed += PlayBackSound;
        EventBus.OnSoundPressed += PlayUISound;
        EventBus.OnVibratePressed += PlayUISound;
        EventBus.OnSettingsPressed += PlayUISound;
        EventBus.OnTutorialPressed += PlayUISound;
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnSoundPressed -= ToggleSound;
        EventBus.OnNextBlockStart -= PlayNextBlockSound;
        EventBus.OnBackPressed -= PlayBackSound;
        EventBus.OnSoundPressed -= PlayUISound;
        EventBus.OnVibratePressed -= PlayUISound;
        EventBus.OnSettingsPressed -= PlayUISound;
        EventBus.OnTutorialPressed -= PlayUISound;
    }
    private void InitSound()
    {
        bool isSound = true;
        if (PlayerPrefs.GetInt(_sfxVolumeParam, 1) == 0)
        {
            isSound = false;
        }
        _isSound = isSound;
        UpdateMixerVolume();
        OnSoundSet?.Invoke(_isSound);
    }
    public void ToggleSound()
    {
        _isSound = !_isSound; 

        UpdateMixerVolume();  

        PlayerPrefs.SetInt(_sfxVolumeParam, _isSound ? 1 : 0);
    }
    private void UpdateMixerVolume()
    {
        _gameMixer.SetFloat(_sfxVolumeParam, _isSound ? 0f : -80f);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        _sfxSource.pitch = 1f;
        _sfxSource.PlayOneShot(clip);
    }
    public void PlayPerfectSound(AudioClip clip, int comboCount)
    {
        if (clip == null) return;
        float currentPitch = 1f + (comboCount * 0.1f);
        _sfxSource.pitch = Mathf.Clamp(currentPitch, 1f, 2.5f);

        _sfxSource.PlayOneShot(clip);
    }
    public void PlayNextBlockSound()
    {
        int index = UnityEngine.Random.Range(0, _gameSoundSO.CutSounds.Length);
        PlaySound(_gameSoundSO.CutSounds[index]);
    }
    public void PlayUISound()
    {
        PlaySound(_gameSoundSO.UiClickSound);
    }
    public void PlayBackSound(BackCaller caller)
    {
        PlaySound(_gameSoundSO.UiClickSound);
    }
}
