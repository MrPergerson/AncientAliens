using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;
using AncientAliens.TileObjects;

namespace AncientAliens.Combinations
{
    public class TestCombine : TileObjectCombine
    {

        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.Location = location;
            transform.position = location.center;
            location.isLocked = true;

            StartCoroutine(ProcessCombineAction());
        }

        protected override IEnumerator ProcessCombineAction()
        {
            print("combining");   
            yield return new WaitForSeconds(2);
            print("finished");
            Location.isLocked = false;

            var newTileObject = Instantiate(output, Location.center, Quaternion.identity);
            if(newTileObject.TryGetComponent(out TileObject tileObj))
            {
                Location.ClearTile();
                Location.AddTileObject(tileObj);
            }

            Destroy(tileObjA.gameObject);
            Destroy(tileObjB.gameObject);
            Destroy(gameObject);
        }
    }
}
