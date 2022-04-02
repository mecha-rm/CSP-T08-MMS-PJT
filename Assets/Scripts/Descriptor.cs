using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds the name and the description of the item.
public class Descriptor : MonoBehaviour
{
    // the gameplay manager.
    public GameplayManager manager;

    // the secondary name of the object.
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
}
