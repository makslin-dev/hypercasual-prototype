using System;
using Zenject;

public class ScoreManager : IInitializable, IDisposable
{ 
    public int Score { get; private set; } = -1;
    public Action<int> OnScoreIncreased;
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
    }
    private void UnsubscribeFromEvents()
    {
        EventBus.OnNextBlockStart -= IncreaseScore;
    }
    private void IncreaseScore()
    {
        Score++;
        OnScoreIncreased.Invoke(Score);

        if (Score > _playerPrefsManager.LoadBestScore())
        {
            _playerPrefsManager.SaveBestScore(Score);
        }
    }
    public void Dispose()
    {
        UnsubscribeFromEvents();
    }
}
