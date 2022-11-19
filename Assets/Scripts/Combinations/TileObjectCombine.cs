using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;
using UnityEngine.UI;
using AncientAliens.TileObjects;

namespace AncientAliens.Combinations
{
    public abstract class TileObjectCombine : MonoBehaviour
    {
        [SerializeField] protected TileObject tileObjA;
        [SerializeField] protected TileObject tileObjB;
        [SerializeField] protected GameObject output;
        [SerializeField] protected Tile location;
        [SerializeField] protected float combineTime = 2;
        protected Image progressGraphic;
        protected float starttime;

        protected CombineSoundPlayer soundPlayer;
        [SerializeField] protected bool playsSound;

        protected virtual void Awake()
        {
            progressGraphic = GetComponentInChildren<Image>();
           
            if(TryGetComponent(out CombineSoundPlayer soundPlayer))
            {
                this.soundPlayer = soundPlayer;
                playsSound = true;
            }

        }

        public abstract void Execute(TileObject a, TileObject b, Tile location);

        public virtual void Cancel()
        {
            StopAllCoroutines();
            location.isLocked = false;
            
            if (playsSound)
                soundPlayer.PlayCombineCancelSFX();
        }

        protected abstract IEnumerator ProcessCombineAction();

        protected virtual IEnumerator CombineTimer()
        {
            progressGraphic.fillAmount = 0;
            starttime = Time.time;

            while (Time.time - starttime < combineTime)
            {
                progressGraphic.fillAmount = (Time.time - starttime) / combineTime;
                yield return new WaitForEndOfFrame();

            }
        }

        protected virtual void HideTimer()
        {
            progressGraphic.gameObject.SetActive(false);
        }
    }

}
