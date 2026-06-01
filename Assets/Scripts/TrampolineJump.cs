using UnityEngine;
using KinematicCharacterController.Walkthrough.SimpleJumping;
using KinematicCharacterController;

public class TrampolineJump : MonoBehaviour
{
    public float Force;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MyCharacterController cc = collision.gameObject.GetComponent<MyCharacterController>();
            KinematicCharacterMotor kcm = cc.Motor;
            Vector3 forceDir = kcm.CharacterUp;
            if (kcm.GroundingStatus.FoundAnyGround && !kcm.GroundingStatus.IsStableOnGround)
            {
                forceDir = kcm.GroundingStatus.GroundNormal;
            }
            cc.AddVelocity(forceDir * Force);
        }
    }
}
