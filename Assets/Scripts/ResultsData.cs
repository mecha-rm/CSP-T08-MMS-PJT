using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the end game data for the results screen.
public class ResultsData : MonoBehaviour
{
    // the time it took for the player to beat the game.
    public float completionTime = 0.0F;

    // if 'true', the player got the easter egg certificate.
    public bool gotCertificate = false;

    // Start is called before the first frame update
    void Start()
    {
        // the end game data will be carried over to another scene.
        // this should be destroyed once the end screen grabs it.
        DontDestroyOnLoad(gameObject);
    }
}
