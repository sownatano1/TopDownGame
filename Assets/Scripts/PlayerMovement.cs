using NUnit.Framework.Internal.Filters;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //Inputs
    public float playerSpeed = 5f;
    private float xInput;
    private float zInput;
    private KeyCode attack = KeyCode.Space;

    //Attack
    public float attackDelay = 1;
    public Transform firePos;
    private bool isCooldown = false;
    public Image cooldownImage;
    public int kills;

    public Rigidbody rb;

    [Header("Attack Prefabs")]
    //Attack Prefab
    public GameObject bulletPrefab;
    public GameObject swordAttackPrefab;
    public GameObject arrowAttackPrefab;
    public GameObject fireBallPrefab;

    [Header("Coins")]
    //Coins
    public TextMeshProUGUI shopCoins;
    public TextMeshProUGUI coinText;
    public int coins;

    [Header("Classes")]
    //Classes (Noob is the first class)
    public bool isNoob = true;
    public bool isArcher = false;
    public bool isKnight = false;
    public bool isMage = false;

    void Update()
    {
        //Attack when the key is pressed
        if (Input.GetKey(attack) && isCooldown == false)
        {
            StartCoroutine(BulletCooldown());
            Attack();
        }

        //Hold "I" to get a lot of money (For debug)
        if(Input.GetKey(KeyCode.I))
        {
            CoinsAmount(10);
        }
    }

    void FixedUpdate()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(xInput, 0, zInput);

        //Normalize the movement if is walking in diagonal or faster than normal
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        transform.Translate(movement * playerSpeed * Time.deltaTime);
    }

    //Attack for each class
    void Attack()
    {
        if (isNoob) Instantiate(bulletPrefab, firePos.position, firePos.rotation);

        if (isKnight) Instantiate(swordAttackPrefab, firePos.position, firePos.rotation);

        if (isArcher) Instantiate(arrowAttackPrefab, firePos.position, firePos.rotation);

        if (isMage) Instantiate(fireBallPrefab, firePos.position, firePos.rotation);
    }

    //Cooldown for attacking
    private IEnumerator BulletCooldown()
    {
        //Can't attack if the cooldown is true
        isCooldown = true;
        float time = 0f;

        //If the time reach a certain time (that is bigger than the attackDelay) the cooldown is finished
        while (time < attackDelay)
        {
            time += Time.deltaTime;
            cooldownImage.fillAmount = 1 - (time / attackDelay);
            yield return null;
        }

        cooldownImage.fillAmount = 0;
        isCooldown = false;
    }

    public void CoinsAmount(int amount)
    {
        //Keep count of the amount of coins that the player has in the UI and at the shop UI
        coins += amount;
        coinText.text = coins.ToString();
        shopCoins.text = coinText.text;
    }
}
