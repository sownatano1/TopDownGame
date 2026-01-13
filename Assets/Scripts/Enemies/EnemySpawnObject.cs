using UnityEngine;

public class EnemySpawnObject : MonoBehaviour
{
    public GameObject normalEnemy;
    public GameObject shootingEnemy;
    public GameObject heavyEnemy;

    public bool isNormalEnemy = false;
    public bool isShootingEnemy = false;
    public bool isHeavyEnemy = false;

    public float timeToSpawn = 2;

    void Start()
    {
        Invoke("SpawnEnemy", timeToSpawn);
    }

    void SpawnEnemy()
    {
        //Spawn position where the real enemy will be spawned
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);

        //Spawn the real enemy
        if (isNormalEnemy) Instantiate(normalEnemy, spawnPos, transform.rotation);
        if (isShootingEnemy) Instantiate(shootingEnemy, spawnPos, transform.rotation);
        if (isHeavyEnemy) Instantiate(heavyEnemy, spawnPos, transform.rotation);

        Destroy(gameObject);
    }
}
