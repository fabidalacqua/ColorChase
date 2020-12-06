using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class MultiplayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playersPrefabs;

    [SerializeField]
    private PlayerHUD[] _playersHUDs;

    [SerializeField]
    private PlayerFollower[] _playersFollowers;

    [SerializeField]
    private PlayerScore[] _playersScores;

    [SerializeField]
    private Countdown _countdown;

    private PlayerInputManager _playerInputManager;

    private int _playerCount = 0;

    private int _curRound = 0;

    private List<PlayerInput> _playersInputs;

    private void Awake()
    {
        _playersInputs = new List<PlayerInput>();
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        // Instantiate all joined players
        InstantiatePlayersWithSetDevices();

        // Add listener for activate players when countdown is over
        _countdown.OnEndCountdown.AddListener(ActivatePlayers);

        // Start countdown
        StartRound();
    }

    private void InstantiatePlayersWithSetDevices()
    {
        // Get the number of players joined in selection screen
        int NumberPlayers = PlayerPrefs.GetInt("number_players");
        string devicePath = null;
        string controlScheme = null;

        for (int i = 0; i < NumberPlayers; i++)
        {
            devicePath = PlayerPrefs.GetString("player_" + i + "_device", null);
            controlScheme = PlayerPrefs.GetString("player_" + i + "_controlScheme", null);
            
            if (devicePath != null && controlScheme != null)
            {
                // Get character prefab for player
                int character = PlayerPrefs.GetInt($"player_{i}_character");
                _playerInputManager.playerPrefab = _playersPrefabs[character];

                // Instantiate player with device
                PlayerInput playerInput = _playerInputManager.JoinPlayer(i,
                    pairWithDevice: InputSystem.GetDevice(devicePath), controlScheme: controlScheme);
                _playersInputs.Add(playerInput); 
            }
        }
    }

    private void ActivatePlayers()
    {
        // Activate all players input
        foreach (PlayerInput p in _playersInputs)
        {
            p.gameObject.SetActive(true);
            p.ActivateInput();
        }
    }

    private void DeactivatePlayers()
    {
        // Deactivate all players input
        foreach (PlayerInput p in _playersInputs)
        {
            p.gameObject.SetActive(false);

            // Restart player's UI
            _playersHUDs[p.playerIndex].Restart();
            _playersFollowers[p.playerIndex].Restart();

            p.DeactivateInput();
        }
    }

    private void PlayerDied()
    {
        _playerCount--;
        if (_playerCount <= 1)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        foreach (PlayerInput p in _playersInputs)
        {
            // Find the player who won the round
            if (p.gameObject.activeInHierarchy)
            {
                p.GetComponent<PlayerController>().OnWonRound.Invoke(_curRound);
            }
        }

        DeactivatePlayers();
    }

    public void StartRound()
    {
        _curRound++;
        _countdown.Begin(6);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        _playerCount++;

        player.transform.SetParent(transform);

        int index = player.playerIndex;
        // Setup player's UI
        _playersHUDs[index].Setup(player.gameObject);
        _playersFollowers[index].Setup(player.gameObject);
        _playersScores[index].Setup(player.gameObject);

        player.GetComponent<PlayerHealth>().OnDied.AddListener(PlayerDied);
    }
}