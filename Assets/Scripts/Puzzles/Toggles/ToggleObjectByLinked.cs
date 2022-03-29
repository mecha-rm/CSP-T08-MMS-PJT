﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// toggles the paired object if all the linked objects are deactivated.
public class ToggleObjectByLinked : ToggleObjectOnClick
{
    // toggles off once all the linkedo objects are off.
    public List<ToggleObjectOnClick> linkedObjects;

    // if 'true', it's checking for objects being active.
    // if 'false', it's checking for objects being inactive.
    public bool checkingActive = false;

    // if 'true', toggling the object is checked automatically in every update.
    public bool checkInUpdate = true;

    // if 'true', only the linked components are checked.
    // if 'false', whether or not the object is checked.
    public bool checkLinkedComponents;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // activates all linked objects
    // 'active' pertains the activating the object, and 'enabled' refers to the component.
    public void SetActiveAllLinkedObjects(bool active, bool enabled)
    {
        // activates all linked objects.
        foreach(ToggleObjectOnClick obj in linkedObjects)
        {
            obj.gameObject.SetActive(active);
            obj.enabled = enabled;
        }
    }

    // tries to toggle this object.
    // if 'allInactive' is true, it goes based on all objects being inactive or disabled.
    // if 'allInactive' is false, it goes based on all objects being active or enabled.
    // see 'checkLinkedComponents'
    public bool TryToggle(bool toggleIfTrue)
    {
        // the parameter used to check if this is active or not.
        bool active = checkingActive;

        // checks if the entity should be toggled or not.
        bool toggle = true;

        // checks to see if all objects are inactive.
        foreach (ToggleObjectOnClick obj in linkedObjects)
        {
            // determines if the components should be checked, or the whole objects.
            if(checkLinkedComponents) // components
            {
                // if even one enabled parameter does not match the 'active' argument, then the toggle can't happen.
                if (obj.enabled != active)
                {
                    toggle = false;
                    break;
                }
            }
            else // objects
            {
                // if even one activeSelf parameter does not match the 'active' argument, then the toggle can't happen.
                if (obj.gameObject.activeSelf != active)
                {
                    toggle = false;
                    break;
                } 
            }
        }

        // if this should be toggled if this returns true, do it.
        if (toggle && toggleIfTrue)
            base.OnToggle();

        return toggle;

    }

    // called when the object should be toggled.
    protected override void OnToggle()
    {
        // try to toggle on click.
        TryToggle(true);
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // if this should be checked every update.
        if(checkInUpdate)
        {
            // go to toggle.
            OnToggle();
        }
    }
}
