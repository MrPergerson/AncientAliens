using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;
using AncientAliens.TileObjects;

namespace AncientAliens.Combinations
{
    public class SandBrickAndWonderCombine : TileObjectCombine
    {
        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.Location = location;
            transform.position = location.center;
            location.isLocked = true;

            StartCoroutine(ProcessCombineAction());

            // One sound being used for this tile is the StartSFX
            if (playsSound)
            {
                soundPlayer.PlayCombineStartSFX();
            }

    }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForEndOfFrame();

            Location.isLocked = false;

            GameManager.Instance.WonderBuildProgress += 10;

            var sandBrick = Location.ExtractTopTileObject(); // assuming sandbrick was placed on the top
            //print(sandBrick.Type);

            sandBrick.DestroySelf();
            Destroy(gameObject);


        }

        public override void Cancel()
        {
            StopAllCoroutines();
            Location.isLocked = false;

            var sandBrick = Location.ExtractTopTileObject(); // assuming sandbrick was placed on the top
            sandBrick.DestroySelf();

            if (playsSound)
                soundPlayer.PlayCombineCancelSFX();

            Destroy(this.gameObject);
        }
    }
}
