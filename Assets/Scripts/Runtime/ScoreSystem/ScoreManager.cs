using Cysharp.Threading.Tasks.Triggers;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private GameManager _gameManager;
    public int Score { get; private set; } = -1;

    public Action<int> OnScoreIncreased;
    private void Awake()
    {
        Instance = this;
        _gameManager.OnNextBlockStart += IncreaseScore;
    }
    private void IncreaseScore()
    {
        Score++;
        OnScoreIncreased.Invoke(Score);

        if (Score > PlayerPrefsManager.Instance.LoadBestScore())
        {
            PlayerPrefsManager.Instance.SaveBestScore(Score);
        }
    }
}
