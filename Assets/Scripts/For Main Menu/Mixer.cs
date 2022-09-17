using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    private Slider _slider;

    private float _volume;
    public float Volume 
    { 
        get => _volume;
        private set
        {
            _audioMixer.audioMixer.SetFloat(_audioMixer.name ,value);
            _slider.value = value;
            _volume = value;
        }
    }

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    public void ChangeVolume(float volume) => Volume = volume;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
    }
}