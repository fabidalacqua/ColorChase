using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private float _speed = 5f;

	[SerializeField]
	private float _jumpForce = 150f;

	[SerializeField]
	private Rigidbody2D _rigidbody2D;

	[SerializeField]
	private LayerMask _whatIsGround;

	[SerializeField]
	private Transform _groundCheck;

	private float _moveSpeed;

	private bool _facingRight = true;

	// Radius of the overlap circle to determine if grounded
	const float _groundedRadius = .05f;
	// Whether or not the player is grounded
	private bool _grounded;

    private void FixedUpdate()
	{
		_grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_grounded = true;
			}
        }

		Move(_moveSpeed);
	}

	private void Move(float moveSpeed)
	{
		_rigidbody2D.velocity = new Vector2(moveSpeed, _rigidbody2D.velocity.y);

		_animator.SetFloat("Speed", Mathf.Abs(moveSpeed));

		if (moveSpeed > 0 && !_facingRight)
		{
			Flip();
		}
		else if (moveSpeed < 0 && _facingRight)
		{
			Flip();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;
		// Rotate
		transform.Rotate(0f, 180f, 0f);
	}

	public void SetMove(float move)
	{
		_moveSpeed = move * _speed;
	}

	public bool Jump()
	{
		if (_grounded)
		{
			_grounded = false;

			_rigidbody2D.AddForce(new Vector2(0f, _jumpForce));

			_animator.SetTrigger("IsJumping");

			return true;
		}
		return false;
	}
}