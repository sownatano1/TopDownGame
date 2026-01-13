using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
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
        //If the player press the key Q it plays the ability
        if (Input.GetKey(KeyCode.Q) && player.isArcher && isPowerCooldown == false && isActivated)
        {
            //Position where the turret will spawn
            Vector3 turretPos = new Vector3(player.firePos.position.x, player.firePos.position.y - 2.5f, player.firePos.position.z);
            StartCoroutine(PowerCooldown());
            //Spawn turret
            Instantiate(powerPrefab, turretPos, player.firePos.rotation);
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
