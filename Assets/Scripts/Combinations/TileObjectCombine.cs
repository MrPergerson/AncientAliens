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
        protected TileObject tileObjA;
        protected TileObject tileObjB;
        [SerializeField] protected GameObject output;
        protected Tile _location;
        protected float combineTime = 1;
        protected Image progressGraphic;
        protected float starttime;

        protected CombineSoundPlayer soundPlayer;
        [SerializeField] protected bool playsSound;

        protected virtual void Awake()
        {
            progressGraphic = transform.GetChild(0).GetChild(1).GetComponent<Image>();
           
            if(TryGetComponent(out CombineSoundPlayer soundPlayer))
            {
                this.soundPlayer = soundPlayer;
                playsSound = true;
            }

        }

        public Tile Location
        {
            get { return _location; }
            protected set { _location = value; }
        }

        public abstract void Execute(TileObject a, TileObject b, Tile location);

        public virtual void Cancel()
        {
            StopAllCoroutines();         
            Location.isLocked = false;

            Location.ClearTile();

            tileObjA.DestroySelf();
            tileObjB.DestroySelf();
            
            if (playsSound)
                soundPlayer.PlayCombineCancelSFX();

            Destroy(this.gameObject);
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

        private void OnDestroy()
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.activeCombinations.Remove(this);
            }
        }
    }

}
