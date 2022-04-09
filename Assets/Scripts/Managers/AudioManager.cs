using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an event system for audio components in the game. This is primarily for sound effects.
// unlike with the other managers, there are multiples of these.
public class AudioManager : MonoBehaviour
{
    // public AudioSource Door_SFX_RM1_EXT;

    // the list of audio sources.
    [Tooltip("The list of audio sources for the manager.")]
    public List<AudioSource> sources = new List<AudioSource>();

    // the list of audio clips.
    [Tooltip("A list of clips to pull from. This is only needed for playing clips by index. A clip does not need to be in this list for it to be used.")]
    public List<AudioClip> clips = new List<AudioClip>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // grabs the child components.
        if (sources.Count == 0)
        {
            // new list
            List<AudioSource> temp = new List<AudioSource>();

            // grabs the children.
            GetComponentsInChildren<AudioSource>(temp);

            // adds the range.
            sources.AddRange(temp);

        }
    }

    // returns audio source 0. Does not do a null check.
    public AudioSource Source0
    {
        get
        {
            if (sources.Count > 0)
                return sources[0];
            else
                return null;
        }
    }

    // returns audio clip 0.
    public AudioClip Clip0
    {
        get
        {
            if (clips.Count > 0)
                return clips[0];
            else
                return null;
        }
    }

    // plays the audio source with the clip.
    public bool PlayAudio(AudioSource source, AudioClip clip, bool playOneShot = true)
    {
        // one of these were not found.
        if (source == null || clip == null)
            return false;

        // checks if one shot clip should be played.
        if (playOneShot)
        {
            source.PlayOneShot(clip);
        }
        else // play the new audio.
        {
            source.Stop();
            source.clip = clip;
            source.Play();
        }

        return true;
    }

    // plays the audio clip with source 0.
    public bool PlayAudio(AudioClip clip, bool playOneShot = true)
    {

        return PlayAudio(Source0, clip, playOneShot);
    }

    // plays an audio clip. By default, audio source 0 is used.
    // if 'playOneShot' is used, then PlayOneShot() is used instead of the regular play function.
    public bool PlayAudio(int sourceIndex, int clipIndex, bool playOneShot = true)
    {
        // clip out of bounds.
        if (clipIndex < 0 || clipIndex >= clips.Count)
        {
            Debug.LogError("Clip not available.");
            return false;
        }

        // source out of bounds.
        if (sourceIndex < 0 || sourceIndex >= sources.Count)
        {
            Debug.LogError("Source not available.");
            return false;
        }

        // grabs the source and the clip.
        AudioSource source = sources[sourceIndex];
        AudioClip clip = clips[clipIndex];

        // plays the audio.
        return PlayAudio(source, clip, playOneShot);
    }

    // plays the audio clip with source 0.
    public bool PlayAudio(int clipIndex, bool playOneShot = true)
    {
        // plays the audio.
        return PlayAudio(0, clipIndex, playOneShot);
    }

}
