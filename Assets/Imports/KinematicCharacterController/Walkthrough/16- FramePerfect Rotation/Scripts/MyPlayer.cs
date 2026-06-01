using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using System.Linq;
using UnityEngine.InputSystem;

namespace KinematicCharacterController.Walkthrough.SimpleJumping
{
    public class MyPlayer : MonoBehaviour
    {
        public ExampleCharacterCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public MyCharacterController Character;
        public float XSens;
        public float YSens;
        float mouseLookAxisUp;
        float mouseLookAxisRight;
        bool toggleZoom = false;
        float moveAxisX;
        float moveAxisY;
        bool jump;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            OrbitCamera.IgnoredColliders.Clear();
            OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            HandleCameraInput();
            Character.PostInputUpdate(Time.deltaTime, OrbitCamera.transform.forward);
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            moveAxisX = context.ReadValue<Vector2>().y;
            moveAxisY = context.ReadValue<Vector2>().x;
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            mouseLookAxisRight = context.ReadValue<Vector2>().x * XSens;
            mouseLookAxisUp = context.ReadValue<Vector2>().y * YSens;
        }
        public void OnToggleZoom(InputAction.CallbackContext context)
        {
            if (context.action.WasPerformedThisFrame()) toggleZoom = true;
            else toggleZoom = false;
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.action.IsPressed()) jump = true;
            else jump = false;
        }
        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = 0f;
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
            if (toggleZoom)
            {
                OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
            }
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = moveAxisX;
            characterInputs.MoveAxisRight = moveAxisY;
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = jump;

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
    }
}