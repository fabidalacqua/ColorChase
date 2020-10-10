using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	#region Serialized Fields

	[SerializeField]
	private float _moveSpeed = 5f;

	[SerializeField]
	private float _jumpForce = 150f;

	[SerializeField]
	private LayerMask _whatIsGround;

	[SerializeField]
	private Transform _groundCheck;

	[SerializeField]
	private Transform _ceilingCheck;

	[SerializeField]
	private Animator _animator;

	#endregion

	#region Private variables

	private Rigidbody2D _rigidbody2D;

	private float _move = 0;

	private bool _facingRight = true;

	// Radius of the overlap circle to determine if grounded
	const float _groundedRadius = .05f;
	// Whether or not the player is grounded.
	private bool _grounded;

	#endregion

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

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

		// TODO: ta funfando mas ta feio isso aqui

		if (_grounded)
        {
			_animator.SetBool("IsJumping", false);
		}

		float x = _move * _moveSpeed;
		Move(x);

		_animator.SetFloat("Speed", Mathf.Abs(x));
	}

	private void Move(float x)
	{
		_rigidbody2D.velocity = new Vector2(x, _rigidbody2D.velocity.y);
		
		if (_move > 0 && !_facingRight)
		{
			Flip();
		}
		else if (_move < 0 && _facingRight)
		{
			Flip();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void SetMove(float move)
	{
		_move = move;

	}

	public void Jump()
	{
		if (_grounded)
		{
			_animator.SetBool("IsJumping", true);
			_grounded = false;
			_rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
		}
	}
}
