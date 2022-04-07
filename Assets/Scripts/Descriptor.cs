using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds the name and the description of the item.
public class Descriptor : MonoBehaviour
{
    // the gameplay manager.
    public GameplayManager manager;

    // the secondary name/label of the object.
    // not called 'name' because 'name' is the name of the object.
    public string label = "";

    // the description of the descriptor.
    public string description = "";

    // shows the label if one is set.
    [Tooltip("Shows the label in the inspect text if it is available.")]
    public bool showLabel = true;

    // Start is called before the first frame update
    void Start()
    {
        // manager object.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // gets the name of the game object an saves it as the secondary name.
        if (label == "")
            label = name;
    }

    // the name saved to the descriptor.
    public string Label
    {
        get
        {
            return label;
        }

        set
        {
            label = value;
        }
    }

    // the saved description.
    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    // copies the content from the copied description.
    public void CopyContent(Descriptor sourceDesc)
    {
        label = sourceDesc.label;
        description = sourceDesc.description;
    }
}
