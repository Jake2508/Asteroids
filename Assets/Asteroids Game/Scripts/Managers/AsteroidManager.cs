using UnityEngine;


[System.Serializable]
public class SpawnEnemyPrefab
{
    public GameObject ePrefab;
    [Range(0f, 1f)] public float spawnChance;
}

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager instance;
    float timer;

    [SerializeField] SpawnEnemyPrefab[] enemyPrefabs;
    [SerializeField] float spawnTimer;


    [SerializeField] private Vector2[] spawnPoints;
    [SerializeField] private Color spawnPointColor = Color.green;
    private bool right;
    private bool up;


    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 position = GetRandomSpawnPoint();
        // Add player position to random modifier

        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            float totalChance = 0f;

            foreach (SpawnEnemyPrefab enemyPrefab in enemyPrefabs)
            {
                totalChance += enemyPrefab.spawnChance;
            }
            float randomValue = Random.value * totalChance;


            foreach (SpawnEnemyPrefab enemyPrefab in enemyPrefabs)
            {
                if (randomValue < enemyPrefab.spawnChance)
                {
                    if (enemyPrefab.ePrefab != null)
                    {
                        GameObject e = Instantiate(enemyPrefab.ePrefab, position, Quaternion.identity);
                        e.transform.parent = transform;
                    }
                    else
                    {
                        Debug.Log("Prefab is not assigned");
                    }
                    break;
                }
                randomValue -= enemyPrefab.spawnChance;
            }
        }
        else
        {
            Debug.Log("No Drop Prefabs assigned");
        }
    }

    private Vector2 GetRandomSpawnPoint()
    {
        return spawnPoints[(GenerateRandomIndex())];
    }

    private int GenerateRandomIndex()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return randomIndex;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = spawnPointColor;

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            Vector3 worldSpawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, 0);
            Gizmos.DrawSphere(transform.position + worldSpawnPoint, 0.25f);
        }
    }
}
