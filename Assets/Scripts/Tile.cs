using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class Tile
    {
        public Vector2 index;
        public Vector3 position;
        public Vector3 center;

        private float size;


        public Tile(Vector2 index, Vector3 position, float size)
        {
            this.index = index;
            this.position = position;
            this.size = size;

            center = new Vector3(position.x + size / 2, 0, position.z + size / 2);
        }

        public override bool Equals(object obj)
        {
            var otherTile = obj as Tile;

            if (otherTile != null)
            {
                return this.position == otherTile.position;
            }
            else
            {
                return base.Equals(obj);

            }

        }

        public override int GetHashCode()
        {
            return this.position.GetHashCode();
        }
    }
}
