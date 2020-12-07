﻿using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

//TODO: Working settings.
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
        SetVolume(PlayerPrefs.GetFloat("volume", 1f));
        SetColorblindMode(PlayerPrefs.GetInt("colorType", 0) == 1 ? true : false);
        _saveButton.gameObject.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        _volumeSlider.value = volume;

        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetColorblindMode(bool colorblind)
    {
        _toggleColor.isOn = colorblind;

        _colorType = colorblind ? ColorType.Accessible : ColorType.Default;
        // Must save (reload scene) to see changes
        _saveButton.gameObject.SetActive(true);
    }

    public void Save()
    {
        ColorManager.Instance.SetColorPalette(_colorType);
        PlayerPrefs.SetInt("colorType", (int)_colorType);

        // Reaload scene
        if (_onSave != null)
            _onSave.Invoke();
    }
}