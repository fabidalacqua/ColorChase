using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelector : MonoBehaviour
{
    private CharacterSelector _characterSelector = null;

    public void Join(CharacterSelector characterSelector)
    {
        _characterSelector = characterSelector;
        _characterSelector.Joined();
    }

    public void OnJoin(InputAction.CallbackContext ctx)
    {
        /*if (_characterSelector != null)
        {
            _characterSelector.SelectCharacter();
        }*/
    }

    public void OnLeftArrow(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        _characterSelector.PreviousCharacter();
    }

    public void OnRightArrow(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        _characterSelector.NextCharacter();
    }

    public void OnLeave(InputAction.CallbackContext ctx)
    {
        _characterSelector.Left();
    }
}
