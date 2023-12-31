//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Common/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""07242882-c159-44dd-ac39-9d94448e10c0"",
            ""actions"": [
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f4d1a726-9b0c-42ba-9636-790fa6060383"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""48a13446-6fa3-4eca-9531-4eca0f62a37e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Touch"",
                    ""type"": ""Button"",
                    ""id"": ""4f707ef1-0536-422d-b80a-987063d951cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""d18fa373-2a34-46c2-af87-b9f116dfbcbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""Value"",
                    ""id"": ""280f61b6-cec6-4fac-9df5-49c6ec1fbbfe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Detail"",
                    ""type"": ""Button"",
                    ""id"": ""583732eb-84f0-48d0-9d90-ddf9e57e2f6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""4f077195-7425-4e89-b41c-586d97b3b27b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""af5e7ac4-d1c9-4d72-8a38-f73a8250325a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4178a2bf-0cbc-48e7-a994-5562a116b80a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6f811258-69c9-46a0-ab6d-c4feb707ad54"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ec3fcaf5-0b06-42a7-bb86-6ed4bbec8cf3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c1159b35-e511-452f-a888-8ff89d112bf2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ca0343-6f2c-4ad9-ac2e-41d416e9414c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""3a4a50f1-1a5d-4278-9a08-e3f54742208a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c4937011-521a-486f-872c-6ffd7e83ff54"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ad0fba53-b7f4-4cf3-b5ee-74f05ca24e65"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d270cb11-7fb4-4546-b275-3a16461a0a91"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5abfd57e-5403-4e06-bc0c-164e127551c5"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Detail"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveCamera = m_Gameplay.FindAction("MoveCamera", throwIfNotFound: true);
        m_Gameplay_RotateCamera = m_Gameplay.FindAction("RotateCamera", throwIfNotFound: true);
        m_Gameplay_Touch = m_Gameplay.FindAction("Touch", throwIfNotFound: true);
        m_Gameplay_Cancel = m_Gameplay.FindAction("Cancel", throwIfNotFound: true);
        m_Gameplay_TouchPosition = m_Gameplay.FindAction("TouchPosition", throwIfNotFound: true);
        m_Gameplay_Detail = m_Gameplay.FindAction("Detail", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MoveCamera;
    private readonly InputAction m_Gameplay_RotateCamera;
    private readonly InputAction m_Gameplay_Touch;
    private readonly InputAction m_Gameplay_Cancel;
    private readonly InputAction m_Gameplay_TouchPosition;
    private readonly InputAction m_Gameplay_Detail;
    public struct GameplayActions
    {
        private @PlayerInput m_Wrapper;
        public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCamera => m_Wrapper.m_Gameplay_MoveCamera;
        public InputAction @RotateCamera => m_Wrapper.m_Gameplay_RotateCamera;
        public InputAction @Touch => m_Wrapper.m_Gameplay_Touch;
        public InputAction @Cancel => m_Wrapper.m_Gameplay_Cancel;
        public InputAction @TouchPosition => m_Wrapper.m_Gameplay_TouchPosition;
        public InputAction @Detail => m_Wrapper.m_Gameplay_Detail;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MoveCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCamera;
                @RotateCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCamera;
                @Touch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouch;
                @Touch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouch;
                @Touch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouch;
                @Cancel.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancel;
                @TouchPosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTouchPosition;
                @Detail.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDetail;
                @Detail.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDetail;
                @Detail.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDetail;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @Touch.started += instance.OnTouch;
                @Touch.performed += instance.OnTouch;
                @Touch.canceled += instance.OnTouch;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @Detail.started += instance.OnDetail;
                @Detail.performed += instance.OnDetail;
                @Detail.canceled += instance.OnDetail;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnTouch(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnDetail(InputAction.CallbackContext context);
    }
}
