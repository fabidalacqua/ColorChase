using UnityEngine;
using UnityEngine.UI;

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
        _playerItem = player.GetComponentInChildren<PlayerItem>();
        _playerHealth = player.GetComponentInChildren<PlayerHealth>();

        // Set listeners for item and health changes
        _playerItem.OnChangeColor.AddListener(ChangeColor);
        _playerHealth.OnTakeDamage.AddListener(TakeDamage);
        _playerHealth.OnDied.AddListener(Died);

        // Set sprite image for choosen character
        PlayerAnimation animations = player.GetComponent<PlayerAnimation>();
        _characterBase.sprite = animations.idleBaseSprite;
        _characterFront.sprite = animations.idleFrontSprite;
    }

    public void Restart()
    {
        // Restart HUD for character
        for (int i = 0; i < _hearts.Length; i++)
        {
             _hearts[i].sprite = _fullHeart;
        }

        _deadPlayer.gameObject.SetActive(false);
        _characterBase.color = ColorManager.Instance.GetColor(ColorOption.None);
        
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
