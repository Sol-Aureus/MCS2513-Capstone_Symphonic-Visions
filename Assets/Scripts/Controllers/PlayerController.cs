using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Makes functions easy to access
    public static PlayerController main;

    [Header("Player Stats")]
    [SerializeField] private int health = 100; // Player's health
    [SerializeField] private int attack = 10; // Player's attack power
    [SerializeField] private int defense = 5; // Player's defense power

    [Header("Stat Buffs")]
    [SerializeField] private float healthBuff = 1; // Buff to player's health
    [SerializeField] private float attackBuff = 1; // Buff to player's attack power
    [SerializeField] private float defenseBuff = 0; // Buff to player's defense power

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI healthText; // UI text for displaying health
    [SerializeField] private TextMeshProUGUI attackText; // UI text for displaying attack power
    [SerializeField] private TextMeshProUGUI defenseText; // UI text for displaying defense power
    [SerializeField] private GameObject fightTextBox; // UI text box for displaying fight-related messages
    [SerializeField] private TextMeshProUGUI fightText; // UI text for displaying fight-related messages
    [SerializeField] private Slider healthSlider; // UI slider for displaying health

    private int maxHealth; // Maximum health of the player
    private int currentHealth; // Current health of the player
    private int currentAttack; // Current attack power of the player
    private int currentDefense; // Current defense power of the player
    private bool isBlocked = false; // Flag to check if the player is blocked

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateStats();
    }

    // Function to update the player's health slider
    public void UpdateHealth()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
        Debug.Log($"Player Health Updated: {currentHealth}/{maxHealth}");
    }

    public void ResetHealth()
    {
        // Reset the player's health to the maximum health
        currentHealth = maxHealth;
        UpdateHealth();
        Debug.Log("Player's health has been reset to maximum.");
    }

    // Function to initialize the player's stats
    public void UpdateStats()
    {
        // Initialize the player's stats at the start of a fight
        maxHealth = Mathf.RoundToInt(health * healthBuff);
        currentAttack = Mathf.RoundToInt(attack * attackBuff);
        float scaledDefense = defense * defenseBuff * 0.01f;
        currentDefense = Mathf.RoundToInt((scaledDefense / (1 + scaledDefense)) * 100);
        Debug.Log($"Fight started with Health: {currentHealth}, Attack: {currentAttack}, Defense: {currentDefense}");
    }

    // Function to get the player's current health
    public int GetHealth()
    {
        return currentHealth / maxHealth;
    }

    // Function to set the player's health
    public void MultiplyHealth(float multiplier)
    {
        healthBuff += multiplier;
    }

    // Function to get the player's attack power
    public int GetAttack()
    {
        return currentAttack;
    }

    // Function to set the player's attack power
    public void MultiplyAttack(float multiplier)
    {
        attackBuff += multiplier;
    }

    // Function to get the player's defense power
    public void Defend()
    {
        isBlocked = true;
    }

    // Function to set the player's defense power
    public void MultiplyDefense(float multiplier)
    {
        defenseBuff += multiplier;
    }

    // OnGUI is called for rendering and handling GUI events
    private void OnGUI()
    {
        healthText.text = $"Max Health: {health} (+{maxHealth - health})";
        attackText.text = $"Attack: {attack} (+{currentAttack - attack})";
        defenseText.text = $"Defense: {defense} (+{currentDefense - defense})";
    }

    // Function to damage the player
    public void TakeDamage(int damage)
    {
        if (isBlocked)
        {
            isBlocked = false; // Reset block status after blocking
            damage = Mathf.RoundToInt(damage * (1 - ((float)currentDefense / 100))); // Reduce damage based on defense

            fightText.text += $"\nYou blocked the attack!\n" +
                $"You took {damage} damage";
            fightTextBox.SetActive(true);

            currentHealth -= damage;
            UpdateHealth();
        }
        else
        {
            fightText.text += $"\nYou took {damage} damage";
            fightTextBox.SetActive(true);

            currentHealth -= damage;
            UpdateHealth();
        }
    }
}
