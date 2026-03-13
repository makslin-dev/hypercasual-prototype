using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager Instance { get; private set; }

    private const string BEST_SCORE_KEY = "BestScore";
    private void Awake()
    {
        InitSingleton();
    }
    private void InitSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, score);
    }
    public int LoadBestScore()
    {
      return PlayerPrefs.GetInt(BEST_SCORE_KEY);
    }
}
