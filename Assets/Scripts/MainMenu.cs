using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    public static bool gameStarted = false;

    private void Start()
    {
        
    }

    public void StartGame()
    {
        gameObject.SetActive(false);

        AudioManager.PlayStartAudio();

        //SceneManager.LoadScene(1);
        
        gameStarted = true;

        PlayerPrefs.SetInt("SkipIntro", 0);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
