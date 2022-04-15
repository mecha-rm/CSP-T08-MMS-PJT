using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the class for the keypad object.
public class Keypad : PuzzleMechanic
{
    // the text for the keypad
    public string text = "*****";

    // the default text.
    private string defaultText = "*****";

    // the length limit for the text.
    // if it's negative, then there is no limit.
    public int lengthLimit = -1;

    // the passcode for this keypad.
    // there is only one keypad, and this is its passcode, so this is the default.
    public string passcode = "44789";

    // the canvas used for displaying the text.
    public Canvas textCanvas;

    // the text display
    public Text textDisplay;

    // the list of keypad keys.
    [Tooltip("The list of keys for the keypad, which are enabled/disabled with this component.")]
    public List<KeypadKey> keys = new List<KeypadKey>();

    [Header("Audio")]

    // the audio manager for the lock. This is used by the individual keys, but it's put here for ease of access.
    public AudioManager audioManager;

    // audio clip for the keypad being pressed.
    public AudioClip keyPressedClip;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // looks in the children for the component.
        if (textCanvas == null)
            textCanvas = GetComponentInChildren<Canvas>();

        // text canvas is set.
        if (textCanvas != null)
        {
            // if not set, this script will set the world camera (event camera).
            // this is needed for the text display.
            if (textCanvas.worldCamera == null)
                textCanvas.worldCamera = Camera.main;
        }

        // keys for the keypad.
        if(keys.Count != 0)
        {
            GetComponentsInChildren<KeypadKey>(true, keys);
        }    
            

        // deault text set to whatever's saved as the default.
        defaultText = text;

        // uses the main audio manager.
        if (audioManager == null)
            audioManager = GameplayManager.Current.audioManager;

        // loads up the keypad clip.
        if (keyPressedClip == null)
            keyPressedClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_KEYPAD_BUTTON_PRESS_CUT");
    }

    // hides the text.
    public void HideText()
    {
        // deactivates the text object.
        if (textDisplay != null)
            textDisplay.gameObject.SetActive(false);
    }

    // shows the text.
    public void ShowText()
    {
        // shows the text object.
        if (textDisplay != null)
            textDisplay.gameObject.SetActive(true);
    }

    // toggles the text.
    public void ToggleText()
    {
        // toggles the text object.
        if (textDisplay != null)
            textDisplay.gameObject.SetActive(!textDisplay.gameObject.activeSelf);
    }

    // adds text to the text object.
    public void AddCharacter(char ch)
    {
        text += ch.ToString();
    }

    // adds a whole string.
    public void AddString(string str)
    {
        text += str;
    }

    // removes a character.
    public void RemoveLastCharacter()
    {
        // if there are character's to remove.
        if (text.Length > 0)
            text = text.Substring(0, text.Length - 1);
        else // no character's left.
            text = "";
    }

    // clears out the text.
    public void ClearText()
    {
        text = "";
    }

    // used to confirm a text entry.
    public bool ConfirmEntry()
    {
        // checks if text is equal to right passcode.
        bool correct = (text == passcode);

        // output message.
        Debug.Log((correct) ? "SUCCESS" : "FAIL");

        // if the combination is correct and the puzzle is set.
        if (correct && puzzle != null)
            puzzle.OnPuzzleCompletion(this);

        solved = correct;
        return correct;
    }

    // play the sound for when the key is pressed.
    public void PlayKeyPressedSound()
    {
        // plays the key pressed clip.
        if (audioManager != null)
            audioManager.PlayAudio(keyPressedClip);
    }

    // initiates the main action for this puzzle.
    public override void InitiateMainAction()
    {
        // if this can't be interacted with, do nothing.
        if (!interactable)
            return;

        ConfirmEntry();
    }

    // called when the puzzle mechanic component is enabled.
    public override void OnComponentEnable()
    {
        foreach (KeypadKey key in keys)
            key.enabled = true;
    }

    // called when the puzzle mechanic component is disabled.
    public override void OnComponentDisable()
    {
        foreach (KeypadKey key in keys)
            key.enabled = false;
    }

    // checks if the puzzle was completed successfully successful.
    public override bool IsPuzzleComplete()
    {
        // checks if complete.
        return solved;
    }

    // resets the puzzle.
    public override void ResetMechanic()
    {
        // the mechanic has been reset.
        base.ResetMechanic();

        // reset to the default text.
        text = defaultText;

        // called to reset the puzzle.
        if (puzzle != null)
            puzzle.OnPuzzleReset();
    }

    // Update is called once per frame
    protected new void Update()
    {
        // limits the text length.
        if (lengthLimit >= 0 && text.Length > lengthLimit)
            text = text.Substring(0, lengthLimit);

        // updates the text display every frame.
        if (textDisplay != null)
        {
            // if the text has changed, update it.
            if (textDisplay.text != text)
                textDisplay.text = text;
        }

        // the bounds should still be checked, even if the puzzle is inactive.
        // as such, the update goes down here instead.
        base.Update();
    }
}
