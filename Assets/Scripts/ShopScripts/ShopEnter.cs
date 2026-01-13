using TMPro;
using UnityEngine;

public class ShopEnter : MonoBehaviour
{
    public GameObject shopEnterCamera;
    public GameObject shopCamera;
    public GameObject playerCamera;
    public GameObject cursor;
    public GameObject player, gameUI;

    public CursorFollower cursorFollower;

    public LineRenderer lineRenderer;
    public Transform endLine;

    private void Update()
    {
        //End of the line is in the shop
        lineRenderer.SetPosition(0, endLine.position);

        //Start of the line is on the player
        lineRenderer.SetPosition(1, player.transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        //If the player enter shop, activate and disable some stuffs
        if (other.CompareTag("Player"))
        {
            lineRenderer.enabled = false;
            shopEnterCamera.SetActive(true);
            playerCamera.SetActive(false);
            cursor.SetActive(false);
            Invoke("ShopCamera", 1f);
            cursorFollower.isCursor = false;
        }
    }

    void ShopCamera()
    {
        gameUI.SetActive(false);
        shopCamera.SetActive(true);
        player.SetActive(false);
    }
}
