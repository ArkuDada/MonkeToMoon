using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
    
        SceneManager.LoadScene(1);
    }
public GameObject credits;
    public void Credits()
    {
        credits.SetActive(true);
    }
    public void CloseCredits()
    {
        credits.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }void Start()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (var obj in objects)
        {
            if (obj.hideFlags == HideFlags.DontSave)
            {
                Destroy(obj);
            }
        }
    }
}
