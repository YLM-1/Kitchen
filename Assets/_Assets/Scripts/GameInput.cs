using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance{get;set;}
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnPauseAction;
    private void Awake()
    {
        Instance=this;


        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();// 启用玩家输入系统

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed+=Pause_performed;
    }
    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed-= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed-=Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)// 回调参数包含本次输入事件的详细信息
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inpurVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inpurVector = inpurVector.normalized;
        return inpurVector;
    }
}
