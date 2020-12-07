using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    
    [SerializeField]
    private Sound[] _sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_sounds, s => s.name == name);
        _audioSource.PlayOneShot(sound.audioClip);
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
}
