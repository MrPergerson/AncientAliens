using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;
using AncientAliens.TileObjects;

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

            combineTime = GameRules.peopleAndRockCombineTIme;

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

                var people = location.ExtractTopTileObject();
                people.DestroySelf();

                TileObject tileStone = tileObjA.Type == "SandStone" ? tileObjA : tileObjB;
                tileStone.Value -= 10;

                if(tileStone.Value <= 0)
                {
                    location.ExtractTopTileObject();
                    tileStone.DestroySelf();
                }

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
