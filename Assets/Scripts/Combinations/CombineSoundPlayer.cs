using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AncientAliens.Combinations
{
    [RequireComponent(typeof(AudioSource))]
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

            SFX_audioSource = GetComponent<AudioSource>();


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
            if (SFX_Combine != null && SFX_Combine.Count > 0)
            {
                playingLoop = true;

                if (SFX_Combine.Count == 1)
                {
                    SFX_audioSource.clip = SFX_Combine[0];
                    SFX_audioSource.Play();
                }
                else
                {
                    StartCoroutine(ProcessCombineSounds());

                }


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
