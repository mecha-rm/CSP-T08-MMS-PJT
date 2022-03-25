using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

// the ending manager for the game.
public class EndManager : Manager
{
    // the length of the game the player just completed.
    // TODO: this is here for testing purposes. This value only needs to be updated once, so this can be a local variable.
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
    public Text bonusText;

    // Screenshot stuff from Robert
    [DllImport("__Internal")]
    private static extern void openWindow(string url);
   
    // Start is called before the first frame update
    protected new void Start()
    {
        // changes frame rate
        // this is called by the title manager, so in normal play this isn't called.
        if (Application.isEditor)
            base.Start();

        // finds the input field if not set.
        // TODO: if there's more than one input field remove this.
        if (nameInputField == null)
            nameInputField = FindObjectOfType<InputField>();    

        // RESULTS DATA //

        // tries to find an endgame data object.
        ResultsData resultsData = FindObjectOfType<ResultsData>(true);

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
                bonusText.text = "Got Bonus Item!";
            else
                bonusText.text = "Did not get bonus item.";
        }
            

    }

    // starts the game scene.
    public void ReturnToTitle()
    {
        SceneHelper.LoadScene("TitleScene");
    }
    
    //sends player to mining matters website
    public void LearnMore()
    {
        Application.OpenURL("https://miningmatters.ca/");
    }

    //captures screenshot of Certificate of completion
    public void CaptureScreen()
    {
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
