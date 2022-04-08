using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script for the settings to start the game.
public class GameStartInfo : MonoBehaviour
{
    // screen reader bool
    public bool useScreenReader;

    // high contrast bool
    public bool useHighContrast;

    // Start is called before the first frame update
    void Start()
    {
        // the start game data will be carried over to another scene.
        // this should be destroyed once the game screen grabs the information from it.
        DontDestroyOnLoad(gameObject);
    }

}
