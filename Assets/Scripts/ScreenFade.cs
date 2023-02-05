using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeDuration = 2;
    /*[SerializeField] private Color color;*/
 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (fadeIn)
        {
            if (fadeScreen.canvasRenderer.GetAlpha() < 1)
            {
                fadeScreen.canvasRenderer.SetAlpha(fadeScreen.canvasRenderer.GetAlpha()+Time.deltaTime);
                if (fadeScreen.canvasRenderer.GetAlpha() >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {
            Debug.Log("Fadeeeeee");

            if (fadeScreen.canvasRenderer.GetAlpha() >= 0)
            {
                fadeScreen.canvasRenderer.SetAlpha(fadeScreen.canvasRenderer.GetAlpha()-Time.deltaTime);
                if (fadeScreen.canvasRenderer.GetAlpha() == 0)
                {
                    fadeOut = false;
                    /*
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                #1#
                    SceneManager.LoadScene(0);
                }
            }
        }*/
    }

    public static void FadeIn()
    {
        /*
        fadeOut = true; 
    */
    }
    public static void FadeOutScene() 
    {
        /*
        fadeOut = true; 
    */
    }

    /*private IEnumerator FadeRoutine(float alphaIn, float alphaOut) 
    {
        float timer = 0;

        /*Fade the Color#1#
        while (timer <= fadeDuration)
        {
            /*
            Color newColor = color;
            #1#
            fadeScreen.get = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            /*
            rend.material.SetColor("_Color", newColor);
            #1#
            
            timer += Time.deltaTime;
            yield return null;
        }
        /* Makes sure that it exits with the right color#1#
        Color endColor = color;
        endColor.a = alphaOut;
        rend.material.SetColor("_Color", endColor);
    }*/
}
