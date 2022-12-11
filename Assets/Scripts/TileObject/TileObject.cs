using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;


namespace AncientAliens.TileObjects
{
    public class TileObject : MonoBehaviour
    {
        [SerializeField] string _type;
        [SerializeField] Tile _tile;
        //TileObjectRules rules;
        [SerializeField] bool _canBeMoved = true;
        int _value = 1;

        Transform highLight;

        public TileObjectSoundPlayer soundPlayer;
        public AnimationControl aniControl;
        [SerializeField] protected bool playsSound;

        public string Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        public bool CanBeMoved {
            get { return _canBeMoved; }
            private set { _canBeMoved = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Tile Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;
            }
        }

        private void Awake()
        {
            if(TryGetComponent(out TileObjectSoundPlayer soundPlayer))
            {
                this.soundPlayer = soundPlayer;
                playsSound = true;
            }

            aniControl = GetComponentInChildren<AnimationControl>();

            transform.parent = GameManager.Instance.TileObjContainer;
            


            for(int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Highlight")
                {
                    highLight = transform.GetChild(i);
                    highLight.gameObject.SetActive(GameManager.Instance.HighlightTiles);
                }
            }
            
        }

        private void Start()
        {

            switch(Type)
            {
                case "People":
                    GameManager.Instance.PeopleCount++;
                    Value = GameRules.peopleValue;
                    break;
                case "SandStone":
                    Value = GameRules.rockValue;
                    break;
                case "SandBrick":
                    Value = GameRules.brickValue;
                    break;
                case "Barbarian":
                    Value = GameRules.barbarianValue;
                    break;
                case "City":
                    Value = GameRules.cityValue;
                    break;
            }


            if (playsSound)
                soundPlayer.PlayOnCreateSFX();
        }

        public void DestroySelf()
        {

            if (Type == "People")
                GameManager.Instance.PeopleCount--;

            if (playsSound)
                soundPlayer.PlayOnDestroySFX();

            Destroy(this.gameObject);
        }


        public void ShowHighlight(bool show)
        {

           highLight.gameObject.SetActive(show);

        }

    }
}
