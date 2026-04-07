using UnityEngine;

[CreateAssetMenu(fileName = "GameSound",menuName = "GameAudio")]
public class GameSoundSO : ScriptableObject
{
    public AudioClip[] CutSounds;
    public AudioClip UiClickSound;
}
