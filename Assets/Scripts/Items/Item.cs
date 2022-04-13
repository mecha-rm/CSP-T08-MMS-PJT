using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an item in the game world. Items are collected my checking the mouse script in the GameplayManager.
public class Item : MonoBehaviour
{
    // the item's descriptor.
    public Descriptor descriptor;

    // the ID for stacking the item for the UI display.
    // TODO: maybe hide this from the inspector?
    [Tooltip("Items with the same ID are stacked together for the inventory display. Blank IDs do not stack with anything.")]
    public string itemId = "";

    // the icon for this item to be used in the user interface.
    // the items CANNOT be destroyed, otherwise the icons are lost.
    // TODO: see if there's a more efficient way to do this.
    public Sprite itemIcon;

    [Header("Audio")]

    // the audio manager used to play the clip.
    public AudioManager audioManager;

    // the audio clip for using this item.
    public AudioClip audioClip;

    // if an audio clip is set, use it.
    [Tooltip("Plays the saved audio clip when the item is being used.")]
    public bool playAudio = true;

    // ID names.

    // item IDs
    // puzzle piece
    public const string PUZZLE_PIECE_ID = "puzzle-piece";

    // key id.
    public const string KEY_ID = "key";

    // note id.
    public const string NOTE_ID = "note";
    
    // treasure
    public const string TREASURE_ID = "treasure";

    // certificate
    public const string CERTIFICATE_ID = "certificate";


    // Start is called before the first frame update
    protected virtual void Start()
    {
        // description not set.
        if (descriptor == null)
        {
            // try to get the component.
            if (!TryGetComponent<Descriptor>(out descriptor))
            {
                // adds the description component.
                descriptor = gameObject.AddComponent<Descriptor>();
            }
        }

        // grabs the audio manager.
        if (audioManager == null)
            audioManager = GameplayManager.Current.audioManager;
    }

    // called when the item is put into the player's inventory.
    public virtual void OnItemGet()
    {
        // ...
    }

    // call this when the item is being used.
    public virtual void OnItemUse()
    {
        // plays the audio clip when the item is taken.
        if (playAudio && audioClip != null)
            audioManager.PlayAudio(audioClip);
    }

    // call this when the item is being taken from the player.
    public virtual void OnItemLose()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
