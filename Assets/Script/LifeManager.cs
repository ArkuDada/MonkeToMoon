using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoSingleton<LifeManager>
{
    public Transform spawnPoint;
    public GameObject deadBody;
     public int birthYear;
     public int Age => em.year - birthYear;
     public float AgeFloat => em.year - birthYear ;
     public EraManager em => EraManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AgeCheck()
    {
        if (Age > em.CurrentEraData.lifeSpan)
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        birthYear = em.year;
        var body = Instantiate(deadBody, PlayerController.Instance.transform.position, Quaternion.identity);
        EraManager.Instance.freezeTime = true;
        yield return new WaitForSeconds(2);
        Destroy(body);
        PlayerController.Instance.transform.position = spawnPoint.position;
        PlayerController.Instance.gameObject.SetActive(true);
        EraManager.Instance.freezeTime = false;
        
    }
}
