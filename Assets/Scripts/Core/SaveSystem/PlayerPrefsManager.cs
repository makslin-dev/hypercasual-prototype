using UnityEngine;

public class PlayerPrefsManager
{ 
    private const string BEST_SCORE_KEY = "BestScore";

    public void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, score);
    }
    public int LoadBestScore()
    {
      return PlayerPrefs.GetInt(BEST_SCORE_KEY);
    }
}
