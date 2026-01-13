using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class ActivateAbilities : MonoBehaviour
{
    private Button button;
    public TextMeshProUGUI coinText;
    public Image coinImage;
    public int coinAmount;
    public PlayerMovement player;
    public bool pw1, pw2, pw3, pw4, pw5;

    private Turret turret;
    private MultipleArrows multipleArrows;
    private Dash dash;
    private ExplosiveArrow explosiveArrow;
    private ExtraArrow extraArrow;

    public GameObject ability;

    void OnEnable()
    {
        //Find all the abilities and their scripts
        turret = FindAnyObjectByType<Turret>().GetComponent<Turret>();
        multipleArrows = FindAnyObjectByType<MultipleArrows>().GetComponent<MultipleArrows>();
        dash = FindAnyObjectByType<Dash>().GetComponent<Dash>();
        explosiveArrow = FindAnyObjectByType<ExplosiveArrow>().GetComponent<ExplosiveArrow>();
        extraArrow = FindAnyObjectByType<ExtraArrow>().GetComponent<ExtraArrow>();

        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPressed);
    }

    void ButtonPressed()
    {
        if (player.coins >= coinAmount)
        {
            //Remove the amount of the money that the player spend
            player.CoinsAmount(-coinAmount);

            //Activate the ability so we can see in the hud
            ability.SetActive(true);

            //Disable the coins and the button
            coinText.enabled = false;
            coinImage.enabled = false;
            button.interactable = false;
        }

        //If the button is set to pw1 (power1) it activate the first ability which is Extra Arrow
        if (pw1) extraArrow.isActivated = true;
        if (pw2) explosiveArrow.isActivated = true;
        if (pw3) dash.isActivated = true;
        if (pw4) multipleArrows.isActivated = true;
        if (pw5) turret.isActivated = true;
    }
}
