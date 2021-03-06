using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the manager for the TitleScene.
public class TitleManager : Manager
{
    // the current title manager.
    private static TitleManager current;

    // the screens for the menu.
    [Header("Screens")]
    // screen 1 (staritng screen)
    public GameObject screen1;

    // screen2 
    public GameObject screen2;

    // screen 3
    public GameObject screen3;

    // controls group.
    public GameObject controls;

    // objective group.
    public GameObject objective;

    // checks if on screen 1.
    private bool isOnScreen1 = true;

    // audio manager
    [Header("Audio")]
    // the audio manager for the title screen.
    public AudioManager audioManager;

    // the button audio.
    public AudioClip buttonClip;

    [Header("Accessibility")]

    // finds the screen reader toggles.
    // these toggles are not meant to be used with the mouse, so that component is disabled.
    
    // high contrast
    [Tooltip("High contrast toggle, which is a visual to show if the screen reader is enabled or not. " +
        "Use the correspond key to toggle it instead.")]
    public Toggle highContrastToggle;

    // screen reader
    [Tooltip("Screen reader toggle, which is a visual to show if the screen reader is enabled or not. " +
        "Use the correspond key to toggle it instead.")]
    public Toggle screenReaderToggle;

    // accessibility settings.
    
    // high contrast
    [Tooltip("Enables high contrast.")]
    public bool useHighContrast = false;

    // screen reader.
    [Tooltip("Enables screen reader.")]
    public bool useScreenReader = false;
    
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // there is only one
        current = this;
    }

    // Start is called before the first frame update
    protected new void Start() 
    {
        // changes frame rate at the start of the game.
        base.Start();

        current = this;

        //initialize all required menu objects
        
        // screen 1 not set.
        if(screen1 == null)
            screen1 = GameObject.Find("Screen 1"); // active objects only
        
        // screen 2 not set.
        if(screen2 == null)
            screen2 = GameObject.Find("Screen 2"); // active objects only

        // screen 3 not set.
        if (screen3 == null)
            screen3 = GameObject.Find("Screen 3"); // active objects only

        // start on screen 1.
        GoToScreen1();

        if(objective == null && screen3 != null)
            objective = screen3.transform.Find("Objective").gameObject;

        if (controls == null && screen3 != null)
            controls = screen3.transform.Find("Controls").gameObject;

        objective.SetActive(false);
        controls.SetActive(true);

        // AUDIO
        // finds the audio manager.
        if (audioManager == null)
            audioManager = FindObjectOfType<AudioManager>();

        // checks for the audio clip.
        if (buttonClip == null)
        {
            // loads the audio clip.
            buttonClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_MAIN_MENU_BUTTON_PRESS");
        }

        // ACCESSIBILITY
        // TODO: search for toggles?
        // high contrast
        if (highContrastToggle != null)
            highContrastToggle.isOn = useHighContrast;

        // screen reader
        if (screenReaderToggle != null)
            screenReaderToggle.isOn = useScreenReader;
    }

    // gets the current manager.
    public static TitleManager Current
    {
        get
        {
            // saves the end manager if not set.
            if (current == null)
                current = FindObjectOfType<TitleManager>(true);

            return current;
        }
    }

    // starts the game scene.
    public void StartGame()
    {
        // grabs the game start info.
        GameStartInfo gsi = FindObjectOfType<GameStartInfo>(true);

        // object not found, so make it.
        if(gsi == null)
        {
            // makes the object.
            GameObject temp = new GameObject("Game Start Info");

            // adds the start info component.
            gsi = temp.AddComponent<GameStartInfo>();
        }

        // use these parameters.
        gsi.useScreenReader = useScreenReader;
        gsi.useHighContrast = useHighContrast;

        // switch the scene.
        SceneHelper.LoadScene("GameScene");
    }

    // display screen 1
    public void GoToScreen1()
    {
        isOnScreen1 = true;
        screen1.SetActive(true);
        screen2.SetActive(false);
        screen3.SetActive(false);

        PlayButtonSound();
    }

    //display screen 2
    public void GoToScreen2()
    {
        isOnScreen1 = false;
        screen1.SetActive(false);
        screen2.SetActive(true);
        screen3.SetActive(false);

        PlayButtonSound();
    }

    //display screen 3
    public void GoToScreen3()
    {
        isOnScreen1 = false;
        screen1.SetActive(false);
        screen2.SetActive(false);
        screen3.SetActive(true);

        PlayButtonSound();
    }

    //display controls
    public void ViewControls()
    {
        objective.SetActive(false);
        controls.SetActive(true);

        PlayButtonSound();
    }

    //display objective
    public void ViewObjective()
    {
        controls.SetActive(false);
        objective.SetActive(true);

        PlayButtonSound();
    }

    // plays a sound.
    public void PlayButtonSound()
    {
        // plays a sound.
        if(audioManager != null)
        {
            // plays the specific clip, otherwise it plays audio 0.
            if(buttonClip != null)
                audioManager.PlayAudio(buttonClip);
            else
                audioManager.PlayAudio(0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //ensures that toggles can only be changed when screen1 GameObject is active
        if(isOnScreen1)
        {
            // toggle the high contrast
            if (Input.GetKeyDown(KeyCode.H))
            {
                useHighContrast = !useHighContrast;

                PlayButtonSound();
            }

            // toggle the screen reader.
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                useScreenReader = !useScreenReader;

                PlayButtonSound();
            }

            // update high contrast visual.
            if (highContrastToggle != null)
                highContrastToggle.isOn = useHighContrast;

            // update screen reader visual.
            if (screenReaderToggle != null)
                screenReaderToggle.isOn = useScreenReader;
            
        }
    }
}
