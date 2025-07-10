using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNavigation : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject statScreen; // Reference to the stats screen
    [SerializeField] private GameObject fightScreen; // Reference to the fight screen
    [SerializeField] private GameObject treasureScreen; // Reference to the treasure screen
    [SerializeField] private GameObject room2Screen; // Reference to the room 2 screen
    [SerializeField] private GameObject room3Screen; // Reference to the room 3 screen

    // Start is called before the first frame update
    void Start()
    {
        HideAllScreens();

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
        statScreen.SetActive(false);
        fightScreen.SetActive(false);
        treasureScreen.SetActive(false);
        room2Screen.SetActive(false);
        room3Screen.SetActive(false);
    }

    // Method to load the stat screen
    public void LoadStatScreen()
    {
        HideAllScreens();
        statScreen.SetActive(true);
    }

    // Method to load the fight screen
    public void LoadFightScreen()
    {
        HideAllScreens();
        fightScreen.SetActive(true);
    }

    // Method to load the treasure screen
    public void LoadTreasureScreen()
    {
        HideAllScreens();
        treasureScreen.SetActive(true);
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
}
