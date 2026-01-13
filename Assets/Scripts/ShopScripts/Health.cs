using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public PlayerMovement player;
    public HealthBar healthBar;

    public Button healthButton;
    public int coinAmount;

    public bool give50 = false;
    public bool give100 = false;

    private void OnEnable()
    {
        //Call GiveHealth if the player press the button
        healthButton.onClick.AddListener(GiveHealth);
    }

    void GiveHealth()
    {
        if (player.coins >= coinAmount)
        {
            //Remove the amount of the money that the player spend
            player.CoinsAmount(-coinAmount);

            //If the button is give50 the player will get half of his health
            if (give50)
            {
                healthBar.currentHealth += healthBar.maxHealth / 2;
            }

            //If the button is give100 the player will get all of his health
            if (give100)
            {
                healthBar.currentHealth += healthBar.maxHealth;
            }
        }
    }
}
