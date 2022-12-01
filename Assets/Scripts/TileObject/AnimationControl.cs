using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class AnimationControl : MonoBehaviour
    {
        Animator[] animators;
        public int idleBlendCount = 0;
        public int attackBlendCount = 0;
        public int peopleBlendCount = 0;

        private void Awake()
        {
            animators = new Animator[transform.childCount];

            for(int i = 0; i < transform.childCount; i++)
            {
                var animator = transform.GetChild(i).GetComponent<Animator>();

                if(animator != null)
                {
                    animators[i] = animator;                  
                }


            }


            PlayIdleAnimation();

        }

        public void PlayIdleAnimation()
        {

            for (int i = 0; i < animators.Length; i++)
            {
                float startPoint = Random.Range(0f, 1f);
                animators[i].Play("Idle", 0, startPoint);
                animators[i].SetFloat("IdleBlend", Random.Range(0, idleBlendCount));
            }
        }

        public void PlayCombiningAttackingAnimation()
        {

            for (int i = 0; i < animators.Length; i++)
            {
                float startPoint = Random.Range(0f, 1f);
                animators[i].Play("Attacking", 0, startPoint);
                animators[i].SetFloat("AttackBlend", Random.Range(0, attackBlendCount));
            }


        }

        public void PlayCombiningMiningAnimation()
        {

            for (int i = 0; i < animators.Length; i++)
            {

                float startPoint = Random.Range(0f, 1f);
                animators[i].Play("Mining", 0, startPoint);
            }


        }

        public void PlayFloatingAnimation()
        {

            for (int i = 0; i < animators.Length; i++)
            {

                float startPoint = Random.Range(0f, 1f);
                animators[i].Play("Floating", 0, startPoint);
            }


        }

        public void PlayCombiningPeopleAnimation()
        {

            for (int i = 0; i < animators.Length; i++)
            {
                float startPoint = Random.Range(0f, 1f);
                animators[i].Play("PeopleCombining", 0, startPoint);
                animators[i].SetFloat("PeopleBlend", Random.Range(0, peopleBlendCount));
            }           
        }
    }
}
