using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantSoundListenner : MonoBehaviour
{
    
    public PlayerWalkSound playerWalkSound;
    public Vector3 Position => gameObject.transform.position;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        playerWalkSound = GetComponent<PlayerWalkSound>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
