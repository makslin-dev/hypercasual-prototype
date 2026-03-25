using TMPro;
using UnityEngine;

public class GameViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
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
        ScoreManager.Instance.OnScoreIncreased += AddScore;
    }
    private void UnsubscribeFromEvents()
    {
        ScoreManager.Instance.OnScoreIncreased -= AddScore;
    }
    public void AddScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
