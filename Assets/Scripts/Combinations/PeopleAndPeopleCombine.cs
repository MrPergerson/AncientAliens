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
            this.Location = location;
            transform.position = location.center;
            location.isLocked = true;
            combineTime = GameRules.peopleAndPeopleCombineTime;

            tileObjA.transform.Rotate(new Vector3(0, 90, 0));

            StartCoroutine(ProcessCombineAction());
            StartCoroutine(CombineTimer());

            tileObjA.aniControl.PlayCombiningPeopleAnimation();
            tileObjB.aniControl.PlayCombiningPeopleAnimation();

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

            var newTileObject = Instantiate(output, Location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObjOutput))
            {
                Location.ClearTile();
                Location.AddTileObject(tileObjOutput);


                tileObjA.aniControl.PlayIdleAnimation();
                tileObjB.aniControl.PlayIdleAnimation();

                tileObjA.transform.Rotate(new Vector3(0, 0, 0));

                // GetClosestEmptyTile() is limited to only the adjcent tiles. If all 8 tiles are full, then destroy the tileObject.
                // This is not intended design, but a temporary solution
                var tile1 = Location.GetClosestEmptyTile();
                if (tile1 != null) tile1.AddTileObject(tileObjA);
                else tileObjA.DestroySelf();

                var tile2 = Location.GetClosestEmptyTile();
                if (tile2 != null) tile2.AddTileObject(tileObjB);
                else tileObjB.DestroySelf();

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
