using UnityEngine;
using UnityEngine.InputSystem;

namespace Selection
{
    public class PlayerSelectionController : MonoBehaviour
    {
        private CharacterSelector _characterSelector = null;

        public int Character { get; private set; }

        public void Join(CharacterSelector characterSelector)
        {
            _characterSelector = characterSelector;
            Character = _characterSelector.Joined();
        }

        public void Leave()
        {
            _characterSelector.Left();
            _characterSelector = null;
        }

        public void OnLeftArrow(InputAction.CallbackContext ctx)
        {
            // Fix to input action triggering multiple times
            if (!ctx.performed) return;

            Character = _characterSelector.PreviousCharacter();
        }

        public void OnRightArrow(InputAction.CallbackContext ctx)
        {
            // Fix to input action triggering multiple times
            if (!ctx.performed) return;

            Character = _characterSelector.NextCharacter();
        }
    }
}

