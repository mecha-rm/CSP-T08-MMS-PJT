using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the truck puzzle.
public class Truck : PuzzleMechanic
{
    // the gameplay manager for the game.
    public GameplayManager manager;

    // the start position.
    public Vector3 startPos;

    // uses the start position.
    [Tooltip("If turue, the starting position is the current position of the truck.")]
    public bool startIsCurrentPos = true;

    // the ending position.
    public Vector3 endPos;

    // // if 'true', the animation is played.
    // // if 'false', the truck is just moved.
    // [Tooltip("Plays the animation instead of snapping the truck to the next position. No animation has been implemented.")]
    // public bool playAnimation;

    // the identifier for the key.
    public string keyId = Item.KEY_ID;

    [Header("Audio")]
    // the manager
    public AudioManager audioManager;

    // the engine clip.
    public AudioClip engineClip;

    // Start is called before the first frame update
    protected new void Start()
    {
        // calls the start function.
        base.Start();

        // grabs the main gameplay manager.
        if (manager == null)
            manager = GameplayManager.Current;

        // saves the position.
        if (startIsCurrentPos)
            startPos = transform.position;

        // the key id for the truck.
        if (keyId == "")
            keyId = Item.KEY_ID;

        // grabs the audio manager.
        if (audioManager == null)
            audioManager = manager.audioManager;

        // loads the engine clip.
        if (engineClip == null)
            engineClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_TRUCK_ENGINE_START");
    }

    // mouse clicked on truck.
    private void OnMouseDown()
    {
        // truck should drive.
        if(!solved)
            DriveTruck();
    }

    // starts the truck.
    public bool DriveTruck()
    {
        // manager not set.
        if (manager == null)
            return false;

        // saves the player.
        Player player = manager.player;

        // player not set.
        if (player == null)
            return false;

        // the player has the key id.
        if(player.HasItem(keyId))
        {
            // takes the key from the player.
            player.TakeItem(keyId);

            // TODO: play animation of the truck moving instead of snapping it to the ending position.

            // move to end position.
            transform.position = endPos;

            // puzzle solved.
            solved = true;

            // the puzzle is complete.
            if (puzzle != null)
                puzzle.OnPuzzleCompletion(this);

            // plays the engine sound.
            PlayEngineSound();
        }
        else
        {
            solved = false;
        }


        return solved;
    }

    // plays the engine sound.
    public void PlayEngineSound()
    {
        // plays the engine clip.
        if (audioManager != null && engineClip != null)
            audioManager.PlayAudio(engineClip);
    }

    // initiate the main action.
    public override void InitiateMainAction()
    {
        DriveTruck();
    }

    // is the puzzle complete.
    public override bool IsPuzzleComplete()
    {
        return solved;
    }

    // on component enable.
    public override void OnComponentEnable()
    {
        // N/A
    }

    // on component disable.
    public override void OnComponentDisable()
    {
        // N/A
    }

    // on the puzzle reset.
    public override void ResetMechanic()
    {
        // the mechanic has been reset.
        base.ResetMechanic();

        transform.position = startPos;
    }

    // Update is called once per frame
    protected new void Update()
    {
        // base update.
        base.Update();
    }
}
