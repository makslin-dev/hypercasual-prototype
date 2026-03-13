using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public int Score { get; private set; } = -1;

    private void Update()
    {
        print(Score);
    }
    private void Awake()
    {
        _gameManager.OnNextBlockStart += IncreaseScore;
    }
    private void IncreaseScore()
    {
        Score++;
        if (Score > PlayerPrefsManager.Instance.LoadBestScore())
        {
            PlayerPrefsManager.Instance.SaveBestScore(Score);
        }
    }
}
