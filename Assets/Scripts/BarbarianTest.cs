using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AncientAliens.GridSystem;

namespace AncientAliens
{
    public class BarbarianTest : MonoBehaviour
    {
        [SerializeField]GameObject barbarian;
        [SerializeField] Vector2 targetIndex;

        private void Start()
        {
            var tileObject = Instantiate(barbarian);
            EasyGrid.AssignTileObjectToTile(tileObject, 8, 0);

            var pathMovement = tileObject.GetComponent<PathMovement>();
            pathMovement.GetPathTo(EasyGrid.GetTileAt(targetIndex));

        }
    }
}
