using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public MyButton playButton;
    public MyButton settingsButton;


    void Start()
    {
        playButton.signalOnClick.AddListener(this.onPlay);
        settingsButton.signalOnClick.AddListener(this.onSettings);

    }

    void onSettings()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void onPlay()
    {
        SceneManager.LoadScene("Level1");
    }
}
