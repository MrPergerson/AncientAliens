using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;


namespace AncientAliens.Combinations
{
    public class SandBrickAndWonderCombine : TileObjectCombine
    {
        public override void Execute(TileObject a, TileObject b, Tile location)
        {
            tileObjA = a;
            tileObjB = b;
            this.location = location;
            transform.position = location.center;
            location.isLocked = true;

            StartCoroutine(ProcessCombineAction());
            
        }

        protected override IEnumerator ProcessCombineAction()
        {

            yield return new WaitForEndOfFrame();

            location.isLocked = false;

            GameManager.Instance.WonderBuildProgress += 10;

            var sandBrick = location.ExtractTopTileObject(); // assuming sandbrick was placed on the top
            //print(sandBrick.Type);

            Destroy(sandBrick.gameObject);
            Destroy(gameObject);


        }
    }
}
