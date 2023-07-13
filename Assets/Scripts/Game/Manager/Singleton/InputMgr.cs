using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InputMgr : MonoSingleton<InputMgr>
{
    public Vector2 camMoveVector;
    public float camRotateValue;

    private PlayerInput playerInput;
    private InputAction camMoveAction;
    private InputAction camRotateAction;
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
            camMoveAction = playerInput.Gameplay.MoveCamera;
            camRotateAction = playerInput.Gameplay.RotateCamera;
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
        camMoveAction.performed += CamMove_performed;
        camRotateAction.performed += CamRotate_performed;
        touchAction.performed += Touch_performed;
    }



    private void DisableInput()
    {
        camMoveAction.performed -= CamMove_performed;
        camRotateAction.performed -= CamRotate_performed;
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
    private void CamMove_performed(InputAction.CallbackContext obj)
    {
        Vector2 valueMove = obj.ReadValue<Vector2>();
        camMoveVector = valueMove;
    }
    private void CamRotate_performed(InputAction.CallbackContext obj)
    {
        float valueRotate = obj.ReadValue<float>();
        camRotateValue = valueRotate;
    }

    private void Touch_performed(InputAction.CallbackContext obj)
    {
        CheckClickAction();
    }

    #endregion
}
