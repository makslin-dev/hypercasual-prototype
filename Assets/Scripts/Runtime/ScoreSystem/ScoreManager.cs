using System;
using Zenject;

public class ScoreManager : IInitializable, IDisposable
{ 
    public int Score { get; private set; } = 0;
    private PlayerPrefsManager _playerPrefsManager;
    [Inject]
    private void Construct(PlayerPrefsManager prefsManager)
    {
        _playerPrefsManager = prefsManager;
    }
    public void Initialize()
    {
        SubscribeToEvents();
    }
    private void SubscribeToEvents()
    {
        EventBus.OnNextBlockStart += IncreaseScore;
        EventBus.OnGameRestarted += RenewScore;
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnNextBlockStart -= IncreaseScore;
        EventBus.OnGameRestarted -= RenewScore;
    }
    private void IncreaseScore()
    {
        Score++;
        EventBus.ChangeScore(Score);

        if (Score > _playerPrefsManager.LoadBestScore())
        {
            _playerPrefsManager.SaveBestScore(Score);
        }
    }
    private void RenewScore()
    {
        Score = 0;
        EventBus.ChangeScore(Score);
    }
    public void Dispose()
    {
        UnsubscribeFromEvents();
    }
}
