using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// causes an object to trigger a screen upon being hit with a ray.
// this is made for going forward a screen without the screen object holding the collider.
public class ScreenTrigger : MonoBehaviour
{
    // the screen that the screen trigger transitons to.
    public RoomScreen screen = null;

    // the collider for the screen trigger.
    public new Collider collider;

    // turns off the collider when in the screen, on otherwise.
    public bool colOffWhenInScreen = true;

    // the debug object used to represent the screen trigger. It deletes this object when the game starts.
    [Tooltip("Use an object to display the screen trigger's collider, but delete it when the game starts using this variable.")]
    public GameObject colliderDisplay;

    // deletes the display of the collider.
    public bool deleteColliderDisplay = true;

    // Start is called before the first frame update
    private void Start()
    {
        // gets the attached collider.
        collider = GetComponent<Collider>();

        // destroys the debug object.
        if (deleteColliderDisplay && colliderDisplay != null)
            Destroy(colliderDisplay);
    }

    // enables the collider.
    public void EnableCollider()
    {
        collider.enabled = true;
    }

    // disables the collider.

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    // toggles collider.
    public void ToggleColliderEnabled()
    {
        collider.enabled = !collider.enabled;
    }

    // checks of the collider is enabled
    public bool IsCollderEnabled()
    {
        return collider.enabled;
    }

    // Update is called once per frame
    private void Update()
    {
        // keep collider enabled no matter what.
        if(colOffWhenInScreen && screen != null)
        {
            // TODO: is it efficient to override this every time?

            // checks if this is the current screen.
            if (screen.manager.currentScreen == screen) // in screen, so turn collider off.
                collider.enabled = false;
            else // not current screen, so turn collider on.
                collider.enabled = true;
        }

    }
}
