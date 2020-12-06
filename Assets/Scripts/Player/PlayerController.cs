using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class IntUnityEvent : UnityEvent<int> {}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private PlayerHealth _playerHealth;

    [SerializeField]
    private PlayerItem _playerItem;

    public bool[] Victories { get; private set; }

    public IntUnityEvent OnWonRound { get; private set; }

    public UnityEvent OnScoreVictory { get; private set; }

    private void Awake()
    {
        Victories = new bool[] { false, false, false, false };

        OnWonRound = new IntUnityEvent();
        OnScoreVictory = new UnityEvent();
    }

    private void Start()
    {
        OnWonRound.AddListener(ScoreVictory);

        _playerHealth.OnDied.AddListener(Deactivate);
    }

    private void Deactivate()
    {
        // Deativate player
        gameObject.SetActive(false);
    }

    private void ScoreVictory(int roundNumber)
    {
        Victories[roundNumber-1] = true;

        if (OnScoreVictory != null)
            OnScoreVictory.Invoke();
    }

    #region Input Handler

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _playerMovement.SetMove(ctx.ReadValue<float>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        _playerMovement.Jump();
    }

    public void OnThrow(InputAction.CallbackContext ctx)
    {
        // Fix to input action triggering multiple times
        if (!ctx.performed) return;

        _playerItem.Throw();
    }

    #endregion

    #region Collision
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
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
