using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _numberText;

    [SerializeField]
    private TMP_Text _tiebreakerText;

    private float _timer;

    private bool _started = false;

    [HideInInspector]
    public UnityEvent onEndCountdown;

    private void Update()
    {
        if (_started)
        {
            _timer -= Time.deltaTime;
            if (_timer >  1)
            {
                int seconds = (int)(_timer % 60);
                // Update text when seconds change
                if (!_numberText.text.Equals(seconds.ToString()))
                {
                    _numberText.text = seconds.ToString();
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

    public void Begin(int countdownTime, bool tiebreaker = false)
    {
        _timer = countdownTime;

         _tiebreakerText.gameObject.SetActive(tiebreaker);

        SetCountdown(true);
    }
}
