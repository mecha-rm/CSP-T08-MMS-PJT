using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// a key for a keypad.
public class KeypadKey : MonoBehaviour
{

    public AudioManager audioManager;


    // the key operation
    public enum keyFunc { text, backspace, clear, confirm }

    // the keypad ths key belongs too.
    public Keypad keypad;

    // the operation tied to this key.
    public keyFunc keyFunction = keyFunc.text;

    // the character attached to this key.
    [Tooltip("Text to be added to the keypad. This only applies if the key operation is set to 'text'")]
    public string textChar = "";

    // TODO: add space for animation.

    // Start is called before the first frame update
    void Start()
    {
        // tries to check the parent for the keypad key.
        if (keypad == null)
            keypad = GetComponentInParent<Keypad>();
    }

    // called when the mouse button is clicked down.
    private void OnMouseDown()
    {
        // performs the function weh
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformFunction();
            audioManager.PlayAudio(audioManager.KeyPad_Pressed);
        }
            
    }

    // performs the function.
    public void PerformFunction()
    {
        // the text character this key provides.
        if (keypad != null)
        {
            // can't interact with the puzzle, so don't do anything.
            if (!keypad.interactable)
                return;

            // checks which function it is.
            switch (keyFunction)
            {
                case keyFunc.text: // text
                    keypad.AddString(textChar);
                    break;

                case keyFunc.backspace: // removes character.
                    keypad.RemoveLastCharacter();
                    break;

                case keyFunc.clear: // clears the text out.
                    keypad.ClearText();
                    break;

                case keyFunc.confirm: // tries to confirm the entry.
                    keypad.ConfirmEntry();
                    break;
            }
        }
    }

}
