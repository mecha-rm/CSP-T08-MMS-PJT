using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the room script.
// NOTE: this may need to be adjusted for setting up the lighting.
public class Room : MonoBehaviour
{
    // the room number.
    public int roomNumber = 0;

    // the gameplay manager.
    public GameplayManager manager;

    // if 'true', the room changes the lighting on enable.
    public bool changeLightingOnEnable = true;

    // if 'true', the room starts with the lights on.
    [Tooltip("Turns the lighting on or off.")]
    public bool lightsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();
    }

    // returns 'true' if the lights are enabled.
    public bool IsLightingEnabled()
    {
        // returns value.
        return lightsOn;
    }

    // NOTE: this only concerns what's in the lighting object.
    // use manager.SetRoomLightingEnabled() for the full functionality.

    // sets lighting enabled value.
    public void SetLightingEnabled(bool lighting)
    {
        // saves value.
        lightsOn = lighting;

        // changes lighting.
        if (manager != null)
        {
            manager.SetRoomLightingEnabled(lightsOn);
        }
        
    }

    // enable the room's lighting.
    public void EnableLighting()
    {
        SetLightingEnabled(true);
    }

    // disables the lighting.
    public void DisableLighting()
    {
        SetLightingEnabled(false);
    }

    // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        // changes the room lighting when the script is enabled.
        if(changeLightingOnEnable)
        {
            // change the lighting.
            SetLightingEnabled(lightsOn);
        }
    }

    // Update is called once per frame
    void Update()
    {      
    }
}
