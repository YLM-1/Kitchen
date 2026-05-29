using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCountersSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
   private AudioSource audioSource;
   private float warningSoundTimer;
   private bool playWaringSound;

   private void Awake()
    {
        audioSource=GetComponent<AudioSource>();
    }

    private void Start()
    {
       stoveCounter.OnStateChanged+=StoveCounter_OnStateChanged;
       stoveCounter.OnProgressChanged+=StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender,IHasprogress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount=.5f;
        playWaringSound=e.progressNormalized>burnShowProgressAmount&&stoveCounter.IsFried();
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound=e.state==StoveCounter.E_State.Frying||e.state==StoveCounter.E_State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Update()
    {
        if(playWaringSound)
        {
             warningSoundTimer-=Time.deltaTime;
             if(warningSoundTimer<0f)
             {
                float warningSoundTimerMax=.2f;
                warningSoundTimer=warningSoundTimerMax;

                SoundManager.instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
