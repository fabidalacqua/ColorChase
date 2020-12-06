using UnityEngine;

//TODO: I'm doing this on the assumption that we won't have time to develop buff/debuff items.
public class Item : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public ColorOption colorOption;

    public int numberOfProj = 2;

    public Projectile projectilePrefab;

    public Sprite Sprite { get; private set; }

    private void Awake()
    {
        Sprite = _spriteRenderer.sprite;
    }

    private void Start()
    {
        Color color = ColorManager.Instance.GetColor(colorOption);
        //TODO: Unfortunely we do not have time to implement acessible options :(
        // Set color option for projectile
        //projectilePrefab.ChangeColor(colorOption, color);
        // Set color for item
        //_spriteRenderer.color = color;
    }
}