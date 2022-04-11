using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a note item.
public class NoteItem : Item
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // name is not set.
        if(descriptor.label == "")
            descriptor.label = "Note";

        // description is not set.
        // the note's description will be what the note says.
        if (descriptor.description == "")
            descriptor.description = "...";

        // NOTE: this causes the notes to be stacked, which means only one can be read.
        // as such, while this is the default, each note has a different id.
        // the user never has two notes at a time, but to be safe the note ids should vary by note.

        // stack id
        if (itemId == "") // saves the note.
            itemId = NOTE_ID;

        // item icon not set.
        if (itemIcon == null)
        {
            // file to be loaded.
            string file = "Images/Inventory/note_icon";

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

    // note title.
    public string Title
    {
        get
        {
            // checks if the descriptor is set.
            if (HasDescriptor())
                return descriptor.label;
            else
                return "";
        }

        set
        {
            // descriptor is set.
            if(HasDescriptor())
                descriptor.label = value;
        }
    }

    // note text.
    public string Text
    {
        get
        {
            // descriptor is set.
            if (HasDescriptor())
                return descriptor.description;
            else
                return "";
        }

        set
        {
            // descriptor is set.
            if (HasDescriptor())
                descriptor.description = value;
        }
    }
    
    // if the note item has a descriptor, it returns this variable.
    public bool HasDescriptor()
    {
        // descriptor is not set, so try to find it.
        if (descriptor == null)
            descriptor = GetComponent<Descriptor>();

        return descriptor != null;
    }

    // gets the text.
    public string GetText()
    {
        return descriptor.description;
    }

    // sets the note's text.
    public void SetText(string text)
    {
        descriptor.description = text;
    }
}
