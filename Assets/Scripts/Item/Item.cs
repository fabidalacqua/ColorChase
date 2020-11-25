using UnityEngine;

//TODO: I'm doing this on the assumption that we won't have time to develop buff/debuff items.
public class Item : MonoBehaviour
{
    [SerializeField]
    private ColorOption _colorOption;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public Projectile projectilePrefab;

    public int numberOfProj = 2;

    private void Start()
    {
        Color color = ColorManager.Instance.GetColor(_colorOption);
        // Change color for icon and projectile
        _spriteRenderer.color = color;
        projectilePrefab.spriteRenderer.color = color;
    }
}