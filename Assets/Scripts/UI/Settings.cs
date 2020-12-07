using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

//TODO: Working settings.
public class Settings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Toggle _toggleColorblindMode;

    [SerializeField]
    private UnityEvent _onSave;

    private void Start()
    {
       /* SetVolume(PlayerPrefs.GetFloat("volume", 1f));
        SetColorblindMode(PlayerPrefs.GetInt("colorblindmode", 0) == 1 ? true : false);*/
    }

    public void SetVolume(float volume)
    {
       /* Debug.Log(volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);*/
    }

    public void SetColorblindMode(bool colorblind)
    {
       /* Debug.Log(colorblind);
        ColorType colorType = colorblind ? ColorType.Accessible : ColorType.Default;
        ColorManager.Instance.SetColorPalette(colorType);*/
    }
}
