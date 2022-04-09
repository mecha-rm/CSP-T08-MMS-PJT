using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Door_SFX_RM1_EXT;
    public AudioSource KeyPad_Pressed;
    //public AudioSource ScrewDriver_SFX;

    public AudioSource Padlock_SFX;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayAudio(AudioSource src)
    {

        src.PlayOneShot(src.clip);


    }
    // Update is called once per frame

}
