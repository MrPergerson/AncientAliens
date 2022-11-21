using AncientAliens.GridSystem;
using AncientAliens.TileObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.Combinations
{
    public class PeopleAndSandBrickCombine : TileObjectCombine
    {
        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.Location = location;
            transform.position = location.center;
            location.isLocked = true;
            combineTime = GameRules.peopleAndBrickCombineTime;

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

            var newTileObject = Instantiate(output, Location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObj))
            {

                Location.ClearTile();
                tileObjA.DestroySelf();
                tileObjB.DestroySelf();

                Location.AddTileObject(tileObj);

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
