using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

// the ending manager for the game.
public class EndManager : Manager
{
    // the current end manager.
    private static EndManager current;

    // the length of the game the player just completed.
    // TODO: this is here for testing purposes. This value only needs to be updated once, so it doesn't need to be saved here.
    public float completionTime = 0.0F;

    // if the end manager has the certificate.
    public bool gotCertificate;

    // user interface 
    [Header("UI")]

    // the name input field
    public InputField nameInputField;

    // used to change opacity of name field
    private int sign = -1;

    // the text for the completion time.
    public Text timeText;

    // the text for the bonus item.
    // TODO: maybe replace this with a symbol rather than have text?
    public Text bonusText;

    // the screenshot button.
    public Button screenshotButton;

    // Screenshot stuff from Robert
    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    //open link in new tab
    [DllImport("__Internal")]
    private static extern void OpenURLInExternalWindow(string url);

    // audio manager
    [Header("Audio")]
    // the audio manager for the title screen.
    public AudioManager audioManager;

    // the button audio.
    public AudioClip buttonClip;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // there is only one
        current = this;
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        // changes frame rate
        // this is called by the title manager, so in normal play this isn't called.
        if (Application.isEditor)
            base.Start();

        current = this;

        // finds the input field if not set.
        // TODO: if there's more than one input field remove this.
        if (nameInputField == null)
            nameInputField = FindObjectOfType<InputField>();

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

        // RESULTS DATA //

        // tries to find an endgame data object.
        GameEndInfo resultsData = FindObjectOfType<GameEndInfo>(true);

        // there is endgame data.
        if(resultsData != null)
        {
            // grabs the final time.
            completionTime = resultsData.completionTime;

            // the player got the certificate.
            gotCertificate = resultsData.gotCertificate;


            // destroy the object and by extension this script.
            Destroy(resultsData.gameObject);
        }

        // updates the time text.
        if (timeText != null)
            timeText.text = "Completion Time: " + Timer.TimeToString(completionTime, false);

        // updates the bonus text.
        if (bonusText != null)
        {
            // TODO: maybe replace this with an icon instead of a text component.
            if(gotCertificate)
                bonusText.text = "You Got the Bonus Item!";
            else
                bonusText.text = "You Did Not Get the Bonus Item.";
        }

        // if the screenshot button has been set.
        if(screenshotButton != null)
        {
            // TODO: comment this out to disable the screenshot button in the editor.
            // the game doesn't break if the button is pressed in the editor, but it won't work either.

            // // if not in the editor, and the platform is a webgl player
            // if(!Application.isEditor && Application.platform == RuntimePlatform.WebGLPlayer)
            // {
            //     screenshotButton.interactable = true;
            // }
            // else // disable button since it can't be used.
            // {
            //     screenshotButton.interactable = false;
            // }
        }
            

    }

    // gets the current manager.
    public static EndManager Current
    {
        get
        {
            // saves the end manager manager if not set.
            if (current == null)
                current = FindObjectOfType<EndManager>(true);

            return current;
        }
    }

    // starts the game scene.
    public void ReturnToTitle()
    {
        // the audio doesn't get played on scene switch, likely due to the object being destroyed.
        // // plays the button sound before leaving.
        // PlayButtonSound();

        SceneHelper.LoadScene("TitleScene");
    }
    
    //sends player to mining matters website
    public void LearnMore()
    {
        // plays the button sound.
        PlayButtonSound();

        // Application.OpenURL("https://miningmatters.ca/");
        #if !UNITY_EDITOR
            OpenURLInExternalWindow("https://miningmatters.ca/");
        #endif
    }

    //captures screenshot of Certificate of completion
    public void CaptureScreen()
    {
        // plays the button sound.
        PlayButtonSound();

        StartCoroutine(UploadPNG());
    }

    IEnumerator UploadPNG()
    {
        //We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String(bytes);

        var image_url = "data:image/png;base64," + encodedText;

        #if !UNITY_EDITOR
            openWindow(image_url);
        #endif
    }

    // plays a sound.
    public void PlayButtonSound()
    {
        // plays a sound.
        if (audioManager != null)
        {
            // plays the specific clip, otherwise it plays audio 0.
            if (buttonClip != null)
                audioManager.PlayAudio(buttonClip);
            else
                audioManager.PlayAudio(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //continually re-focus textbox in case user clicks off it
        nameInputField.Select();
        nameInputField.ActivateInputField();

        //updates opacity of enter name field if user has not entered anything
        if(nameInputField.text == "")
        {
            Color placeholderColor = nameInputField.placeholder.color;

            //if placeholder alpha is above 0.9 start to decrease alpha value
            if(placeholderColor.a > 0.9)
            {
                sign = -1;
            }
            //if placeholder alpha is below 0.1 start to increase alpha value
            else if(placeholderColor.a < 0.1)
            {
                sign = 1;
            }

            //calculate new alpha value
            placeholderColor.a = placeholderColor.a + sign * 0.01F;

            //update alpha value
            nameInputField.placeholder.color = placeholderColor;
        }
    }

}
