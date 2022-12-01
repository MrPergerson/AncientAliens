using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientAliens
{
    public class RisingIcon : MonoBehaviour
    {
        public float duration = 2f;
        public float speed = .1f;

        private void Start()
        {
            Destroy(this.gameObject, duration);
        }

        private void Update()
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            transform.LookAt(Camera.main.transform);
        }

        private void RotateUnitSprite()
        {
            Vector3 targetVector = Camera.main.transform.position - transform.position;
            float newYAngle = Mathf.Atan2(targetVector.z, targetVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, -1 * newYAngle, 0);
        }
    }
}
