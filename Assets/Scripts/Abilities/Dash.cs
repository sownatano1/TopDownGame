using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Animations;

public class Dash : MonoBehaviour
{
    [Header("Power")]
    public Image dashImage;
    public float dashForce = 15;
    public float dashTime = 0.2f;
    public GameObject arrowsPrefab;
    public Transform backFirePos;

    public float powerDelay = 5;
    public bool isPowerCooldown = false;
    public bool isDashing = false;
    public CursorFollower cursorFollower;
    public Rigidbody playerRb;
    public Transform player;

    public bool isActivated = false;

    private void Update()
    {
        //Use the ability when the key 3 is pressed
        if (Input.GetKey(KeyCode.Alpha3) && isPowerCooldown == false && isActivated)
        {
            StartCoroutine(DashCooldown());
            Dashing();
            Instantiate(arrowsPrefab, backFirePos.position, backFirePos.rotation);

            //Makes the player turn backward to make it appear that he shot behind him
            player.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }

    public void Dashing()
    {
        if (isDashing)
        {
            //Find the cursor 
            GameObject cursor = GameObject.FindWithTag("Cursor");

            //Find the position where the player will dash to
            Vector3 direction = ((cursor.transform.position - cursorFollower.transform.position).normalized * dashForce);

            //Dash towards the direction of the cursor
            playerRb.AddForce(direction, ForceMode.Impulse);

            //Disable cursor follower so the player can rotate backward
            cursorFollower.enabled = false;

            //Call ResetDash after the dashTime has ended
            Invoke(nameof(ResetDash), dashTime);
        }
    }

    void ResetDash() 
    {
        isDashing = false; 
        //If is not dashing anymore it activate the cursor back
        cursorFollower.enabled = true;  
    }

    public IEnumerator DashCooldown()
    {
        isPowerCooldown = true;
        isDashing = true;
        float time = 0;

        //If the time reach a certain time (that is bigger than the powerDelay) the cooldown is finished
        while (time < powerDelay)
        {
            time += Time.deltaTime;
            dashImage.fillAmount = 1 - (time / powerDelay);
            yield return null;
        }

        isPowerCooldown = false;
        dashImage.fillAmount = 0;
    }
}
