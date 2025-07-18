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
    private int currentHealth = 0; // Current health of the enemy
    [SerializeField] private int attack = 0; // Player's attack power
    [SerializeField] private int defense = 0; // Player's defense power
    [SerializeField] private int attackCharge = 0; // Attack charge for the enemy

    [SerializeField] private Sprite[] listOfSprites; // List of sprites to be used for enemy pictures

    [Header("Enemy Action Chances")]
    [SerializeField] private float chanceToAttack; // Chance for the enemy to attack
    [SerializeField] private float chanceToDefend; // Chance for the enemy to defend
    [SerializeField] private float chanceToCharge; // Chance for the enemy to make the next attack stronger
    [SerializeField] private float chanceToThink; // Chance for the enemy to do nothing

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI fightText; // UI text for displaying any fight-related messages
    [SerializeField] private GameObject fightTextBox; // UI text for displaying enemy stats
    [SerializeField] private Image enemyImage; // UI image for displaying the enemy picture
    [SerializeField] private Slider healthSlider; // UI slider for displaying enemy health

    private bool isBlocked = false; // Flag to check if the player has blocked an attack
    private bool fightEnded = false; // Flag to check if the fight has ended

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

    private int nextAction = 0; // Index for the next action to be performed

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
        currentHealth = health; // Set current health to initial health
        remainingPoints -= health;

        attack = Mathf.RoundToInt(Random.Range(10, remainingPoints * 0.7f));
        remainingPoints -= attack;

        float scaledDefense = remainingPoints * 0.01f;
        defense = Mathf.RoundToInt((scaledDefense / (1 + scaledDefense)) * 100);

        int randomIndex = Random.Range(0, enemyTypes.Length);

        enemyName = enemyTypes[randomIndex].enemyName;
        enemyDescription = enemyTypes[randomIndex].enemyDescription;
        enemyImage.sprite = enemyTypes[randomIndex].enemyPicture;

        DescribeEnemy();
        UpdateHealth();

        fightEnded = false; // Reset fight ended flag

        Debug.Log($"Enemy Stats - Health: {health}, Attack: {attack}, Defense: {defense}");
    }

    // Function to update the fight text with enemy stats
    private void DescribeEnemy()
    {
        fightText.text = $"{enemyName}:\n" +
                          $"Health: {health}\n" +
                          $"Attack: {attack}\n" +
                          $"Defense: {defense}\n" +
                          $"{enemyDescription}";

        ChooseAction(); // Choose the first action for the enemy
        NextActionText();

        fightTextBox.SetActive(true);
    }

    // Function to delay hiding the fight text
    private IEnumerator HideFightTextAfterDelay(float delay)
    {
        StartCoroutine(TextToSpeakControl.main.SpeakText(fightText.text, 1.2f)); // Start the end fight coroutine
        yield return new WaitForSeconds(delay);
        fightTextBox.SetActive(false);
        fightText.text = ""; // Clear the fight text after hiding
    }
    
    // Function to delay hiding the fight text
    private IEnumerator EndFight(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideFightTextAfterDelay(0f);
        RoomManager.main.GenerateRooms(); // Regenerate rooms after the fight ends
    }

    // Function to choose an action based on random chance
    public void ChooseAction()
    {
        float randomIndex = Random.value;

        if (randomIndex < chanceToAttack)
        {
            nextAction = 0; // Attack
        }
        else if (randomIndex < chanceToAttack + chanceToDefend)
        {
            nextAction = 1; // Defend
        }
        else if (randomIndex < chanceToAttack + chanceToDefend + chanceToCharge)
        {
            nextAction = 2; // Charge
        }
        else
        {
            nextAction = 3; // Think
        }

    }

    // Function to use the queued action
    public void UseQueuedAction()
    {
        if (fightEnded)
        {
            return; // If the fight has ended, do not execute any action
        }
        if (nextAction == 0)
        {
            Attack();
        }
        else if (nextAction == 1)
        {
            Defend();
        }
        else if (nextAction == 2)
        {
            Charge();
        }
        else if (nextAction == 3)
        {
            Think();
        }
        ChooseAction(); // Choose the next action after the current one is executed
        NextActionText();
    }

    // Function to display the next action text
    private void NextActionText()
    {
        if (nextAction == 0)
        {
            fightText.text += $"\n{enemyName} is preparing to attack!";
        }
        else if (nextAction == 1)
        {
            fightText.text += $"\n{enemyName} is going to defend!";
        }
        else if (nextAction == 2)
        {
            fightText.text += $"\n{enemyName} is going to charge its next attack!";
        }
        else if (nextAction == 3)
        {
            fightText.text += $"\n{enemyName} will take some time to think about its next move!";
        }
        StartCoroutine(HideFightTextAfterDelay(12f));
    }

    // Function to deal damage to the player
    private void Attack()
    {
        fightText.text += $"{enemyName} attacks you for {attack * (attackCharge + 1)} damage!";
        fightTextBox.SetActive(true);

        PlayerController.main.TakeDamage(attack * (attackCharge + 1));
        attackCharge = 0; // Reset attack charge after using it
    }

    // Function to block the next attack
    private void Defend()
    {
        isBlocked = true; // Set block status to true
        fightText.text += $"{enemyName} is blocking your attack!";
        fightTextBox.SetActive(true);
    }

    // Function to charge the next attack
    private void Charge()
    {
        attackCharge++;
        fightText.text += $"{enemyName} is charging its next attack!";
        fightTextBox.SetActive(true);
    }

    // Function to make the enemy think, simulating a delay in action
    private void Think()
    {
        fightText.text += $"{enemyName} is thinking...";
        fightTextBox.SetActive(true);
    }
    
    // Function to damage the player
    public void TakeDamage(int damage)
    {
        if (isBlocked)
        {
            fightText.text += $"You attacked the enemy for {PlayerController.main.GetAttack()} damage!\n";

            isBlocked = false; // Reset block status after blocking
            damage = Mathf.RoundToInt(damage * (1 - ((float)defense / 100))); // Reduce damage based on defense

            fightText.text += $"{enemyName} blocked the attack!\n" +
                $"They took {damage} damage\n";
            fightTextBox.SetActive(true);

            currentHealth -= damage;
            UpdateHealth();
        }
        else
        {
            fightText.text += $"You attacked the enemy for {PlayerController.main.GetAttack()} damage!\n";
            fightText.text += $"{enemyName} took {damage} damage\n";
            fightTextBox.SetActive(true);

            currentHealth -= damage;
            UpdateHealth();
        }
        StartCoroutine(HideFightTextAfterDelay(12f));

        if (currentHealth <= 0)
        {
            fightText.text = $"{enemyName} has been defeated!\n"
                + "You gain +8% to all stats!";
            fightTextBox.SetActive(true);
            PlayerController.main.MultiplyHealth(0.08f);
            PlayerController.main.MultiplyAttack(0.08f);
            PlayerController.main.MultiplyDefense(0.08f);
            fightEnded = true; // Set fight ended flag to true
            StartCoroutine(EndFight(4f));
        }
    }

    // Function to show the enemy's health in the UI
    public void UpdateHealth()
    {
        healthSlider.value = (float)currentHealth / health;
        Debug.Log($"Enemy Health Updated: {currentHealth}/{health}");
    }
}
