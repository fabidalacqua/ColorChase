using UnityEngine;
using UnityEngine.UI;
using Player;

namespace PlayerUI
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField]
        private Sprite _wonRound;

        [SerializeField]
        private Image[] _rounds;

        private PlayerController _playerController = null;

        public void Setup(GameObject player)
        {
            if (_playerController == null)
            {
                // Add listener to player score
                _playerController = player.GetComponent<PlayerController>();
                _playerController.onScoreVictory.AddListener(UpdateScore);
            }
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
}