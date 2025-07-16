using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Makes functions easy to access
    public static EnemyController main;

    [Header("Enemy Stats")]
    [SerializeField] private int startingPoints = 100; // Starting points for the enemy
    [SerializeField] private int health = 0; // Player's health
    [SerializeField] private int attack = 0; // Player's attack power
    [SerializeField] private int defense = 0; // Player's defense power

    [SerializeField] private Sprite[] listOfSprites; // List of sprites to be used for enemy pictures

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI fightText; // UI text for displaying any fight-related messages
    [SerializeField] private GameObject fightTextBox; // UI text for displaying enemy stats
    [SerializeField] private Image enemyImage; // UI image for displaying the enemy picture

    private EnemyType[] enemyTypes;

    private struct EnemyType
    {
        // Constructor to initialize an enemy with its properties
        public EnemyType(string name, string description, Sprite picture)
        {
            enemyName = name;
            enemyDescription = description;
            enemyPicture = picture;
        }

        // Properties of the enemy
        public string enemyName { get; } // Name of the enemy
        public string enemyDescription { get; }  // Description of the enemy
        public Sprite enemyPicture { get; } // Picture of the enemy
    }

    private string enemyName;
    private string enemyDescription;

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

        // Initialize the enemy names and descriptions
        enemyTypes = new EnemyType[]
        {
            new EnemyType("Goblin", "A small, mischievous creature.", listOfSprites[0]),
            new EnemyType("Orc", "A brutish warrior with a bad temper.", listOfSprites[1]),
            new EnemyType("Troll", "A large, slow-moving giant.", listOfSprites[2]),
            new EnemyType("Dragon", "A fearsome beast that breathes fire.", listOfSprites[3]),
            new EnemyType("Vampire", "A bloodsucking creature of the night.", listOfSprites[4]),
            new EnemyType("Skeleton", "A rattling undead warrior.", listOfSprites[5]),
            new EnemyType("Werewolf", "A ferocious beast under the full moon.", listOfSprites[6]),
            new EnemyType("Witch", "A cunning spellcaster with dark magic.", listOfSprites[7]),
            new EnemyType("Golem", "A hulking creature made of stone.", listOfSprites[8]),
            new EnemyType("Shade", "A shadowy figure that drains life.", listOfSprites[9])
        };
    }

    // Allocate points to health, attack, and defense based on starting points and floor level
    public void AllocatePoints(int floor)
    {
        int remainingPoints = Mathf.RoundToInt(startingPoints * (1 + floor * 0.05f));

        health = Mathf.RoundToInt(Random.Range(10, remainingPoints * 0.8f));
        remainingPoints -= health;

        attack = Mathf.RoundToInt(Random.Range(10, remainingPoints * 0.8f));
        remainingPoints -= attack;

        float scaledDefense = remainingPoints * 0.01f;
        defense = Mathf.RoundToInt((scaledDefense / (1 + scaledDefense)) * 100);

        int randomIndex = Random.Range(0, enemyTypes.Length);

        enemyName = enemyTypes[randomIndex].enemyName;
        enemyDescription = enemyTypes[randomIndex].enemyDescription;
        enemyImage.sprite = enemyTypes[randomIndex].enemyPicture;

        Debug.Log($"Enemy Stats - Health: {health}, Attack: {attack}, Defense: {defense}");
    }

    // Function to update the fight text with enemy stats
    public void DescribeEnemy()
    {
        fightText.text = $"{enemyName}:\n" +
                          $"Health: {health}\n" +
                          $"Attack: {attack}\n" +
                          $"Defense: {defense}\n" +
                          $"{enemyDescription}";
        fightTextBox.SetActive(true);
        StartCoroutine(HideFightTextAfterDelay(5f));
    }

    // Function to delay hiding the fight text
    private IEnumerator HideFightTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fightTextBox.SetActive(false);
    }

}
