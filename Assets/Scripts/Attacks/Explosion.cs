using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 8;
    public float time = 0.5f;   
    
    void Start()
    {
        //Destroy the explosion after some time
        Invoke("Destroy", time);
    }

    void OnTriggerEnter(Collider other)
    {
        //Damage the enemy if he is in the explosion
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>() != null)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                //Damage
                enemy.enemyCurrentHealth -= damage;
            }

            if (other.GetComponent<ShootingEnemy>() != null)
            {
                ShootingEnemy shootingEnemy = other.GetComponent<ShootingEnemy>();
                //Damage
                shootingEnemy.enemyCurrentHealth -= damage;
            }
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
