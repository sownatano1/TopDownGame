using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public NavMeshAgent enemy;
    public NavMeshSurface ground;
    private bool isEnemiesDead = false;
    public bool stopDetecting = false;
    public StartCircle startCircle;
    public TextMeshProUGUI waveTMP;

    public int wavesNumber = 0;
    public int enemiesNumber = 4;

    public List<GameObject> enemies = new List<GameObject>();

    public int waveSpawnShootingEnemies = 4;
    public int waveSpawnHeavyEnemies = 9;

    void Update()
    {
        //Wave number UI
        waveTMP.text = wavesNumber.ToString();

        //Detect the number of enemies in the game
        GameObject[] enemyGO = GameObject.FindGameObjectsWithTag("Enemy");

        //If there are no enemies (if they are all dead) it activate isEnemiesDead
        if (enemyGO.Length == 0 && stopDetecting == false)
        {
            isEnemiesDead = true;
        }

        if (isEnemiesDead == true)
        {
            //Update the waves and it restart the circle
            wavesNumber++;
            enemiesNumber++;
            startCircle.restartCircle = true;
            isEnemiesDead = false;
            stopDetecting = true;
            startCircle.shopCollider.enabled = true;
        }

        //Press P to kill all the enemies (For debug)
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Kill all");
            for (int i = 0; i < enemiesNumber; i++) 
            { 
                Destroy(enemyGO[i]);
            }
        }
    }

    private GameObject NewEnemies()
    {
        //If the wave reach a certain number it will start to spawn new type of enemies
        //Heavy enemies
        if (wavesNumber >= waveSpawnHeavyEnemies)
        {
            //Take a random enemy from the list from 0 to 2 (3 type of enemies)
            int enemyIndex = Random.Range(0, 3);
            GameObject newEnemies2 = enemies[enemyIndex];
            return newEnemies2;
        }

        //Shooting enemies
        if (wavesNumber >= waveSpawnShootingEnemies)
        {
            //Take a random enemy from the list from 0 to 1 (2 type of enemies)
            int enemyIndex = Random.Range(0, 2);
            GameObject newEnemies = enemies[enemyIndex];
            return newEnemies;
        }

        //Only one type of enemy (the first one) is being spawned here (for the waves at the beginning)
        else
        {
            GameObject firstEnemy = enemies[0];
            return firstEnemy;
        }
    }

    private Vector3 RandomEnemySpawnPosition()
    {
        //Create a float for both X and Z axis and it takes a random value
        float randomX = Random.Range(42, -42);
        float randomZ = Random.Range(42, -42);
        
        //Turn both random value from the axis into a Vector where the enemy can spawn
        Vector3 randomPos = new Vector3(randomX, 0, randomZ);

        return randomPos;
    }

    public void EnemySpawn(int EnemiesAmount)
    {
        //An amount of enemies are spawned
        for (int i = 0; i < EnemiesAmount; i++)
        {
            //Spawn a enemy in a random position using the method RandomEnemySpawnPosition()
            Instantiate(NewEnemies(), RandomEnemySpawnPosition(), enemy.transform.rotation);
        }
    }
}
