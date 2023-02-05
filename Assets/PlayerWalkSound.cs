using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSound : MonoBehaviour
{
    
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private List<AudioClip> walkSounds;
    // Start is called before the first frame update
    public void Mute(bool isMute)
    {
        audioSource.mute = isMute;
    }

    public void ChangeWalkSound(int index)
    {
        audioSource.clip = walkSounds[index];
        audioSource.Play();
    }
}
