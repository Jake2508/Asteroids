using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;


    private void Awake()
    {
        // Bind Buttons to Show to trigger GameManager time resets
        playButton.onClick.AddListener(() =>
        {
            //SoundManager.Instance.PlayButtonSound();
            SceneManager.LoadScene(1);

        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();

        });
    }
}
