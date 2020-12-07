using UnityEngine;
using UnityEngine.UI;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private GameObject[] _items;

    private Camera _mainCamera;

    private PlayerItem _playerItem;

    private bool _ready = false;

    private Image[][] _itensImg;

    public void Setup(GameObject player)
    {
        // Get main camera
        _mainCamera = FindObjectOfType<Camera>();
        // Get player item component
        _playerItem = player.GetComponentInChildren<PlayerItem>();

        // Listeners for pick and throwing item
        _playerItem.OnPickUp.AddListener(PickUp);
        _playerItem.OnThrow.AddListener(Throw);

        // Get images base and front
        for (int i = 0; i < _items.Length; i++)
        {
            _itensImg[i] = _items[i].GetComponentsInChildren<Image>();
        }

        _ready = true;
    }

    void Update()
    {
        if (_ready)
        {
            // Update UI position to player
            _rectTransform.position =
                RectTransformUtility.WorldToScreenPoint(_mainCamera, _playerItem.transform.position);
        }
    }

    public void Restart()
    {
        // Set active false to all items
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].gameObject.SetActive(false);
        }
    }

    private void Throw()
    {
        // Set active false to used item
        for (int i = 0; i < _items.Length; i++)
        {
            if (i >= _playerItem.Item.numberOfProj)
                _items[i].gameObject.SetActive(false);
        }
    }

    private void PickUp()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            // Base item image
            _itensImg[i][0].sprite = _playerItem.Item.BaseSprite;
            _itensImg[i][0].color = _playerItem.Item.Color;
            // Front item image
            _itensImg[i][1].sprite = _playerItem.Item.FrontSprite;
            // Show itens
            _items[i].SetActive(true);
        }
    }
}