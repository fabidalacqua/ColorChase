using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeReference]
    private PlayerController _player;

    [SerializeField]
    private Sprite _wonRound;

    [SerializeField]
    private Image[] _rounds;

    private void Awake()
    {
        // Add listener to player score
        _player.OnScoreVictory.AddListener(UpdateScore);
    }

    private void UpdateScore()
    {
        for (int i = 0; i < _rounds.Length; i++)
        {
            // Player won round, change sprite
            if (_player.Victories[i])
                _rounds[i].sprite = _wonRound;
        }
    }
}
