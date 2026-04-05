using TMPro;
using UnityEngine;
using Zenject;

public class GameViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private ScoreManager _scoreManager;
    [Inject]
    private void Construct(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        _scoreManager.OnScoreIncreased += AddScore;
    }
    private void UnsubscribeFromEvents()
    {
        _scoreManager.OnScoreIncreased -= AddScore;
    }
    public void AddScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
