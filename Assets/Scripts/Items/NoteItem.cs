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
