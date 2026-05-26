using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance{get;private set;}
     private AudioSource audioSource;
    private float volume=0.3f;
    private void Awake()
    {
        audioSource=GetComponent<AudioSource>();
        Instance=this;
    }
     public void ChangeVolume()
    {
        volume+=0.1f;
        if(volume>1f)
        {
            volume=0f;
        }
        audioSource.volume=volume;
    }

    public float GetVolume()
    {
        return  volume;
    }
}
