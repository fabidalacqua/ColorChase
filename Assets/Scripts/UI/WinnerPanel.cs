using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnerPanel : MonoBehaviour
{
    [SerializeField]
    private Image _playerImg;

    [SerializeField]
    private TMP_Text _playerLabel;

    //TODO: With winner panel active, any player must press A to go to main menu
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
