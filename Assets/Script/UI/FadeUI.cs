using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoSingleton<FadeUI>
{
    public Image fadeImage;
    
    public float fadeSpeed = 1f;

    public bool shouldFadeToBlack;

    private void Start()
    {
        fadeImage.color = Color.black;
    }

    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.black, fadeSpeed * Time.deltaTime);
        }
        else
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        }
    }
[Button]
    public void ResetFade()
    {
        fadeImage.color = Color.clear;
        shouldFadeToBlack = false;
    }
}
