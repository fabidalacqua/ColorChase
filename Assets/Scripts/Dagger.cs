using UnityEngine;

public class Dagger : MonoBehaviour
{
    [SerializeField]
    private float _speed = 200f;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [HideInInspector]
    public char color;

    private void Start()
    {
        _rigidbody2D.AddForce(transform.right * _speed);
    }

    public void ChangeColor(char color)
    {
        this.color = color;
        _spriteRenderer.color = ReturnColor(color);
    }

    private Color ReturnColor(char color)
    {
        switch (color)
        {
            case 'b':
                return Color.blue;
            case 'p':
                return new Color(0.49f, 0.0f, 1.0f, 1.0f);
            case 'r':
                return Color.red;
            case 'y':
                return Color.yellow;
        }

        return Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
