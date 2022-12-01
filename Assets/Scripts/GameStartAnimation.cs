using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class GameStartAnimation : MonoBehaviour
    {
        [SerializeField] float animationLength;
        [SerializeField] Camera cam;
        [SerializeField] Transform start;
        [SerializeField] Transform end;
        float starttime;

        public delegate void AnimationFinished();
        public event AnimationFinished onAnimationFinished;



        public void StartAnimation()
        {
            
            StartCoroutine(PlayAnimation());
            //start = cam.transform;
        }

        IEnumerator PlayAnimation()
        {
            GameManager.Instance.SetUpLevel();

            float lerpTime = 0;

            starttime = Time.time;

            
            while (Time.time - starttime < animationLength)
            {
                lerpTime = (Time.time - starttime) / animationLength;
                cam.transform.position = Vector3.Lerp(start.position, end.position, lerpTime);
                cam.transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, lerpTime);
                yield return new WaitForEndOfFrame();

            }

            
            GameManager.Instance.StartGame();
        }
    }
}
