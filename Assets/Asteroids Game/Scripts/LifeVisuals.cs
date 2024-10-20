using UnityEngine;

public class LifeVisuals : MonoBehaviour
{
    public static LifeVisuals instance;
    [SerializeField] GameObject[] lives;
    
    private int remainingLives = 1;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateLivesVisual()
    {
        int lastIndex = lives.Length - remainingLives;
        remainingLives++;

        lives[lastIndex].SetActive(false);
    }
}
