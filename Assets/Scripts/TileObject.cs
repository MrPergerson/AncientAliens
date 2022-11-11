using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class TileObject : MonoBehaviour
    {
        [SerializeField] Tile _tile;
        //TileObjectRules rules;
        [SerializeField] bool _canBeMoved = true;
        [SerializeField] int _value = 1;
        [SerializeField] bool _canShareTile = true;
        [SerializeField] enum CombineMethod { SameTile, AdjcentTile };

        public bool CanBeMoved {
            get { return _canBeMoved; }
            private set { _canBeMoved = value; }
        }

        public bool CanShareTile
        {
            get { return _canShareTile; }
            private set { _canShareTile = value; }
        }

        public int value
        {
            get { return _value; }
            private set { _value = value; }
        }

        public Tile Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;
            }
        }

    }
}
