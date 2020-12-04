using UnityEngine;
using UnityEngine.Events;

public class PlayerItem : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimation _animation;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Transform _throwPoint;

    [HideInInspector]
    public UnityEvent onPickUp;

    [HideInInspector]
    public UnityEvent onThrow;

    public Item Item { get; private set; }

    // Just to not bug the pick up function 
    private readonly float _coolDown = 1f;

    private float _timer;

    private bool _canPickUp = false;

    public ColorOption ColorOption { get; private set; }

    public void Start()
    {
        ColorOption = ColorOption.None;
        ChangePlayerColor(ColorOption);

        Item = null;
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
        _spriteRenderer.color = ColorManager.Instance.GetColor(ColorOption);
    }
}
