using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    public static bool gameStarted = false;
    [SerializeField] private Animator camAnim;
    [SerializeField] private Animator holeAnim;

    public void StartGame()
    {
        startButton.SetActive(false);
        quitButton.SetActive(false);


        AudioManager.PlayStartAudio();
        //SceneManager.LoadScene(1);
        camAnim.SetBool("gameStarted1", true);
        holeAnim.SetBool("gameStarted", true);


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
