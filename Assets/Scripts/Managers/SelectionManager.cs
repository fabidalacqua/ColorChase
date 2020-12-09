using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class SelectionManager : MonoBehaviour
{
    [Header("Input Actions")]

    [SerializeField]
    private InputAction _joinAction = null;

    [SerializeField]
    private InputAction _startAction = null;

    [SerializeField]
    private InputAction _mainMenuAction = null;

    [Header("Action Events")]

    [SerializeField]
    private UnityEvent _onStart;

    [SerializeField]
    private UnityEvent _onMainMenu;

    [Header("UI Game Objects")]

    [SerializeField]
    private CharacterSelector[] _characterSelectors;

    [SerializeField]
    private GameObject _startInfo;

    private List<PlayerInput> _playersInput;

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playersInput = new List<PlayerInput>();
    }

    private void PlayerJoin(InputAction.CallbackContext obj)
    {
        _playerInputManager.JoinPlayerFromActionIfNotAlreadyJoined(obj);
    }

    private void ReturnToMainMenu(InputAction.CallbackContext obj)
    {
        // Disconect all players

        if (_onMainMenu != null)
            _onMainMenu.Invoke();
    }

    private void StartGame(InputAction.CallbackContext obj)
    {
        // Save players infos
        SetPlayerPrefs();

        // Disconect all players

        if (_onStart != null)
            _onStart.Invoke();
    }

    private void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("number_players", _playersInput.Count);
        for (int i = 0; i < _playersInput.Count; i++)
        {
            PlayerPrefs.SetString("player_" + i + "_device", _playersInput[i].devices[0].layout);
            PlayerPrefs.SetString("player_" + i + "_controlScheme", _playersInput[i].currentControlScheme);
            // Get choosen character
            var controller = _playersInput[i].GetComponent<PlayerSelectionController>();
            PlayerPrefs.SetInt("player_" + i + "_character", controller.Character);
        }
    }

    private void OnEnable()
    {
        // Enable joining at player input manager
        _playerInputManager.EnableJoining();

        // Enable join action and subscribe to event
        _joinAction.Enable();
        _joinAction.started += PlayerJoin;

        // Enable main menu action and subscribe to event
        _mainMenuAction.Enable();
        _mainMenuAction.started += ReturnToMainMenu;
    }

    private void OnDisable()
    {
        // Disable join action and unsubscribe to event
        _joinAction.Disable();
        _joinAction.started -= PlayerJoin;

        // Disable main menu action and unsubscribe to event
        _mainMenuAction.Disable();
        _mainMenuAction.started -= ReturnToMainMenu;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        bool hasSelector = false;
        for (int i = 0; !hasSelector && i < _characterSelectors.Length; i++)
        {
            if (_characterSelectors[i].Available)
            {
                playerInput.GetComponent<PlayerSelectionController>().Join(_characterSelectors[i]);
                
                _playersInput.Add(playerInput);

                hasSelector = true;
            }
        }

        // If have at least 2 players, allow the game to start
        /*if (_playersInput.Count >= 2)
        {*/
            _startInfo.SetActive(true);
            _startAction.Enable();
            _startAction.started += StartGame;
        /*}*/
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        // Remove player input from list
        playerInput.GetComponent<PlayerSelectionController>().Leave();
        _playersInput.Remove(playerInput);

        // If not have at least 2 players, not allow the game to start
        if (_playersInput.Count < 2)
        {
            _startInfo.SetActive(false);
            _startAction.Disable();
            _startAction.started -= StartGame;
        }
    }
}