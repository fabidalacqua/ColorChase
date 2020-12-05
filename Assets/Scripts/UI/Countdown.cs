using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;

    private float _timer;

    private bool _started = false;

    public UnityEvent OnEndCountdown { get; private set; }

    private void Awake()
    {
        OnEndCountdown = new UnityEvent();
    }

    private void Update()
    {
        if (_started)
        {
            _timer -= Time.deltaTime;
            if (_timer >  0)
            {
                int seconds = (int)(_timer % 60);
                _label.text = seconds.ToString();
            }
            else
            {
                SetCountdown(false);

                if (OnEndCountdown != null)
                    OnEndCountdown.Invoke();
            }
        }
    }

    private void SetCountdown(bool start)
    {
        gameObject.SetActive(start);
        _started = start;
    }

    public void Begin(int countdownTime)
    {
        _timer = countdownTime;
        SetCountdown(true);
    }
}
