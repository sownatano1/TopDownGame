using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 2f;
    public float bulletSpeed = 20;
    public bool canPerforate = false;

    void Update()
    {
        //Make the bullet go foward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //If the bullet collides on enemy it will get his script and deal damage
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>() != null)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                //Damage
                enemy.enemyCurrentHealth -= bulletDamage;
            }

            if (other.GetComponent<ShootingEnemy>() != null)
            {
                ShootingEnemy shootingEnemy = other.GetComponent<ShootingEnemy>();
                //Damage
                shootingEnemy.enemyCurrentHealth -= bulletDamage;
            }

            //If the bullet can't perforate it is destroyed
            if (!canPerforate)
            {
                Destroy(gameObject);
            }
        }

        //Destroy the bullet when collides in a wall
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
