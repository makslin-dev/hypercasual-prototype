using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("GameMixer")]
    [SerializeField] private AudioMixer _gameMixer;
    [Header("Parameter Names")]
    [SerializeField] private string _sfxVolumeParam = "GameVolume";

    public event Action<bool> OnSoundSet;

    private bool _isSound = true;

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Awake()
    {
        Instance = this;
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
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnSoundPressed -= ToggleSound;
    }
    private void InitSound()
    {
        bool isSound = true;
        if (PlayerPrefs.GetInt(_sfxVolumeParam, 1) == 0)
        {
            isSound = false;
        }
        _isSound = isSound;
        OnSoundSet?.Invoke(_isSound);
    }
    public void ToggleSound()
    {
        if (!_isSound)
        {
            _gameMixer.SetFloat(_sfxVolumeParam, -80f);
        }
        else
        {
            _gameMixer.SetFloat(_sfxVolumeParam, 0f);
        }
        _isSound = !_isSound;
        print("SOUND" + _isSound);
        PlayerPrefs.SetInt(_sfxVolumeParam, _isSound ? 1 : 0);
    }
}
