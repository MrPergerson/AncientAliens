using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientAliens.UFO
{
    public class SimpleMove : MonoBehaviour
    {
        [SerializeField, Range(0f, 20f)] float moveSpeed = 3f;
        //[SerializeField, Range(0f, 20f)] float maxAcceleration = 10f;

        Vector3 direction = Vector3.zero;

        private void Update()
        {
           transform.position = transform.position + direction * moveSpeed * Time.deltaTime;
        }

        private void LateUpdate()
        {
            direction = Vector3.zero;
        }

        public void MoveToDirection(Vector3 direction)
        {
            this.direction = direction;
            this.direction.Normalize();

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }


    }
}
