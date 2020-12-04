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
    private Sprite _noHeart;

    [SerializeField]
    private Image[] _hearts;

    //TODO: Update life and player color during the game
}
