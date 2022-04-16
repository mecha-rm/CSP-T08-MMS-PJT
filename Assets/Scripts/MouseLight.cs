using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// a light that tracks around the mouse.
public class MouseLight : MonoBehaviour
{ 
    // a mouse object for getting the mouse position.
    public Mouse mouse;

    // the post-processing volume for the cene.
    public PostProcessVolume postProcessVolume;

    // TODO: doesn't seem like this stopped the lag, so it isn't really used.
    // - reducing the frame rate seemed to fix the lag, but the project could probably be optimized more.
    // the update rate for the mouse light in seconds.
    public float updateRate = 0.0F;

    // the update timer for updating the light position.
    private float updateTimer = 0.0F;

    // PREDICTION //
    // if set to 'true' the code will try to predict the light position.
    public bool usePrediction = true;

    // the old view pos of the mouse.
    private Vector2 mouseVP0;

    // the newest view pos of the mouse.
    private Vector2 mouseVP1;

    // // the factor for moving with prediction.
    // private float moveFactor = 0.0F;

    // EFFECT //

    // the post processing effect from the profile being used.
    private Vignette effect;

    // the default center value.
    private Vector2 effectCenterDefault;

    // if 'true', the effect is altered by the mouse.
    public bool alterEffect = true;

    // TODO: this is null when re-entering the scene from GameScene > EndScene > TitleScene. Fix it.
    // the light that follows the mouse cursor.
    // public Light mouseLight;

    // Start is called before the first frame update
    void Start()
    {
        // finds the mouse script.
        if (mouse == null)
            mouse = FindObjectOfType<Mouse>();

        // tries to find the post processing volume.
        // NOTE: this shouldn't be used anymore since now the high contrast has its own post processing.
        if (postProcessVolume == null)
        {
            postProcessVolume = FindObjectOfType<PostProcessVolume>();
        }

        // gets the vingette settings.
        if(postProcessVolume != null)
            effect = postProcessVolume.sharedProfile.GetSetting<Vignette>();

        // override the value through code.
        if (effect != null)
        {
            // center.overrideState is what enables the element.
            // it's effectively the same as enabled or disabling the component.
            effect.center.overrideState = true;
            effectCenterDefault = effect.center.value; // saves default.
        }


        // // light component not set.
        // if(mouseLight == null)
        // {
        //     // finds the mouse light.
        //     mouseLight = GetComponent<Light>();
        // 
        //     // gets component from children.
        //     if (mouseLight == null)
        //         mouseLight = GetComponentInChildren<Light>();
        // }

        // gets the mouse's current position and saves it for prediction.
        // both are the same.
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseVP0 = new Vector2(viewPos.x, viewPos.y);
        mouseVP1 = mouseVP0;
    }

    // light is not enabled.
    public bool IsLightEnabled()
    {
        return alterEffect;
    }

    // sets whethr or not the light should be enabled.
    public void SetLightEnabled(bool e)
    {
        alterEffect = e;


        // re-centre the effect?
        // if (alterEffect == false)
        //     effect.center.value = new Vector2(0.5F, 0.5F);
            
    }

    // enables the light.
    public void EnableLight()
    {
        SetLightEnabled(true);
    }

    // disables the light.
    public void DisableLight()
    {
        SetLightEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        // updates the timer.
        updateTimer += Time.deltaTime;

        // if the effect should be altered, and the alloted time has passed.
        if(alterEffect && updateTimer >= updateRate)
        {
            // TODO: maybe check to see if the mouse has actually moved for more efficiency.
            // gets the viewpoint position ([0, 1] range)
            Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            
            // gets the view pos as a 2D vector.
            Vector2 viewPos2d = new Vector2(viewPos.x, viewPos.y);

            // only update if the mouse position has actually changed.
            if(effect.center.value != viewPos2d)
            {
                // move the vingette
                effect.center.value = new Vector2(viewPos.x, viewPos.y);
                // Debug.Log(effect.center.value.ToString());

                // reset the timer, and update the new positions.
                updateTimer = 0.0F;
                mouseVP0 = mouseVP1;
                mouseVP1 = viewPos;
            }
        }
        // not time to update to the exact position, and use prediction.
        // if the mouse hasn't moved, then don't update.
        else if (usePrediction && mouseVP0 != mouseVP1)
        {
            // gets the vector between the two mouse points.
            Vector2 v = mouseVP1 - mouseVP0;

            // forms a new position going at the provided speed.
            Vector2 newPos = v.normalized * (v.magnitude) * Time.deltaTime;

            // alter the effect.
            effect.center.value += newPos;
        }
        
    }

    // OnDestroy is called when the object is being destroyed.
    private void OnDestroy()
    {
        // return to default value.
        if (effect != null)
            effect.center.value = effectCenterDefault; 
    }

    // Sent to all objects before the apllication quits.
    private void OnApplicationQuit()
    {
        // return to default value.
        if (effect != null)
            effect.center.value = effectCenterDefault;
    }
}
