using UnityEngine;
using UnityEngine.UI;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private Image[] _items;

    private Camera _mainCamera;

    private PlayerItem _playerItem;

    private void Awake()
    {
        // Get main camera
        _mainCamera = FindObjectOfType<Camera>();
        // Get player item component
        _playerItem = _player.GetComponentInChildren<PlayerItem>();
    }

    private void Start()
    {
        // Listeners for pick and throwing item
        _playerItem.onPickUp.AddListener(PickUp);
        _playerItem.onThrow.AddListener(Throw);
    }

    void Update()
    {
        // Update UI position to player
        _rectTransform.position = 
            RectTransformUtility.WorldToScreenPoint(_mainCamera, _playerItem.transform.position);
    }

    private void Throw()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (i >= _playerItem.Item.numberOfProj)
                _items[i].gameObject.SetActive(false);
        }
        
    }

    private void PickUp()
    {
        // Set sprites for items ui and set active true
        foreach (Image i in _items)
        {
            i.sprite = _playerItem.Item.Sprite;
            i.gameObject.SetActive(true);
        }
    }
}
