using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // Makes functions easy to access
    public static PlayerStats main;

    [Header("Player Stats")]
    [SerializeField] private int health = 100; // Player's health
    [SerializeField] private int attack = 10; // Player's attack power
    [SerializeField] private int defense = 5; // Player's defense power\

    [Header("Stat Buffs")]
    [SerializeField] private float healthBuff = 1; // Buff to player's health
    [SerializeField] private float attackBuff = 1; // Buff to player's attack power
    [SerializeField] private float defenseBuff = 0; // Buff to player's defense power

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI healthText; // UI text for displaying health
    [SerializeField] private TextMeshProUGUI attackText; // UI text for displaying attack power
    [SerializeField] private TextMeshProUGUI defenseText; // UI text for displaying defense power

    private float maxHealth; // Maximum health of the player
    private float currentHealth; // Current health of the player
    private float currentAttack; // Current attack power of the player
    private float currentDefense; // Current defense power of the player

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

    // Function to initialize the player's stats
    public void UpdateStats()
    {
        // Initialize the player's stats at the start of a fight
        maxHealth = Mathf.RoundToInt(health * healthBuff);
        currentHealth = Mathf.RoundToInt(health * healthBuff);
        currentAttack = Mathf.RoundToInt(attack * attackBuff);
        currentDefense = Mathf.RoundToInt((1 - (1 / (1 + defense * defenseBuff))) * 100);
        Debug.Log($"Fight started with Health: {currentHealth}, Attack: {currentAttack}, Defense: {currentDefense}");
    }

    // Function to get the player's current health
    public float GetHealth()
    {
        return maxHealth/currentHealth;
    }

    // Function to set the player's health
    public void MultiplyHealth(float multiplier)
    {
        healthBuff += multiplier;
    }

    // Function to get the player's attack power
    public float GetAttack()
    {
        return currentAttack;
    }

    // Function to set the player's attack power
    public void MultiplyAttack(float multiplier)
    {
        attackBuff += multiplier;
    }

    // Function to get the player's defense power
    public float GetDefense()
    {
        return currentDefense;
    }

    // Function to set the player's defense power
    public void MultiplyDefense(float multiplier)
    {
        defenseBuff += multiplier;
    }

    // OnGUI is called for rendering and handling GUI events
    private void OnGUI()
    {
        healthText.text = $"Max Health: {maxHealth}";
        attackText.text = $"Attack: {currentAttack}";
        defenseText.text = $"Defense: {currentDefense}";
    }
}
