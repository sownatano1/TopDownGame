using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Coins : MonoBehaviour
{
    private PlayerMovement player;
    private GameObject playerGO;
    public float speed = 5;
    public float radius = 8;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerGO = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //If the coin is closer to the player position it start go to the player direction
        if (Vector3.Distance(playerGO.transform.position, transform.position) < radius)
        {
            transform.position += ((playerGO.transform.position - transform.position) * speed * Time.deltaTime);
        }
    }

    //Trigger to detect if the coin has collided with the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.CoinsAmount(1);
            Destroy(gameObject);
        }
    }
}
