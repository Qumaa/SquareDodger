using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance;

        private AudioSource audioSource;
        private float volume;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            audioSource = GetComponent<AudioSource>();
        
            // Загрузите сохраненное значение громкости музыки или установите значение по умолчанию
            volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        
            // Примените громкость к AudioSource
            audioSource.volume = volume;
        }

        public void PlayMusic(AudioClip music)
        {
            audioSource.clip = music;
            audioSource.Play();
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        public void SetVolume(float value)
        {
            volume = value;
            audioSource.volume = volume;
        
            // Сохраните значение громкости музыки
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }
}
