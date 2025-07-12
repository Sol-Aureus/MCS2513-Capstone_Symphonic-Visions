using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ItemManager : MonoBehaviour
{
    [Header("Item Displays")]
    [SerializeField] private Image leftItemPicture; // Picture display for the item on the left
    [SerializeField] private TextMeshProUGUI leftItemText; // Text display for the item on the left
    [SerializeField] private Image rightItemPicture; // Picture display for the item on the right
    [SerializeField] private TextMeshProUGUI rightItemText; // Text display for the item on the right

    [Header("Images")]
    [SerializeField] private Sprite[] listOfSprites; // List of sprites to be used for item pictures

    [Header("Item List")]
    [SerializeField] private Item[] itemList; // List of items available in the game

    private struct Item
    {
        // Constructor to initialize an item with its properties
        public Item(string name, string description, Sprite picture, float healthBuff, float attackBuff, float defenseBuff)
        {
            itemName = name;
            itemDescription = description;
            itemPicture = picture;
            this.healthBuff = healthBuff;
            this.attackBuff = attackBuff;
            this.defenseBuff = defenseBuff;
        }

        // Properties of the item
        public string itemName { get; } // Name of the item
        public string itemDescription { get; }  // Description of the item
        public Sprite itemPicture { get; } // Picture of the item (not used in this example, but can be set later)
        public float healthBuff { get; }  // Health buff provided by the item
        public float attackBuff { get; }  // Attack buff provided by the item
        public float defenseBuff { get; }  // Defense buff provided by the item
    }

    private Item leftItem; // Item currently displayed on the left
    private Item rightItem; // Item currently displayed on the right

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize the item list with some example items
        itemList = new Item[]
        {
            new Item("Health Potion", "Restores 50 health points.", listOfSprites[0], 50, 0, 0),
            new Item("Attack Boost", "Increases attack power by 20.", listOfSprites[1], 0, 20, 0),
            new Item("Defense Shield", "Increases defense by 15.", listOfSprites[2], 0, 0, 15)
        };
    }

    // Start is called before the first frame update
    void Start()
    {
       Debug.Log("Item Manager Initialized");
       foreach (var item in itemList)
       {
            Debug.Log($"Item: {item.itemName}, Description: {item.itemDescription}, Health Buff: {item.healthBuff}, Attack Buff: {item.attackBuff}, Defense Buff: {item.defenseBuff}");
       }
    }

    // Function to set the right item to a random item from the list
    public void SetItemDisplay()
    {
        // Set the left item to a random item from the list
        int randomIndex1 = Random.Range(0, itemList.Length);
        int randomIndex2 = Random.Range(0, itemList.Length);
        while (randomIndex2 == randomIndex1)
        {
            // Ensure the right item is different from the left item
            randomIndex2 = Random.Range(0, itemList.Length);
        }

        leftItemPicture.sprite = itemList[randomIndex1].itemPicture;
        leftItemText.text = itemList[randomIndex1].itemName + "\n" + itemList[randomIndex1].itemDescription;
        leftItem = itemList[randomIndex1];

        rightItemPicture.sprite = itemList[randomIndex2].itemPicture;
        rightItemText.text = itemList[randomIndex2].itemName + "\n" + itemList[randomIndex2].itemDescription;
        rightItem = itemList[randomIndex2];

        Debug.Log($"Left Item Set: {itemList[randomIndex1].itemName}");
        Debug.Log($"Right Item Set: {itemList[randomIndex2].itemName}");
    }

    // Function to grab the item on the left
    public void GrabLeftItem()
    {
        // Logic to grab the item on the left
        Debug.Log("Grabbed left item");
    }

    // Function to grab the item on the right
    public void GrabRightItem()
    {
        // Logic to grab the item on the right
        Debug.Log("Grabbed right item");
    }
}
