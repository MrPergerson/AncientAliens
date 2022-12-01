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
            this.Location = location;
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

            Location.isLocked = false;            

            var newTileObject1 = Instantiate(output, Location.center, Quaternion.identity);
            if (newTileObject1.TryGetComponent(out TileObject tileObjOutput1))
            {
                // GetClosestEmptyTile() is limited to only the adjcent tiles. If all 8 tiles are full, then destroy the tileObject.
                // This is not intended design, but a temporary solution
                var tile1 = Location.GetClosestEmptyTile();
                if (tile1 != null) tile1.AddTileObject(tileObjOutput1);
                else tileObjOutput1.DestroySelf();

            }

            Location.ClearTile();
            if(tileObjA.Type == "City")
            {
                Location.AddTileObject(tileObjA);
                Location.AddTileObject(tileObjB);
            }
            else
            {
                Location.AddTileObject(tileObjB);
                Location.AddTileObject(tileObjA);
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
