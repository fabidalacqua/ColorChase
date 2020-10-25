using UnityEngine;
using UnityEngine.Events;

// TODO: Maybe this is not the better way to do this, but I'll leave just for the prototype
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform _throwPoint;

    // this is not good
    [SerializeField]
    private GameObject _daggerPrefab;

    [HideInInspector]
    public UnityEvent onChangeColor;

    private Weapon _weapon;

    public void PickWeapon(Weapon weapon)
    {
        _weapon = weapon;
        // Remove the weapon from gameplay
        _weapon.gameObject.SetActive(false);
    }

    public void ThrowDagger()
    {
        if (_weapon.Quantity > 0)
        {
            Instantiate(_daggerPrefab, _throwPoint.position, _throwPoint.rotation);
            _weapon.Quantity--;
        }

        if (_weapon.Quantity == 0)
        {
            // Remove color
        }
    }
}
