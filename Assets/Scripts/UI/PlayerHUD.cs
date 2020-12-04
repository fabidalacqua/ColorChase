﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Image _characterBase;

    [SerializeField]
    private Image _characterFront;

    [SerializeField]
    private Image _deadPlayer;

    [SerializeField]
    private Sprite _noHeart;

    [SerializeField]
    private Image[] _hearts;

    private PlayerItem _playerItem;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerItem = _player.GetComponentInChildren<PlayerItem>();
        _playerHealth = _player.GetComponentInChildren<PlayerHealth>();
    }

    private void Start()
    {
        // Set listeners for item and health changes
        _playerItem.OnChangeColor.AddListener(ChangeColor);
        _playerHealth.OnTakeDamage.AddListener(TakeDamage);
        _playerHealth.OnDied.AddListener(Died);
    }

    //TODO: Update sprite for character's player in the begining
    public void SetCharacter()
    {
        /*_characterBase.sprite =
        _characterFront.sprite = */
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

    private void Died()
    {
        // Set active true to deadPlayer image
        _deadPlayer.gameObject.SetActive(true);
    }
}