using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private GameObject[] roomsTwo; // Array to hold all room buttons
    [SerializeField] private GameObject[] roomsThree; // Array to hold all room buttons
    [SerializeField] private GameObject[] floors; // Array to hold room layouts

    [Header("Chances")]
    [SerializeField] private float chanceForTreasure; // Chance for treasure in each room
    [SerializeField] private float chanceForEnemy; // Chance for enemy in each room
    [SerializeField] private float chanceForMystery; // Chance for a 50:50 chance of treasure or enemy
    [SerializeField] private float chanceForThreeEvents; // Chance for a room with three events (treasure, enemy, mystery)

    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to initialize rooms with random events
    public void GenerateRooms()
    {
        foreach (GameObject floor in floors)
        {
            // Set the floor inactive
            floor.SetActive(false);
        }

        float randomValue = Random.value;

        // Chance to determine if we use rooms with three events or two events
        if (randomValue < chanceForThreeEvents)
        {
            floors[1].SetActive(true); // Activate the floor with three events
            foreach (GameObject room in roomsThree)
            {
                // Randomly determine the event for each room
                randomValue = Random.value;
                if (randomValue < chanceForTreasure)
                {
                    // Room contains treasure
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Treasure!";
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy)
                {
                    // Room contains an enemy
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy!";
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy + chanceForMystery)
                {
                    // Room contains a mystery (50:50 chance of treasure or enemy)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Unknown";
                }
                else
                {
                    // Room is empty (should never happen due to the way chances are set)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                }
            }
        }
        else
        {
            floors[0].SetActive(true); // Activate the floor with two events
            foreach (GameObject room in roomsTwo)
            {
                // Randomly determine the event for each room
                randomValue = Random.value;
                if (randomValue < chanceForTreasure)
                {
                    // Room contains treasure
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Treasure!";
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy)
                {
                    // Room contains an enemy
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy!";
                }
                else if (randomValue < chanceForTreasure + chanceForEnemy + chanceForMystery)
                {
                    // Room contains a mystery (50:50 chance of treasure or enemy)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Unknown";
                }
                else
                {
                    // Room is empty (should never happen due to the way chances are set)
                    room.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                }
            }
        }
    }
}
