using UnityEngine;

namespace Softweather.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundFXController : MonoBehaviour
    {
        [SerializeField] private AudioClip fireSound;
        [SerializeField] private float fireSoundVolume = 1f;

        private bool isSoundMuted;
        private AudioSource myAudioSource;

        private void Awake()
        {
            myAudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            LoadSettings();
        }

        public void PlayFireSound()
        {
            if (isSoundMuted) return;

            myAudioSource.PlayOneShot(fireSound, fireSoundVolume);
        }

        private void LoadSettings()
        {
            isSoundMuted = PlayerPrefs.GetInt(PlayerPrefsValues.IsMuted) == 1;
        }
    }
}