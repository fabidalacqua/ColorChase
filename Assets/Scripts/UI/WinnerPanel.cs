using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player;

public class WinnerPanel : MonoBehaviour
{
    [SerializeField]
    private Image _playerImg;

    [SerializeField]
    private TMP_Text _playerLabel;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetWinner(GameObject player, int number)
    {
        gameObject.SetActive(true);
        // Set player number
        _playerLabel.text = "PLAYER " + number.ToString();
        // Set sprite image for choosen character
        PlayerAnimation animations = player.GetComponent<PlayerAnimation>();
        _playerImg.sprite = animations.idleBaseSprite;

        AudioManager.Instance.Play("win");
    }
}
