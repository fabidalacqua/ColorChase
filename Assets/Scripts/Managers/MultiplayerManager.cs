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
    private Transform[] _spawnPoints;

    [SerializeField]
    private Countdown _countdown;

    [SerializeField]
    private GameObject _endRoundPanel;

    [SerializeField]
    private WinnerPanel _winnerPanel;

    [SerializeField]
    private float _numberRounds = 4;

    private PlayerInputManager _playerInputManager;

    private int _playerCount = 0;

    private int _curRound = 0;

    private List<PlayerInput> _playersInputs;

    // Tiebreaker variables
    private bool _tiebreaker = false;

    List<PlayerInput> _tiebreakerPlayers;

    private void Awake()
    {   
        _playerInputManager = GetComponent<PlayerInputManager>();

        _playersInputs = new List<PlayerInput>();

        _tiebreakerPlayers = new List<PlayerInput>();
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
                int character = PlayerPrefs.GetInt("player_"+ i +"_character");
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
        if (!_tiebreaker)
            ActivatePlayers(_playersInputs);
        else // Is a tiebreaker round
            ActivatePlayers(_tiebreakerPlayers);
    }

    private void ActivatePlayers(List<PlayerInput> playerInputs)
    {
        foreach (PlayerInput p in playerInputs)
        {
            // Place player in position
            p.gameObject.transform.position = _spawnPoints[p.playerIndex].position;
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
        PlayerInput curWinner = null;
        foreach (PlayerInput p in _playersInputs)
        {
            // Find the player who won the round
            if (p.gameObject.activeInHierarchy)
            {
                p.GetComponent<PlayerController>().OnWonRound.Invoke(_curRound);
                // Set round winner (used in tiebreaker)
                curWinner = p;
            }
        }

        DeactivatePlayers();

        if (!_tiebreaker)
            _endRoundPanel.SetActive(true);
        else
            _winnerPanel.SetWinner(curWinner.gameObject, curWinner.playerIndex + 1);
    }

    //TODO: With end round panel active, any player must press A to continue
    public void StartRound()
    {
        if (!_tiebreaker)
        {
            _curRound++;
            if (_curRound <= _numberRounds)
                _countdown.Begin(6);
            else
                EndGame();
        }
        else // Tiebreaker round
        {
            _countdown.Begin(6);
        }
    }

    private void EndGame()
    {
       PlayerInput winner =  GetWinner();

        if (winner != null)
        {
            _winnerPanel.SetWinner(winner.gameObject, winner.playerIndex+1);
        }
        else // Null winner means tiebreaker round
        {
            // Start last round
            StartRound();
        }
    }

    private PlayerInput GetWinner()
    {
        PlayerInput winner = null;

        int victories, maxVictories = -1;

        // Check who won or if need a tiebreaker
        foreach (PlayerInput p in _playersInputs)
        {
            victories = p.GetComponent<PlayerController>().VictoriesCount;
            if (victories > maxVictories)
            {
                // Set winner 
                maxVictories = victories;
                winner = p;

                _tiebreaker = false;
                // Add player for a possible tiebreaker
                _tiebreakerPlayers.Add(p);
            }
            else if (victories == maxVictories)
            {
                winner = null;

                _tiebreaker = true;
                _tiebreakerPlayers.Add(p);
            }
        }

        return winner;
    }

    public void OnJoinedPlayer(PlayerInput player)
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