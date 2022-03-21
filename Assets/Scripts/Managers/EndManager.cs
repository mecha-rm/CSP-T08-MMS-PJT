using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the ending manager for the game.
public class EndManager : Manager
{
    // the name input field
    public InputField nameInputField;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
