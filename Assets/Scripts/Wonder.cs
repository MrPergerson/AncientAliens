using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class Wonder : MonoBehaviour
    {

        [SerializeField] Color hitFlash;
        [SerializeField] float flashLength = .5f;

        [SerializeField] AudioClip hit01;
        [SerializeField] AudioClip hit02;
        float starttime;

        AudioSource audioSource;

        Color normalColor;

        MeshRenderer meshRenderer;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            meshRenderer = GetComponent<MeshRenderer>();
            normalColor = meshRenderer.material.color;


        }


        void Update()
        {
            //meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, hitFlash, .1f);

        }

        public void Flash()
        {
            StartCoroutine(ProcessFlash());
        }

        IEnumerator ProcessFlash()
        {
            audioSource.PlayOneShot(hit01);
            audioSource.PlayOneShot(hit02);

            float lerpTime = 0;

            starttime = Time.time;

            var flashTime = flashLength / 2;

            while (Time.time - starttime < flashLength)
            {
                lerpTime = (Time.time - starttime) / flashTime;
                meshRenderer.material.color = Color.Lerp(normalColor, hitFlash, lerpTime);
                yield return new WaitForEndOfFrame();

            }

            starttime = Time.time;

            while (Time.time - starttime < flashTime)
            {
                lerpTime = (Time.time - starttime) / flashTime;
                meshRenderer.material.color = Color.Lerp(hitFlash, normalColor, lerpTime);
                yield return new WaitForEndOfFrame();

            }


        }
    }
}
