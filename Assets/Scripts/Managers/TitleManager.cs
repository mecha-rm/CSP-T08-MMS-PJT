using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the manager for the TitleScene.
public class TitleManager : Manager
{

    public bool screenReaderToggle = false;
    public bool highContrastToggle = false;

    private GameObject screen1;
    private GameObject screen2;
    private GameObject screen3;

    private GameObject controls;
    private GameObject objective;

    private bool isOnScreen1 = true;

    private Image screenReaderCheckmark;
    private Image highContrastCheckmark;

    // Start is called before the first frame update
    protected new void Start() 
    {
        // changes frame rate at the start of the game.
        base.Start();

        //initialize all required menu objects
        screenReaderCheckmark = GameObject.Find("Screen Reader Checkmark").GetComponent<Image>();
        highContrastCheckmark = GameObject.Find("High Contrast Checkmark").GetComponent<Image>();
        
        screen1 = GameObject.Find("Screen 1");
        screen2 = GameObject.Find("Screen 2");
        screen3 = GameObject.Find("Screen 3");

        screen2.SetActive(false);
        screen3.SetActive(false);

        objective = screen3.transform.Find("Objective").gameObject;
        controls = screen3.transform.Find("Controls").gameObject;

        objective.SetActive(false);
        controls.SetActive(true);
    }

    // starts the game scene.
    public void StartGame()
    {
        SceneHelper.LoadScene("GameScene");
    }

    // display screen 1
    public void GoToScreen1()
    {
        isOnScreen1 = true;
        screen1.SetActive(true);
        screen2.SetActive(false);
    }

    //display screen 2
    public void GoToScreen2()
    {
        isOnScreen1 = false;
        screen1.SetActive(false);
        screen2.SetActive(true);
        screen3.SetActive(false);
    }

    //display screen 3
    public void GoToScreen3()
    {
        screen2.SetActive(false);
        screen3.SetActive(true);
    }

    //display controls
    public void ViewControls()
    {
        objective.SetActive(false);
        controls.SetActive(true);
    }

    //display objective
    public void ViewObjective()
    {
        controls.SetActive(false);
        objective.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        //ensures that toggles can only be changed when screen1 GameObject is active
        if(isOnScreen1){

            if (Input.GetKeyDown("0")){

                //flips screen reader toggle boolean
                screenReaderToggle = !screenReaderToggle;

                //shows/hides checkmark in screen reader box
                screenReaderCheckmark.enabled = screenReaderToggle;
            }

            if (Input.GetKeyDown("h")){

                //flips high contrast toggle boolean
                highContrastToggle = !highContrastToggle;

                //shows/hides checkmark in high contrast box
                highContrastCheckmark.enabled = highContrastToggle;
            }
        }
    }
}
