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

        [SerializeField] GameObject progress20;
        [SerializeField] GameObject progress40;
        [SerializeField] GameObject progress60;
        [SerializeField] GameObject progress100;

        GameObject currentModel; 

        float starttime;

        AudioSource audioSource;

        Color normalColor;

        MeshRenderer meshRenderer;

        void Start()
        {

            audioSource = GetComponent<AudioSource>();
            //UpdateModel(GameManager.Instance.WonderBuildProgress);

        }

        public void UpdateModel(int progress)
        {

            if(progress >= 100)
            {
                currentModel = progress100;
                progress100.SetActive(true);
                progress60.SetActive(false);
                progress40.SetActive(false);
                progress20.SetActive(false);
                
            }
            else if(progress >= 60)
            {
                currentModel = progress60;
                progress100.SetActive(false);
                progress60.SetActive(true);
                progress40.SetActive(false);
                progress20.SetActive(false);
            }
            else if(progress >= 40)
            {
                currentModel = progress40;
                progress100.SetActive(false);
                progress60.SetActive(false);
                progress40.SetActive(true);
                progress20.SetActive(false);
            }
            else
            {
                currentModel = progress20;
                progress100.SetActive(false);
                progress60.SetActive(false);
                progress40.SetActive(false);
                progress20.SetActive(true);
            }

            UpdateModelReferences();
        }

        void UpdateModelReferences()
        {
            meshRenderer = currentModel.GetComponentInChildren<MeshRenderer>();
            normalColor = meshRenderer.material.color;
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

            while (Time.time - starttime < flashTime)
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
