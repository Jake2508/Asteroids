using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] ScoreUI scoreUI;


    private int score;


    private void Awake()
    {
        instance = this;
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreUI();
    }

    public void TakeScore(int scoreToTake)
    {
        score -= scoreToTake;
        if(score < 0)
        {
            score = 0;
        }
        UpdateScoreUI();
    }
    public int GetScore()
    {
        return score;
    }


    private void UpdateScoreUI()
    {
        scoreUI.UpdateScoreTextUI(score);
    }
}
