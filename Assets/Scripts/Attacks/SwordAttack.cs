using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int attackDamage = 2;
    public float vanishTime = 0.5f;
 
    void Start()
    {
        //Call the method "Destroy" when it is spawned
        Invoke("Destroy", vanishTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.gameObject != null)
            {
                enemy.enemyCurrentHealth -= attackDamage;
            }
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
