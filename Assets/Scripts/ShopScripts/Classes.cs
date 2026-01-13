using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Classes : MonoBehaviour
{
    public PlayerMovement player;
    public Button button;
    public TextMeshProUGUI amountCoinText;
    private int coinAmount = 4;
    public GameObject classUI;
    public GameObject[] classButtons;
    public GameObject[] buttonsUI;

    public bool isArcher = false;
    public bool isKnight = false;
    public bool isMage = false;

    void Start()
    {
        //Call Class if the button is pressed
        button.onClick.AddListener(Class);

        amountCoinText.text = coinAmount.ToString();
    }
    void Class()
    {
        //If the player has more coins than the amount needed, he is able to buy
        if (player.coins >= coinAmount)
        {
            //The player loose the first initial class
            player.isNoob = false;

            //If the button is the Archer button, the player becomes an archer
            if (isArcher) player.isArcher = true;

            //If the button is the Knight button, the player becomes an knight
            if (isKnight) player.isKnight = true;

            //If the button is the Mage button, the player becomes an mage
            if (isMage) player.isMage = true;

            //Remove the amount of the money that the player spend
            player.CoinsAmount(-coinAmount);

            //Activate the class UI
            classUI.SetActive(true);

            //Deactivate all the buttons
            foreach (GameObject go in classButtons)
            {
                go.SetActive(false);
            }
        }
    }
}