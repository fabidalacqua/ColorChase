using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    // TODO: Animations for throwing/picking up item
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Item _item = null;

    // Just to not bug the pick up function 
    private readonly float _coolDown = 1f;

    private float _timer;

    private bool _canPickUp = false;

    public ColorOption ColorOption { get; private set; }

    public void Start()
    {
        ColorOption = ColorOption.None;
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
            _canPickUp = false;
            _timer = 0f;

            if (_item != null)
            {
                // Just destroy item when player pick up another
                // (avoiding calling Destroy function too often)
                Destroy(_item.gameObject);
                _item = null;
            }

            _item = item;
            _item.gameObject.SetActive(false);

            ChangePlayerColor(_item.colorOption);
        }
    }

    public void Throw()
    {
        if (_item != null)
        {
            if (_item.numberOfProj > 0)
            {
                Instantiate(_item.projectilePrefab, transform.position, transform.rotation);

                _item.numberOfProj--;
                if (_item.numberOfProj == 0)
                {
                    ChangePlayerColor(ColorOption.None);
                }
            }
        }
    }

    private void ChangePlayerColor(ColorOption colorOption)
    {
        ColorOption = colorOption;
        _spriteRenderer.color = ColorManager.Instance.GetColor(ColorOption);
    }
}
