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
    }

    // note title.
    public string Title
    {
        get
        {
            // TODO: descriptor may not be set.

            return descriptor.label;
        }

        set
        {
            descriptor.label = value;
        }
    }

    // note text.
    public string Text
    {
        get
        {
            return descriptor.description;
        }

        set
        {
            descriptor.description = value;
        }
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
