// GENERATED AUTOMATICALLY FROM 'Assets/Input Actions/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""52af3d35-e681-4b26-8279-ba0886e1a05b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e7be6ba4-4c68-48f9-a061-228d8af454ec"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fa8c4c3b-e328-49ec-8bd5-7ec43c8bc582"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""7f3ad788-6493-4474-9283-ee0fcbd818ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""76607c69-7a9b-4cbb-b540-c2ef4e6776ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""adKeys"",
                    ""id"": ""3fbd54e4-14ac-4839-a1ce-b3e3b010d59c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9f4acd85-4432-48b8-8779-7df812f416f2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""af2288d6-3deb-4b03-a678-3de9984c5766"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""arrowKeys"",
                    ""id"": ""9f206cbd-8b59-4a13-a2cd-c5991da3550b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e895f547-a147-419e-ab2f-bf7bf6352123"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c138f373-1cec-40c3-9f8d-3df748509eda"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""leftstick"",
                    ""id"": ""b9d141d1-ff7d-418a-af53-236538431ac8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0c7f69ab-7a65-488f-9158-1ede758a0c6c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""18b569a8-bcb3-408e-9e72-e491993e0e10"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""dpad"",
                    ""id"": ""2af06f16-1b18-48c5-8b9d-3465387920e0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e874f37e-c12b-494b-b1e1-c21687bbbd40"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""39d82d21-ae14-49ab-90f0-30814e3801cd"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""525e45aa-7a8e-4f67-a271-a3343c3bcffd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""505aca61-dc43-4ebb-aa30-3e9970ea7515"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d158d30-3d08-480f-b5b0-32b7e2193727"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20a68b89-36e2-4fac-ba9c-ee2934df1808"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f08e1cb-2fc3-487b-aa41-d9beea7da95c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd6d19a7-2ee7-4a1d-a7a3-425738da9cb0"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""165c6ea4-a06d-47bf-b766-90eefd1ccf67"",
            ""actions"": [
                {
                    ""name"": ""Join"",
                    ""type"": ""Button"",
                    ""id"": ""5f24a8dc-6437-4a3d-8000-a8f57ee98d01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ff810d7b-f380-460e-ad6a-835613981e2f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2090bb65-b600-45b6-b0da-4f5f1be5bf90"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""811c32a5-d6a6-444d-8942-bf7824b8cc21"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Throw = m_Gameplay.FindAction("Throw", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Join = m_Menu.FindAction("Join", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Throw;
    private readonly InputAction m_Gameplay_Dash;
    public struct GameplayActions
    {
        private @PlayerInputActions m_Wrapper;
        public GameplayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Throw => m_Wrapper.m_Gameplay_Throw;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Throw.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThrow;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Join;
    public struct MenuActions
    {
        private @PlayerInputActions m_Wrapper;
        public MenuActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Join => m_Wrapper.m_Menu_Join;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Join.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                @Join.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
                @Join.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoin;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Join.started += instance.OnJoin;
                @Join.performed += instance.OnJoin;
                @Join.canceled += instance.OnJoin;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnJoin(InputAction.CallbackContext context);
    }
}
