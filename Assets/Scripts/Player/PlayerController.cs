﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Items;

public class IntUnityEvent : UnityEvent<int> {}

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement _playerMovement;

        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private PlayerItem _playerItem;

        public bool[] Victories { get; private set; }

        public int VictoriesCount { get; private set; }

        [HideInInspector]
        public UnityEvent onScoreVictory = new UnityEvent();

        private void Awake()
        {
            Victories = new bool[] { false, false, false, false };
            VictoriesCount = 0;
        }

        public void ScoreVictory(int roundNumber)
        {
            Victories[roundNumber - 1] = true;
            VictoriesCount++;

            if (onScoreVictory != null)
                onScoreVictory.Invoke();
        }

        public void ScoreToZero()
        {
            VictoriesCount = 0;
            Victories = new bool[] { false, false, false, false };
        }

        public void Restart()
        {
            _playerHealth.Restart();
            _playerItem.Restart();
        }

        #region Input Handler

        public void OnMove(InputAction.CallbackContext ctx)
        {
            _playerMovement.SetMove(ctx.ReadValue<float>());
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            // Fix to input action triggering multiple times
            if (PauseMenu.isPaused || !ctx.performed) return;

            _playerMovement.Jump();
        }

        public void OnThrow(InputAction.CallbackContext ctx)
        {
            // Fix to input action triggering multiple times
            if (PauseMenu.isPaused || !ctx.performed) return;

            _playerItem.Throw();
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            // Fix to input action triggering multiple times
            if (PauseMenu.isPaused || !ctx.performed) return;

            _playerMovement.Dash();
        }

        #endregion

        #region Collision

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Feet":
                    TakeDamage();
                    break;
                case "Projectile":
                    TakeDamage(collision);
                    break;
                case "Head":
                        _playerMovement.Avoid();
                    break;
                case "Item":
                    _playerItem.PickUp(collision.gameObject.GetComponent<Item>());
                    break;

            }
        }

        private void TakeDamage(Collider2D collision = null)
        {
            if (collision != null) // Projectile collision
            {
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();
                int damage = projectile.GetRelativeDamage(_playerItem.ColorOption);
                Destroy(collision.gameObject);

                _playerHealth.TakeDamage(damage);
            }
            else // Jump on head
                _playerHealth.TakeDamage();
        }

        #endregion
    }
}