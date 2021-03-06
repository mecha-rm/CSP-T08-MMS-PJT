using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// elevator puzzle.
public class Elevator : PuzzleMechanic
{
    // the elevator's cable.
    public ElevatorCable cable;

    // if 'true', clicking on the elevator acts as a pull.
    // if 'false', it doesn't.
    public bool elevatorIsCable = true;

    // the amount of pulls done.
    public int pulls = 0;

    // goes by how many times the cable needs to be pulled.
    public int pullsNeeded = 2;

    // timer values.
    [Header("Timer")]

    // time for counting down the amount of pulls.
    public float resetTimer = 0.0F;

    // the timer for resetting the amount of pulls done.
    public float resetTimerMax = 2.0F;

    // have the timer run.
    public bool runTimer = true;

    // [Header("Other")]

    [Header("Audio")]

    // the audio manager.
    public AudioManager audioManager;

    // the bell clip.
    public AudioClip bellClip;

    // TOOD: have space for opening the door in here or in the puzzle script.

    // TODO: have space for triggering animation.

    // Start is called before the first frame update
    protected new void Start()
    {
        // calls the base start.
        base.Start();

        // gets the elevator cable.
        if (cable == null)
            cable = GetComponent<ElevatorCable>();

        // the amount of pulls needed.
        if (pullsNeeded <= 0)
            pullsNeeded = 2;

        // set this to the max.
        resetTimer = resetTimerMax;

        // grabs the main audio manager.
        if (audioManager == null)
            audioManager = GameplayManager.Current.audioManager;

        // bell clip is not set.
        if (bellClip == null)
        {
            // load the audio.
            bellClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_BELL_RINGING");
        }
    }

    // Called when the mouse button is pressed down.
    public void OnMouseDown()
    {
        // if the elevator should act as a cable.
        if(elevatorIsCable)
        {
            // called to pull the cable on the left mouse click.
            if (Input.GetKeyDown(KeyCode.Mouse0))
                PullCable();
        }
        
    }

    // pulls the cable.
    public void PullCable(int times)
    {
        // can't interact with the puzzle, so don't do anything.
        if (!interactable)
            return;

        // increases amount of pulls.
        pulls += times;

        // restart timer.
        resetTimer = resetTimerMax;

        // TODO: play animation.
    }

    // pulls the cable once.
    public void PullCable()
    {
        // pull only once.
        PullCable(1);

        // plays the bell sound.
        PlayBellSound();
    }

    // plays the bell sound.
    public void PlayBellSound()
    {
        // plays the bell audio clip.
        if (audioManager != null && bellClip != null)
            audioManager.PlayAudio(bellClip);
    }

    // initiates the main action for this puzzle.
    public override void InitiateMainAction()
    {
        PullCable();
    }

    // called when the puzzle mechanic component is enabled.
    public override void OnComponentEnable()
    {
        // enable cable if set.
        if (cable != null)
            cable.enabled = true;
    }

    // called when the puzzle mechanic component is disabled.
    public override void OnComponentDisable()
    {
        // hide cable if set.
        if (cable != null)
            cable.enabled = false;
    }

    // returns 'true' if the puzzle is complete.
    public override bool IsPuzzleComplete()
    {
        return solved;
    }

    // resets the puzzle.
    public override void ResetMechanic()
    {
        // the mechanic has been reset.
        base.ResetMechanic();


        pulls = 0;
        resetTimer = resetTimerMax;
        runTimer = true;
    }

    // Update is called once per frame
    protected new void Update()
    {
        // calls the base update.
        base.Update();

        // timer should be running.
        if(runTimer)
        {
            // reduces the timer.
            resetTimer -= Time.deltaTime;

            // timer is now at 0.
            if (resetTimer <= 0.0F)
            {
                // amount of pulls has been reached.
                if (pulls == pullsNeeded)
                {
                    // puzzle complete.
                    solved = true;

                    // open the door.
                    if (puzzle != null)
                        puzzle.OnPuzzleCompletion(this);

                    // stop running the timer since the puzzle is complete.
                    runTimer = false;

                    Debug.Log("Right amount of pulls.");
                }
                else
                {
                    // puzzle note complete.
                    solved = false;

                    // reset pull count.
                    pulls = 0;

                    Debug.Log("Wrong amount of pulls.");
                }

                // reset the time.
                resetTimer = resetTimerMax;
            }
        }

        
    }
}
