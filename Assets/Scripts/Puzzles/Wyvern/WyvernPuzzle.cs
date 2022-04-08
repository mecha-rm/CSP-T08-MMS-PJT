using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernPuzzle : Puzzle
{
    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // Hide Wyvern, I dont think this is the best way to do this
        // however I cant think of another way
        //GameObject wyvern = GameObject.Find("Wyvern");

        Wyvern wyvern = FindObjectOfType<Wyvern>();

        if(wyvern != null)
            wyvern.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
