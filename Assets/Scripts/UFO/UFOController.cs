using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncientAliens.UFO
{
    [RequireComponent(typeof(SimpleMove), typeof(TractorBeam), typeof(UFOSoundPlayer))]
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

        private void DisableFunctionality()
        {
            simpleMove.enabled = false;
            tractorBeam.enabled = false;
            soundPlayer.enabled = false;
        }

        private void EnableFunctionality()
        {
            simpleMove.enabled = true;
            tractorBeam.enabled = true;
            soundPlayer.enabled = true;
        }

        private void Start()
        {
            soundPlayer.PlayAMB01(soundPlayer.AMB_UFO_Idle);
            soundPlayer.PlayAMB02(soundPlayer.AMB_UFO_Flyaround);

            simpleMove.onStartedMoving += StartedMoving;
            simpleMove.onStoppedMoving += StoppedMoving;
        }

        private void StartedMoving()
        {
            //soundPlayer.
            soundPlayer.MuteAMB02(false);
            
        }

        private void StoppedMoving()
        {
            soundPlayer.MuteAMB02(true);
        }

        private void OnEnable()
        {
            controls.Enable();
            controls.Player.Action.performed += (ctx) => {
                if (tractorBeam.HasTileObject())
                {
                    var type = tractorBeam.GetTileObjectType();
                    var result = tractorBeam.DropTileObject();

                    if (result)
                    {
                        soundPlayer.PlayDropSoundForType(type);
                    }

                }
                else
                {
                    var result = tractorBeam.PickUpTileObject();

                    if(result)
                    {
                        soundPlayer.PlayPickUpSoundForType(tractorBeam.GetTileObjectType());
                    }
                    //if (result) print("Picked up obj");

                }
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
