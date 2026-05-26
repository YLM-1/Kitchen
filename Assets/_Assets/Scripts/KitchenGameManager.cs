using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
   public static KitchenGameManager Instance{get;private set;}
   public event EventHandler OnStateChange;
   public event EventHandler OnGamePaused;
   public event EventHandler OnGameUnPaused;
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
    private float gamePlayingTimer;
    private float gamePlayingTimerMax=30f;
    private bool isGamePaused=false;

    private void Awake()
    {
        state = E_State.WaitingToStart;
        Instance=this;//必须实例当前对象
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender,System.EventArgs e)
    {
        TogglePauseGame();
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
                    gamePlayingTimer=gamePlayingTimerMax;
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
        // Debug.Log(state);
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

    public bool IsGameOver()
    {
        return state==E_State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1-gamePlayingTimer/gamePlayingTimerMax;
    }

    public void TogglePauseGame()
    {
        isGamePaused=!isGamePaused;
        if(isGamePaused)
        {
           Time.timeScale=0f;
           OnGamePaused?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Time.timeScale=1f;
            OnGameUnPaused?.Invoke(this,EventArgs.Empty);
        }
    }
}
