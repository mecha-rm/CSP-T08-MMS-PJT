using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: implement light box.
// toggles something on or off if the correct item is had.
public class ToggleObjectWithItem : ToggleObjectOnClick
{
    [Header("ToggleObjectWithItem")]

    // the gameplay manager.
    public GameplayManager manager;

    // the item (stack ids) for the used items.
    [Tooltip("The items being checked for. If no IDs are provided, the object is toggled without needing anything.")]
    public List<string> itemIds;

    // NOTE: this doesn't allow for multiple instances of a given item being needed.
    // this isn't needed for the project, so it will not be programmed.

    // if 'true', all items must be in the player's possession for this to work.
    [Tooltip("If 'true', all items in the list are needed for the toggle to happen." +
        "This only requires one of each item to trigger the toggle if set to true.")]
    public bool needAll = true;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // finds the manager.
        if(manager == null)
            manager = FindObjectOfType<GameplayManager>();

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
        else if(itemIds.Count != 0 && manager.player != null)
        {
            // requirements met.
            bool toggle = true;

            // goes through each item.
            foreach (string itemId in itemIds)
            {
                // checks if the player has the item.
                bool result = manager.player.HasItem(itemId);

                // player has the item.
                if(result)
                {
                    // item should be toggled.
                    toggle = true;

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
                    if(needAll)
                        break;
                }
            }

            // this should be toggled.
            if (toggle)
                base.OnToggle();
        }
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
