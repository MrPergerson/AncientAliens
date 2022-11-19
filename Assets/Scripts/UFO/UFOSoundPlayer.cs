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

        private AudioSource AMB_audioSource_01;
        private AudioSource AMB_audioSource_02;
        private AudioSource SFX_audioSource_01;

        private void Awake()
        {
            GameObject ambAS01 = new GameObject("AMB_Audio 01");
            GameObject ambAS02 = new GameObject("AMB_Audio 02");
            GameObject sfxAS = new GameObject("SFX_Audio");

            ambAS01.transform.parent = this.transform;
            ambAS02.transform.parent = this.transform;
            sfxAS.transform.parent = this.transform;

            ambAS01.transform.localPosition = Vector3.zero;
            ambAS02.transform.localPosition = Vector3.zero;
            sfxAS.transform.localPosition = Vector3.zero;

            AMB_audioSource_01 = ambAS01.AddComponent<AudioSource>();
            AMB_audioSource_02 = ambAS01.AddComponent<AudioSource>();
            SFX_audioSource_01 = sfxAS.AddComponent<AudioSource>();

            if (AMB_Mixer)
            {
                AMB_audioSource_01.outputAudioMixerGroup = AMB_Mixer;
                AMB_audioSource_02.outputAudioMixerGroup = AMB_Mixer;
            }
            if (SFX_Mixer) SFX_audioSource_01.outputAudioMixerGroup = SFX_Mixer;


        }

        public void PlayAMB01(AudioClip clip)
        {
            //audioSource.Stop();
            AMB_audioSource_01.clip = clip;
            
            AMB_audioSource_01.loop = true;
            AMB_audioSource_01.Play();
        }

        public void PlayAMB02(AudioClip clip)
        {
            //audioSource.Stop();
            AMB_audioSource_02.clip = clip;

            AMB_audioSource_02.loop = true;
            AMB_audioSource_02.Play();
        }

        public void MuteAMB01(bool value)
        {

            AMB_audioSource_01.mute = value;
        }

        public void MuteAMB02(bool value)
        {
            AMB_audioSource_02.mute = value;
        }

        public void PlayPickUpSoundForType(string type)
        {
            if(type == "People")
            {
                int randomIndex = Random.Range(0, SFX_Pickup_Villager.Count-1);

                SFX_audioSource_01.PlayOneShot(SFX_Pickup_Villager[randomIndex]);
            }
            else if (type == "SandBrick")
            {
                int randomIndex = Random.Range(0, SFX_Pickup_SandBrick.Count - 1);

                SFX_audioSource_01.PlayOneShot(SFX_Pickup_SandBrick[randomIndex]);
            }
            else
            {
                SFX_audioSource_01.PlayOneShot(SFX_Pickup);
            }
        }

        public void PlayDropSoundForType(string type)
        {
            if (type == "People")
            {
                SFX_audioSource_01.PlayOneShot(SFX_Drop);
            }
            else if (type == "SandBrick")
            {
                int randomIndex = Random.Range(0, SFX_Drop_SandBrick.Count - 1);

                SFX_audioSource_01.PlayOneShot(SFX_Drop_SandBrick[randomIndex]);
            }
            else
            {
                SFX_audioSource_01.PlayOneShot(SFX_Drop);
            }
        }
    }
}
