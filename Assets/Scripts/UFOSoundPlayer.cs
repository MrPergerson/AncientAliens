using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AncientAliens
{
    public class UFOSoundPlayer : MonoBehaviour
    {
        public AudioMixerGroup AMB_Mixer;
        public AudioMixerGroup SFX_Mixer;
        public AudioClip AMB_UFO_Flyaround;
        public AudioClip AMB_UFO_Idle;
        public AudioClip SFX_Drop;
        public AudioClip SFX_Pickup;
        public List<AudioClip> SFX_Pickup_SandBrick;
        public List<AudioClip> SFX_Drop_SandBrick;
        public List<AudioClip> SFX_Pickup_Villager;

        private AudioSource AMB_audioSource;
        private AudioSource SFX_audioSource;

        private void Awake()
        {
            GameObject ambAS = new GameObject("AMB_Audio");
            GameObject sfxAS = new GameObject("SFX_Audio");

            ambAS.transform.parent = this.transform;
            sfxAS.transform.parent = this.transform;

            ambAS.transform.localPosition = Vector3.zero;
            sfxAS.transform.localPosition = Vector3.zero;

            AMB_audioSource = ambAS.AddComponent<AudioSource>();
            SFX_audioSource = sfxAS.AddComponent<AudioSource>();

            if (AMB_Mixer) AMB_audioSource.outputAudioMixerGroup = AMB_Mixer;
            if (SFX_Mixer) SFX_audioSource.outputAudioMixerGroup = SFX_Mixer;
        }

        public void PlayAMB(AudioClip clip)
        {
            //audioSource.Stop();
            AMB_audioSource.clip = clip;
            
            AMB_audioSource.loop = true;
            AMB_audioSource.Play();
        }

        public void PlayPickUpSoundForType(string type)
        {
            if(type == "People")
            {
                int randomIndex = Random.Range(0, SFX_Pickup_Villager.Count-1);

                SFX_audioSource.PlayOneShot(SFX_Pickup_Villager[randomIndex]);
            }
            else if (type == "SandBrick")
            {
                int randomIndex = Random.Range(0, SFX_Pickup_SandBrick.Count - 1);

                SFX_audioSource.PlayOneShot(SFX_Pickup_SandBrick[randomIndex]);
            }
        }

        public void PlayDropSoundForType(string type)
        {
            if (type == "People")
            {
                SFX_audioSource.PlayOneShot(SFX_Drop);
            }
            else if (type == "SandBrick")
            {
                int randomIndex = Random.Range(0, SFX_Drop_SandBrick.Count - 1);

                SFX_audioSource.PlayOneShot(SFX_Drop_SandBrick[randomIndex]);
            }
        }
    }
}
