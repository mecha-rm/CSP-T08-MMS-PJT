using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script for the inventory icon.
public class ItemIcon : MonoBehaviour
{
    // the image icon for the inventory item.
    public Image iconImage;

    // if 'true', the image is hidden on start.
    public bool showImageOnStart = false;

    // the text for the amount of this item in the player's inventory.
    public Text amountText;

    // if 'true', the text is hidden on start.
    public bool showTextOnStart = false;

    // the descriptor for the item icon.
    public Descriptor descriptor;

    // the default descriptor name.
    private string defaultDescLabel = "Item";

    // the default descriptor description.
    private string defaultDescDesc = "An icon for an item in the player's inventory.";

    // Start is called before the first frame update
    void Start()
    {
        // if the icon image is not set.
        if(iconImage == null)
        {
            // tries to grab the image component.
            iconImage = GetComponent<Image>();

            // tries to find an image component in the list.
            if (iconImage == null)
                iconImage = GetComponentInChildren<Image>();
        }

        // shows/hides the image.
        iconImage.enabled = showImageOnStart;

        // if the amount text is not set.
        if (amountText == null)
        {
            // tries to find an text component in the list.
            if (amountText == null)
                amountText = GetComponentInChildren<Text>();
        }

        // if not set, try to find it.
        if (descriptor == null)
        {
            // if the component isn't found, add it.
            if(!TryGetComponent<Descriptor>(out descriptor))
            {
                // adds a descriptor.
                descriptor = gameObject.AddComponent<Descriptor>();
            }
        }

        // descriptor is set.
        if(descriptor != null)
        {
            // change name.
            if (descriptor.label == "")
                descriptor.label = defaultDescLabel;

            // change description.
            if (descriptor.description == "")
                descriptor.description = defaultDescDesc;
        }
            

        // shows/hides the text.
        amountText.enabled = showTextOnStart;

    }

    // updates the icon image.
    public void UpdateIcon(Sprite newSprite, int amount, bool showAmount, Descriptor newDesc = null)
    {
        // updates the sprite.
        iconImage.sprite = newSprite;
        iconImage.enabled = (newSprite != null);

        // updates the text.
        amountText.text = "x" + amount.ToString();
        amountText.enabled = showAmount;

        // copies the values of the descriptor.
        if(newDesc != null)
        {
            // copies the content from the provided descriptor.
            descriptor.CopyContent(newDesc);
        }

    }

    // resets the icon to its default values.
    public void ResetIcon()
    {
        // shows/hides the image.
        iconImage.sprite = null; // no image
        iconImage.enabled = false; // hide since there's no image.

        // number text is 0, and hide amount.
        amountText.text = "x0";
        amountText.enabled = false;

        // update description.
        descriptor.label = defaultDescLabel;
        descriptor.description = defaultDescDesc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
