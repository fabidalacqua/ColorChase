using UnityEngine;
using UnityEngine.Events;
using CustomColor;
using Items;

namespace Player
{
    public class PlayerItem : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimation _animation;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Transform _throwPoint;

        // Just to not bug the pick up function 
        private readonly float _coolDown = 1f;

        private float _timer;

        private bool _canPickUp = false;

        public Item Item { get; private set; }

        public ColorOption ColorOption { get; private set; }

        public UnityEvent onPickUp = new UnityEvent();

        public UnityEvent onThrow = new UnityEvent();

        public ColorUnityEvent onChangeColor = new ColorUnityEvent();

        private void Awake()
        {
            ColorOption = ColorOption.None;
            Item = null;
        }

        private void Start()
        {
            ChangePlayerColor(ColorOption);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _coolDown)
                _canPickUp = true;
        }

        public void PickUp(Item item)
        {
            if (_canPickUp)
            {
                // AudioManager.Instance.Play("pickup");

                _canPickUp = false;
                _timer = 0f;

                if (Item != null)
                {
                    // Just destroy item when player pick up another
                    // (avoiding calling Destroy function too often)
                    Destroy(Item.gameObject);
                    Item = null;
                }

                Item = item;
                Item.gameObject.SetActive(false);

                ChangePlayerColor(Item.colorOption);

                if (onPickUp != null)
                    onPickUp.Invoke();
            }
        }

        public void Throw()
        {
            if (Item != null)
            {
                if (Item.numberOfProj > 0)
                {
                    // AudioManager.Instance.Play("throw");

                    _animation.Throw();

                    Instantiate(Item.projectilePrefab, _throwPoint.position, _throwPoint.rotation);

                    Item.numberOfProj--;
                    if (Item.numberOfProj == 0)
                    {
                        ChangePlayerColor(ColorOption.None);
                    }

                    if (onThrow != null)
                        onThrow.Invoke();
                }
            }
        }

        private void ChangePlayerColor(ColorOption colorOption)
        {
            ColorOption = colorOption;
            Color color = ColorManager.Instance.GetColor(ColorOption);
            _spriteRenderer.color = color;

            if (onChangeColor != null)
                onChangeColor.Invoke(color);
        }
    }
}
