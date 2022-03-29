using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// an entry into the lock.
public class LockEntry : MonoBehaviour
{
    // the combination lock.
    public CombinationLock comboLock;

    // the index of the combination lock.
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        // tries to find the combination lock.
        if (comboLock == null)
            comboLock = GetComponentInParent<CombinationLock>();
    }

    // on the entry being clocked on.
    private void OnMouseDown()
    {
        // increaes the entry.
        if (comboLock != null)
            comboLock.IncreaseEntryByOne(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
