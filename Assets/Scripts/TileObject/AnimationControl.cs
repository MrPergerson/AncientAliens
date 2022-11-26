using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class AnimationControl : MonoBehaviour
    {
        Animator[] animators;
        public int idleBlendCount = 0;

        private void Awake()
        {
            animators = new Animator[transform.childCount];

            for(int i = 0; i < transform.childCount; i++)
            {

                animators[i] = transform.GetChild(i).GetComponent<Animator>();
            }

           
            for(int i = 0; i < animators.Length; i++)
            {
                
                float startPoint = Random.Range(0f, 1f);
                animators[i].Play("Idle", -1, startPoint);
                animators[i].SetFloat("IdleBlend", Random.Range(0, idleBlendCount));
            }

        }
    }
}
