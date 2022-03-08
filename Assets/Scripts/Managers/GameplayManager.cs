using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used for saving user-interface components.

// manages gameplay operations.
public class GameplayManager : Manager
{
    // the player
    public Player player;

    // the mouse operations for the game.
    public Mouse mouse;

    // post processing object.
    // this is used to simulate the flashlight.
    public GameObject postProcessing;

    // the current screen. It also saves the next screens.
    [Tooltip("The current screen. This this to the starting screen when you start running the game.")]
    public RoomScreen currentScreen;

    // getting rid of this feature until we can make it work in a more streamlined way.
    // if 'true', inactive rooms are disabled.
    // [Tooltip("Disables all but the current room when the game starts.")]
    // public bool disableRoomsOnStart = true;

    // the last object that has been clicked.
    private GameObject lastClicked = null;

    // inventory UI
    [Header("UI/Inventory")]
    public ItemIcon item1;
    public ItemIcon item2;
    public ItemIcon item3;
    public ItemIcon item4;
    public ItemIcon item5;

    // the user interface buttons.
    [Header("UI/Buttons")]
    public Button leftScreenButton;
    public Button rightScreenButton;
    public Button forwardScreenButton;
    public Button backScreenButton;

    // Start is called before the first frame update
    new void Start()
    {
        // sets the frame rate.
        // This happens in the title scene, so this function call is only needed when testing.
        if(Application.isEditor)
            base.Start();

        // finds the player in the scene.
        if (player == null)
            player = FindObjectOfType<Player>();

        // if no mouse is set, find it.
        if (mouse == null)
            mouse = FindObjectOfType<Mouse>();

        // if the post processing object has not been set.
        if(postProcessing == null)
        {
            // looks for the post processing volume n the scene.
            UnityEngine.Rendering.PostProcessing.PostProcessVolume temp =
                FindObjectOfType<UnityEngine.Rendering.PostProcessing.PostProcessVolume>(true);
            
            // saves the game object.
            if (temp != null)
                postProcessing = temp.gameObject;

        }

        // // if no current screen is set, just set a random room.
        // if (currentScreen == null)
        //     currentScreen = FindObjectOfType<RoomScreen>();

        // enables this screen.
        if (currentScreen != null)
        {
            currentScreen.manager = this;
            currentScreen.EnableScreen();
        }

        // // if any of the item components are empty.
        // if(item1 == null || item2 == null || item3 == null || item4 == null || item5 == null)
        // {
        //     // grabs all the components (will likely be out of order)
        //     ItemIcon[] icons = FindObjectsOfType<ItemIcon>();
        // 
        //     // puts in components.
        //     for(int i = 0; i < icons.Length; i++)
        //     {
        //         // finds empty object.
        //         switch(i)
        //         {
        //             case 0:
        //                 if (item1 == null)
        //                     item1 = icons[i];
        //                 break;
        //             case 1:
        //                 if (item2 == null)
        //                     item2 = icons[i];
        //                 break;
        //             case 2:
        //                 if (item3 == null)
        //                     item3 = icons[i];
        //                 break;
        //             case 3:
        //                 if (item4 == null)
        //                     item4 = icons[i];
        //                 break;
        //             case 4:
        //                 if (item5 == null)
        //                     item5 = icons[i];
        //                 break;
        //         }
        //     }
        // }
            
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
        // gets the list of the player's items.
        List<Item> itemList = new List<Item>(player.inventory);

        // queue of item icons.
        Queue<ItemIcon> itemIcons = new Queue<ItemIcon>();

        // adds all item icons.
        itemIcons.Enqueue(item1);
        itemIcons.Enqueue(item2);
        itemIcons.Enqueue(item3);
        itemIcons.Enqueue(item4);
        itemIcons.Enqueue(item5);

        // gets all the items.
        while (itemList.Count > 0 && itemIcons.Count > 0)
        {
            // index stack
            Stack<int> indexes = new Stack<int>();

            // the amount in this stack.
            int amount = 0;

            // grabs first item, and puts it in the list.
            Item item = itemList[0];
            indexes.Push(0);
        
            // blank IDs do not stack.
            if(item.stackId != "")
            {
                // checks for stackable items.
                // skips the first index since it has already been put into the list.
                for (int i = 1; i < itemList.Count; i++)
                {
                    // items should stack.
                    if (itemList[i].stackId == item.stackId)
                        indexes.Push(i); // save index.

                }
            }

            // saves the amount of items in the stack.
            amount = indexes.Count;

            // while there are remaining indexes.
            while(indexes.Count > 0)
            {
                itemList.RemoveAt(indexes.Pop()); // removes at index
            }

            // grabs the first item icon, and removes it from the queue.
            ItemIcon itemIcon = itemIcons.Dequeue();

            // updates the icon.
            // if there is only one item in the stack the number is not listed.
            itemIcon.UpdateIcon(item.itemIcon, amount, (amount > 1));
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
    
    // TODO: turn off local emissive lights in the rooms.

    // if the room's lighting is enabled then turn off the post processing layer.
    public bool IsRoomLightingEnabled()
    {
        // if(postProcessing != null)
        //     return !postProcessing.activeSelf;
        // 
        // return false;

        // does the current room have its lighting enabled?
        return currentScreen.room.IsLightingEnabled();
    }

    // sets if the room lights should be enabled.
    public void SetRoomLightingEnabled(bool e)
    {
        // turn on post processing to simulate flashlight if lights are off.
        // postProcessing.SetActive(!e);

        // call this function to change the settings.
        // currentScreen.room.SetLightingEnabled(e);
        
        // enable the mouse light (post-processing effect)
        player.SetMouseLightEnabled(!e);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: take this feature out until it can be implemented in a more streamlined way.
        // // if the current room has been set.
        // // this needs to happen after all the start() functions finish.
        // if (currentScreen.room != null && disableRoomsOnStart)
        // {
        //     // finds all rooms
        //     Room[] rooms = FindObjectsOfType<Room>();
        // 
        //     // disables all other rooms.
        //     foreach (Room room in rooms)
        //     {
        //         // disables all but the current room.
        //         if (room != currentScreen.room)
        //             room.gameObject.SetActive(false);
        //     }
        // 
        //     // rooms were disabled, so don't do it again.
        //     disableRoomsOnStart = false;
        // }

        // checks if the last clicked object is another screen.
        if (mouse.lastClickedObject != lastClicked)
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
