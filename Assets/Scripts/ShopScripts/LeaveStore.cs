using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeaveStore : MonoBehaviour
{
    public Button leaveButton;
    public ShopEnter shopScript;
    public Collider shopCollider;

    void Update()
    {
        leaveButton.onClick.AddListener(LeaveShop);
    }

    void LeaveShop()
    {
        //When the player leave the store it activates back some stuffs
        shopScript.player.SetActive(true);
        shopScript.shopCamera.SetActive(false);
        shopScript.playerCamera.SetActive(true);
        shopScript.shopEnterCamera.SetActive(false);
        shopScript.cursor.SetActive(true);
        shopCollider.enabled = false;
        shopScript.cursorFollower.isCursor = true;
        shopScript.gameUI.SetActive(true);

        Invoke("ShopCooldown", 4f);
    }

    //Cooldown when the player leave the shop so he doesn't enter again
    void ShopCooldown()
    {
        shopCollider.enabled = true;
    }
}
