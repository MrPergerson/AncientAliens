using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace AncientAliens
{
    public class Settings : MonoBehaviour
    {
        public Slider MasterVolume;
        public Slider MusicVolume;
        public Slider SFXVolume;

        public AudioMixerGroup master;
        public AudioMixerGroup music;
        public AudioMixerGroup sfx;

        private void Start()
        {
            MasterVolume.onValueChanged.AddListener(UpdateMasterMixer);
            MasterVolume.minValue = .0001f;
            MasterVolume.maxValue = 1;
            MasterVolume.value = 1;

            MusicVolume.onValueChanged.AddListener(UpdateMusicMixer);
            MusicVolume.minValue = .0001f;
            MusicVolume.maxValue = 1;
            MusicVolume.value = 0.2407442f;

            SFXVolume.onValueChanged.AddListener(UpdateSFXMixer);
            SFXVolume.minValue = .0001f;
            SFXVolume.maxValue = 1;
            SFXVolume.value = 1;
        }

        private void UpdateMasterMixer(float value)
        {
            master.audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
        }

        private void UpdateMusicMixer(float value)
        {
            music.audioMixer.SetFloat("AMB", Mathf.Log10(value) * 20);
        }

        private void UpdateSFXMixer(float value)
        {
            sfx.audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        }

    }
}
