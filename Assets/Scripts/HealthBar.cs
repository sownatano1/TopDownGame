using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 1;
    public float currentHealth = 1;

    public GameObject gameOverScreen;
    public GameObject gameUI;
    private SpawnManager spawnManager;
    public TextMeshProUGUI waveNumber;

    private CursorFollower cursorFollower;

    public Button restartButton;
    
    void Start()
    {
        //In the start the player current health is the same as the player max health
        currentHealth = maxHealth;

        spawnManager = FindAnyObjectByType<SpawnManager>().GetComponent<SpawnManager>();
        cursorFollower = FindAnyObjectByType<CursorFollower>().GetComponent<CursorFollower>();

        restartButton.onClick.AddListener(Restart);
    }

    void Update()
    {
        //The fill amount of the health bar has the same value to the player current health
        healthBar.fillAmount = currentHealth;

        //If the current health reach 0 or lower the player loose
        if (currentHealth <= 0)
        {
            GameOver();
        }

        //If the current health is bigger than the max health it limits to not have more than the max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        waveNumber.text = spawnManager.wavesNumber.ToString();
    }

    void GameOver()
    {
        //Activate the game over screen
        gameOverScreen.SetActive(true);

        //Disable the game UI
        gameUI.SetActive(false);

        //Activate the cursor
        cursorFollower.isCursor = false;
    }

    void Restart()
    {
        //Load the scene so it restart everything
        SceneManager.LoadScene("MainScene");
    }
}