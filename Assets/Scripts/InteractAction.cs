using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// causes an action to happen when an element is interacted with by the player.
public abstract class InteractAction : MonoBehaviour
{
    public List<string> itemIds = new List<string>(); 

    // TODO: implement for the truck

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // the action being triggered.
    public abstract void Action();

    // Update is called once per frame
    void Update()
    {
        
    }
}
