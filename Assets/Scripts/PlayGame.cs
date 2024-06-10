using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayGame : MonoBehaviour
{
    // Update is called once per frame
    public void PlayGameScene()
    {
        SCManager.instance.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
