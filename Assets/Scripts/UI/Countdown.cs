using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;

    private float _timer;

    private bool _started = false;

    public UnityEvent onEndCountdown;

    private void Update()
    {
        if (_started)
        {
            _timer -= Time.deltaTime;
            if (_timer >  0)
            {
                int seconds = (int)(_timer % 60);
                // Update text when seconds change
                if (!_label.text.Equals(seconds.ToString()))
                {
                    _label.text = seconds.ToString();
                    AudioManager.Instance.Play("countdown");
                } 
            }
            else
            {
                SetCountdown(false);

                if (onEndCountdown != null)
                    onEndCountdown.Invoke();
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
