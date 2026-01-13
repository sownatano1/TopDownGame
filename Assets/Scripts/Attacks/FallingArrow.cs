using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FallingArrow : MonoBehaviour
{
    public float bulletDamage = 2f;
    public float bulletSpeed = 20;
    public Rigidbody rb;
    private GameObject cursor;
    private Vector3 direction;
    public float velocity = 5f;
    public GameObject explosionArea;

    void Start()
    {
        cursor = GameObject.FindWithTag("Cursor");

        //Makes the arrow go upward when spawned
        rb.AddForce(Vector3.up, ForceMode.Impulse);

        //Take the position where it needs to go (at the cursor position)
        direction = (cursor.transform.position - transform.position).normalized;
    }

    void Update()
    {
        //Go to the direction position
        transform.position += (direction * velocity * Time.deltaTime);

        //Rotate the arrow
        transform.Rotate(new Vector3(2, 0, 0)); 
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy the arrow and create a explosion when it hit the ground
        if (other.CompareTag("Ground"))
        {
            Instantiate(explosionArea, transform.position, explosionArea.transform.rotation);
            Destroy(gameObject);
        }

        //Bounce back if it hit a wall
        if (other.CompareTag("Wall"))
        {
            direction = -direction;
            rb.AddForce(Vector3.up, ForceMode.Impulse);
        }

        //Bouce forward if it get on top of an wall
        if (other.CompareTag("TopWall"))
        {
            direction += direction.normalized;
            rb.mass += 1f;
            rb.AddForce(Vector3.up.normalized, ForceMode.Impulse);
        }
    }
}
