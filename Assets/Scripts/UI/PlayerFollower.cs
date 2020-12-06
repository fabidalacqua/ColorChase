using UnityEngine;
using UnityEngine.UI;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private Image[] _items;

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
        _playerItem.OnPickUp.AddListener(PickUp);
        _playerItem.OnThrow.AddListener(Throw);

        player.SetActive(true);

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
        // Set sprites for picked up item and set active true
        foreach (Image i in _items)
        {
            i.sprite = _playerItem.Item.Sprite;
            i.gameObject.SetActive(true);
        }
    }
}
