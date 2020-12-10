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

    private bool _tiebreaker = false;

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
        // Enable pause action
        EnablePause();
        // Remove players from tiebreaker
        if (_tiebreaker)
            _multiplayerManager.SetPlayersForTiebreaker();
    }

    private void NextRound()
    {
        // Deactivate all players
        _multiplayerManager.DeactivatePlayers();
        // Hide end round panel
        _roundPanel.SetActive(false);
        // Disable confirm Action
        DisableConfirm();

        if (_tiebreaker)
        {
            _curRound = 1;
            Debug.Log("Begin tiebreaker");
            _countdown.Begin(_countdownTime, true);
        }
        else // Normal round
        {
            _curRound++;
            if (_curRound <= _numberRounds)
                _countdown.Begin(_countdownTime);
            else
                EndGame();
        }
    }

    private void EndRound()
    {
        // Disable pause action
        DisablePause();

        // Get round winner
        GameObject player = _multiplayerManager.Winner;
        if (player != null)
        {
            // Set score
            player.GetComponent<PlayerController>().ScoreVictory(_curRound);
        }

        if (_tiebreaker)
        {
            Debug.Log("End tiebreaker");
            EndGame();
        }
        else
        {
            // Show end round panel
            _roundPanel.SetActive(true);

            // Stop item manager and destroy remaining items
            _itemManager.roundStarted = false;
            _itemManager.DestroyAll();

            // Enable confirm action
            EnableConfirm();
        }
    }

    private void EndGame()
    {
        // Activate all players
        _multiplayerManager.ActivatePlayers();
        // Get winner player
        GameObject winner = null;
        int winnerIndex = _multiplayerManager.GetGameWinner(out winner);

        if (winnerIndex >= 0)
        {
            // Set values for winner panel
            _winnerPanel.SetWinner(winner, winnerIndex + 1);
            Invoke("EnableGameOver", 3f);
        }
        else
        {
            Tiebreaker();
        }
    }

    private void Tiebreaker()
    {
        _tiebreaker = true;
        NextRound();
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

    private void EnablePause()
    {
        _pauseAction.Enable();
        _pauseAction.started += Pause;
    }

    private void DisablePause()
    {
        _pauseAction.Disable();
        _pauseAction.started -= Pause;
    }

    #endregion
}
