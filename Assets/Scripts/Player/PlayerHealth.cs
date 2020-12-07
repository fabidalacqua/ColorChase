using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimation _animation;

    [SerializeField]
    private int _maxHealth = 2;

    [SerializeField]
    private float _invulnerableTime = 2f;

    private int _health;

    private bool _isVulnerable = true;

    private float _timer = 0;

    public IntUnityEvent OnTakeDamage { get; private set; }

    public UnityEvent OnDied { get; private set; }

    private void Awake()
    {
        OnTakeDamage = new IntUnityEvent();
        OnDied = new UnityEvent();
    }

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
            AudioManager.Instance.Play("hurt");

            _animation.Hurt();

            _health -= damage;
            _isVulnerable = false;

            if (OnTakeDamage != null)
                OnTakeDamage.Invoke(_health);

            if (_health <= 0)
            {
                AudioManager.Instance.Play("die");

                if (OnDied != null)
                    OnDied.Invoke();
            }
        }
    }
}
