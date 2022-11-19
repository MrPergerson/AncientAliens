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
            StartCoroutine(CombineTimer());
        }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForSeconds(combineTime);

            location.isLocked = false;

            var newTileObject = Instantiate(output, location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObj))
            {

                var people = location.ExtractTopTileObject();
                Destroy(people.gameObject);

                TileObject tileStone = tileObjA.Type == "SandStone" ? tileObjA : tileObjB;
                tileStone.Value -= 10;

                if(tileStone.Value <= 0)
                {
                    location.ExtractTopTileObject();
                    Destroy(tileStone.gameObject);
                }

                location.AddTileObject(tileObj);
                
                Destroy(gameObject);
            }


        }
    }
}
