using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the ending manager for the game.
public class EndManager : Manager
{
    // the name input field
    public InputField nameInputField;

    //used to change opacity of name field
    private int sign = -1;

    // the length of the game the player just completed.
    // TODO: this is here for testing purposes. This value only needs to be updated once, so this can be a local variable.
    public float completionTime = 0.0F;

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
            // TODO: gets the values and save them.
            // grabs the final time.
            completionTime = resultsData.completionTime;

            // TODO: CHECK CERTIFICATE VALUE

            // destroy the object and by extension this script.
            Destroy(resultsData.gameObject);
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
        //TODO: ask Robert how they implemented this
        //did they use this SaveFileDialog? looked like it in the last game
        //System.Windows.Forms.SaveFileDialog;
        //does this only work for windows machines? what about macs?

        //this funtion expects hard coded file path value, we prob dont want to use
        //ScreenCapture.CaptureScreenshot("C:/image.png");

        print("I do nothing right now");
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
