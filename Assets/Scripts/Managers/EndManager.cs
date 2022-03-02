using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the ending manager for the game.
public class EndManager : Manager
{
    // Start is called before the first frame update
    protected new void Start()
    {
        // changes frame rate
        // this is called by the title manager, so in normal play this isn't called.
        if (Application.isEditor)
            base.Start();
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
