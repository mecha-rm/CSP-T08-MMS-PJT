using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// assistant script for managing scenes.
public class SceneHelper : MonoBehaviour
{
    // changes the scene using the scene number.
    public static void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);

    }

    // changes the scene using the scene name.
    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // returns the skybox of the scene.
    public static Material GetSkybox()
    {
        return RenderSettings.skybox;
    }

    // sets the skybox of the scene.
    public static void SetSkybox(Material newSkybox)
    {
        RenderSettings.skybox = newSkybox;
    }

    // returns 'true' if the game is in full screen.
    public static bool IsFullScreen()
    {
        return Screen.fullScreen;
    }

    // sets 'full screen' mode for the game.
    public static void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    // toggles the full screen for the game.
    public static void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    // called to change the screen size.
    public void SetResolution(int width, int height, FullScreenMode mode)
    {
        Screen.SetResolution(width, height, mode);
    }

    // called to change the screen size.
    public void SetResolution(int width, int height, FullScreenMode mode, bool fullScreen)
    {
        SetResolution(width, height, mode);
        Screen.fullScreen = fullScreen;
    }

    // exits the game
    public void QuitApplication()
    {
        Application.Quit();
    }
}
