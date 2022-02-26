using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the ending manager for the game.
public class EndManager : Manager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // starts the game scene.
    public void ReturnToTitle()
    {
        SceneHelper.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
