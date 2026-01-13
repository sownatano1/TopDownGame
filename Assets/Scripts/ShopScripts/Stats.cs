using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI atkSpeed;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI kills;

    public PlayerMovement player;
    public Damage damageScript;
    private int atkSpeedValue = 1;
    private ATKSpeed atkSpeedScript;

    private void OnEnable()
    {
        atkSpeedScript = FindAnyObjectByType<ATKSpeed>().GetComponent<ATKSpeed>();
    }

    void Update()
    {
        //Make the text show the value of the attack damage
        if (player.isArcher || player.isMage) damage.text = damageScript.bulletAttack.bulletDamage.ToString();
        if (player.isKnight) damage.text = damageScript.swordAttack.attackDamage.ToString();

        //Make the text show the numbers of kills
        kills.text = player.kills.ToString();

        //The player attack speed can't get lower than 0.2
        if (player.attackDelay <= 0.2)
        {
            atkSpeed.text = "Max";

            //Disable the button
            atkSpeedScript.attackSpeedButton.interactable = false;
            atkSpeedScript.coinText.enabled = false;
            atkSpeedScript.coinImage.enabled = false;
        }
    }
    public void SpeedAttack (int amount)
    {
        atkSpeedValue += amount;

        //Make the text show the value of the attack speed
        atkSpeed.text = atkSpeedValue.ToString();
    }
}
