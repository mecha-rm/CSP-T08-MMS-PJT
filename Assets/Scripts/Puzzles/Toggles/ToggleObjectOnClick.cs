using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toggles the activity of the object upon being clicked.
// NOTE: this does not seem to like spherical colliders in 2D. It seems fine with 3D Box Colliders in 2D though.
// So, do NOT use sphereical colliders if you plan on using these functions in orthographic mode.
// this is a general function, so this is NOT a child of PuzzleMechanic.
public class ToggleObjectOnClick : MonoBehaviour
{
    // game object this component relates to.
    [Tooltip("The object this script toggles.")]
    public GameObject pairedObject = null;

    // TODO: make a function to play an animation and then check if its done.
    // // an animation that can be played.
    // public Animator anim;
    // 
    // // the name of the animation that will be played.
    // public string animName;

    // settings
    [Header("Settings")]

    // becomes 'true' when the object should toggle.
    private bool toggle = false;

    // key for the mouse.
    [Tooltip("The key checked for the Mouse events (default: left mouse button).")]
    public KeyCode key = KeyCode.Mouse0;

    // use this for mouse interactions if you don't want to use the OnClickFunction.
    [Tooltip("Used if the object with mouse functions is seperate from the one that htis script is attached to.")]
    public MouseInteraction interact;

    // if 'true', unity mouse operations triggered on this object also apply.
    [Tooltip("Use the Unity mouse functions for triggering the toggle.")]
    public bool useUnityMouse = true;

    // if 'true', the component is enabled/disabled instead of the object being activated/deactivated.
    [Tooltip("Changed the enabled parameter for the paired object instead of the activeSelf parameter if true." +
        "It doesn't work if the ToggleObjectOnClick component isn't attached to the object.")]
    public bool changeToggleComponent = false;

    // // if available, play the animation before this happens.
    // public bool playAnimation = true;

    // Start is called before the first frame update
    protected void Start()
    {
        // if go is set to null.
        if (pairedObject == null)
            pairedObject = gameObject;
    }

    // called when the mouse button goes down
    private void OnMouseDown()
    {
        // TODO: this only gets called if the left mouse button is clicked.
        // move this to the update loop so that this actually does something.
        // use mouseOver instead.
        // key has been pressed down.
        if (useUnityMouse && Input.GetKeyDown(key))
            toggle = true;
    }

    // called to check the interaction variable.
    private void OnInteractDown()
    {
        // checks if the mouse key is down.
        if (interact.MouseDown && Input.GetKeyDown(key))
            toggle = true;
    }

    // called when the entity is toggled.
    protected virtual void OnToggle()
    {
        toggle = false; // allows deactivation again if activated again.

        // checks if the component should be toggled, or the whole object.
        if (changeToggleComponent) // toggle component
        {
            ToggleObjectOnClick temp;

            // tries to grab thecomponent.
            if(pairedObject.TryGetComponent(out temp))
            {
                // disables the component only.
                temp.enabled = false;
            }
            else
            {
                // failed.
                Debug.LogError("The object does not have a ToggleObjectOnClick component.");
            }
        }
        else // toggle whole object.
        {
            pairedObject.SetActive(!pairedObject.activeSelf); // deactivates the paired object.
        }

        
        
    }

    // toggles the entity.
    public virtual void Toggle()
    {
        OnToggle();
    }

    // Update is called once per frame
    protected void Update()
    {
        // checks interact variable.
        if (interact != null)
            OnInteractDown();

        // if the object should be deactivated.
        if (toggle)
        {
            // run operations.
            OnToggle();
        }
            

    }
}
