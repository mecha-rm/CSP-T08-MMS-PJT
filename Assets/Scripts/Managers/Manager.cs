using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// general script for shared manager functions.
public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // sets the frame rate.
        // this only really needs to be called at the title scene.
        Application.targetFrameRate = 60;
    }

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
