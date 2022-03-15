using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the combination lock.
public class CombinationLock : MonoBehaviour
{
    // the combination lock entries (four total, but the value can vary).
    public List<int> entries = new List<int>(4);

    // the passcode for the combination lock.
    public string passcode = "1264";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // sets the entry.
    public void SetEntry(int index, int value)
    {
        // index is valid.
        if (index >= 0 && index < entries.Count)
        {
            // adjusts the entry.
            entries[index] = value; // sets new value.
            entries[index] = Mathf.Abs(entries[index]); // positive only
            entries[index] = entries[index] % 9; // range of [0, 9]
        }
    }

    // changes an entry by a given amount. Only [0, 9] are allowed.
    public void AddToEntry(int index, int amount)
    {
        // index is valid.
        if(index >= 0 && index < entries.Count)
        {
            // set the entry.
            SetEntry(index, entries[index] + amount);
        }
    }

    // increases the entry.
    public void IncreaseEntryByOne(int index)
    {
        AddToEntry(index, 1);
    }

    // decreases the entry.
    public void DecreaseEntryByOne(int index)
    {
        AddToEntry(index, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
