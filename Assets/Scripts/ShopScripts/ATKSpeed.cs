using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ATKSpeed : MonoBehaviour
{
    public PlayerMovement player;

    public Button attackSpeedButton;
    private int coinAmount = 4;
    public TextMeshProUGUI coinText;
    public Image coinImage;
    public float attackSpeedAmount = 1/10;
    private Stats stats;

    private void OnEnable()
    {
        // Find the stats script when the UI is enabled
        stats = FindAnyObjectByType<Stats>().GetComponent<Stats>();
        //Call AttackSpeedBoost if the player press the button
        attackSpeedButton.onClick.AddListener(AttackSpeedBoost);
    }

    void AttackSpeedBoost()
    {
        if (player.coins >= coinAmount)
        {
            //Update the text of the player attack speed in the shop stats 
            stats.SpeedAttack(1);

            //Decreases the player attack speed (Make it shoot faster)
            player.attackDelay -= attackSpeedAmount;

            //Remove the amount of the money that the player spend
            player.CoinsAmount(-coinAmount);

            //Make more expensive every time when the player buy the damage boost
            coinAmount += 2;

            //Shows the amount of coin that the player need on the text
            coinText.text = coinAmount.ToString();
        }
    }
}
