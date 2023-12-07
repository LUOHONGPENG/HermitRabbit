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
    private InputAction cancelAction;
    private InputAction touchPositionAction;
    private InputAction detailAction;

    private bool isInitInput = false;

    public bool isPressDetail = false;

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
            cancelAction = playerInput.Gameplay.Cancel;
            touchPositionAction = playerInput.Gameplay.TouchPosition;
            detailAction = playerInput.Gameplay.Detail;
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
        cancelAction.performed += Cancel_performed;
        detailAction.performed += Detail_performed;
        detailAction.canceled += Detail_canceled;
    }

    private void Detail_canceled(InputAction.CallbackContext context)
    {
        isPressDetail = false;
    }

    private void Detail_performed(InputAction.CallbackContext context)
    {
        isPressDetail = true;
    }

    private void DisableInput()
    {
        camMoveAction.performed -= CamMove_performed;
        camRotateAction.performed -= CamRotate_performed;
        touchAction.performed -= Touch_performed;
        cancelAction.performed -= Cancel_performed;
        detailAction.performed -= Detail_performed;
        detailAction.canceled -= Detail_canceled;

        if (playerInput != null)
        {
            playerInput.Disable();
        }
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

    private void Cancel_performed(InputAction.CallbackContext obj)
    {
        CheckCancelAction();
    }


    #endregion
}
