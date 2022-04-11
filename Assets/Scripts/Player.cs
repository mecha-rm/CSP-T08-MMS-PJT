using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the class for the player.
public class Player : MonoBehaviour
{
    // the current player.
    private static Player current;

    // the gameplay manager for the game.
    public GameplayManager manager;

    // the player's inventory.
    public List<Item> inventory = new List<Item>();

    // the mouse light for the mouse.
    public MouseLight mouseLight;

    // sets the current player.

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        current = this;

        // finds the gameplay manager if it's not set.
        if(manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // finds the mouse light in the scene.
        if (mouseLight == null)
            mouseLight = FindObjectOfType<MouseLight>(true);
    }

    // grabs the current player.
    public static Player Current
    {
        get
        {
            if (current == null)
                current = FindObjectOfType<Player>(true);

            return current;
        }
    }

    // returns 'true' if the mouse light component is enabled
    public bool IsMouseLightEnabled()
    {
        return mouseLight.IsLightEnabled();
    }

    // sets mouse light enabled (enables component).
    public void SetMouseLightEnabled(bool e)
    {
        mouseLight.SetLightEnabled(e);
    }

    // gets an item in the player's inventory using the item index.
    // this does not remove the item from the inventory.
    public Item GetItemInInventory(int index)
    {
        // checks bounds of index.
        if (index < 0 || index >= inventory.Count)
            return null;
        else
            return inventory[index];
    }

    // gets the first item with the provided ID.
    // this does not remove the item from the inventory.
    public Item GetItemInInventory(string itemId)
    {
        // goes through all inventory items.
        for (int i = 0; i < inventory.Count; i++)
        {
            // item ID matches, so return item.
            if (inventory[i].itemId == itemId)
            {
                return inventory[i];
            }
        }

        // item not found.
        return null;
    }

    // gives the player an item.
    public void GiveItem(Item item)
    {
        inventory.Add(item);
        manager.RefreshInventoryDisplay();
    }

    // takes an item from the player. Returns null if this failed.
    public bool TakeItem(Item item)
    {
        // removes the item. Returns null if the item does not exist.
        bool removed = inventory.Remove(item);
        
        // update the display and return the result.
        manager.RefreshInventoryDisplay();
        return removed;
    }

    // takes an item based on the item ID (1st instance only).
    public Item TakeItem(string itemId)
    {
        // goes through all inventory items.
        for(int i = 0; i < inventory.Count; i++)
        {
            // item ID matches, so return item.
            if(inventory[i].itemId == itemId)
            {
                Item item = inventory[i];
                inventory.Remove(item);
                return item;
            }
        }

        // update display.
        manager.RefreshInventoryDisplay();

        return null;
    }

    // removes all items with a given ID.
    public List<Item> TakeItems(string itemId)
    {
        // the list of the removed items.
        List<Item> removedItems = new List<Item>();

        // removes all items.
        for(int i = inventory.Count - 1; i >= 0; i--)
        {
            // item ID match.
            if(inventory[i].itemId == itemId)
            {
                removedItems.Add(inventory[i]);
                inventory.RemoveAt(i);
            }
        }

        manager.RefreshInventoryDisplay();
        return removedItems;
    }

    // checks if the player has a given item.
    public bool HasItem(string itemId)
    {
        // goes through inventory.
        foreach(Item item in inventory)
        {
            // id match.
            if (item.itemId == itemId)
                return true;
        }

        // no id match.
        return false;
    }

    // returns 'true' if the player has all of the puzzle pieces.
    public bool HasAllPuzzlePieces()
    {
        // the last piece is already in the frame, so the player must collect it before they can fill the frame.
        // TODO: maybe rework this?
        int ownedPieces = 0;

        // the stack id
        string stackId = Item.PUZZLE_PIECE_ID;

        // goes through the inventory.
        foreach(Item item in inventory)
        {
            if (item.itemId == stackId)
                ownedPieces++;
        }

        // all puzzle pieces collected.
        return (ownedPieces == GameplayManager.PuzzlePieceCount);
    }

    // Update is called once per frame
    void Update()
    {
        // if the space bar was pressed.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // iniates whatever highlighted action the player has.
            manager.InitiateMainAction();
        }

        // ITEM ICON DESCRIPTIONS
        // TODO: maybe check if the description shouldn't change in some cases?

        // icon 1
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            manager.ReadItem1Descriptor();
        }
        // icon 2
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            manager.ReadItem2Descriptor();
        }
        // icon 3
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            manager.ReadItem3Descriptor();
        }
        // icon 4
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            manager.ReadItem4Descriptor();
        }
        // icon 5
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            manager.ReadItem5Descriptor();
        }

    }
}
