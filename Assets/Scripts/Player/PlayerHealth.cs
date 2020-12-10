using UnityEngine;
using UnityEngine.Events;

namespace Player
{
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

        [HideInInspector]
        public IntUnityEvent onTakeDamage = new IntUnityEvent();

        [HideInInspector]
        public IntUnityEvent onDied = new IntUnityEvent();

        public int playerIndex = -1;

        private void Start()
        {
            Restart();
        }

        public void Restart()
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

                if (onTakeDamage != null)
                    onTakeDamage.Invoke(_health);

                if (_health <= 0)
                {
                    AudioManager.Instance.Play("die");

                    if (onDied != null)
                        onDied.Invoke(playerIndex);
                }
            }
        }
    }

}