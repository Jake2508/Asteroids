using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody2D rb;
    public float maxThrust;
    public float maxSpin;
    private SpriteRenderer sprite;

    [Header("Asteroid Stats")]
    [SerializeField] private AsteroidSize asteroidSize;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float scaleFactor = 1f;
    private float gravityScalar = 1f;
    private int points;

    [Header("Asteroid Types")]
    [SerializeField] private GameObject[] smallerAsteroids;
    private const float A_Offset = 1.25f;


    public enum AsteroidSize
    { 
        Big,
        Medium,
        Small,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckAsteroidSize();

        if(AsteroidManager.instance != null)
        {
            transform.parent = AsteroidManager.instance.transform;
        }

        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        AddForces();
    }

    private void AddForces()
    {
        // Apply Thrust + Spin
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), (Random.Range(-maxThrust, maxThrust)));
        float spin = Random.Range(-maxSpin, maxSpin);
        rb.AddForce(thrust * gravityScalar);
        rb.AddTorque(spin * gravityScalar);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Destroy Bullet
            Destroy(other.gameObject);

            AsteroidHit();
        }

        if (other.CompareTag("Sensor"))
        {
            sprite.color = Color.yellow;
        }

    }


    public void BlackHoleInteraction()
    {
        AsteroidHit();
    }


    private void AsteroidHit()
    {
        // check size of asteroid to destroy
        if (asteroidSize == AsteroidSize.Big)
        {
            AddScore();

            Vector2 M_offset = new Vector2(Random.Range(-A_Offset, A_Offset), (Random.Range(-0.5f, 0.5f)));

            // spawn 2 medium asteroids 
            Instantiate(smallerAsteroids[(GenerateRandomIndex())], ((Vector2)transform.position + M_offset), transform.rotation);
            Instantiate(smallerAsteroids[(GenerateRandomIndex())], ((Vector2)transform.position - M_offset), transform.rotation);

            // destroy current asteroid
            Destroy(gameObject);

        }
        else if (asteroidSize == AsteroidSize.Medium)
        {
            AddScore();

            Vector2 S_offset = new Vector2(Random.Range(-A_Offset, A_Offset), (Random.Range(-0.5f, 0.5f)));

            // spawn 2 small asteroids
            Instantiate(smallerAsteroids[(GenerateRandomIndex())], ((Vector2)transform.position + S_offset), transform.rotation);
            Instantiate(smallerAsteroids[(GenerateRandomIndex())], ((Vector2)transform.position - S_offset), transform.rotation);

            // destroy current asteroid
            Destroy(gameObject);
        }
        else if (asteroidSize == AsteroidSize.Small)
        {
            AddScore();

            
            // destroy current asteroid
            Destroy(gameObject);
        }

        ExplosionAudio();

        // Check Drop
        DropOnDestroy pickup = GetComponent<DropOnDestroy>();
        if(pickup != null)
        {
            pickup.CheckDrop();
        }

        //spawn an particle effect/explosion
        GameObject newExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        newExplosion.transform.localScale *= scaleFactor;
    }

    private void ExplosionAudio()
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.ExplosionSound();
        }
    }

    private void AddScore()
    {
        
        if(ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(points);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sensor"))
        {
            sprite.color = Color.white;
            gravityScalar += 0.2f;
            AddForces();
        }
    }

    private void CheckAsteroidSize()
    {
        switch (asteroidSize)
        {
            case AsteroidSize.Big:
                SetPointValue(15);
                break;

            case AsteroidSize.Medium:
                SetPointValue(10);
                break;

            case AsteroidSize.Small:
                SetPointValue(5);
                break;

            default:
                break;
        }
    }

    private void SetPointValue(int pointValue)
    {
        points = pointValue;
    }

    private int GenerateRandomIndex()
    {
        int randomIndex = Random.Range(0, smallerAsteroids.Length);
        return randomIndex;
    }
}