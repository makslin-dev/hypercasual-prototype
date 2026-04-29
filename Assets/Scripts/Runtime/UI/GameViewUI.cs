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
        EventBus.OnScoreChanged += AddScore;
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnScoreChanged -= AddScore;
    }
    public void AddScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
