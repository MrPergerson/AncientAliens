using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientAliens.UFO
{
    public class SimpleMove : MonoBehaviour
    {
        //[SerializeField, Range(0f, 20f)] float maxAcceleration = 10f;

        Vector3 direction = Vector3.zero;

        public bool isMoving;

        public delegate void StartedMoving();
        public event StartedMoving onStartedMoving;

        public delegate void StoppedMoving();
        public event StartedMoving onStoppedMoving;

        private void Start()
        {

            GameRules.ufoCurrentSpeed = GameRules.ufoMaxSpeed;


        }

        private void Update()
        {
            var gm = GameManager.Instance;
            if (gm.GameStarted && !gm.GameOver)
                transform.position = transform.position + direction * GameRules.ufoCurrentSpeed * Time.deltaTime;
        }

        private void LateUpdate()
        {
            direction = Vector3.zero;
        }

        public void MoveToDirection(Vector3 direction)
        {
            
            this.direction = direction;
            //this.direction.Normalize();

            var magnitude = direction.sqrMagnitude;
            if (magnitude > 0 && isMoving == false)
            {
                isMoving = true;
                onStartedMoving?.Invoke();

            }
            else if(magnitude == 0 && isMoving == true)
            {
                isMoving = false;
                onStoppedMoving?.Invoke();
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }


    }
}
