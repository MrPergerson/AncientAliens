using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens.TileObjects
{
    public class BarbarianAI : MonoBehaviour
    {
        PathMovement path;

        private void Awake()
        {
            path = GetComponent<PathMovement>();
            path.onPathReached += StartDamagingWonder;
        }

        private void OnDestroy()
        {
            path.onPathReached -= StartDamagingWonder;
        }

        private void StartDamagingWonder()
        {
            StartCoroutine(DamageWonder());
        }

        IEnumerator DamageWonder()
        {
            while(path.pathReached)
            {

                if (GameManager.Instance.GamePaused)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                yield return new WaitForSeconds(2);

                if (GameManager.Instance.GamePaused)
                {
                    continue;
                }

                GameManager.Instance.WonderBuildProgress -= 5;

            }
        }
    }
}
