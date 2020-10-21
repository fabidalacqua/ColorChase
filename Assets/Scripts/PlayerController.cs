using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    
    private PlayerHealth _health;

    private Animator _animator;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();

        _health = GetComponent<PlayerHealth>();

        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_movement.MoveSpeed));
    }

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

    //public OnDash

    //public OnAttack 

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // Weapon collision
        ///Debug.Log("oi");
        // Item collision
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jump on head collision
        if (collision.CompareTag("Player"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
        _animator.SetTrigger("IsHurt");
    }
}
