using UnityEngine;


public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer char_sprite;

    [Header ("Movement")]
    public float thrust;
    public float rotationThrust;
    private float thrustInput;
    private float turnInput;


    [Header("Health")]
    private bool alive = true;
    private int lives = 3;
    private bool damageImmunity = false; 

    [Header("Weapons")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] private float bulletForce;
    [SerializeField] CircleCollider2D circleCollider;
    private float r_timer;
    private float reloadTimer = 0.25f;

    [Header("Particles")]
    [SerializeField] private GameObject P_thruster;
    [SerializeField] private GameObject P_Explosion;
    private Animator animator;
    private GravitationalPull gravity;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponentInChildren<CircleCollider2D>();
        animator = GetComponent<Animator>();
        gravity = GetComponent<GravitationalPull>();
    }

    private void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        r_timer -= Time.deltaTime;
        if (r_timer < 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isAlive())
            {
                Fire();
                r_timer = reloadTimer;
            }
        }

    }

    private void FixedUpdate()
    {   
        // thrust force
        rb.AddRelativeForce(Vector2.up * thrustInput);
        // rotatation
        transform.Rotate(-Vector3.forward * turnInput * Time.deltaTime * rotationThrust);
    }

    private void Fire()
    {
        // Instantiate Bullet & Add Force
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.BulletShotSound();
        }


        Destroy(newBullet, 5f);
    }

    private void Repulsion()
    {
        gravity.gravitationalForce = -3f;
    }

    private void Attract()
    {
        gravity.gravitationalForce = 3f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            // Pickup functionality ++ Add a cast
        }
        else
        {
            if (other.relativeVelocity.magnitude >= 2 && damageImmunity != true)
            {
                Death();
            }
        }

    }

    public void Death()
    {
        // Set Death 
        alive = false;
        damageImmunity = true;

        // Show Explosion Visual 
        GameObject newExplosion = Instantiate(P_Explosion, transform.position, transform.rotation);

        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.ExplosionSound();
        }

        // Death Anim
        animator.SetTrigger("death");

        // Repulsion Force & Disable Colliders

        gameObject.tag = "Dead";

        if (lives == 0)
        {
            // Trigger Game Over UI 
            GameManager.instance.GameOver();

            Cursor.visible = true;
        }
        else
        {
            Invoke("Respawn", 2f);
        }

        lives--;
    }

    private void Respawn()
    {
        // Reset Velocity & Transform
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        animator.SetTrigger("respawn");
        char_sprite.color = Color.yellow;
        Repulsion();

        UpdateLifeUI();
        Invoke("ResetDamageImmunity", 1.5f);
    }

    private void UpdateLifeUI()
    {
        LifeVisuals.instance.UpdateLivesVisual();
    }

    private void ResetDamageImmunity()
    {
        // Play a simple animation to show its ended

        // Update Sprite + Collisions
        char_sprite.color = Color.white;
        gameObject.tag = "Player";
        damageImmunity = false;
        alive = true;
        Attract();
    }

    private bool isMoving()
    {
        if(thrustInput > 0)
        {
            return true;
        }
        return false;
    }
    public bool isAlive()
    {
        return alive;
    }

}
