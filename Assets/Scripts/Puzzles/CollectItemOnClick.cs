using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// collects the item upon it being clicked on.
public class CollectItemOnClick : ToggleActiveOnClick
{
    // the item that will be enabled or disabled upon this click occurring.
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
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
        // calls base function.
        base.OnToggle();

        Debug.Log("Item toggled");

        // TODO: give item to player before toggling it.
        // also, maybe take out the conditional statement here?

        // toggles item.
        if(gameObject != item.gameObject)
            item.gameObject.SetActive(!item.gameObject.activeSelf);
    }
}
