using UnityEngine;

//TODO: I'm doing this on the assumption that we won't have time to develop buff/debuff items.
public class Item : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _baseSpriteRenderer;

    [SerializeField]
    private SpriteRenderer _frontSpriteRenderer;

    public ColorOption colorOption;

    public int numberOfProj = 2;

    public Projectile projectilePrefab;

    public Sprite BaseSprite { get; private set; }

    public Sprite FrontSprite { get; private set; }

    public Color Color { get; private set; }

    private void Awake()
    {
        BaseSprite = _baseSpriteRenderer.sprite;
        FrontSprite = _frontSpriteRenderer.sprite;
    }

    private void Start()
    {
        Color = ColorManager.Instance.GetColor(colorOption);
        // Set color option for projectile
        projectilePrefab.ChangeColor(colorOption, Color);
        // Set color for item
        _baseSpriteRenderer.color = Color;
    }
}