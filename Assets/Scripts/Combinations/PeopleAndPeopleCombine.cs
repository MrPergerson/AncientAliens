using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;
using UnityEngine.UI;
using AncientAliens.TileObjects;

namespace AncientAliens.Combinations
{
    public class PeopleAndPeopleCombine : TileObjectCombine
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

            if (playsSound)
            {
                soundPlayer.PlayCombineStartSFX();
                soundPlayer.PlayCombineLoopSFX();
            }

        }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForSeconds(2);
 
            location.isLocked = false;

            var newTileObject = Instantiate(output, location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObjOutput))
            {
                location.ClearTile();
                location.AddTileObject(tileObjOutput);

                // GetClosestEmptyTile() is limited to only the adjcent tiles. If all 8 tiles are full, then destroy the tileObject.
                // This is not intended design, but a temporary solution
                var tile1 = location.GetClosestEmptyTile();
                if (tile1 != null) tile1.AddTileObject(tileObjA);
                else Destroy(tileObjA.gameObject); 

                var tile2 = location.GetClosestEmptyTile();
                if (tile2 != null) tile2.AddTileObject(tileObjB);
                else Destroy(tileObjB.gameObject);

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
}
