using UnityEngine;
using UnityEngine.UI;


public class PauseUI : MonoBehaviour
{
    public static PauseUI instance;
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        instance = this;

        // Bind Buttons to Show to trigger GameManager time resets
        resumeButton.onClick.AddListener(() =>
        {
            //SoundManager.Instance.PlayButtonSound();
            GameManager.instance.TogglePause();
        });

        quitButton.onClick.AddListener(() =>
        {
            //SoundManager.Instance.PlayButtonSound();
            Application.Quit();
        });


        Hide();
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
