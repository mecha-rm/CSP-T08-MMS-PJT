using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an item in the game world.
// TODO: maybe make an inventory script?
public class Item : MonoBehaviour
{
    // the name for the item.
    public string itemName = "";

    // the description for the item. 
    public string itemDesc = "";

    // the ID for stacking the item for the UI display.
    // TODO: maybe hide this from the inspector?
    [Tooltip("Items with the same ID are stacked together for the inventory display. Blank IDs do not stack with anything.")]
    public string stackId = "";

    // the icon for this item to be used in the user interface.
    // the items CANNOT be destroyed, otherwise the icons are lost.
    // TODO: see if there's a more efficient way to do this.
    public Sprite itemIcon;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // called when the item is put into the player's inventory.
    public virtual void OnItemGet()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
