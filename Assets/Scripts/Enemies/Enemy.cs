using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    private Transform player;
    private PlayerMovement playerScript;
    public GameObject coin;

    public float enemyHealth = 10f;
    public float enemyCurrentHealth = 10f;
    private bool canEnemyAttack = true;
    private float enemyAttackDelay = 1.5f;

    public float enemyDamage;
    public float attackRadius = 3;

    private HealthBar healthBar;

    public bool isHeavyEnemy = false;

    void Start()
    {
        //Larger attack radius because the heavy enemy is a little bit larger than a normal enemy
        if (isHeavyEnemy) { attackRadius = 3.5f; }

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

        //If enemy current health is lower or equal to 0 he dies
        if (enemyCurrentHealth <= 0)
        {
            KillEnemy();
        }

        //If the player is in the enemy attack radius he will get attacked
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRadius && canEnemyAttack)
        {
            StartCoroutine(EnemyAttackDelay());
            healthBar.currentHealth = healthBar.currentHealth - enemyDamage;
        }
    }
        
    //A delay when the enemy is attacking
    private IEnumerator EnemyAttackDelay()
    {
        canEnemyAttack = false;
        float time = 0;

        while(time <= enemyAttackDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }

        canEnemyAttack = true;
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
