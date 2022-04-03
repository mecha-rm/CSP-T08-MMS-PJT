﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds the name and the description of the item.
public class Descriptor : MonoBehaviour
{
    // the gameplay manager.
    public GameplayManager manager;

    // the secondary name of the object.
    // not called 'name' because 'name' is the name of the object.
    public string secondName = "";

    // the description of the descriptor.
    public string description = "";

    // Start is called before the first frame update
    void Start()
    {
        // manager object.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // gets the name of the game object an saves it as the secondary name.
        if (secondName == "")
            secondName = name;
    }

    // the name saved to the descriptor.
    public string Name
    {
        get
        {
            return secondName;
        }

        set
        {
            secondName = value;
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
}
