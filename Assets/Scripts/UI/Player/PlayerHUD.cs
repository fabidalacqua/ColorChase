using UnityEngine;
using UnityEngine.UI;
using Player;
using CustomColor;

namespace PlayerUI
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField]
        private Image _characterBase;

        [SerializeField]
        private Image _characterFront;

        [SerializeField]
        private Image _deadPlayer;

        [SerializeField]
        private Sprite _fullHeart;

        [SerializeField]
        private Sprite _noHeart;

        [SerializeField]
        private Image[] _hearts;

        private PlayerItem _playerItem;

        private PlayerHealth _playerHealth;

        public void Setup(GameObject player)
        {
            if (_playerItem == null || _playerHealth == null)
            {
                _playerItem = player.GetComponentInChildren<PlayerItem>();
                _playerHealth = player.GetComponentInChildren<PlayerHealth>();

                // Set listeners for item and health changes
                _playerItem.onChangeColor.AddListener(ChangeColor);
                _playerHealth.onTakeDamage.AddListener(TakeDamage);
                _playerHealth.onDied.AddListener(Died);

                // Set sprite image for choosen character
                PlayerAnimation animations = player.GetComponentInChildren<PlayerAnimation>();
                _characterBase.sprite = animations.idleBaseSprite;
                _characterFront.sprite = animations.idleFrontSprite;

                gameObject.SetActive(true);
            }

            Restart();
        }

        private void Restart()
        {
            // Restart HUD for character
            for (int i = 0; i < _hearts.Length; i++)
                _hearts[i].sprite = _fullHeart;

            _deadPlayer.gameObject.SetActive(false);
            ChangeColor(ColorManager.Instance.GetColor(ColorOption.None));
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void ChangeColor(Color color)
        {
            // Change player color
            _characterBase.color = color;
        }

        private void TakeDamage(int curHealth)
        {
            // Set sprite to noHeart if lost health
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i >= curHealth)
                    _hearts[i].sprite = _noHeart;
            }
        }

        private void Died(int index = -1)
        {
            // Set active true to deadPlayer image
            _deadPlayer.gameObject.SetActive(true);
            ChangeColor(ColorManager.Instance.GetColor(ColorOption.None));
        }
    }

}