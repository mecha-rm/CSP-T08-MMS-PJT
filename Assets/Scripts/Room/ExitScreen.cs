using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the screen for exiting the game.
public class ExitScreen : RoomScreen
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // called when entering the screen.
    public override void OnScreenEnter()
    {
        base.OnScreenEnter();

        // exits the game the moment this screen is entered.
        // as such, this screen can point to nothing, basically.
        // TODO: maybe put up a message or something?
        SceneHelper.ChangeScene("EndScene");
    }

    // called when exiting the screen.
    public override void OnScreenExit()
    {
        base.OnScreenExit();

        // changes to the end screen.
        // this doesn't get used since the game ends.
        // maybe you can use this to end the scene, and the enter screen can be used to post a message?
        // though in that case, you need to call this at the end of the message.
        // SceneHelper.ChangeScene("EndScene");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
