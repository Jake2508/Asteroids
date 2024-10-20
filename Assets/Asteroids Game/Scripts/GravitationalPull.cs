using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravitationalPull : MonoBehaviour
{
    // Gravitational constant
    public float gravitationalForce = 1;

    // Mass of asteroids/black holes
    public float ObjectMass = 100f;
    public bool blackHole = false;


    private bool active = false;
    private Rigidbody2D rbTarget;


    private void Awake()
    {
        rbTarget = GetComponent<Rigidbody2D>();
    }

    // Apply gravitational force to all asteroids
    private void FixedUpdate()
    {
        // Should probably rework below method or create entirely new functionality
        if (blackHole && active)
        {
            string[] additionalTags = { "LargeAsteroid", "Asteroid", "Player" };
            List<GameObject> allObjects = new List<GameObject>();

            // Find asteroids for each tag and add them to the list
            foreach (string tag in additionalTags)
            {
                GameObject[] asteroidsWithTag = GameObject.FindGameObjectsWithTag(tag);
                allObjects.AddRange(asteroidsWithTag);
            }

            // Convert the list to an array if needed
            GameObject[] listedObjects = allObjects.ToArray();

            if (listedObjects.Length > 0 && rbTarget != null)
            {
                foreach (GameObject Object in listedObjects)
                {
                    //Debug.Log(Object.name);
                    ApplyGravitationalForce(Object);
                }
            }
        }
        else
        {
            string[] additionalTags = { "LargeAsteroid", "Pickup" };
            List<GameObject> allObjects = new List<GameObject>();

            // Find asteroids for each tag and add them to the list
            foreach (string tag in additionalTags)
            {
                GameObject[] asteroidsWithTag = GameObject.FindGameObjectsWithTag(tag);
                allObjects.AddRange(asteroidsWithTag);
            }

            // Convert the list to an array if needed
            GameObject[] listedObjects = allObjects.ToArray();

            if (listedObjects.Length > 0 && rbTarget != null)
            {
                foreach (GameObject Object in listedObjects)
                {
                    //Debug.Log(Object.name);
                    ApplyGravitationalForce(Object);
                }
            }
        }

    }


    // Apply gravitational force to a specific asteroid
    public void ApplyGravitationalForce(GameObject asteroid)
    {
        Rigidbody2D rbAsteroid = asteroid.GetComponent<Rigidbody2D>();

        // Calculate distance between asteroid and player
        Vector2 direction = rbTarget.position - rbAsteroid.position;
        float distance = direction.magnitude;

        // Calculate gravitational force
        float forceMagnitude = gravitationalForce * (ObjectMass * rbTarget.mass) / Mathf.Pow(distance, 2);

        // Apply gravitational force to the asteroid
        Vector2 force = direction.normalized * forceMagnitude;
        rbAsteroid.AddForce(force);
    }

    public void Activate()
    {
        active = true;
    }
}
