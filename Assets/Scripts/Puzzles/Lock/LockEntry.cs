using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an entry into the lock.
public class LockEntry : MonoBehaviour
{
    // the combination lock.
    public CombinationLock comboLock;

    // Start is called before the first frame update
    void Start()
    {
        // tries to find the combination lock.
        if (comboLock == null)
            comboLock = GetComponentInParent<CombinationLock>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
