using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputMgr : MonoSingleton<InputMgr>
{
    public Vector2 moveCamVector;

    private PlayerInput playerInput;
    private InputAction moveCameraAction;
    private InputAction touchAction;
    private InputAction touchPositionAction;

    private bool isInitInput = false;

    #region Basic
    public IEnumerator IE_Init()
    {
        InitInput();
        yield break;
    }

    private void InitInput()
    {
        if (!isInitInput)
        {
            playerInput = new PlayerInput();
            moveCameraAction = playerInput.Gameplay.MoveCamera;
            touchAction = playerInput.Gameplay.Touch;
            touchPositionAction = playerInput.Gameplay.TouchPosition;
            isInitInput = true;
        }
    }

    private void EnableInput()
    {
        if(playerInput == null)
        {
            InitInput();
        }

        playerInput.Enable();
        moveCameraAction.performed += MoveCamera_performed;
        touchAction.performed += Touch_performed;
    }

    private void DisableInput()
    {
        moveCameraAction.performed -= MoveCamera_performed;
        touchAction.performed -= Touch_performed;
        playerInput.Disable();
    }

    private void OnEnable()
    {
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }
    #endregion


    #region Bind
    private void MoveCamera_performed(InputAction.CallbackContext obj)
    {
        Vector2 valueMove = obj.ReadValue<Vector2>();
        moveCamVector = valueMove;
    }

    private void Touch_performed(InputAction.CallbackContext obj)
    {
        if (CheckWhetherHitUnit())
        {
            return;
        }

        if (CheckWhetherHitMap())
        {
            return;
        }

        Debug.Log("NoHit");
    }

    #endregion
}
