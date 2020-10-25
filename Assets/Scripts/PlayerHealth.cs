using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 3;

    [SerializeField]
    private float _invulnerableTime = 2f;

    [HideInInspector]
    public UnityEvent onDeath;

    // Maybe it is needed to update a ui, or anything
    [HideInInspector]
    public UnityEvent onTakeDamage;

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
            _health -= damage;

            _isVulnerable = false;

            if (_health <= 0)
            {
                // This is really bad
                Destroy(gameObject);
                onDeath.Invoke();
            }
            else
            {
                onTakeDamage.Invoke();
            }
        }
    }
}
