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

    private int _playerCount = 4;

    private void Awake()
    {
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
        /*NumberOfPlayers = PlayerPrefs.GetInt("Number of Players");
        PlayerInputManager manager = GetComponent<PlayerInputManager>();
        string devicePath = null;
        string controlScheme = null;
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            if ((devicePath = PlayerPrefs.GetString($"Player_{i}_device", null)) != null && (controlScheme = PlayerPrefs.GetString($"Player_{i}_controlScheme", null)) != null)
            {
                int element = PlayerPrefs.GetInt($"Player_{i}_element", 0);
                manager.playerPrefab = PlayerPrefabs[element];
                _players.Add(manager.JoinPlayer(element, pairWithDevice: InputSystem.GetDevice(devicePath), controlScheme: controlScheme));
            }
        }
        */
    }

    private void ActivatePlayers()
    {
        /*
        foreach (PlayerInput player in _players)
        {
            player.SetActive(true);
            player.ActivateInput();
        }*/
    }

    //TODO: Need to execute this when some player die
    // Need to identify player index
    private void DeactivatePlayers(int index = -1)
    {
        if (index == -1) // Deactivate all players input
        {
            /*foreach (PlayerInput player in _players)
            {
                player.SetActive(false);
                player.DeactivateInput();
            }*/
        }
        else
        {
            //_player.SetActive(false);
            //_players[index].DeactivateInput();
        }
    }

    //TODO: Add this function in every player onDied event
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
        /*foreach (PlayerInput p in _players)
        {
            if (p.gameObject.activeInHierarchy)
            {
                //TODO: The last player standing wons
                // Invoke onWonRound for that player
            }
        }*/

        DeactivatePlayers();
    }

    public void StartRound()
    {
        _countdown.Begin(6);
    }

    // <summary>
    /// Called when a new player input is instantiated
    /// Makes him a child of this gameObject and activate its UI
    /// </summary>
    /// <param name="player"></param>
    public void OnPlayerJoined(PlayerInput player)
    {
        /*player.transform.SetParent(this.transform);

        _playerCount++;

        int index = player.playerIndex;
        _playerUIs[index].gameObject.SetActive(true);
        _playerUIs[index].Setup(player.gameObject);

        player.GetComponent<HealthSystem>().onDeath += HandlePlayer_onDeath;*/
    }
}

public enum Character
{
    None, Plum, Lime, Thunder
}