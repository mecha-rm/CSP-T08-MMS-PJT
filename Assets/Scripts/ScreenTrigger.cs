using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// causes an object to trigger a screen upon being hit with a ray.
// this is made for going forward a screen without the screen object holding the collider.
public class ScreenTrigger : MonoBehaviour
{
    // the screen that the screen trigger transitons to.
    public RoomScreen screen = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // sets the next screen.
    public void SetScreen(Screen currScreen)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
