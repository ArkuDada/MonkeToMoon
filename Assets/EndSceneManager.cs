using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private Animator CameraAnimator;
   [SerializeField] private List<GameObject> endSceneObjects;
    void Start()
    {
        FadeUI.Instance.shouldFadeToBlack = false;

        foreach(var e in endSceneObjects)
        {
            e.SetActive(false);
        }   
        
        endSceneObjects[(int)GameManager.Instance.VictoryEra].SetActive(true);

        StartCoroutine(PlayEndSequence());
    }
    
    IEnumerator PlayEndSequence()
    {
        yield return new WaitForSeconds(1);
        CameraAnimator.SetTrigger("play");
    }
}
