using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    private Transform player;
    private PlayerMovement playerScript;
    public GameObject coin;

    public float enemyHealth = 10f;
    public float enemyCurrentHealth = 10f;
    private bool canEnemyShoot = true;
    public float enemyShootingDelay = 2f;

    public GameObject bullet;
    public float enemyDamage;

    private Rigidbody rb;
    private Vector3 lastPos;

    private HealthBar healthBar;

    public float shootRadius = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = FindAnyObjectByType<HealthBar>().GetComponent<HealthBar>();
        enemyCurrentHealth = enemyHealth;
        player = GameObject.FindWithTag("Player").transform;
        playerScript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //If the player is in the game it set the enemy destination as the player position
        if (player != null)
        {
            enemy.SetDestination(player.position);
        }

        //If enemy current health is 0 he dies
        if (enemyCurrentHealth <= 0)
        {
            KillEnemy();
        }

        //If the player is inside the radius the enemy will shoot
        if (Vector3.Distance(player.transform.position, transform.position) <= shootRadius)
        {
            //Make the enemy look at the player
            transform.LookAt(player.transform.position);

            if (canEnemyShoot)
            {
                StartCoroutine(EnemyShootingDelay());
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    //Shooting delay 
    private IEnumerator EnemyShootingDelay()
    {
        canEnemyShoot = false;
        float time = 0;

        while (time <= enemyShootingDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }

        canEnemyShoot = true;
    }

    void KillEnemy()
    {
        //Position where the coin is going to be spawned
        Vector3 coinPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);

        //When the enemy die it spawns a coin
        Instantiate(coin, coinPos, coin.transform.rotation);

        Destroy(gameObject);
        playerScript.kills += 1;
    }
}
