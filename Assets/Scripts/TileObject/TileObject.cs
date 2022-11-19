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
        [SerializeField] int _value = 1;
        [SerializeField] bool _canShareTile = true;
        [SerializeField] enum CombineMethod { SameTile, AdjcentTile };

        TileObjectSoundPlayer soundPlayer;
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

        public bool CanShareTile
        {
            get { return _canShareTile; }
            private set { _canShareTile = value; }
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
        }

        private void Start()
        {
            if (Type == "People")
                GameManager.Instance.PeopleCount++;

            if (playsSound)
                soundPlayer.PlayOnCreateSFX();
        }

        private void OnDestroy()
        {
            if (Type == "People")
                GameManager.Instance.PeopleCount--;

            if (playsSound)
                soundPlayer.PlayOnDestroySFX();
        }

    }
}
