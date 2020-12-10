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

    public GameObject Winner {get; private set; }

    private PlayerInputManager _playerInputManager;

    private int _numberPlayers = 0;

    private int _playersCount = 0;

    private List<PlayerInput> _playersInputs;

    private bool[] _alivePlayers;

    private List<int> _tiebreakerPlayerIndex;

    private bool _tie = false;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playersInputs = new List<PlayerInput>();
        _alivePlayers = new bool[] { true, true, true, true };
        _tiebreakerPlayerIndex = new List<int>();
    }

    private void Start()
    {
        // Instantiate all joined players
        InstantiatePlayersWithSetDevices();
    }

    private void PlayerDied(int playerIndex)
    {
        DeadPlayer(playerIndex);

        if (_playersCount == 1)
        {
            SetRoundWinner();
            if (onLastPlayerStanding != null)
                onLastPlayerStanding.Invoke();
        }
    }

    private void DeadPlayer(int playerIndex)
    {
        _playersCount--;
        _alivePlayers[playerIndex] = false;
        DeactivatePlayer(_playersInputs[playerIndex]);
        _playersFollowers[playerIndex].Restart();
    }
 
    private void SetRoundWinner()
    {
        Winner = null;
        for (int i = 0; i < _numberPlayers; i++)
        {
            if (_alivePlayers[i])
                Winner = _playersInputs[i].gameObject;
        }
    }

    public int GetGameWinner(out GameObject winner)
    {
        PlayerInput playerInput = null;
        int victories, maxVictories = -1;
        // Change tie to false
        _tie = false;

        // Check who won or if need a tiebreaker
        for (int i = 0; i < _playersInputs.Count && !_tie; i++) 
        {
            victories = _playersInputs[i].GetComponent<PlayerController>().VictoriesCount;
            if (victories > maxVictories)
            {
                // Set winner 
                maxVictories = victories;
                playerInput = _playersInputs[i];
                _tie = false;
                _tiebreakerPlayerIndex.Clear();
                _tiebreakerPlayerIndex.Add(i);
            }
            else if (victories == maxVictories)
            {
                _tie = true;
                _tiebreakerPlayerIndex.Add(i);
            }
        }

        if (_tie)
        {
            // No winner, need tiebreaker
            winner = null;
            return -1;
        }
        else
        {
            // Return winner
            winner = playerInput.gameObject;
            return playerInput.playerIndex;
        }
    }

    public void SetPlayersForTiebreaker()
    {
        foreach (PlayerInput p in _playersInputs)
        {
            // Remove player from tiebreaker
            if (!_tiebreakerPlayerIndex.Contains(p.playerIndex))
                DeadPlayer(p.playerIndex);
            else // Set score for tiebreakers player to zero
                p.GetComponent<PlayerController>().ScoreToZero();
        }
    }

    public void ActivatePlayers()
    {
        _playersCount = 0;
        foreach (PlayerInput p in _playersInputs)
        {
            ActivatePlayer(p);
        }
    }

    private void ActivatePlayer(PlayerInput playerInput)
    {
        // Activate player
        playerInput.gameObject.SetActive(true);
        playerInput.ActivateInput();
        _alivePlayers[playerInput.playerIndex] = true;
        _playersCount++;
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

                SetOnDiedEvent(playerInput);

                DeactivatePlayer(playerInput);

                _playersCount++;
            }
        }

        _numberPlayers = playersCount;
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