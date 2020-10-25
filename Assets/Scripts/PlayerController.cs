using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _movement;

    [SerializeField]
    private PlayerHealth _health;

    [SerializeField]
    private PlayerWeapon _weapon;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private ColorTable _colorTable;

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_movement.MoveSpeed));
    }

    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _movement.SetMove(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        if (_movement.Jump())
        {
            _animator.SetTrigger("IsJumping");
        }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        if (_movement.Jump())
        {
            _animator.SetTrigger("IsJumping");
        }
    }

    public void OnThrow(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        Debug.Log("Throw weapon");
        _weapon.ThrowDagger();
    }

    #endregion

    //TODO: This is not the better way to do this, but I'll leave just for the prototype
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jump on head collision
        if (collision.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " took damage on the head made by " + collision.name);
            TakeDamage();
        }
        else if (collision.CompareTag("Item"))
        {
            Debug.Log("Pick a item");
            _weapon.PickWeapon(collision.gameObject.GetComponent<Weapon>());
        }
        else if (collision.CompareTag("Weapon"))
        {
            Debug.Log(gameObject.name + " took damage made by " + collision.name);
            Dagger dagger = collision.gameObject.GetComponent<Dagger>();
            TakeDamage(_colorTable.GetRelativeDamage((PlayerColor)dagger.color, _weapon.color));
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage = 1)
    {
        _health.TakeDamage(damage);
        _animator.SetTrigger("IsHurt");
    }
}
