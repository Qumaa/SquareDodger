using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class MusicVolumeController : MonoBehaviour
    {
        public Slider volumeSlider;
        public AudioSource musicSource;

        private void Start()
        {
           
            volumeSlider.value = musicSource.volume;

           
            volumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        }

        private void UpdateMusicVolume(float volume)
        {
           
            musicSource.volume = volume;
        }
    }
}
