using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int _maxHealth = 2;

    [SerializeField]
    private float _invulnerableTime = 2f;

    private int _health;

    private bool _isVulnerable = true;

    private float _timer = 0;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (!_isVulnerable)
        {
            _timer += Time.deltaTime;

            if (_timer >= _invulnerableTime)
            {
               _isVulnerable = true;
               _timer = 0;
            }
        }
    }

    public void TakeDamage(int damage = 1)
    {
        if (_isVulnerable)
        {
            _animator.SetTrigger("IsHurt");

            _health -= damage;
            _isVulnerable = false;

            if (_health <= 0)
            {
                _animator.SetTrigger("IsDead");
            }
        }
    }
}
