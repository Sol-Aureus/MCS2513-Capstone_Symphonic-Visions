using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    // Makes functions easy to access
    public static RoomManager main;

    [Header("Rooms")]
    [SerializeField] private GameObject[] roomsTwo; // Array to hold all room buttons
    [SerializeField] private GameObject[] roomsThree; // Array to hold all room buttons
    [SerializeField] private Selectable[] statList; // Array of the stat buttons for the rooms
    [SerializeField] private GameObject[] floors; // Array to hold room layouts
    [SerializeField] private GameObject treasureRoom; // Reference to the treasure room prefab
    [SerializeField] private GameObject enemyRoom; // Reference to the enemy room prefab


    [Header("Chances")]
    [SerializeField] private float chanceForTreasure; // Chance for treasure in each room
    [SerializeField] private float chanceForEnemy; // Chance for enemy in each room
    [SerializeField] private float chanceForMystery; // Chance for a 50:50 chance of treasure or enemy
    [SerializeField] private float chanceForThreeEvents; // Chance for a room with three events (treasure, enemy, mystery)

    [Header("Ui Script")]
    [SerializeField] private SetUIInteraction uiInteraction; // Reference to the UI interaction script
    [SerializeField] private GameNavigation gameNavigation; // Reference to the game navigation script

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

    // Method to initialize rooms with random events
    public void GenerateRooms()
    {
        gameNavigation.HideAllScreens(); // Hide all screens before generating rooms

        float randomValue = Random.value;

        // Chance to determine if we use rooms with three events or two events
        if (randomValue < chanceForThreeEvents)
        {
            gameNavigation.LoadRoom3Screen(); // Activate the floor with three events
            foreach (GameObject room in roomsThree)
            {
                // Randomly determine the event for each room
                randomValue = Random.value;
                if (randomValue < chanceForTreasure)
                {
                    // Room contains treasure
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Treasure!";
                    room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadTreasureScreen); // Set the button to load the treasure screen
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy)
                {
                    // Room contains an enemy
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy!";
                    room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadFightScreen); // Set the button to load the treasure screen
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy + chanceForMystery)
                {
                    // Room contains a mystery (50:50 chance of treasure or enemy)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Unknown";
                    if (Random.value < 0.5f)
                    {
                        room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadTreasureScreen);
                    }
                    else
                    {
                        room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadFightScreen); // Set the button to load the fight screen
                    }
                }
                else
                {
                    // Room is empty (should never happen due to the way chances are set)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                }
            }

            // Set the UI interaction to the first room with three events
            uiInteraction.SetUiElement(statList[1]);
            uiInteraction.JumpToElement();
        }
        else
        {
            gameNavigation.LoadRoom2Screen(); // Activate the floor with three events

            foreach (GameObject room in roomsTwo)
            {
                // Randomly determine the event for each room
                randomValue = Random.value;
                if (randomValue < chanceForTreasure)
                {
                    // Room contains treasure
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Treasure!";
                    room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadTreasureScreen); // Set the button to load the treasure screen
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy)
                {
                    // Room contains an enemy
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy!";
                    room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadFightScreen); // Set the button to load the treasure screen
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy + chanceForMystery)
                {
                    // Room contains a mystery (50:50 chance of treasure or enemy)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Unknown";
                    if (Random.value < 0.5f)
                    {
                        room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadTreasureScreen);
                    }
                    else
                    {
                        room.GetComponent<Button>().onClick.AddListener(gameNavigation.LoadFightScreen); // Set the button to load the fight screen
                    }
                }
                else
                {
                    // Room is empty (should never happen due to the way chances are set)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                }
            }

            // Set the UI interaction to the first room with three events
            uiInteraction.SetUiElement(statList[0]);
            uiInteraction.JumpToElement();
        }
    }
}
