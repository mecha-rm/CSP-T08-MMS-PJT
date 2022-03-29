using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// collects the item upon it being clicked on.
public class CollectItemOnClick : ToggleObjectOnClick
{
    [Header("CollectItemOnClick")]

    // grabs the player from the manager.
    public GameplayManager manager;

    // the item that will be enabled or disabled upon this click occurring.
    [Tooltip("The item to be given to the player. This overrides pairedObject.")]
    public Item item;

    // Start is called before the first frame update
    protected new void Start()
    {
        // calls the base function.
        base.Start();

        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // if null, try to grab own component.
        if (item == null)
            item = GetComponent<Item>();

        // the item is not set to null.
        if (item != null && pairedObject == null)
            pairedObject = item.gameObject;
    }

    // overrides the deactivation function.
    protected override void OnToggle()
    {
        // if the manager has not been set.
        if (manager == null)
        {
            Debug.LogError("The gameplay manager is not set.");
            return;
        }

        // if the player has not been set.
        if(manager.player == null)
        {
            Debug.LogError("Could not find player in manager.");
            return;
        }

        // gives the player the item.
        manager.player.GiveItem(item);

        // calls base function.
        // this disables the item object if that's the paired object.
        base.OnToggle();

        // TODO: give item to player before toggling it.
        // also, maybe take out the conditional statement here?

        // // toggles item.
        // if(gameObject != item.gameObject)
        //     item.gameObject.SetActive(!item.gameObject.activeSelf);
    }
}
