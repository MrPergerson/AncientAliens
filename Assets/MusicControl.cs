using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class MusicControl : MonoBehaviour
    {
        AudioSource audioSource;
        private float startingVolume;
        private float volume;

        [SerializeField] float fadeInTime = 2;
        float starttime;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            startingVolume = audioSource.volume;
            audioSource.volume = 0;

            StartCoroutine(fadeInVolume());
        }

        IEnumerator fadeInVolume()
        {
            starttime = Time.time;
            float lerpValue = 0;

            while (Time.time - starttime < fadeInTime)
            {
                lerpValue = (Time.time - starttime) / fadeInTime;

                audioSource.volume = Mathf.Lerp(0, startingVolume, lerpValue);

                yield return new WaitForEndOfFrame();

            }
        }
    }
}
