using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultipleArrows : MonoBehaviour
{
    [Header("Power")]
    public Image powerImage;
    public GameObject powerPrefab;

    public float powerDelay;
    public bool isPowerCooldown = false;
    public PlayerMovement player;

    public Transform firePos1, firePos2, firePos3, firePos4;

    public bool isActivated = false;

    private void Update()
    {
        //If the player press the key E it plays the ability
        if (Input.GetKey(KeyCode.E) && player.isArcher && isPowerCooldown == false && isActivated)
        {
            StartCoroutine(PowerCooldown());
            //All 5 arrows are spawned in diferent position and rotation
            Instantiate(powerPrefab, player.firePos.position, player.firePos.rotation);
            Instantiate(powerPrefab, firePos1.position, firePos1.rotation);
            Instantiate(powerPrefab, firePos2.position, firePos2.rotation);
            Instantiate(powerPrefab, firePos3.position, firePos3.rotation);
            Instantiate(powerPrefab, firePos4.position, firePos4.rotation);
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
