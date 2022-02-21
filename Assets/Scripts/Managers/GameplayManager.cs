using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used for saving user-interface components.

// manages gameplay operations.
public class GameplayManager : MonoBehaviour
{
    // the player
    public Player player;

    // the mouse operations for the game.
    public Mouse mouse;

    // the current screen. It also saves the next screens.
    public RoomScreen currentScreen;

    // the last object that has been clicked.
    private GameObject lastClicked = null;

    // inventory UI
    [Header("UI/Inventory")]
    public Image item1;
    public Image item2;
    public Image item3;
    public Image item4;
    public Image item5;

    // the user interface buttons.
    [Header("UI/Buttons")]
    public Button leftScreenButton;
    public Button rightScreenButton;
    public Button forwardScreenButton;
    public Button backScreenButton;

    // Start is called before the first frame update
    void Start()
    {
        // finds the player in the scene.
        if (player == null)
            player = FindObjectOfType<Player>();

        // if no mouse is set, find it.
        if (mouse == null)
            mouse = FindObjectOfType<Mouse>();

        // // if no current screen is set, just set a random room.
        // if (currentScreen == null)
        //     currentScreen = FindObjectOfType<RoomScreen>();

        // enables this screen.
        if (currentScreen != null)
        {
            currentScreen.manager = this;
            currentScreen.EnableScreen();
        }
            
    }

    // switches to the left screen.
    public void SwitchToLeftScreen()
    {
        SwitchScreen(0);
    }

    // switches to the right screen.
    public void SwitchToRightScreen()
    {
        SwitchScreen(1);
    }

    // switches to the forward screen.
    public void SwitchToForwardScreen()
    {
        SwitchScreen(2);
    }

    // switches to the back screen.
    public void SwitchToBackScreen()
    {
        SwitchScreen(3);
    }

    // switches the screen.
    public void SwitchScreen(int screen)
    {
        // if the current screen is not set.
        if (currentScreen == null)
        {
            Debug.LogError("No current screen set. Cannot move.");
            return;
        }

        // sets the screen.
        switch (screen)
        {
            case 0: // left
                currentScreen.SwitchToLeftScreen();
                break;

            case 1: // right
                currentScreen.SwitchToRightScreen();
                break;

            case 2: // forward
                currentScreen.SwitchToForwardScreen();
                break;

            case 3: // back
                currentScreen.SwitchToBackScreen();
                break;
        }
    }

    // refreshes the inventory display for the game.
    public void RefreshInventoryDisplay()
    {
        // queues up all of the item icons.
        Queue<Image> itemIcons = new Queue<Image>();
        itemIcons.Enqueue(item1);
        itemIcons.Enqueue(item2);
        itemIcons.Enqueue(item3);
        itemIcons.Enqueue(item4);
        itemIcons.Enqueue(item5);

        // goes through all items.
        for(int i = 0; i < player.inventory.Count && itemIcons.Count != 0; i++)
        {
            Image itemIcon = itemIcons.Dequeue(); // grabs the first one.
            itemIcon.sprite = player.inventory[0].itemIcon;
        }

        // removes all other icons.
        while(itemIcons.Count != 0)
        {
            itemIcons.Peek().sprite = null;
            itemIcons.Dequeue();
        }
    }

    // tries to get the room screen from the clicked object.
    private bool GetRoomScreenFromClicked()
    {
        // SCREEN TRIGGER CHECK
        // checks for a screen trigger.
        ScreenTrigger st = null;

        // checks the room screen.
        RoomScreen rs = null;

        // tries to grab the screen trigger first.
        if (mouse.lastClickedObject.TryGetComponent<ScreenTrigger>(out st))
        {
            // sets the room screen to the screen trigger's screen.
            rs = st.screen;
        }

        // the room screen is still set to null, so check for a room screen component.
        if (rs == null)
        {
            // tries to grab the room screen component.
            rs = mouse.lastClickedObject.GetComponent<RoomScreen>();
        }

        // screen found, and it's not the screen you're currently in.
        if (rs != null && rs != currentScreen)
        {
            // sets the forward screen of the last clicked object.
            currentScreen.forwardScreen = rs;

            // saves the back screen for the forward screen.
            rs.backScreen = currentScreen;

            // screen switched.
            return true;
        }

        // screen did not switch.
        return false;
    }

    // tries to get the item from the clicked object.
    private bool GetItemFromClicked()
    {
        Item item;

        // tries to grab the item component.
        if (mouse.lastClickedObject.TryGetComponent<Item>(out item))
        {
            // TODO: maybe make this a function in the Item class?
            player.inventory.Add(item);
            item.OnItemGet();
            RefreshInventoryDisplay();
            item.gameObject.SetActive(false);
        }

        return false;

    }

    // Update is called once per frame
    void Update()
    {
        // checks if the last clicked object is another screen.
        if(mouse.lastClickedObject != lastClicked)
        {
            // saves the clicked object.
            lastClicked = mouse.lastClickedObject;

            // something has been clicked.
            if(lastClicked != null)
            {
                bool success;

                // tries to grab the room screen from the clicked object.
                success = GetRoomScreenFromClicked();

                // TODO: this may not be needed, but an item shouldn't be set to a room screen anyway.
                if (!success)
                    GetItemFromClicked();
            }
        }

        // no last clicked object. This is kind of a quick fix.
        // if (lastClicked == null && currentScreen.forwardScreen != null)
        //     currentScreen.forwardScreen = null;

        // enables/disables buttons as needed.
        // TODO: these probably don't have to happen every frame. Could be optimized.
        {
            leftScreenButton.interactable = (currentScreen.leftScreen != null);
            rightScreenButton.interactable = (currentScreen.rightScreen != null);
            forwardScreenButton.interactable = (currentScreen.forwardScreen != null);
            backScreenButton.interactable = (currentScreen.backScreen != null);
        }

    }
}
