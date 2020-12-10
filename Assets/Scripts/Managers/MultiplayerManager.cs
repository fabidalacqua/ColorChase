using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using PlayerUI;
using Player;

[RequireComponent(typeof(PlayerInputManager))]
public class MultiplayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playersPrefabs;

    [SerializeField]
    private Transform[] _spawnPoints;

    [SerializeField]
    private PlayerHUD[] _playersHUD;

    [SerializeField]
    private PlayerFollower[] _playersFollowers;

    [SerializeField]
    private PlayerScore[] _playersScores;

    [HideInInspector]
    public UnityEvent onLastPlayerStanding = new UnityEvent();

    private PlayerInputManager _playerInputManager;

    private int _playersCount = 0;

    private List<PlayerInput> _playersInputs;

    private bool[] _alivePlayers = new bool[4];

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playersInputs = new List<PlayerInput>();
    }

    private void Start()
    {
        // Instantiate all joined players
        InstantiatePlayersWithSetDevices();
    }

    private void PlayerDied(int playerIndex)
    {
        _playersCount--;
        _alivePlayers[playerIndex] = false;

        if (_playersCount == 1 && onLastPlayerStanding != null)
            onLastPlayerStanding.Invoke();
    }

    public GameObject GetRoundWinner()
    {
        GameObject winner = null;
        for (int i = 0; i < _alivePlayers.Length; i++)
        {
            if (_alivePlayers[i])
                winner = _playersInputs[i].gameObject;
        }
        return winner;
    }

    public int GetGameWinner(out GameObject winner)
    {
        PlayerInput playerInput = null;
        int victories, maxVictories = -1;

        // Check who won or if need a tiebreaker
        foreach (PlayerInput p in _playersInputs)
        {
            victories = p.GetComponent<PlayerController>().VictoriesCount;
            if (victories > maxVictories)
            {
                // Set winner 
                maxVictories = victories;
                playerInput = p;
            }
        }

        winner = playerInput.gameObject;

        return playerInput.playerIndex;
    }

    public void ActivatePlayers()
    {
        _playersCount = 0;
        foreach (PlayerInput p in _playersInputs)
        {
            // Activate player
            p.gameObject.SetActive(true);
            p.ActivateInput();
            _playersCount++;
            _alivePlayers[p.playerIndex] = true;
        }
    }

    public void DeactivatePlayers()
    {
        foreach (PlayerInput p in _playersInputs)
        {
            // Deactivate player
            DeactivatePlayer(p);
        }
    }

    private void DeactivatePlayer(PlayerInput playerInput)
    {
        // Deactivate input and game object
        playerInput.gameObject.SetActive(false);
        playerInput.DeactivateInput();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        // Set parent for player gameobject 
        playerInput.transform.SetParent(transform);
        // Player index
        int index = playerInput.playerIndex;
        // Place player in position
        playerInput.gameObject.transform.position = _spawnPoints[index].position;
        // Restart player health and item
        playerInput.gameObject.GetComponent<PlayerController>().Restart();
        // Setup player's UI
        _playersHUD[index].Setup(playerInput.gameObject);
        _playersFollowers[index].Setup(playerInput.gameObject);
        _playersScores[index].Setup(playerInput.gameObject);
    }

    private void InstantiatePlayersWithSetDevices()
    {
        // Get the number of players joined in selection screen
        int playersCount = PlayerPrefs.GetInt("players_count");
        string devicePath = null;
        string controlScheme = null;

        for (int i = 0; i < playersCount; i++)
        {
            devicePath = PlayerPrefs.GetString("player_" + i + "_device", null);
            controlScheme = PlayerPrefs.GetString("player_" + i + "_controlScheme", null);

            if (devicePath != null && controlScheme != null)
            {
                // Get character prefab for player
                int character = PlayerPrefs.GetInt("player_" + i + "_character");
                _playerInputManager.playerPrefab = _playersPrefabs[character];

                // Instantiate player with device
                PlayerInput playerInput = _playerInputManager.JoinPlayer(i,
                    pairWithDevice: InputSystem.GetDevice(devicePath), controlScheme: controlScheme);
                // Add to playerInput list
                _playersInputs.Add(playerInput);

                DeactivatePlayer(playerInput);

                SetOnDiedEvent(playerInput);

                _playersCount++;
            }
        }
    }

    private void SetOnDiedEvent(PlayerInput playerInput)
    {
        PlayerHealth health = playerInput.gameObject.GetComponentInChildren<PlayerHealth>();
        // Set player index
        health.playerIndex = playerInput.playerIndex;
        // Add listener for player death
        health.onDied.AddListener(PlayerDied);
    }
}