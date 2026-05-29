using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance{get;set;}

    private const string PLAYER_PREFS_BINDINGS="InputBindings";
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public event EventHandler OnBindingRebind;

    public enum E_Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause
    }
    private void Awake()
    {
        Instance=this;


        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
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
        // Debug.Log(GetBindingText(E_Binding.Move_Up));
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
    public string GetBindingText(E_Binding binding)
    {
        switch (binding)
        {
            default:
            case E_Binding.Move_Up:
                 return playerInputActions.Player.Move.bindings[1].ToDisplayString(); 
            case E_Binding.Move_Down:
                 return playerInputActions.Player.Move.bindings[2].ToDisplayString(); 
            case E_Binding.Move_Left:
                 return playerInputActions.Player.Move.bindings[3].ToDisplayString(); 
            case E_Binding.Move_Right:
                 return playerInputActions.Player.Move.bindings[4].ToDisplayString(); 
            case E_Binding.Interact:
                 return playerInputActions.Player.Interact.bindings[0].ToDisplayString(); 
            case E_Binding.InteractAlternate:
                 return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString(); 
            case E_Binding.Pause:
                 return playerInputActions.Player.Pause.bindings[0].ToDisplayString(); 
        }
    }
    public void RebindBinding(E_Binding binding,Action onActionRebound)
    {
        //先禁用新输入系统
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case E_Binding.Move_Up:
                 inputAction=playerInputActions.Player.Move;
                 bindingIndex=1;
                 break;
            case E_Binding.Move_Down:
                 inputAction=playerInputActions.Player.Move;
                 bindingIndex=2;
                 break;
            case E_Binding.Move_Left:
                 inputAction=playerInputActions.Player.Move;
                 bindingIndex=3;
                 break;
            case E_Binding.Move_Right:
                 inputAction=playerInputActions.Player.Move;
                 bindingIndex=4;
                 break;
            case E_Binding.Interact:
                 inputAction=playerInputActions.Player.Interact;
                 bindingIndex=0;
                 break;     
            case E_Binding.InteractAlternate:
                 inputAction=playerInputActions.Player.InteractAlternate;
                 bindingIndex=0;
                 break;
            case E_Binding.Pause:
                 inputAction=playerInputActions.Player.Pause;
                 bindingIndex=0;
                 break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
           callback.Dispose();
           
           playerInputActions.Player.Enable(); 

           onActionRebound();
           PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,playerInputActions.SaveBindingOverridesAsJson());
           PlayerPrefs.Save();

           OnBindingRebind?.Invoke(this,EventArgs.Empty);
        })
        .Start();

    }
}
