using UnityEngine;
using UnityEngine.UI;

namespace Selection
{
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField]
        private CharactersContainer _characters;

        [SerializeField]
        private GameObject _joined;

        [SerializeField]
        private Image _leftArrow, _rightArrow;

        [SerializeField]
        private Image _characterImage;

        private int _curIndex = -1;

        public bool Available { get; private set; }

        private void Awake()
        {
            if (_characters == null)
                Debug.LogError("Missing characters object reference.");

            Available = true;
        }

        public int Joined()
        {
            AudioManager.Instance.Play("select");

            _joined.SetActive(true);
            Available = false;

            NextCharacter();

            return _curIndex;
        }

        public void Left()
        {
            _joined.SetActive(false);
            Available = true;
            SetCharacterToAvailable(_curIndex);
        }

        public int NextCharacter()
        {
            // Keep the previous character index
            int prevIndex = _curIndex;
            do
            {
                _curIndex++;
                if (_curIndex >= _characters.list.Length)
                    _curIndex = 0;
            }
            while (!_characters.list[_curIndex].available);

            ArrowBlink(_rightArrow);
            // If had a previous index, define it as available
            SetCharacterToAvailable(prevIndex);
            // Set current character
            SetCharacter();

            return _curIndex;
        }

        public int PreviousCharacter()
        {
            // Keep the previous character index
            int prevIndex = _curIndex;
            do
            {
                _curIndex--;
                if (_curIndex < 0)
                    _curIndex = _characters.list.Length - 1;
            }
            while (!_characters.list[_curIndex].available);

            ArrowBlink(_leftArrow);

            // If had a previous index, define it as available
            SetCharacterToAvailable(prevIndex);
            // Set current character
            SetCharacter();

            return _curIndex;
        }

        private void SetCharacter()
        {
            _characterImage.sprite = _characters.list[_curIndex].sprite;
            _characters.list[_curIndex].available = false;
        }

        private void SetCharacterToAvailable(int index)
        {
            if (index != -1)
                _characters.list[index].available = true;
        }

        private void ArrowBlink(Image arrow)
        {
            AudioManager.Instance.Play("select");
            arrow.color = Color.black;
            Invoke("ReturnToWhite", .1f);
        }

        private void ReturnToWhite()
        {
            _leftArrow.color = Color.white;
            _rightArrow.color = Color.white;
        }
    }
}