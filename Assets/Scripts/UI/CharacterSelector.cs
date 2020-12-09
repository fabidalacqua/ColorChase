using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _characters;

    [SerializeField]
    private GameObject _joined;

    [SerializeField]
    private Image _leftArrow, _rightArrow;

    [SerializeField]
    private Image _characterImage;

    public bool[] _available;

    public int Index { get; private set; }

    public bool Available { get; private set; }

    private void Awake()
    {
        Available = true;
        Index = 0;
    }

    public void Joined()
    {
        _joined.SetActive(true);
        Available = false;
    }

    public void Left()
    {
        _joined.SetActive(false);
        Available = true;
    }

    public void NextCharacter()
    {
        Index++;

        if (Index >= _characters.Length)
            Index = 0;

        _characterImage.sprite = _characters[Index];

        _rightArrow.color = Color.black;
        Invoke("ReturnToWhite", .1f);
    }

    public void PreviousCharacter()
    {
        Index--;

        if (Index < 0)
            Index = _characters.Length - 1;

        _characterImage.sprite = _characters[Index];

        _leftArrow.color = Color.black;
        Invoke("ReturnToWhite", .1f);
    }

    public void SelectCharacter()
    {
        //Remover personagem da lista de disponiveis para que os outros player nao escolham
        //
    }

    private void ReturnToWhite()
    {
        _leftArrow.color = Color.white;
        _rightArrow.color = Color.white;
    }
}
