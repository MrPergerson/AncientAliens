using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class TileObject : MonoBehaviour
    {
        Tile _tile;
        //TileObjectRules rules;
        bool _canBeMoved = true;
        int _value = 1;
        bool _canShareTile = true;
        enum CombineMethod { SameTile, AdjcentTile };

        public bool CanBeMoved {
            get { return _canBeMoved; }
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
