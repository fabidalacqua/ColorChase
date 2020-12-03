using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    private ColorPalette _default, _accessible;

    private ColorPalette _active;

    public ColorTable colorTable;

    public static ColorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _active = _default;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //TODO: Reload scene to change all colors (made this way thinking in a dropdown value)
    public void SetColorPalette(int colorType = 0)
    {
        if ((ColorType)colorType == ColorType.Default)
        {
            _active = _default;
        }
        else
        {
            _active = _accessible;
        }
    }

    public Color GetColor(ColorOption option)
    {
        foreach (ColorPair cp in _active.colors)
        {
            if (cp.option == option)
                return cp.color;
        }
        return _active.colors[0].color;
    }
}
