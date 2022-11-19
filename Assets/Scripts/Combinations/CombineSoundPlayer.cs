using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AncientAliens.Combinations
{
    public class CombineSoundPlayer : MonoBehaviour
    {
        public AudioMixerGroup SFX_Mixer;

        public AudioClip SFX_CombineStart;
        public AudioClip SFX_CombineEnd;
        public AudioClip SFX_CombineCancel;
        public List<AudioClip> SFX_Combine;

        private AudioSource SFX_audioSource;

        private bool playingLoop;

        private void Awake()
        {
            GameObject sfxAS = new GameObject("SFX_Audio");

            sfxAS.transform.parent = this.transform;

            sfxAS.transform.localPosition = Vector3.zero;

            SFX_audioSource = sfxAS.AddComponent<AudioSource>();

            if (SFX_Mixer) SFX_audioSource.outputAudioMixerGroup = SFX_Mixer;
        }

        public void PlayCombineStartSFX()
        {
            if(SFX_CombineStart)
            {
                SFX_audioSource.PlayOneShot(SFX_CombineStart);

            }
        }

        public void PlayCombineLoopSFX()
        {
            if(SFX_Combine != null && SFX_Combine.Count > 0)
            {
                playingLoop = true;
                StartCoroutine(ProcessCombineSounds());

            }    


        }

        private IEnumerator ProcessCombineSounds()
        {
            yield return new WaitForSeconds(Random.Range(.3f, .5f));

            while (playingLoop)
            {
                int randomIndex = Random.Range(0, SFX_Combine.Count - 1);
                SFX_audioSource.PlayOneShot(SFX_Combine[randomIndex]);

                yield return new WaitForSeconds(Random.Range(0.5f, 1f));

            }
        }

        public void StopCombineLoopSFX()
        {
            playingLoop = false;
        }

        public void PlayCombineEndSFX()
        {
            if(SFX_CombineEnd)
            {
                SFX_audioSource.PlayOneShot(SFX_CombineEnd);

            }
        }

        public void PlayCombineCancelSFX()
        {
            if(SFX_CombineCancel)
            {
                SFX_audioSource.PlayOneShot(SFX_CombineCancel);

            }
        }
    }
}
