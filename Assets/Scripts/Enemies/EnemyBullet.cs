using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletDamage = 0.1f;
    public float bulletSpeed = 20;
    private HealthBar healthBar;

    void Update()
    {
        //Make the bullet go foward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //If the enemy bullet hit the player it will deal damage
        if (other.CompareTag("Player"))
        {
            //Find the enemy script in the enemy
            HealthBar healthBar = other.GetComponent<HealthBar>();
            if (healthBar.gameObject != null)
            {
                //Damage
                healthBar.currentHealth -= bulletDamage;

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
