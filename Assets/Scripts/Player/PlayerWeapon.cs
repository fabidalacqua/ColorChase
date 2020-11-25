using UnityEngine;
using UnityEngine.Events;

// TODO: This is not the better way to do this, but I'll leave just for the prototype
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform _throwPoint;

    // this is not good
    [SerializeField]
    private GameObject _daggerPrefab;

    //private Weapon _weapon;

    public ColorOption color = ColorOption.None;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    /*
    public void PickWeapon(Weapon weapon)
    {
        if (_weapon != null)
        {
            Destroy(_weapon.gameObject);
        }

        _weapon = weapon;

        color = (PlayerColor)_weapon.Color;
        _spriteRenderer.color = ReturnColor(_weapon.Color);
        // Remove the weapon from gameplay
        _weapon.gameObject.SetActive(false);
    }*/

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


    public void ThrowDagger()
    {
        /*if (_weapon != null) 
        {
            if (_weapon.Quantity > 0)
            {
                GameObject dagger = Instantiate(_daggerPrefab, _throwPoint.position, _throwPoint.rotation);
                dagger.GetComponent<Dagger>().ChangeColor( _weapon.Color);
                _weapon.Quantity--;
            }

            if (_weapon.Quantity == 0)
            {
                // Remove color
                Destroy(_weapon.gameObject);
                _weapon = null;
                color = PlayerColor.None;
                _spriteRenderer.color = Color.white;

            }
        }
        else
        {
            Debug.Log(gameObject.name + " does not have a weapon to throw");
        }*/
    }
}
