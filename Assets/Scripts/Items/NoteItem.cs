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
        if(desc.secondName == "")
            desc.secondName = "Note";

        // description is not set.
        if (desc.description == "")
            desc.description = "...";
    }

    // note title.
    public string Title
    {
        get
        {
            return desc.secondName;
        }

        set
        {
            desc.secondName = value;
        }
    }

    // note text.
    public string Text
    {
        get
        {
            return desc.description;
        }

        set
        {
            desc.description = value;
        }
    }

    // gets the text.
    public string GetText()
    {
        return desc.description;
    }

    // sets the note's text.
    public void SetText(string text)
    {
        desc.description = text;
    }
}
