using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toggles the activity of the object upon being clicked.
public class ToggleActiveOnClick : MonoBehaviour
{
    // game object this component relates to.
    public GameObject pairedObject = null;

    // TODO: include space for animation.

    // becomes 'true' when the object should toggle.
    private bool toggle = false;

    // key for the mouse.
    public KeyCode key = KeyCode.Mouse0;

    // use this for mouse interactions if you don't want to use the OnClickFunction.
    public MouseInteraction interact;

    // if 'true', unity mouse operations triggered on this object also apply.
    public bool useUnityMouse = true;

    // Start is called before the first frame update
    void Start()
    {
        // if go is set to null.
        if (pairedObject == null)
            pairedObject = gameObject;
    }

    // called when the mouse button goes down
    private void OnMouseDown()
    {
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
        pairedObject.SetActive(!pairedObject.activeSelf); // deactivates the paired object.
    }

    // Update is called once per frame
    void Update()
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
