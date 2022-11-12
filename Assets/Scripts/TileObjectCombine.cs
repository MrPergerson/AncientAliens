using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public abstract class TileObjectCombine : MonoBehaviour
    {
        [SerializeField] protected TileObject tileObjA;
        [SerializeField] protected TileObject tileObjB;
        [SerializeField] protected GameObject output;
        [SerializeField] protected Tile location;

        public abstract void Execute(TileObject a, TileObject b, Tile location);

        public virtual void Cancel()
        {
            StopAllCoroutines();
        }

        protected abstract IEnumerator ProcessCombineAction();
    }

}
