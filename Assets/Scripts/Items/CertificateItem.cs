using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the certificate item for the player.
// TODO: delete this, since it doesn't need unique functionality.
public class CertificateItem : Item
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // name
        if (descriptor.label == "")
            descriptor.label = "Certificate";

        // description
        if (descriptor.description == "")
            descriptor.description = "A certificate for understanding Earthquakes, however this doesn’t seem to be useful in getting out of the mine.";

        // stack id
        if (itemId == "") // stack all puzzle pieces.
            itemId = CERTIFICATE_ID;

        // item icon not set.
        if (itemIcon == null)
        {
            // certificate icon.
            string file = "Images/Inventory/certificate_icon";

            // loads resource.
            Sprite temp = Resources.Load<Sprite>(file);

            // checks if valid.
            if (temp != null)
            {
                // if the object can be converted from a sprite.
                itemIcon = temp;
            }
        }

    }
}
