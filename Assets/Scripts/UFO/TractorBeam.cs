using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;
using AncientAliens.TileObjects;

namespace AncientAliens
{
    public class TractorBeam : MonoBehaviour
    {
        TileObject tileObject;
        [SerializeField] bool DEBUG;

        [SerializeField] GameObject tractorBeamVFX01;
        [SerializeField] GameObject tractorBeamVFX02;
        [SerializeField] GameObject tractorBeamVFX03;
        ParticleSystem tractorBeam01;
        ParticleSystem tractorBeam02;
        ParticleSystem tractorBeam03;
        [SerializeField] Transform tractorBeamVFXAnchor;

        private void Awake()
        {
            tractorBeam01 = Instantiate(tractorBeamVFX01, tractorBeamVFXAnchor).GetComponent<ParticleSystem>();
            tractorBeam02 = Instantiate(tractorBeamVFX02, tractorBeamVFXAnchor).GetComponent<ParticleSystem>();          
            tractorBeam02.gameObject.SetActive(false);

            tractorBeam03 = Instantiate(tractorBeamVFX03, tractorBeamVFXAnchor).GetComponent<ParticleSystem>();
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 4f, LayerMask.GetMask("Ground")))
            {
                tractorBeam03.gameObject.transform.position = hit.point;
            }
        }

        public Tile GetTileBelow()
        {

            Tile tile = EasyGrid.GetTileAt(transform.position);

            //print(tile.index);


            return tile;
        }

        public string GetTileObjectType()
        {
            if (HasTileObject())
                return tileObject.Type;
            else
                return "";
        }

        public bool HasTileObject()
        {
            return tileObject != null;
        }

        public bool PickUpTileObject()
        {
            var tile = GetTileBelow();

            if (tile == null)
            {
                if (DEBUG) print(gameObject + ": Tile is NULL");
                return false;
            }

            var tileObject = tile.ExtractTopTileObject();

            if (tileObject == null)
            {
                if (DEBUG) print(gameObject + ": Failed to get tileObject");
                return false;
            }

            if (tileObject.Type == "SandBrick")
                GameRules.ufoCurrentSpeed *= GameRules.ufoSandBrickSlowDown;
            else if (tileObject.Type == "People")
                tileObject.aniControl.PlayFloatingAnimation();

            this.tileObject = tileObject;
            SetTileObjectPositionToTractorBeam();

            tractorBeam01.gameObject.SetActive(false);
            tractorBeam02.gameObject.SetActive(true);

            return true;
        }

        public bool DropTileObject()
        {
            var tile = GetTileBelow();

            if (tile == null)
            {
                if(DEBUG) print(gameObject + ": Tile is NULL");
                return false;
            }

            // IF pyramid ... this doesn't belong here but it's an easy solution

            var result = tile.AddTileObject(tileObject);

            if(result == true)
            {
                SetTileObjectPositionToTile(tile);
                GameRules.ufoCurrentSpeed = GameRules.ufoMaxSpeed;
                
                tractorBeam01.gameObject.SetActive(true);
                tractorBeam02.gameObject.SetActive(false);

                if (tileObject.Type == "People")
                    tileObject.aniControl.PlayIdleAnimation();


                tileObject = null;

                return true;
            }
            else
            {
                if (DEBUG) print(gameObject + ": Failed to add tileObject to tile at " + tile.index);
                return false;
            }

        }

        private void SetTileObjectPositionToTractorBeam()
        {
            tileObject.transform.parent = this.transform;

            if(tileObject.Type == "People")
            {
                tileObject.transform.localPosition = new Vector3(0, -1.25f, -0.17f);
            }
            else
            {
                tileObject.transform.localPosition = new Vector3(0, -0.8f, 0);

            }
        }

        private void SetTileObjectPositionToTile(Tile tile)
        {
            tileObject.transform.parent = null;
            tileObject.transform.position = tile.center;
        }
    }


}
