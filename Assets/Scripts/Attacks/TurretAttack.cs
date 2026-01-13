using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurretAttack : MonoBehaviour
{
    public Transform firePos;
    public float shootSpeed = 2f;
    private GameObject enemy;
    public float radius = 8f;
    public LayerMask enemyMask;
    public LayerMask wallMask;
    private bool canShoot = true;

    public GameObject bulletPrefab;
    public float turretTimeToDestroy = 10;
    void Start()
    {
        //Destroy the turret after some time
        Invoke(nameof(DestroyTurret), turretTimeToDestroy);
    }

    void Update()
    {
        FindNearestEnemy();
      
        if (enemy != null)
        {
            //Make the turret look at the enemies
            transform.LookAt(enemy.transform.position - new Vector3(0, 1, 0));
        }
    }

    public void FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyMask);
        int index = 0;
        float shortestDist = 100000;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Enemy"))
                {
                    float dist = Vector3.Distance(colliders[i].transform.position, transform.position);
                    if (dist < shortestDist)
                    {
                        shortestDist = dist;
                        index = i;
                    }
                }
            }
            enemy = colliders[index].gameObject;

            //The start of the raycast
            Vector3 startPos = transform.position;

            //Calculate the enemy position
            Vector3 enemyPos = enemy.transform.position - transform.position;

            RaycastHit hit;

            //If the raycast hit an enemy
            if (Physics.Raycast(startPos, enemyPos, out hit, enemyPos.magnitude, enemyMask))
            {
                //And if he is not behind a wall and can shoot
                if (!Physics.Raycast(startPos, enemyPos, out hit, enemyPos.magnitude, wallMask) && canShoot)
                {
                    //Start the coroutine to shoot
                    StartCoroutine(Shoot());
                }
            }

            //Detect if there is a wall in front of the enemy
            if (Physics.Raycast(startPos, enemyPos, out hit, enemyPos.magnitude, wallMask))
            {
                print("There is a wall in the way");
            }

            Debug.DrawRay(startPos, enemyPos);
        }
    }
    public IEnumerator Shoot()
    {
        canShoot = false;

        //Wait for the turret to shoot again (cooldown)
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;

        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }

    void DestroyTurret()
    {
        Destroy(gameObject);
    }
}
