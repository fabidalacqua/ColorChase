using UnityEngine;
using UnityEngine.UI;
using Player;

namespace PlayerUI
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private GameObject[] _items;

        private Image[][] _itemsImg = null;

        private Camera _mainCamera;

        private PlayerItem _playerItem;

        private PlayerHealth _playerHealth;

        private bool _ready = false;

        public void Setup(GameObject player)
        {
            if (_playerItem == null || _playerHealth == null)
            {
                // Get main camera
                _mainCamera = FindObjectOfType<Camera>();
                // Get player item component
                _playerItem = player.GetComponentInChildren<PlayerItem>();
                _playerHealth = player.GetComponentInChildren<PlayerHealth>();

                // Listeners for pick and throwing item
                _playerItem.onPickUp.AddListener(PickUp);
                _playerItem.onThrow.AddListener(Throw);
            }

            Restart();
        }

        private void Restart()
        {
            // Set active false to all items
            for (int i = 0; i < _items.Length; i++)
                _items[i].SetActive(false);

            _ready = false;
        }

        private void Update()
        {
            if (_ready)
            {
                // Update UI position to player
                _rectTransform.position =
                    RectTransformUtility.WorldToScreenPoint(_mainCamera, _playerItem.transform.position);
            }
        }

        private void Throw()
        {
            // Set active false to used item
            for (int i = 0; i < _items.Length; i++)
            {
                if (i >= _playerItem.Item.numberOfProj)
                {
                    _items[i].SetActive(false);
                }
            }

            if (_playerItem.Item.numberOfProj == 0)
                _ready = false;
        }

        private void PickUp()
        {
            if (_itemsImg == null)
                _itemsImg = new Image[2][];

            for (int i = 0; i < _items.Length; i++)
            {
                if (_itemsImg[i] == null)
                {
                    _itemsImg[i] = _items[i].GetComponentsInChildren<Image>();
                }
                // Base item image
                _itemsImg[i][0].sprite = _playerItem.Item.BaseSprite;
                _itemsImg[i][0].color = _playerItem.Item.Color;
                // Front item image
                _itemsImg[i][1].sprite = _playerItem.Item.FrontSprite;
                // Show itens
                _items[i].SetActive(true);
            }
            _ready = true;
        }
    }
}