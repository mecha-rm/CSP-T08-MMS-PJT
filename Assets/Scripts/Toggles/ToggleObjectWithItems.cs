using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toggles something on or off if the correct item(s) are held by the player.
public class ToggleObjectWithItems : ToggleObjectOnClick
{
    [Header("ToggleObjectWithItem")]

    // the gameplay manager.
    public GameplayManager gameManager;
    
    // the audio manager.
    public AudioManager audioManager;


    // the item (stack ids) for the used items.
    [Tooltip("The items being checked for. If no IDs are provided, the object is toggled without needing anything.")]
    public List<string> itemIds;

    // NOTE: this doesn't allow for multiple instances of a given item being needed.
    // this isn't needed for the project, so it will not be programmed.

    // if 'true', all items must be in the player's possession for this to work.
    [Tooltip("If 'true', all items in the list are needed for the toggle to happen." +
        "This only requires one of each item to trigger the toggle if set to true.")]
    public bool needAll = true;

    // take the items once they are used.
    [Tooltip("Takes the items used to toggle the object.")]
    public bool takeItems = true;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // saves the gameplay manager.
        if (gameManager == null)
            gameManager = GameplayManager.Current;

        // saves the audio manager.
        if (audioManager == null)
            audioManager = gameManager.audioManager;

    }

    // called when the user attempts to toggle something.
    protected override void OnToggle()
    {
        // no items, so no requirements to toggle the object.
        if (itemIds.Count == 0)
        {
            base.OnToggle();
        }
        // checks object toggle.
        else if (itemIds.Count != 0 && gameManager.player != null)
        {
            // requirements met.
            bool toggle = true;

            // saves the player.
            Player player = gameManager.player;

            // the list of audio clips. Only the first one is played for now.
            List<AudioClip> clips = new List<AudioClip>();

            // goes through each item.
            foreach (string itemId in itemIds)
            {
                // checks if the player has the item.
                bool result = player.HasItem(itemId);

                // player has the item.
                if (result)
                {
                    // the item.
                    Item item = player.GetItemInInventory(itemId);

                    // adds the audio clip if it's available.
                    if (item.playAudio && item.audioClip != null)
                        clips.Add(item.audioClip);


                    // item should be toggled.
                    toggle = true;

                    // plays the audio

                    audioManager.PlayAudio(player.GetItemInInventory(itemId).audioClip);


                    // takes the item if this is true.
                    if (takeItems)
                        player.TakeItem(itemId);

                    // if only one item is needed, leave the loop.
                    // the requirement has been met.
                    if (!needAll)
                        break;
                }
                else
                {
                    // shouldn't toggle.
                    toggle = false;

                    // all of them are needed.
                    // one was not found, so this can't be toggled.
                    if (needAll)
                        break;
                }
            }

            // this should be toggled.
            if (toggle)
            {
                // plays the audio.
                if (clips.Count != 0 && audioManager != null)
                {
                    // plays sound 0.
                    // TODO: maybe play multiple sounds at once?
                    audioManager.PlayAudio(clips[0]);
                }

                // TODO: move audio play here.

                base.OnToggle();
            }
                
        }

        // if items were taken, refresh the inventory display.
        if (takeItems)
            gameManager.RefreshInventoryDisplay();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
