using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the manager for the TitleScene.
public class TitleManager : Manager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // starts the game scene.
    public void StartGame()
    {
        SceneHelper.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
