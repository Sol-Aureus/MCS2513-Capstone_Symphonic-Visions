using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int health = 100; // Player's health
    [SerializeField] private int attack = 10; // Player's attack power
    [SerializeField] private int defense = 5; // Player's defense power\

    [Header("Stat Buffs")]
    [SerializeField] private float healthBuff = 0; // Buff to player's health
    [SerializeField] private float attackBuff = 0; // Buff to player's attack power
    [SerializeField] private float defenseBuff = 0; // Buff to player's defense power

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI healthText; // UI text for displaying health
    [SerializeField] private TextMeshProUGUI attackText; // UI text for displaying attack power
    [SerializeField] private TextMeshProUGUI defenseText; // UI text for displaying defense power

    private float maxHealth; // Maximum health of the player
    private float currentHealth; // Current health of the player
    private float currentAttack; // Current attack power of the player
    private float currentDefense; // Current defense power of the player

    // Start is called before the first frame update
    void Start()
    {
        StartFight();
    }

    // Function to initialize the player's stats
    public void StartFight()
    {
        // Initialize the player's stats at the start of a fight
        maxHealth = health * healthBuff; ;
        currentHealth = health * healthBuff;
        currentAttack = attack * attackBuff;
        currentDefense = defense * defenseBuff;
        Debug.Log($"Fight started with Health: {currentHealth}, Attack: {currentAttack}, Defense: {currentDefense}");
    }

    // Function to get the player's current health
    public float GetHealth()
    {
        return maxHealth/currentHealth;
    }

    // Function to set the player's health
    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    // Function to get the player's attack power
    public float GetAttack()
    {
        return currentAttack;
    }

    // Function to set the player's attack power
    public void SetAttack(float newAttack)
    {
        currentAttack = newAttack;
    }

    // Function to get the player's defense power
    public float GetDefense()
    {
        return currentDefense;
    }

    // Function to set the player's defense power
    public void SetDefense(float newDefense)
    {
        currentDefense = newDefense;
    }

    // OnGUI is called for rendering and handling GUI events
    private void OnGUI()
    {
        healthText.text = $"Max Health: {maxHealth}";
        attackText.text = $"Attack: {currentAttack}";
        defenseText.text = $"Defense: {currentDefense}";
    }
}
