using UnityEngine;
using UnityEngine.UI;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private Image[] _itemsBase;

    [SerializeField]
    private Image[] _itemsFront;

    private Camera _mainCamera;

    private PlayerItem _playerItem;

    private bool _ready = false;

    public void Setup(GameObject player)
    {
        // Get main camera
        _mainCamera = FindObjectOfType<Camera>();
        // Get player item component
        _playerItem = player.GetComponentInChildren<PlayerItem>();

        // Listeners for pick and throwing item
        _playerItem.onPickUp.AddListener(PickUp);
        _playerItem.onThrow.AddListener(Throw);
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
        for (int i = 0; i < _itemsBase.Length; i++)
        {
            _itemsBase[i].GetComponentInParent<GameObject>().SetActive(false);
        }
    }

    private void Throw()
    {
        // Set active false to used item
        for (int i = 0; i < _itemsBase.Length; i++)
        {
            if (i >= _playerItem.Item.numberOfProj)
            {
                _itemsBase[i].GetComponentInParent<GameObject>().SetActive(false);
                _ready = false;
            }
        }
    }

    private void PickUp()
    {
        _ready = true;
        for (int i = 0; i < _itemsBase.Length; i++)
        {
            // Base item image
            _itemsBase[i].sprite = _playerItem.Item.BaseSprite;
            _itemsBase[i].color = _playerItem.Item.Color;
            // Front item image
            _itemsFront[i].sprite = _playerItem.Item.FrontSprite;
            // Show itens
            _itemsBase[i].GetComponentInParent<GameObject>().SetActive(true);
        }
    }
}