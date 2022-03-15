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

    // TODO: doesn't seem like this stopped the lag.
    // the update rate for the mouse light in seconds.
    public float updateRate = 0.0F;

    // the update timer for updating the light position.
    private float updateTimer = 0.0F;

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
        if (postProcessVolume == null)
        {
            postProcessVolume = FindObjectOfType<PostProcessVolume>(true);
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
            // original
            // // updates to mouse world position.
            // Vector3 newNormal = Rotation.NormalTowardsMouse3D(transform.position, Camera.main);
            // 
            // // sets new forward.
            // transform.forward = newNormal;

            // TODO: maybe check to see if the mouse has actually moved.
            // gets the viewpoint position ([0, 1] range)
            Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            // move the vingette
            // Vector2Parameter v2p = new Vector2Parameter();
            effect.center.value = new Vector2(viewPos.x, viewPos.y);
            // Debug.Log(effect.center.value.ToString());

            updateTimer = 0.0F;
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
