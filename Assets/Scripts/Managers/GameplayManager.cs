using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used for saving user-interface components.
using UnityEngine.Rendering.PostProcessing; // post processing volume.

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

    // public Room room;

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

    // the name of the object clicked on.
    public string inspectName = "...";

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

        // grabs the timer component.
        if (timer == null)
            timer = GetComponent<Timer>();
            
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
            lastClicked = null;

        // TODO: sometimes the forward screen seems to be set automatically when it shouldn't. Try to fix that.

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
        player.SetMouseLightEnabled(!e);
    }

    // refreshes the game manager with its descriptor.
    public void RefreshDescriptor()
    {
        // no descriptor.
        if (descriptor == null)
            return;

        inspectName = descriptor.secondName;
        inspectDesc = descriptor.description;
    }

    // sets the descriptor.
    public void SetDescriptor(Descriptor newDesc)
    {
        descriptor = newDesc;
        RefreshDescriptor();
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
        ResultsData resultsData = FindObjectOfType<ResultsData>();

        // this object doe not exist, so make a new object.
        if (resultsData == null)
        {
            GameObject temp = new GameObject("Game Results");
            resultsData = temp.AddComponent<ResultsData>();
        }

        // saves the current time.
        if(timer != null)
            resultsData.completionTime = timer.currentTime;

        // saves this value.
        resultsData.gotCertificate = hasCertificate;

        // goes to the end screen.
        SceneHelper.LoadScene("EndScene");
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

                // tries to grab a descriptor.
                Descriptor tempDesc;

                // tries to find a descriptor object.
                if(lastClicked.gameObject.TryGetComponent<Descriptor>(out tempDesc))
                {
                    // replaces the descriptor.
                    SetDescriptor(tempDesc);
                }
                else
                {
                    // clear out the name and description.
                    inspectName = inspectDefault;
                    inspectDesc = inspectDefault;
                }
            }
        }

        // updates the text element.
        if (inspectText != null)
        {
            // if the overhead text has not been updated.
            if(inspectText.text != inspectDesc)
                inspectText.text = inspectDesc;
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
