using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }


    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject scoreUI;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;


    private void Awake()
    {
        Instance = this;


        retryButton.onClick.AddListener(() =>
        {
            //SoundManager.Instance.PlayButtonSound();
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);

        });

        quitButton.onClick.AddListener(() =>
        {
            //SoundManager.Instance.PlayButtonSound();
            Application.Quit();
        });

        Hide();
    }


    public void UpdateScoreText()
    {
        scoreText.text = ScoreManager.instance.GetScore().ToString("000000");

        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString("000000");
    }

    public void Show()
    {
        UpdateScoreText();
        scoreUI.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
