using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class StartCircle : MonoBehaviour
{
    public Image circle;
    public GameObject startArea;
    public SphereCollider sphereCollider;
    private bool inArea = false;
    public bool restartCircle = false;
    public SpawnManager spawnManager;
    public Collider shopCollider;

    void Update()
    {
        if (inArea == true)
        {
            //The speed that the circle is filled (the smaller the value = the slower it fills)  
            circle.fillAmount += 0.005f;

            //If the circle is filled 100% it activates the enemy spawn and deactivates other stuffs
            if (circle.fillAmount >= 1)
            {
                sphereCollider.enabled = false;
                spawnManager.EnemySpawn(spawnManager.enemiesNumber);
                circle.gameObject.SetActive(false);
                startArea.SetActive(false);
                spawnManager.stopDetecting = false;
                inArea = false;
                shopCollider.enabled = false;
            }
        }

        //If the player is not in the area or left the circle
        if (inArea == false) 
        {
            //Make the circle loose the fill amount
            circle.fillAmount -= 0.05f;

            //If it gets to 0 amount, it stays in 0.
            if (circle.fillAmount <= 0 )
            {
                circle.fillAmount = 0;
            }
        }

        //If the circle is reseted it activate the circle game object and collider again
        if (restartCircle == true)
        {
            circle.gameObject.SetActive(true);
            sphereCollider.enabled = true;
            startArea.SetActive(true);
            restartCircle = false;
        }
    }

    //Trigger to detect if the player has entered the circle area
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    //Trigger to detect if the player has left the circle area
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}
