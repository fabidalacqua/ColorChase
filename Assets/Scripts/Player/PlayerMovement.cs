using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimation _animation;

        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private float _jumpForce = 100f;

        [SerializeField]
        private Vector2 _wallJumpForce = new Vector2(5, 10);

        [SerializeField]
        private Vector2 _avoidForce = new Vector2(5, 10);

        [SerializeField]
        private float _canMoveTimer = .5f;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private LayerMask _whatIsGround;

        [SerializeField]
        private Transform _groundCheck;

        [SerializeField]
        private Transform _wallCheck;

        private float _moveSpeed;

        private bool _facingRight = true;

        const float _groundedRadius = .05f;

        private bool _grounded;

        private bool _wall;

        private bool _canMove = true;

        [SerializeField]
        private float _dashSpeed;

        [SerializeField]
        private float _canDashTimer = 0.2f;

        private bool _canDash = true;

        private void FixedUpdate()
        {
            // Check if the player is grounded
            _grounded = HasOverlapedColliders(_groundCheck);
            _wall = false;
            if (!_grounded)
            {
                // Check if player is touching a wall if it is not grounded
                _wall = HasOverlapedColliders(_wallCheck);
            }

            if (_canMove)
                Move(_moveSpeed);


        }

        private bool HasOverlapedColliders(Transform transform)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _groundedRadius, _whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    return true;
                }
            }
            return false;
        }

        private void Move(float moveSpeed)
        {
            _rigidbody2D.velocity = new Vector2(moveSpeed, _rigidbody2D.velocity.y);

            _animation.Walk(moveSpeed);

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

        private void SetCanMoveToTrue()
        {
            _canMove = true;
        }

        public void SetMove(float move)
        {
            _moveSpeed = move * _speed;
        }

        public void Avoid()
        {
            // AudioManager.Instance.Play("jump");

            _canMove = false;
            Invoke("SetCanMoveToTrue", _canMoveTimer);

            _rigidbody2D.AddForce(_avoidForce * new Vector2(_facingRight ? -1 : 1, 1f), ForceMode2D.Impulse);
        }

        public void Jump()
        {
            if (_grounded)
            {
                // AudioManager.Instance.Play("jump");

                _grounded = false;

                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));

                _animation.Jump();
            }
            else if (_wall)
            {
                // AudioManager.Instance.Play("jump");

                _wall = false;

                _canMove = false;
                Invoke("SetCanMoveToTrue", _canMoveTimer);

                _rigidbody2D.AddForce(_wallJumpForce * new Vector2(_facingRight ? -1 : 1, 1f), ForceMode2D.Impulse);

                //TODO: Dash like animation
                //_animation.Jump();
            }
        }
        private void SetCanDashToTrue()
        {
            _canDash = true;
            _rigidbody2D.velocity = Vector2.zero;
        }
   
    private void SetCanDashToTrue()
    {
        _canDash = true;
        // _rigidbody2D.velocity = Vector2.zero;
    }

        public void Dash()
        {
            //checks if its not already on dash
            if (_canDash)
            {
                _canMove = false;
                _canDash = false;

                Invoke("SetCanMoveToTrue", _canMoveTimer);
                Invoke("SetCanDashToTrue", _canDashTimer);

                int dir = _facingRight ? 1 : -1;
                _rigidbody2D.velocity = new Vector2(dir * _dashSpeed, _rigidbody2D.velocity.y);
                _animation.Dash();
            }
        }
    }
}