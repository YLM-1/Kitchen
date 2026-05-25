using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
   public static KitchenGameManager Instance{get;private set;}

   public event EventHandler OnStateChange;
   private enum E_State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private E_State state;
    private float waitingToStartTimer=1f;
    private float countdownToStartTimer=3f;
    private float gamePlayingTimer=10f;

    private void Awake()
    {
        state = E_State.WaitingToStart;
        Instance=this;//必须实例当前对象
    }

    private void Update()
    {
        switch(state)
        {
            case E_State.WaitingToStart:
                 waitingToStartTimer-=Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state=E_State.CountdownToStart;
                    OnStateChange?.Invoke(this,new EventArgs());
                }
                break;
            case E_State.CountdownToStart:
                 countdownToStartTimer-=Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state=E_State.GamePlaying;
                    OnStateChange?.Invoke(this,new EventArgs());
                }
                break; 
            case E_State.GamePlaying:
                 gamePlayingTimer-=Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state=E_State.GameOver;
                    OnStateChange?.Invoke(this,new EventArgs());
                }
                break;  
            case E_State.GameOver:
                break;                  
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state==E_State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state==E_State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
}
