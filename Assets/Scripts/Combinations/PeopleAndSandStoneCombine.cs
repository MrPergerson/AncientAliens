using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens.Combinations
{
    public class PeopleAndSandStoneCombine : TileObjectCombine
    {
        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.location = location;
            transform.position = location.center;
            location.isLocked = true;

            StartCoroutine(ProcessCombineAction());
        }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForSeconds(2);

            location.isLocked = false;

            var newTileObject = Instantiate(output, location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObj))
            {
                location.ClearTile();
                location.AddTileObject(tileObj);
                
                Destroy(tileObjA.gameObject);
                Destroy(tileObjB.gameObject);
                Destroy(gameObject);
            }


        }
    }
}
