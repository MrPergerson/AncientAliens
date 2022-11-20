using AncientAliens.GridSystem;
using AncientAliens.TileObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.Combinations
{
    public class SandBrickAndBarbarianCombine : TileObjectCombine
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
            //StartCoroutine(CombineTimer());
        }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForEndOfFrame();

            location.isLocked = false;


            var barbarian = tileObjA.Type == "Barbarian" ? tileObjA : tileObjB;

            if(!location.PeekAtTopTileObject().Equals(barbarian))
            {
                location.RemoveTileObject(barbarian);
                barbarian.DestroySelf();

                if (playsSound)
                {
                    soundPlayer.StopCombineLoopSFX();
                }
            }
 
            //HideTimer();

            Destroy(gameObject, 2);
            


        }
    }
}
