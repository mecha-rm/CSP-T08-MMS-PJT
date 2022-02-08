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
    public Collider trigCollider;

    // Start is called before the first frame update
    private void Start()
    {
        // gets the attached collider.
        trigCollider = GetComponent<Collider>();
    }

    // enables the collider.
    public void EnableCollider()
    {
        trigCollider.enabled = true;
    }

    // disables the collider.

    public void DisableCollider()
    {
        trigCollider.enabled = false;
    }

    // toggles collider.
    public void ToggleColliderEnabled()
    {
        trigCollider.enabled = !trigCollider.enabled;
    }

    // checks of the collider is enabled
    public bool IsCollderEnabled()
    {
        return trigCollider.enabled;
    }
}
