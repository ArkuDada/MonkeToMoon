using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        endSceneObjects[(int)EraManager.Instance.VictoryEra].SetActive(true);

        StartCoroutine(PlayEndSequence());
    }
    
    IEnumerator PlayEndSequence()
    {
        yield return new WaitForSeconds(1);
        CameraAnimator.enabled = true;
        CameraAnimator.Play("endCamera");
        yield return new WaitForSeconds(10);
        Destroy(InventoryManager.Instance.gameObject);
        Destroy(EraManager.Instance.gameObject);
        SceneManager.LoadScene("TitleScreen");

    }
}
