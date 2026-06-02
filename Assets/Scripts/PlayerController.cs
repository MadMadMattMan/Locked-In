using UnityEngine;
using KinematicCharacterController;

public class PlayerController : MonoBehaviour, ICharacterController
{
    public KinematicCharacterMotor Motor;
    private void Start()
    {
        Motor.CharacterController = this;
    }
    void ICharacterController.AfterCharacterUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.BeforeCharacterUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    bool ICharacterController.IsColliderValidForCollisions(Collider coll)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.OnDiscreteCollisionDetected(Collider hitCollider)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.PostGroundingUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    void ICharacterController.SetAirMoveSpeedOffset(Vector3 newOffset)
    {
        throw new System.NotImplementedException();
    }
}
