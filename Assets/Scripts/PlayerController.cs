using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _movement.SetMove(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Fix to action triggering multiple times
        if (!ctx.performed) return;

        _movement.Jump();
    }
}
