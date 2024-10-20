using UnityEngine;


public class Pickup : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Physics")]
    public float maxThrust;
    public float maxSpin;
    private SpriteRenderer sprite;

    [Header("Pickup Stats")]
    [SerializeField] private MineralValue mineral;
    [SerializeField] private float scaleFactor = 1f;
    private float gravityScalar = 1f;
    [SerializeField] private int points;
    [SerializeField] private GameObject pickupEffect;


    public enum MineralValue
    {
        Big,
        Medium,
        Small,
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        AddForces();

        Invoke("AutoDestroy", 20f);
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
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(points);
            //Debug.Log("Add  Funds");
            GameObject particle = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            particle.transform.localScale *= scaleFactor;
            if(SoundManager.Instance != null)
            {
                SoundManager.Instance.MineralCollectedSound();
            }

            Destroy(gameObject);
        }
    }

    private void AutoDestroy()
    {
        GameObject particle = Instantiate(pickupEffect, transform.position, Quaternion.identity);
        particle.transform.localScale *= scaleFactor;

        Destroy(gameObject);
    }
}
