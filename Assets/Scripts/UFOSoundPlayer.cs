using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AncientAliens
{
    [RequireComponent(typeof(AudioSource))]
    public class UFOSoundPlayer : MonoBehaviour
    {
        public AudioMixer AMB_Mixer;
        public AudioMixer SFX_Mixer;
        public AudioClip AMB_UFO_Flyaround;
        public AudioClip AMB_UFO_Idle;
        public AudioClip SFX_Drop;
        public AudioClip SFX_Pickup;
        public AudioClip SFX_Pickup_Rock;
        public AudioClip SFX_Drop_Rock;
        public AudioClip SFX_Pickup_Villager;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAMB(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }


    }
}
