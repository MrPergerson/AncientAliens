using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AncientAliens.TileObjects
{
    public class TileObjectSoundPlayer : MonoBehaviour
    {
        public AudioMixerGroup SFX_Mixer;

        public AudioClip SFX_OnCreate;
        public AudioClip SFX_OnDestroy;
        public AudioClip SFX_OnMove;

        private AudioSource SFX_audioSource;

        private void Awake()
        {
            GameObject sfxAS = new GameObject("SFX_Audio");

            sfxAS.transform.parent = this.transform;

            sfxAS.transform.localPosition = Vector3.zero;

            SFX_audioSource = sfxAS.AddComponent<AudioSource>();

            if (SFX_Mixer) SFX_audioSource.outputAudioMixerGroup = SFX_Mixer;
        }

        public void PlayOnCreateSFX()
        {
            if(SFX_OnCreate)
            {
                SFX_audioSource.PlayOneShot(SFX_OnCreate);

            }
        }

        public void PlayOnDestroySFX()
        {
            if(SFX_OnDestroy)
            {
                SFX_audioSource.PlayOneShot(SFX_OnDestroy);

            }
        }

        public void PlayOnMoveSFX()
        {
            if(SFX_OnMove)
            {
                SFX_audioSource.PlayOneShot(SFX_OnMove);

            }
        }

    }
}
