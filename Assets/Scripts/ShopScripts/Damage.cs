using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public SwordAttack swordAttack;
    public Bullet bulletAttack;
    public PlayerMovement player;

    private Button damageButton;
    private int coinAmount = 6;
    public TextMeshProUGUI coinText;

    private void OnEnable()
    {
        //Find the Button component when the UI is enabled
        damageButton = GetComponent<Button>();

        //Call DamageBoost if the player press the button
        damageButton.onClick.AddListener(DamageBoost);
    }

    void DamageBoost()
    {
        //If the player has more coins than the amount needed, he is able to buy
        if (player.coins >= coinAmount)
        {
            //Give the player more attack damage depending on the class
            //(Both Archer and Mage uses bullets)
            if (player.isKnight) swordAttack.attackDamage += 1;
            if (player.isArcher || player.isMage) bulletAttack.bulletDamage += 1;

            //Remove the amount of the money that the player spend
            player.CoinsAmount(-coinAmount);

            //Make more expensive every time when the player buy the damage boost
            coinAmount += 3;

            //Shows the amount of coin that the player need on the text
            coinText.text = coinAmount.ToString();
        }
    }
}
