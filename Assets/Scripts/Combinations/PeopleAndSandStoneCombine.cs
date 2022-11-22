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
            this.Location = location;
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

            Location.isLocked = false;

            var newTileObject = Instantiate(output, Location.center, Quaternion.identity);
            if (newTileObject.TryGetComponent(out TileObject tileObj))
            {

                var people = Location.ExtractTopTileObject();
                people.DestroySelf();

                TileObject tileStone = tileObjA.Type == "SandStone" ? tileObjA : tileObjB;
                tileStone.Value -= 10;

                if(tileStone.Value <= 0)
                {
                    Location.ExtractTopTileObject();
                    tileStone.DestroySelf();
                }

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

        public override void Cancel()
        {
            StopAllCoroutines();
            Location.isLocked = false;

            var people = Location.ExtractTopTileObject();
            people.DestroySelf();

            if (playsSound)
                soundPlayer.PlayCombineCancelSFX();

            Destroy(this.gameObject);
        }
    }
}
