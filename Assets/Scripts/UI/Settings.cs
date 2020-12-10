using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using CustomColor;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider _volumeSlider;

    [SerializeField]
    private Toggle _toggleColor;

    [SerializeField]
    private Button _saveButton;

    [SerializeField]
    private UnityEvent _onSave;

    private ColorType _colorType;

    private void Start()
    {
        float volume = 1;
        audioMixer.GetFloat("volume", out volume);
        SetSlider(volume);

        SetToogle(ColorManager.Instance.GetActiveColorType() == 1 ? true : false);
       
        _saveButton.gameObject.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        SetSlider(volume);
        AudioManager.Instance.Play("select");
    }

    public void SetColorblindMode(bool colorblind)
    {
        SetToogle(colorblind);

        // Must save (reload scene) to if color changed
        _saveButton.gameObject.SetActive(
            ColorManager.Instance.GetActiveColorType() != (int)_colorType);

        AudioManager.Instance.Play("select");
    }

    private void SetSlider(float volume)
    {
        _volumeSlider.value = volume;
        audioMixer.SetFloat("volume", volume);
    }

    private void SetToogle(bool colorblind) {
        _toggleColor.isOn = colorblind;
        _colorType = colorblind ? ColorType.Accessible : ColorType.Default;
    }

    public void Save()
    {
        ColorManager.Instance.SetColorPalette(_colorType);
        // Reaload scene
        if (_onSave != null)
            _onSave.Invoke();
    }
}
