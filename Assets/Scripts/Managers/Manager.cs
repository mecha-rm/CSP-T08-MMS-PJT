using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// general script for shared manager functions.
public class Manager : MonoBehaviour
{
    // toggles the activity of a game object.
    public void ToggleGameObjectActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    // quits the application.
    public void QuitApplication()
    {
        Application.Quit();
    }
}
