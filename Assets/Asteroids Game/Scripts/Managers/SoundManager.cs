using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }


    [SerializeField] private AudioSource button;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource explosion;
    [SerializeField] private AudioSource bulletShot;
    [SerializeField] private AudioSource mineralCollected;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    /*
    // Base Audio Interactions
    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayExplosionSound(Vector2 position)
    {
        AudioSource.PlayClipAtPoint(audioClipRefSO.Explosion, position, volume);
    }
    public void FireShotSound(Vector2 position)
    {
        AudioSource.PlayClipAtPoint(bulletShot, position, volume);
    }

    */
    public void BulletShotSound()
    {
        if (bulletShot != null)
        {
            bulletShot.Play();
        }

    }

    public void PlayButtonSound()
    {
        if (button != null)
        {
            button.Play();
        }

    }

    public void ExplosionSound()
    {
        if (explosion != null)
        {
            explosion.Play();
        }

    }
    public void MineralCollectedSound()
    {
        if (mineralCollected != null)
        {
            mineralCollected.Play();
        }

    }
}

