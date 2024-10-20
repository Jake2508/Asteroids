using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private bool paused = false;


    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void TogglePause()
    {
        if(paused)
        {
            paused = false;
            Time.timeScale = 1;
            PauseUI.instance.Hide();
            Cursor.visible = false;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
            PauseUI.instance.Show();
            Cursor.visible = true;
        }
    }

    public void GameOver()
    {
        if(ScoreManager.instance.GetScore() > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", ScoreManager.instance.GetScore());
        }

        Time.timeScale = 0;
        GameOverUI.Instance.Show();
    }
}
