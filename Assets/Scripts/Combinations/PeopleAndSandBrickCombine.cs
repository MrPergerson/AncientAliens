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

            yield return new WaitForSeconds(combineTime);

            location.isLocked = false;

            var newTileObject = Instantiate(output, location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObj))
            {

                location.ClearTile();
                Destroy(tileObjA.gameObject);
                Destroy(tileObjB.gameObject);

                location.AddTileObject(tileObj);

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
