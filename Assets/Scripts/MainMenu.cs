using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    public static bool gameStarted = false;

    public void StartGame()
    {
        startButton.SetActive(false);
        quitButton.SetActive(false);
        
        SceneManager.LoadScene(1);
        

        Debug.Log("Hellllo");
        gameStarted = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
