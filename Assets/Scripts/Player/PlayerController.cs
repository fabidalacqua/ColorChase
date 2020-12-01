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
        if (collision.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " took damage on the head made by " + collision.name);
            // Jump on head collision
            _playerHealth.TakeDamage();
        }
        else if (collision.CompareTag("Item"))
        {
            _playerItem.PickUp(collision.gameObject.GetComponent<Item>());
        }
        else if (collision.CompareTag("Projectile"))
        {
            Debug.Log(gameObject.name + " took damage made by " + collision.name);

            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            int damage = projectile.GetRelativeDamage(_playerItem.ColorOption);
            Destroy(collision.gameObject);

            _playerHealth.TakeDamage(damage);
        }
    }
}
