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
        public Sprite itemPicture { get; } // Picture of the item
        public float healthBuff { get; }  // Health buff provided by the item
        public float attackBuff { get; }  // Attack buff provided by the item
        public float defenseBuff { get; }  // Defense buff provided by the item
    }

    private Item leftItem; // Item currently displayed on the left
    private Item rightItem; // Item currently displayed on the right

    private Item nullItem; // Default item when no item is selected

    private bool hasTakenItem = false; // Flag to check if an item has been taken

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize the item list with a variety of balanced items (buffs and debuffs)
        itemList = new Item[]
        {
            new Item("Health Potion", "Increases health (10%).", listOfSprites[1], 0.1f, 0, 0),
            new Item("Attack Boost", "Increases attack power (10%).", listOfSprites[2], 0, 0.1f, 0),
            new Item("Defense Shield", "Increases defense (10%).", listOfSprites[3], 0, 0, 0.1f),
            new Item("Berserker Brew", "Greatly increases attack (+20%) but lowers defense (-10%).", listOfSprites[4], 0, 0.2f, -0.1f),
            new Item("Iron Bark", "Boosts defense (+15%) but reduces attack (-5%).", listOfSprites[5], 0, -0.05f, 0.15f),
            new Item("Vitality Elixir", "Increases health (+15%) but lowers attack (-5%).", listOfSprites[6], 0.15f, -0.05f, 0),
            new Item("Glass Cannon", "Massive attack (+25%) but health drops (-10%).", listOfSprites[7], -0.1f, 0.25f, 0),
            new Item("Stone Skin", "Huge defense (+20%) but health drops (-10%).", listOfSprites[8], -0.1f, 0, 0.2f),
            new Item("Balanced Tonic", "Slight boost to all stats (+5%).", listOfSprites[9], 0.05f, 0.05f, 0.05f),
            new Item("Risky Remedy", "Big health boost (+20%) but defense drops (-10%).", listOfSprites[10], 0.2f, 0, -0.1f),
            new Item("Swift Strike", "Attack (+10%) and defense (+5%), but health drops (-5%).", listOfSprites[11], -0.05f, 0.1f, 0.05f),
            new Item("Fortified Brew", "Defense (+10%) and health (+5%), but attack drops (-5%).", listOfSprites[12], 0.05f, -0.05f, 0.1f),
            new Item("Cursed Draught", "All stats drop (-5%).", listOfSprites[13], -0.05f, -0.05f, -0.05f),
            new Item("Hero's Mix", "Health (+10%), attack (+10%), but defense drops (-10%).", listOfSprites[14], 0.1f, 0.1f, -0.1f),
            new Item("Guardian's Gift", "Defense (+15%), health (+5%), but attack drops (-10%).", listOfSprites[15], 0.05f, -0.1f, 0.15f)
        }; 
        nullItem = new Item("Remnants", "The item's effects have been absorbed.", listOfSprites[0], 0, 0, 0);
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

        hasTakenItem = false; // Reset the flag when new items are set

        StartCoroutine(TextToSpeakControl.main.SpeakText($"You found two items: Left item is {leftItem.itemName} and right item is {rightItem.itemName}. {leftItem.itemName}, {leftItem.itemDescription}. {rightItem.itemName}, {rightItem.itemDescription}. Choose wisely!", 2));

        Debug.Log($"Left Item Set: {itemList[randomIndex1].itemName}");
        Debug.Log($"Right Item Set: {itemList[randomIndex2].itemName}");
    }

    // Function to grab the item on the left
    public void GrabLeftItem()
    {
        if (hasTakenItem)
        {
            Debug.Log("An item has already been taken. Cannot grab another item.");
            return; // Exit if an item has already been taken
        }

        PlayerController.main.MultiplyHealth(leftItem.healthBuff);
        PlayerController.main.MultiplyAttack(leftItem.attackBuff);
        PlayerController.main.MultiplyDefense(leftItem.defenseBuff);

        StartCoroutine(TextToSpeakControl.main.SpeakText($"You grabbed the {leftItem.itemName}.", 0)); // Speak the item name and description

        hasTakenItem = true; // Set the flag to true when an item is taken
        leftItemPicture.sprite = nullItem.itemPicture;
        leftItemText.text = nullItem.itemName + "\n" + nullItem.itemDescription;
        leftItem = nullItem; // Set the left item to null item

        Debug.Log("Grabbed left item");
    }

    // Function to grab the item on the right
    public void GrabRightItem()
    {
        if (hasTakenItem)
        {
            Debug.Log("An item has already been taken. Cannot grab another item.");
            return; // Exit if an item has already been taken
        }

        PlayerController.main.MultiplyHealth(rightItem.healthBuff);
        PlayerController.main.MultiplyAttack(rightItem.attackBuff);
        PlayerController.main.MultiplyDefense(rightItem.defenseBuff);

        StartCoroutine(TextToSpeakControl.main.SpeakText($"You grabbed the {rightItem.itemName}.", 0)); // Speak the item name and description

        hasTakenItem = true; // Set the flag to true when an item is taken
        rightItemPicture.sprite = nullItem.itemPicture;
        rightItemText.text = nullItem.itemName + "\n" + nullItem.itemDescription;
        rightItem = nullItem; // Set the left item to null item

        Debug.Log("Grabbed right item");
    }
}
