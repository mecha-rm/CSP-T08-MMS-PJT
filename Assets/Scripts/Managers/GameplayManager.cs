using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used for saving user-interface components.
using UnityEngine.Rendering.PostProcessing; // post processing volume.

// manages gameplay operations.
public class GameplayManager : Manager
{
    // the current gameplay manager. Since there's only 1, this will be set to said variable.
    // set in awake.
    // NOTE: this was a late addition, so we don't use this variable.
    // But for future work, maybe use this instead of saving a manager object.
    private static GameplayManager current;

    // the accessibility manager.
    [Tooltip("The accessibility manager.")]
    public UAP_AccessibilityManager accessibility;

    // becomes 'true', when the first update has been finished.
    private bool passedFirstUpdate = false;

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

    // the list of rooms in the scene.
    [Tooltip("The list of rooms.")]
    public List<Room> rooms = new List<Room>();

    // if 'true', other rooms are disabled when not being used.
    [Tooltip("if 'true', other rooms are disabled when not being used.")]
    public bool disableOtherRooms = true;

    // getting rid of this feature until we can make it work in a more streamlined way.
    // if 'true', inactive rooms are disabled.
    // [Tooltip("Disables all but the current room when the game starts.")]
    // public bool disableRoomsOnStart = true;

    // the last object that has been clicked.
    private GameObject lastClicked = null;

    // if 'true', the player got the bonus easter egg.
    public bool hasCertificate;

    // the timer for the game. There should only be one.
    public Timer timer;

    // descriptor functions.
    [Header("Descriptor")]

    // the descriptor of the object that was clicked on.
    public Descriptor descriptor = null;

    // the inspector default
    private string inspectDefault = "...";

    // contains the old inspector name.
    private string currInspectName;

    // the name of the object clicked on.
    public string inspectName = "";

    // contains the old inspector description.
    private string currInspectDesc;

    // the description of the object clicked on.
    public string inspectDesc = "...";

    // display text for the description information.
    public Text inspectText;

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

    // saves this as the current gameplay manager.
    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        // sets the frame rate.
        // This happens in the title scene, so this function call is only needed when testing.
        if(Application.isEditor)
            base.Start();

        // finds the accessibility manager
        if (accessibility == null)
            accessibility = FindObjectOfType<UAP_AccessibilityManager>(true);

        // makes sure the active gameplay manager is saved.
        current = this;

        // finds the player in the scene.
        if (player == null)
            player = FindObjectOfType<Player>();

        // if no mouse is set, find it.
        if (mouse == null)
            mouse = FindObjectOfType<Mouse>();

        // if the post processing object has not been set.
        if(postProcessing == null)
        {
            // looks to find the post-process volume.
            PostProcessVolume volume = FindObjectOfType<PostProcessVolume>(true);

            // object found.
            if (volume != null)
                postProcessing = volume.gameObject;

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

        // finds the existing rooms.
        if(rooms.Count == 0)
        {
            // finds all rooms.
            Room[] roomArr = FindObjectsOfType<Room>(true);

            // adds the array of rooms.
            rooms.AddRange(roomArr);
        }    

        // grabs the timer component.
        if (timer == null)
            timer = GetComponent<Timer>();

        // inspect text not set.
        if(inspectText == null)
        {
            // looks for the object.
            GameObject temp = GameObject.Find("Inspect Text");

            // trys to grab the text component.
            if (temp != null)
                temp.TryGetComponent<Text>(out inspectText);
        }

        // saves the existing values of these variables.
        currInspectName = inspectName;
        currInspectDesc = inspectDesc;

        // sets starting description.
        if (currentScreen != null)
        {
            SetDescriptor(currentScreen.descriptor);
        }

        // finds the starting information.
        GameStartInfo gsi = FindObjectOfType<GameStartInfo>(true);

        // enables the accessibility
        // TODO: seperate this for screen reader and high contrast
        if (gsi != null)
        {
            // sets the accessibility.
            if(accessibility != null)
                accessibility.gameObject.SetActive(gsi.useScreenReader);

            // destroys object once it's done being used.
            Destroy(gsi.gameObject);
        }
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

        // saves the current secreen.
        RoomScreen cs = currentScreen;


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

        // screens switched, so change this variable.
        if (cs != currentScreen)
        {
            // this prevents the objects from being read twice.
            // it also prevents the user from keeping their click from last screen.
            lastClicked = null;
            mouse.lastClickedObject = null;
        }
            

        // disables all other rooms.
        if (disableOtherRooms)
            DisableAllOtherRooms();

        // TODO: sometimes the forward screen seems to be set automatically when it shouldn't. Try to fix that.
        // maybe it's just an objects being overlayed issue? If so, just move things around.

    }

    // gets the current manager.
    public static GameplayManager Current
    {
        get
        {
            // saves the gameplay manager if not set.
            if (current == null)
                current = FindObjectOfType<GameplayManager>(true);

            return current;
        }
    }

    // returns the player.
    public static Player Player
    {
        get
        {
            // if player not set...
            if (Current.player == null)
                Current.player = FindObjectOfType<Player>();

            // return player.
            return Current.player;
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

    // disables all rooms that are not being used.
    public bool DisableAllOtherRooms()
    {
        // current screen not set.
        if (currentScreen == null)
            return false;

        // current room not set.
        if (currentScreen.room == null)
            return false;

        // no saved rooms.
        if (rooms.Count == 0)
            return false;

        // saves the current room.
        Room currRoom = currentScreen.room;

        // room not in list, so add it.
        if (!rooms.Contains(currRoom))
            rooms.Add(currRoom);

        // goes through every room.
        foreach(Room r in rooms)
        {
            // change active.
            r.gameObject.SetActive(r == currRoom ? true : false);
        }

        // successful.
        return true;
    }

    // refreshes the inventory display for the game.
    public void RefreshInventoryDisplay()
    {
        // gets the list of the player's items.
        List<Item> itemList = new List<Item>(player.inventory);

        // queue of item icons.
        Queue<ItemIcon> itemIcons = new Queue<ItemIcon>();

        // hides and adds all item icons.
        // item1.iconImage.enabled = false;
        itemIcons.Enqueue(item1);

        // item2.iconImage.enabled = false;
        itemIcons.Enqueue(item2);

        // item3.iconImage.enabled = false;
        itemIcons.Enqueue(item3);

        // item4.iconImage.enabled = false;
        itemIcons.Enqueue(item4);

        // item5.iconImage.enabled = false;
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
            if(item.itemId != "")
            {
                // checks for stackable items.
                // skips the first index since it has already been put into the list.
                for (int i = 1; i < itemList.Count; i++)
                {
                    // items should stack.
                    if (itemList[i].itemId == item.itemId)
                        indexes.Push(i); // save index.

                }
            }

            // saves the amount of items in the stack.
            amount = indexes.Count;

            // removes stacked items.
            // while there are remaining indexes.
            while(indexes.Count > 0)
            {
                itemList.RemoveAt(indexes.Pop()); // removes at index
            }

            // grabs the first item icon, and removes it from the queue.
            ItemIcon itemIcon = itemIcons.Dequeue();

            // updates the icon.
            // if there is only one item in the stack the number is not listed.
            itemIcon.UpdateIcon(item.itemIcon, amount, (amount > 1), item.descriptor);
        }

        // resets the rest of the icons.
        while(itemIcons.Count > 0)
        {
            ItemIcon itemIcon = itemIcons.Dequeue();
            itemIcon.ResetIcon();
        }
    }

    // returns the amount of puzzle pieces in the game.
    public static int PuzzlePieceCount
    {
        get
        {
            return 9;
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
            // sets the descriptor.
            SetDescriptor(rs.descriptor);

            /*
             * NOTE: there is an exploit here.
             * If you click on a locked screen the screen will be inacessible, but the object will still be saved.
             * This is because it was still the lastClicked object.
             * If you then unlock the screen and shift screens (but not to the newly unlocked screen)...
             *  it will still be available as the forward screen.
             * This is because it is still the last clicked object, and only goes away once the user clicks on something else.
             * This exploit likely won't come up in practice, but it's still something that should be fixed.
             */

            // change the descriptor, regardless of if the screen is locked or not.
            // message should show if locked or not.
            SetDescriptor(rs.descriptor);

            // can't set if the screen is locked.
            if (rs.locked) // locked
            {
                Debug.Log("Screen locked, so you can't switch to it. If you unlock it, you need to click off and back on it.");

                // NOTE: if you unlock the screen you need to click off and click on it again.

                // the forward screen should not remain set.
                currentScreen.forwardScreen = null;

                return false;
            }
            else // unlocked
            {
                // sets the forward screen of the last clicked object.
                currentScreen.forwardScreen = rs;

                // saves the back screen for the forward screen.
                rs.backScreen = currentScreen;

                // screen switched.
                return true;
            }  
        }

        // screen did not switch.
        return false;
    }

    // tries to get the item from the clicked object.
    private bool GetItemFromClicked()
    {
        // item object.
        Item item;

        // tries to grab the item component.
        if (mouse.lastClickedObject.TryGetComponent<Item>(out item))
        {
            // if the item is active and enabled.
            // this stops the same item from being added twice from checking lastClicked multiple times.
            // this is because the item object is deactivated once the item is gotten.
            if(item.isActiveAndEnabled)
            {
                // TODO: maybe make this a function in the Item class?
                player.inventory.Add(item);
                item.OnItemGet();
                RefreshInventoryDisplay();
                item.gameObject.SetActive(false);
            }
            
        }

        return false;

    }

    // checks if the room's lighting is enabled.
    // this works by altering the post-processing layer.
    public bool IsRoomLightingEnabled()
    {
        // does the current room have its lighting enabled?
        // return currentScreen.room.IsLightingEnabled();


        // this simulates a flashlight, so if it's on, the room lighting is considered off.
        if (postProcessing != null)
            return !postProcessing.activeSelf;
        
        // on by default.
        return true;
    }

    // sets whether the room lighting is on.
    // if it's off, the post processing 
    public void SetRoomLightingEnabled(bool e)
    {
        // variable not set.
        if (postProcessing == null)
            return;

        // turn on post processing to simulate flashlight if lights are off.
        // postProcessing.SetActive(!e);

        // call this function to change the settings.
        // currentScreen.room.SetLightingEnabled(e);


        // if the post-processing effect is on, the lighting is considered "off".
        // Debug.Log("Room Lighting: " + e.ToString());

        // enable the post-processing effect.
        postProcessing.SetActive(!e);

        // sets the mouse light so that it can control the post-processed vingette effect.
        
        // for some reason this wasn't set at the start anymore, so this just makes sure to find it.
        // player.SetMouseLightEnabled(!e);

        Player.SetMouseLightEnabled(!e);
    }

    // DESCRIPTOR //

    // refreshes the game manager with its descriptor.
    public void RefreshDescriptor()
    {
        // no descriptor.
        if (descriptor == null)
            return;

        // set name text.
        inspectName = descriptor.showLabel ? descriptor.label : "";

        // set description text.
        inspectDesc = descriptor.description;
    }

    // sets the descriptor.
    public void SetDescriptor(Descriptor newDesc)
    {
        descriptor = newDesc;
        RefreshDescriptor();
    }

    // reads an item icon's descriptor.
    public void ReadItemDescriptor(int itemNum)
    {
        // reads an item icon.
        switch(itemNum)
        {
            case 1: // 1
                SetDescriptor(item1.descriptor);
                break;
            case 2: // 2
                SetDescriptor(item2.descriptor);
                break;
            case 3: // 3
                SetDescriptor(item3.descriptor);
                break;
            case 4: // 4
                SetDescriptor(item4.descriptor);
                break;
            case 5: // 5
                SetDescriptor(item5.descriptor);
                break;
        }
    }

    // reads item 1's descriptor.
    public void ReadItem1Descriptor()
    {
        ReadItemDescriptor(1);
    }

    // reads item 2's descriptor.
    public void ReadItem2Descriptor()
    {
        ReadItemDescriptor(2);
    }

    // reads item 3's descriptor.
    public void ReadItem3Descriptor()
    {
        ReadItemDescriptor(3);
    }

    // reads item 4's descriptor.
    public void ReadItem4Descriptor()
    {
        ReadItemDescriptor(4);
    }

    // reads item 5's descriptor.
    public void ReadItem5Descriptor()
    {
        ReadItemDescriptor(5);
    }


    // initiates the main action in the game world.
    public void InitiateMainAction()
    {
        // if there is an object available.
        if(lastClicked != null)
        {
            // puzzle mechanic to trigger.
            PuzzleMechanic pm;

            // triggers the puzzle mechanic.
            if (lastClicked.TryGetComponent<PuzzleMechanic>(out pm))
            {
                // triggers the action.
                pm.InitiateMainAction();
                return;
            }
        }

        // no puzzle, so try to go forward a screen instead.
        SwitchToForwardScreen();
    }


    // called when the game ends.
    // this is called by the exit screen.
    public void OnGameEnd()
    {
        // tries to find the results data object if it already exists.
        GameEndInfo resultsData = FindObjectOfType<GameEndInfo>(true);

        // this object doe not exist, so make a new object.
        if (resultsData == null)
        {
            GameObject temp = new GameObject("Game End Info");
            resultsData = temp.AddComponent<GameEndInfo>();
        }

        // saves the current time.
        if(timer != null)
            resultsData.completionTime = timer.currentTime;

        // saves this value.
        resultsData.gotCertificate = hasCertificate;

        // goes to the end screen.
        SceneHelper.LoadScene("EndScene");
    }

    // called on the first update.
    private void OnFirstUpdate()
    {
        // can only be called once.
        if (passedFirstUpdate)
            return;

        // if the other rooms should be disabled.
        if(disableOtherRooms)
        {
            DisableAllOtherRooms();
        }

        // now past the first update.
        passedFirstUpdate = true;
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

        // calls code for the first update.
        // TODO: this isn't efficient, so try to optimize this if possible.
        if (!passedFirstUpdate)
            OnFirstUpdate();

        // checks if the last clicked object is another screen.
        if (mouse.lastClickedObject != lastClicked)
        {
            // saves the clicked object.
            lastClicked = mouse.lastClickedObject;

            Debug.Log("Last Clicked: " + lastClicked.name);

            // something has been clicked.
            if (lastClicked != null)
            {
                // // checks if a pull was successful.
                // bool success;
                
                // saves these old values.
                // if the inspector was not changed, then set the descriptor to the blank message.
                // if the inspector was changed, don't overwrite siad changes.
                string oldInspectName = inspectName;
                string oldInspectDesc = inspectDesc;

                // // tries to grab the room screen from the clicked object.
                // success = GetRoomScreenFromClicked();
                // 
                // // TODO: this may not be needed, but an item shouldn't be set to a room screen anyway.
                // if (!success)
                //     GetItemFromClicked();

                // checks for a room screen.
                GetRoomScreenFromClicked();

                // checks for an item.
                GetItemFromClicked();

                // tries functions on the door.

                // description should be updated if nothing else changed the existing descriptions.
                if (oldInspectName == inspectName && oldInspectDesc == inspectDesc)
                {
                    Descriptor tempDesc;

                    // tries to find a descriptor object.
                    if (lastClicked.gameObject.TryGetComponent<Descriptor>(out tempDesc))
                    {
                        // replaces the descriptor.
                        SetDescriptor(tempDesc);
                    }
                    else
                    {
                        // clear out the name and description.
                        inspectName = ""; // name is blank.
                        inspectDesc = inspectDefault;
                    }
                }
                
            }
        }

        // updates the text element if the inspector values have changed.
        if (inspectText != null && (inspectName != currInspectName || inspectDesc != currInspectDesc))
        {
            // TODO: make this more efficient.

            // // if the overhead text does not contain the inspector description.
            // if(!inspectText.text.Contains(inspectDesc))
            // {
            //     // checks if there's a name.
            //     if(inspectName != "") // there is a name, so include it.
            //     {
            //         inspectText.text = inspectName + ": " + inspectDesc;
            //     }
            //     else // there is no name, so don't include it.
            //     {
            //         inspectText.text = inspectDesc;
            //     }
            // }

            // checks if there's a name.
            if (inspectName != "") // there is a name, so include it.
            {
                // if the description is set, include it.
                // if it isn't, just include the name.
                if(inspectDesc != "")
                    inspectText.text = inspectName + ": " + inspectDesc;
                else
                    inspectText.text = inspectName;
            }
            else // there is no name, so don't include it.
            {
                // description only, even if it's blank.
                inspectText.text = inspectDesc;
            }

            // saves the values.
            currInspectName = inspectName;
            currInspectDesc = inspectDesc;
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
