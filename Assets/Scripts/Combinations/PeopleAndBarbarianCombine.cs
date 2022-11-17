using AncientAliens.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.Combinations
{
    public class PeopleAndBarbarianCombine : TileObjectCombine
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

            if(tileObjA.Type == "People")
            {
                tileObjA.Value -= 5;
                tileObjB.Value -= 10;
            }
            else
            {
                tileObjA.Value -= 10;
                tileObjB.Value -= 5;
            }
            
            if(tileObjA.Value <= 0)
            {
                location.RemoveTileObject(tileObjA);
                Destroy(tileObjA.gameObject);
            }
            if (tileObjB.Value <= 0)
            {
                location.RemoveTileObject(tileObjB);
                Destroy(tileObjB.gameObject);
            }

            Destroy(gameObject);

        }
    }
}
