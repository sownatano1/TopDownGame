using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    void FixedUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
