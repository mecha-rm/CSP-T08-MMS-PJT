using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// switches the screen's light on and off.
public class LightToggle : MonoBehaviour
{
    // the gameplay manager.
    public GameplayManager manager;

    // if 'true', the light switch only works one time.
    public bool oneTime = false;

    // the amount of times the lights were toggled.
    public uint toggleCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // saves the manager.
        if (manager == null)
            manager = GameplayManager.Current;
    }

    // OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    private void OnMouseDown()
    {
        Toggle();
    }

    // flips the switch.
    public void Toggle()
    {
        // toggles the lights on.
        if(!oneTime || (oneTime && toggleCount == 0))
        {
            // toggles the room light.
            if (manager != null)
                manager.ToggleRoomLighting();

            toggleCount++;
        }
        
    }

    // resets the toggle count.
    public void ResetToggleCount()
    {
        toggleCount = 0;
    }
}
