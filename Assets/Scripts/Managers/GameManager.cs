using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Player;
using Items;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MultiplayerManager _multiplayerManager;

    [SerializeField]
    private ItemManager _itemManager;

    [SerializeField]
    private Countdown _countdown;

    [SerializeField]
    private GameObject _roundPanel;

    [SerializeField]
    private WinnerPanel _winnerPanel;

    [SerializeField]
    private float _numberRounds = 4;

    [SerializeField]
    private int _countdownTime = 4;

    [Header("Action Events")]

    [SerializeField]
    private UnityEvent _onPause;

    [SerializeField]
    private UnityEvent _onGameOver;

    [Header("Input actions")]

    [SerializeField]
    private InputAction _confirmAction = null;

    [SerializeField]
    private InputAction _pauseAction = null;

    private int _curRound = 0;

    private bool _gameOver = false;

    private void Start()
    {
        // Add listener to be called when only one player are still alive
        _multiplayerManager.onLastPlayerStanding.AddListener(EndRound);

        // Set listener for end round countdown
        _countdown.onEndCountdown.AddListener(ActivateMultiplayer);
        // Begin first round
        NextRound();
    }

    private void ActivateMultiplayer()
    {
        _multiplayerManager.ActivatePlayers();
        _itemManager.roundStarted = true;
    }

    private void NextRound()
    {
        // Deactivate all players
        _multiplayerManager.DeactivatePlayers();
        // Hide end round panel
        _roundPanel.SetActive(false);
        // Disable confirm Action
        DisableConfirm();

        _curRound++;
        if (_curRound <= _numberRounds)
            _countdown.Begin(_countdownTime);
        else
            EndGame();
    }

    private void EndRound()
    {
        // Get round winner and set score
        GameObject player = _multiplayerManager.GetRoundWinner();
        
        if (player != null)
        {
            player.GetComponent<PlayerController>().ScoreVictory(_curRound);
        }

        // Show end round panel
        _roundPanel.SetActive(true);

        // Stop item manager and destroy remaining items
        _itemManager.roundStarted = false;
        _itemManager.DestroyAll();

        // Enable confirm action
        EnableConfirm();
    }

    private void EndGame()
    {
        // Activate all players
        _multiplayerManager.ActivatePlayers();
        // Get winner player
        GameObject winner = null;
        int winnerIndex = _multiplayerManager.GetGameWinner(out winner);
        // Set values for winner panel
        _winnerPanel.SetWinner(winner, winnerIndex + 1);

        Invoke("EnableGameOver", 3f);
    }

    private void EnableGameOver()
    {
        // Enable confirm action
        EnableConfirm();
        _winnerPanel.ShowInfo();
        _gameOver = true;
    }

    #region Input Handler

    private void Confirm(InputAction.CallbackContext ctx)
    {
        // Was the last round, game is over 
        if (_gameOver)
        {
            if (_onGameOver != null)
                _onGameOver.Invoke();
        }
        else
        {
            NextRound();
        }
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        if (_onPause != null)
            _onPause.Invoke();
    }

    private void EnableConfirm()
    {
        _confirmAction.Enable();
        _confirmAction.started += Confirm;
    }

    private void DisableConfirm()
    {
        _confirmAction.Disable();
        _confirmAction.started -= Confirm;
    }

    private void OnEnable()
    {
        // Enable join action and subscribe to event
        _pauseAction.Enable();
        _pauseAction.started += Pause;
    }

    private void OnDisable()
    {
        // Disable join action and unsubscribe to event
        _pauseAction.Disable();
        _pauseAction.started -= Pause;
    }

    #endregion
}
