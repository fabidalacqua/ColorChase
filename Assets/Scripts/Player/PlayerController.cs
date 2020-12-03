using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private PlayerHealth _playerHealth;

    [SerializeField]
    private PlayerItem _playerItem;

    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _playerMovement.SetMove(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        _playerMovement.Jump();
    }

    public void OnThrow(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        _playerItem.Throw();
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                TakeDamage();
                break;
            case "Projectile":
                TakeDamage(collision);
                break;
            case "Head":
                _playerMovement.Avoid();
                break;
            case "Item":
                _playerItem.PickUp(collision.gameObject.GetComponent<Item>());
                break;
        }
    }

    private void TakeDamage(Collider2D collision = null)
    {
        if (collision != null) // Projectile collision
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            int damage = projectile.GetRelativeDamage(_playerItem.ColorOption);
            Destroy(collision.gameObject);

            _playerHealth.TakeDamage(damage);
        }
        else // Jump on head
            _playerHealth.TakeDamage();
    }
}
