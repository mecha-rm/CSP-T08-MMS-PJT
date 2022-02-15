using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the manager for the TitleScene.
public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // starts the game scene.
    public void StartGame()
    {
        SceneHelper.ChangeScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
