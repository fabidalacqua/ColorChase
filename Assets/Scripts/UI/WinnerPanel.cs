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

    [SerializeField]
    private TMP_Text _infoText;

    [SerializeField]
    private AudioSource _cameraAudioSource;

    private void Start()
    {
        gameObject.SetActive(false);
        _infoText.gameObject.SetActive(false);
    }

    public void SetWinner(GameObject player, int number)
    {
        // Show panel
        gameObject.SetActive(true);

        // Stop music
        _cameraAudioSource.Stop();

        // Set player number
        _playerLabel.text = "PLAYER " + number.ToString();
        // Set sprite image for choosen character
        PlayerAnimation animations = player.GetComponentInChildren<PlayerAnimation>();
        _playerImg.sprite = animations.idleBaseSprite;

        AudioManager.Instance.Play("win");
    }

    public void ShowInfo()
    {
        _infoText.gameObject.SetActive(true);
    }
}
