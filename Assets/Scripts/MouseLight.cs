using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a light that tracks around the mouse.
public class MouseLight : MonoBehaviour
{ 
    // a mouse object for getting the mouse position.
    public Mouse mouse;

    // the light that follows the mouse cursor.
    public Light mouseLight;

    // Start is called before the first frame update
    void Start()
    {
        // finds the mouse script.
        if (mouse == null)
            mouse = FindObjectOfType<Mouse>();

        // light component not set.
        if(mouseLight == null)
        {
            // finds the mouse light.
            mouseLight = GetComponent<Light>();

            // gets component from children.
            if (mouseLight == null)
                mouseLight = GetComponentInChildren<Light>();
        }
    }

    // light is not enabled.
    public bool IsLightEnabled()
    {
        return mouseLight.enabled;
    }

    // sets whethr or not the light should be enabled.
    public void SetLightEnabled(bool e)
    {
        mouseLight.enabled = e;
    }

    // enables the light.
    public void EnableLight()
    {
        mouseLight.enabled = true;
    }

    // disables the light.
    public void DisableLight()
    {
        mouseLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if the mouse light is enabled, adjust the object's forward.
        if(mouseLight.enabled)
        {
            // updates to mouse world position.
            Vector3 newNormal = Rotation.NormalTowardsMouse3D(transform.position, Camera.main);

            // sets new forward.
            transform.forward = newNormal;
        }
        
    }
}
