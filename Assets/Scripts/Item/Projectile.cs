using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private float _speed = 200f;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D.AddForce(transform.right * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grid"))
        {
            Destroy(gameObject);
        }
    }
}
