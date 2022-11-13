using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientAliens.UFO
{
    [RequireComponent(typeof(SimpleMove), typeof(TractorBeam), typeof(UFOSoundPlayer)]
    public class UFOController : MonoBehaviour
    {
        Controls controls;
        SimpleMove simpleMove;
        TractorBeam tractorBeam;

        UFOSoundPlayer soundPlayer;

        private void Awake()
        {
            controls = new Controls();
            simpleMove = GetComponent<SimpleMove>();
            tractorBeam = GetComponent<TractorBeam>();
            soundPlayer = GetComponent<UFOSoundPlayer>();
        }

        private void Start()
        {
            soundPlayer.PlayAMB(soundPlayer.AMB_UFO_Idle);
        }

        private void OnEnable()
        {
            controls.Enable();
            controls.Player.Action.performed += (ctx) => {
                if (tractorBeam.HasTileObject())
                {
                    var result = tractorBeam.DropTileObject();
                    //if (result) print("Dropped obj");

                }
                else
                {
                    var result = tractorBeam.PickUpTileObject();
                    //if (result) print("Picked up obj");

                }
            };

            controls.Player.Move.performed += (ctx) =>
            {
                print("performed");
                soundPlayer.PlayAMB(soundPlayer.AMB_UFO_Flyaround);
            };

            controls.Player.Move.canceled += (ctx) =>
            {
                soundPlayer.PlayAMB(soundPlayer.AMB_UFO_Idle);
            };
        }

        private void OnDisable()
        {
            controls.Disable();
         
        }

        private void Update()
        {
            var moveInput = controls.Player.Move.ReadValue<Vector2>();
            var dir = new Vector3(moveInput.x, 0, moveInput.y);
            simpleMove.MoveToDirection(dir);
        }


    }
}
