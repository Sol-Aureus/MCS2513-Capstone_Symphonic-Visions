using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameNavigation : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject statScreen; // Reference to the stats screen
    [SerializeField] private GameObject fightScreen; // Reference to the fight screen
    [SerializeField] private GameObject treasureScreen; // Reference to the treasure screen
    [SerializeField] private GameObject room2Screen; // Reference to the room 2 screen
    [SerializeField] private GameObject room3Screen; // Reference to the room 3 screen

    [Header("Default Buttons")]
    [SerializeField] private GameObject fightScreenButton; // Reference to the leave button on the stat screen
    [SerializeField] private GameObject treasureScreenButton; // Reference to the leave button on the treasure screen

    [Header("References")]
    [SerializeField] private SetUIInteraction uiInteraction;
    [SerializeField] private ItemManager itemManager; // Reference to the Item Manager for item interactions
    [SerializeField] private AudioClip buttonClickSound; // Sound to play when a button is clicked


    [SerializeField] private EventSystem eventSystem; // Reference to the Event System in the scene
    private GameObject lastSelectedObject; // Store the last selected object for the Event System


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

    // Update is called once per frame
    void Update()
    {
        RoomTest(KeyCode.H);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStats.main.MultiplyHealth(0.1f); // Increase health by 10%
            PlayerStats.main.UpdateStats(); // Update player stats
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStats.main.MultiplyAttack(0.1f); // Increase attack by 10%
            PlayerStats.main.UpdateStats(); // Update player stats
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerStats.main.MultiplyDefense(0.1f); // Increase defense by 10%
            PlayerStats.main.UpdateStats(); // Update player stats
        }
    }

    // Debugging method to quickly test room generation
    private void RoomTest(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            // If the H key is pressed, generate rooms
            RoomManager.main.GenerateRooms();
            Debug.Log("Rooms generated.");
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

        PlayerStats.main.UpdateStats(); // Initialize player stats when the stat screen is opened

        if (isStatScreenActive)
        {
            Time.timeScale = 0; // Pause the game when the stat screen is active
        }
        else
        {
            Time.timeScale = 1; // Resume the game when the stat screen is closed
        }
    }

    // Method to load the fight screen
    public void LoadFightScreen()
    {
        HideAllScreens();
        fightScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(fightScreenButton);
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
        HideAllScreens();
        room2Screen.SetActive(true);
    }

    // Method to load the room 3 screen
    public void LoadRoom3Screen()
    {
        HideAllScreens();
        room3Screen.SetActive(true);
    }

    public void Leave()
    {
        RoomManager.main.GenerateRooms(); // Regenerate rooms when leaving the stat screen
    }

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
}
