using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private Sprite _wonRound;

    [SerializeField]
    private Image[] _rounds;

    private PlayerController _playerController;

    public void Setup(GameObject player)
    {
        _playerController = player.GetComponent<PlayerController>();

        // Add listener to player score
        _playerController.OnScoreVictory.AddListener(UpdateScore);
    }

    private void UpdateScore()
    {
        for (int i = 0; i < _rounds.Length; i++)
        {
            // Player won round, change sprite
            if (_playerController.Victories[i])
                _rounds[i].sprite = _wonRound;
        }
    }
}
