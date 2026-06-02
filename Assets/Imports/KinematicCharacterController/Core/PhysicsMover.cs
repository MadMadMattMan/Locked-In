using System;
using UnityEngine;

namespace KinematicCharacterController
{
     /**
     * Represents the entire state of a PhysicsMover that is pertinent for simulation.
     * Use this to save state or revert to past state
     */
    [System.Serializable]
    public struct PhysicsMoverState {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
    }

    /**
     * Component that manages the movement of moving kinematic rigidbodies for
     * proper interaction with characters
     */
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsMover : MonoBehaviour {
        // The mover's Rigidbody
        [ReadOnly]
        public Rigidbody Rigidbody;

        // Determines if the platform moves with rigidbody.MovePosition (when true), or with rigidbody.position (when false)
        public bool MoveWithPhysics = true;

        // Index of this motor in KinematicCharacterSystem arrays
        [NonSerialized]
        public IMoverController MoverController;

        // Remembers latest position in interpolation
        [NonSerialized]
        public Vector3 LatestInterpolationPosition;

        // Remembers latest rotation in interpolation
        [NonSerialized]
        public Quaternion LatestInterpolationRotation;

         // The latest movement made by interpolation
        [NonSerialized]
        public Vector3 PositionDeltaFromInterpolation;

        // The latest rotation made by interpolation
        [NonSerialized]
        public Quaternion RotationDeltaFromInterpolation;

         // Index of this motor in KinematicCharacterSystem arrays
        public int IndexInCharacterSystem { get; set; }
        
        // Remembers initial position before all simulation are done
        public Vector3 Velocity { get; protected set; }

        // Remembers initial position before all simulation are done
        public Vector3 AngularVelocity { get; protected set; }

        // Remembers initial position before all simulation are done
        public Vector3 InitialTickPosition { get; set; }

        // Remembers initial rotation before all simulation are done
        public Quaternion InitialTickRotation { get; set; }

        // The mover's Transform
        public Transform Transform { get; private set; }

        // The character's position before the movement calculations began
        public Vector3 InitialSimulationPosition { get; private set; }

        // The character's rotation before the movement calculations began
        public Quaternion InitialSimulationRotation { get; private set; }

        // The 
        private Vector3 _internalTransientPosition;


        // The mover's rotation (always up-to-date during the character update phase)
        public Vector3 TransientPosition {
            get {
                return _internalTransientPosition;
            }
            private set {
                _internalTransientPosition = value;
            }
        }

        private Quaternion _internalTransientRotation;

        // The mover's rotation (always up-to-date during the character update phase)
        public Quaternion TransientRotation {
            get {
                return _internalTransientRotation;
            }
            private set {
                _internalTransientRotation = value;
            }
        }

        private void Reset() {
            ValidateData();
        }

        private void OnValidate() {
            ValidateData();
        }

         /**
         * Handle validating all required values
         */
        public void ValidateData() {
            Rigidbody = gameObject.GetComponent<Rigidbody>();

            Rigidbody.centerOfMass = Vector3.zero;
            Rigidbody.maxAngularVelocity = Mathf.Infinity;
            Rigidbody.maxDepenetrationVelocity = Mathf.Infinity;
            Rigidbody.isKinematic = true;
            Rigidbody.interpolation = RigidbodyInterpolation.None;
        }

        private void OnEnable() {
            KinematicCharacterSystem.EnsureCreation();
            KinematicCharacterSystem.RegisterPhysicsMover(this);
        }

        private void OnDisable() {
            KinematicCharacterSystem.UnregisterPhysicsMover(this);
        }

        private void Awake() {
            Transform = this.transform;
            ValidateData();

            TransientPosition = Rigidbody.position;
            TransientRotation = Rigidbody.rotation;
            InitialSimulationPosition = Rigidbody.position;
            InitialSimulationRotation = Rigidbody.rotation;
            LatestInterpolationPosition = Transform.position;
            LatestInterpolationRotation = Transform.rotation;
        }

         /**
         * Sets the mover's position directly
         */
        public void SetPosition(Vector3 position) {
            Transform.position = position;
            Rigidbody.position = position;
            InitialSimulationPosition = position;
            TransientPosition = position;
        }

         /**
         * Sets the mover's rotation directly
         */
        public void SetRotation(Quaternion rotation) {
            Transform.rotation = rotation;
            Rigidbody.rotation = rotation;
            InitialSimulationRotation = rotation;
            TransientRotation = rotation;
        }

         /**
         * Sets the mover's position and rotation directly
         */
        public void SetPositionAndRotation(Vector3 position, Quaternion rotation) {
            Transform.SetPositionAndRotation(position, rotation);
            Rigidbody.position = position;
            Rigidbody.rotation = rotation;
            InitialSimulationPosition = position;
            InitialSimulationRotation = rotation;
            TransientPosition = position;
            TransientRotation = rotation;
        }

         /**
         * Returns all the state information of the mover that is pertinent for simulation
         */
        public PhysicsMoverState GetState() {
            PhysicsMoverState state = new PhysicsMoverState();

            state.Position = TransientPosition;
            state.Rotation = TransientRotation;
            state.Velocity = Velocity;
            state.AngularVelocity = AngularVelocity;

            return state;
        }

         /**
         * Applies a mover state instantly
         */
        public void ApplyState(PhysicsMoverState state) {
            SetPositionAndRotation(state.Position, state.Rotation);
            Velocity = state.Velocity;
            AngularVelocity = state.AngularVelocity;
        }

         /**
         * Caches velocity values based on deltatime and target position/rotations
         */
        public void VelocityUpdate(float deltaTime) {
            InitialSimulationPosition = TransientPosition;
            InitialSimulationRotation = TransientRotation;

            MoverController.UpdateMovement(out _internalTransientPosition, out _internalTransientRotation, deltaTime);

            if (deltaTime > 0f)
            {
                Velocity = (TransientPosition - InitialSimulationPosition) / deltaTime;
                                
                Quaternion rotationFromCurrentToGoal = TransientRotation * (Quaternion.Inverse(InitialSimulationRotation));
                AngularVelocity = (Mathf.Deg2Rad * rotationFromCurrentToGoal.eulerAngles) / deltaTime;
            }
        }

        /**
         * Applies the animation velocity to the player, useful for onExit()
         */
        public void ApplyPhysics() {

        }
    }
}