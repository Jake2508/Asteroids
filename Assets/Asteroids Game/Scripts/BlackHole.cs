using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] GravitationalPull gravitationalPull;
    [SerializeField] GameObject warningPopup;
    [SerializeField] GameObject explosion;
    [SerializeField] private bool menu = false;


    private void Start()
    {
        gravitationalPull.gameObject.SetActive(false);

        // show warning UI
        Invoke("ShowWarningPopup", 1f);
    }

    private void ShowWarningPopup()
    {
        //Debug.Log("Warning");
        Instantiate(warningPopup, transform.position, Quaternion.identity);

        Invoke("EnableGravitationalPull", 2f);
        if (menu)
        {
            return;
        }
        else
        {
            Invoke("AutoDestroy", GenerateRandomRange());
        }
    }

    private void EnableGravitationalPull()
    {
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
        explode.transform.localScale *= 1.2f;

        gravitationalPull.gameObject.SetActive(true);
        gravitationalPull.Activate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.GetComponent<Asteroid>();
            if(asteroid != null)
            {
                asteroid.BlackHoleInteraction();
                //Debug.Log("Asteroid Hit Call");
            }
        }

        if(other.CompareTag("Player"))
        {
            CharacterController character = other.GetComponent<CharacterController>();
            if(character != null)
            {
                character.Death();
                GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
                explode.transform.localScale *= 0.75f;
            }
        }
    }

    private void AutoDestroy()
    {
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
        explode.transform.localScale *= 1.2f;

        Destroy(gameObject);
    }

    private int GenerateRandomRange()
    {
        int randomIndex = Random.Range(8, 15);
        return randomIndex;
    }
}
