using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // starts the game scene.
    public void ReturnToTitle()
    {
        SceneHelper.ChangeScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
