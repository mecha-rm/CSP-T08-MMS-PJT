﻿using System.Collections;
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

    // the game object that contains all the lights that are room specific.
    // these add extra illumination to the game.
    // use this object for any in-room specific lighting that you want.
    // TODO: this should probably be re-worked.
    public GameObject lights;

    // if 'true', the room starts with the lights on.
    public bool lightsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // finds the light object. 
        if (lights == null)
        {
            // finds object.
            Transform tform = transform.Find("Lights");
            
            // object found, so it sets it.
            if (tform != null)
                lights = tform.gameObject;

            // finds a post processing object.
            // lights = GameObject.Find("Post Processing");
        }
    }

    // returns 'true' if the lights are enabled.
    public bool IsLightingEnabled()
    {
        // // no lighting object.
        // if (lights == null)
        //     return false;
        // 
        // return lights.activeSelf;

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

        // no lighting object.
        if (lights == null)
            return;

        lights.SetActive(lighting);
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

    // Update is called once per frame
    void Update()
    {
        // TODO: this is just for testing. It should justbe removed.
        if (manager.IsRoomLightingEnabled() != lightsOn)
        {
            // manager.SetRoomLightingEnabled(lightsOn);
            SetLightingEnabled(lightsOn);
        }
            
    }
}
