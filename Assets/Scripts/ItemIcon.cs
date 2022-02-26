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

        // shows/hides the text.
        amountText.enabled = showTextOnStart;

    }

    // updates the icon image.
    public void UpdateIcon(Sprite newSprite, int amount, bool showAmount)
    {
        // updates the sprite.
        iconImage.sprite = newSprite;
        iconImage.enabled = (newSprite != null);

        // updates the text.
        amountText.text = "x" + amount.ToString();
        amountText.enabled = showAmount;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
