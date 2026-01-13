using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExplosiveArrow : MonoBehaviour
{
    [Header("Power")]
    public Image powerImage;
    public GameObject powerPrefab;

    public float powerDelay;
    public bool isPowerCooldown = false;
    public PlayerMovement player;

    public bool isActivated = false;

    private void Update()
    {
        //Use the ability when the key 2 is pressed
        if (Input.GetKey(KeyCode.Alpha2) && player.isArcher && isPowerCooldown == false && isActivated)
        {
            StartCoroutine(PowerCooldown());
            Instantiate(powerPrefab, player.firePos.position, player.firePos.rotation);
        }
    }

    public IEnumerator PowerCooldown()
    {
        isPowerCooldown = true;
        float time = 0f;

        //If the time reach a certain time (that is bigger than the powerDelay) the cooldown is finished
        while (time < powerDelay)
        {
            time += Time.deltaTime;
            powerImage.fillAmount = 1 - (time / powerDelay);
            yield return null;
        }

        powerImage.fillAmount = 0f;
        isPowerCooldown = false;
    }
}
