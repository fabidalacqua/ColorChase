using UnityEngine;
using CustomColor;

namespace Items
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _baseSpriteRenderer;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private float _speed = 10f;

        [HideInInspector]
        public ColorOption colorOption;

        private void Update()
        {
            _rigidbody2D.velocity = transform.right * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Grid"))
            {
                Destroy(gameObject);
            }
        }

        public void ChangeColor(ColorOption option, Color color)
        {
            colorOption = option;
            _baseSpriteRenderer.color = color;
        }

        public int GetRelativeDamage(ColorOption target)
        {
            return ColorManager.Instance.colorTable.GetRelativeDamage(colorOption, target);
        }
    }

}