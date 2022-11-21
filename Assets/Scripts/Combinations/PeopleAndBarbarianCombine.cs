using AncientAliens.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.TileObjects;

namespace AncientAliens.Combinations
{
    public class PeopleAndBarbarianCombine : TileObjectCombine
    {

        BarbarianAI barbarianAI;


        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.Location = location;
            transform.position = location.center;
            location.isLocked = true;
            combineTime = GameRules.peopleAndBarbarianCombineTime;

            var barbarian = tileObjA.Type == "Barbarian" ? tileObjA : tileObjB;

            if(barbarian.TryGetComponent(out BarbarianAI barbarianAI))
            {
                this.barbarianAI = barbarianAI;
            } else { Debug.LogError("TileObject is missing barbarianAI component"); }

            barbarianAI.isCombining = true;


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
            barbarianAI.isCombining = false;

            if (tileObjA.Type == "People")
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
                Location.RemoveTileObject(tileObjA);
                tileObjA.DestroySelf();
            }
            if (tileObjB.Value <= 0)
            {
                Location.RemoveTileObject(tileObjB);
                tileObjB.DestroySelf();
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
