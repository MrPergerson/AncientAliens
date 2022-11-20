using AncientAliens.GridSystem;
using AncientAliens.TileObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.Combinations
{
    public class CityAndPeopleCombine : TileObjectCombine
    {
        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.location = location;
            transform.position = location.center;
            location.isLocked = true;
            combineTime = GameRules.cityAndPeopleCombineTime;

            StartCoroutine(ProcessCombineAction());
            StartCoroutine(CombineTimer());

            if (playsSound)
            {
                soundPlayer.PlayCombineStartSFX();
                soundPlayer.PlayCombineLoopSFX();
            }

        }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForSeconds(combineTime);

            location.isLocked = false;            

            var newTileObject1 = Instantiate(output, location.center, Quaternion.identity);
            if (newTileObject1.TryGetComponent(out TileObject tileObjOutput1))
            {
                // GetClosestEmptyTile() is limited to only the adjcent tiles. If all 8 tiles are full, then destroy the tileObject.
                // This is not intended design, but a temporary solution
                var tile1 = location.GetClosestEmptyTile();
                if (tile1 != null) tile1.AddTileObject(tileObjOutput1);
                else tileObjOutput1.DestroySelf();

            }

            var newTileObject2 = Instantiate(output, location.center, Quaternion.identity);
            if (newTileObject2.TryGetComponent(out TileObject tileObjOutput2))
            {
                var tile1 = location.GetClosestEmptyTile();
                if (tile1 != null) tile1.AddTileObject(tileObjOutput2);
                else tileObjOutput2.DestroySelf();



            }

            location.ClearTile();
            if(tileObjA.Type == "City")
            {
                location.AddTileObject(tileObjA);
                location.AddTileObject(tileObjB);
            }
            else
            {
                location.AddTileObject(tileObjB);
                location.AddTileObject(tileObjA);
            }


            if (playsSound)
            {
                soundPlayer.StopCombineLoopSFX();
                soundPlayer.PlayCombineEndSFX();
            }

            HideTimer();

            Destroy(gameObject, 2);




        }
    }
}
