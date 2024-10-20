using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public void UpdateScoreTextUI(int score)
    {
        if(score >= 99999)
        {
            score = 50;
            scoreText.text = score.ToString("00000");
            return;
        }
        scoreText.text = score.ToString("00000");
    }
}
