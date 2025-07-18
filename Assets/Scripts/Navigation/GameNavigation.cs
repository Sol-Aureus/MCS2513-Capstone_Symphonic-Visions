using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameNavigation : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject statScreen; // Reference to the stats screen
    [SerializeField] private GameObject fightScreen; // Reference to the fight screen
    [SerializeField] private GameObject treasureScreen; // Reference to the treasure screen
    [SerializeField] private GameObject room2Screen; // Reference to the room 2 screen
    [SerializeField] private GameObject room3Screen; // Reference to the room 3 screen
    [SerializeField] private GameObject deathScreen; // Reference to the death screen

    [Header("Default Buttons")]
    [SerializeField] private GameObject fightScreenButton; // Reference to the leave button on the stat screen
    [SerializeField] private GameObject treasureScreenButton; // Reference to the leave button on the treasure screen

    [Header("References")]
    [SerializeField] private SetUIInteraction uiInteraction;
    [SerializeField] private ItemManager itemManager; // Reference to the Item Manager for item interactions
    [SerializeField] private AudioClip buttonClickSound; // Sound to play when a button is clicked
    [SerializeField] private EventSystem eventSystem; // Reference to the Event System in the scene
    [SerializeField] private TextMeshProUGUI deathText; // Text to display when the player dies
    private GameObject lastSelectedObject; // Store the last selected object for the Event System

    private int currentRoom = 0; // Current room index, used for room navigation


    // Stat screen status
    private bool isStatScreenActive = false;

    // Reset is called when attached to a GameObject in the scene
    private void Reset()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.Log("Did not find an Event System in Scene", context: this);
        }
    }

        // Start is called before the first frame update
    void Start()
    {
        HideAllScreens();
        statScreen.SetActive(isStatScreenActive); // Show the stat screen at the start

        // Initialize the RoomManager to ensure it is ready for room generation
        if (RoomManager.main == null)
        {
            Debug.LogError("RoomManager is not initialized. Please ensure it is set up in the scene.");
        }
        else
        {
            RoomManager.main.GenerateRooms(); // Generate rooms at the start
            Debug.Log("Rooms generated at start.");
        }
    }

    // Method to hide all screens
    public void HideAllScreens()
    {
        fightScreen.SetActive(false);
        treasureScreen.SetActive(false);
        room2Screen.SetActive(false);
        room3Screen.SetActive(false);
    }

    // Method to load the stat screen
    public void LoadStatScreen()
    {
        Debug.Log(eventSystem.currentSelectedGameObject.GetComponentInChildren<Selectable>());
        uiInteraction.SetUiElement(eventSystem.currentSelectedGameObject.GetComponentInChildren<Selectable>()); // Set the UI interaction to the stat screen

        isStatScreenActive = !isStatScreenActive; // Toggle the stat screen status
        statScreen.SetActive(isStatScreenActive);

        PlayerController.main.UpdateStats(); // Initialize player stats when the stat screen is opened

        if (isStatScreenActive)
        {
            StartCoroutine(TextToSpeakControl.main.SpeakText($"Your Health is {PlayerController.main.GetHealth()}. Your Attack is {PlayerController.main.GetAttack()}. Your Defense is {PlayerController.main.GetDefense()}.", 2));
        }
    }

    // Method to load the fight screen
    public void LoadFightScreen()
    {
        HideAllScreens();
        fightScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(fightScreenButton);
        PlayerController.main.ResetHealth(); // Update player stats when the fight screen is opened
        EnemyController.main.AllocatePoints(currentRoom);
    }

    // Method to load the treasure screen
    public void LoadTreasureScreen()
    {
        HideAllScreens();
        treasureScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(treasureScreenButton);
        itemManager.SetItemDisplay(); // Load items when the treasure screen is opened
    }

    // Method to load the room 2 screen
    public void LoadRoom2Screen()
    {
        currentRoom++; // Increment the current room index
        HideAllScreens();
        room2Screen.SetActive(true);
    }

    // Method to load the room 3 screen
    public void LoadRoom3Screen()
    {
        currentRoom++; // Increment the current room index
        HideAllScreens();
        room3Screen.SetActive(true);
    }

    // Method to leave the fight screen and return to the rooms selection
    public void Leave()
    {
        RoomManager.main.GenerateRooms(); // Regenerate rooms when leaving the stat screen
    }

    // Function to debuff the player after running away from a fight
    public void Run()
    {
        PlayerController.main.MultiplyHealth(-0.08f);
        PlayerController.main.MultiplyAttack(-0.08f);
        PlayerController.main.MultiplyDefense(-0.08f);
        Leave(); // Leave the fight screen and return to the rooms selection
    }

    // Function to play the button click sound
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            SoundFXManager.main.PlaySound(buttonClickSound, transform, 1); // Play the button click sound
        }
        else
        {
            Debug.LogWarning("Button click sound is not set.");
        }
    }

    // Function to display the death screen when the player dies
    public void DiplayDeathScreen()
    {
        Time.timeScale = 0; // Pause the game when the death screen is displayed

        deathScreen.SetActive(true); // Show the death screen   
        deathText.text = $"You surived {currentRoom} rooms\n"
            + "You finals stats:\n"
            + $"Health: {PlayerController.main.GetHealth()}\n"
            + $"Attack: {PlayerController.main.GetAttack()}\n"
            + $"Defense: {PlayerController.main.GetDefense()}"; // Set the death text

        StartCoroutine(TextToSpeakControl.main.SpeakText(deathText.text, 2)); // Speak the death text
    }

    // Function to reset the level, can be used to restart the game or reset the current level
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to load the main menu scene
    public void MainMenu()
    {
        Time.timeScale = 1; // Resume the game time before loading the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
