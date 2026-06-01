using KinematicCharacterController;
using KinematicCharacterController.Walkthrough.SimpleJumping;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class TrampolineJump : MonoBehaviour {
    public float Force;
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            MyCharacterController cc = collision.gameObject.GetComponent<MyCharacterController>();
            KinematicCharacterController.Walkthrough.SimpleJumping.MyPlayer.jump = true;

            cc.AddVelocity(Vector3.up * Force);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            KinematicCharacterController.Walkthrough.SimpleJumping.MyPlayer.jump = false;
    }
}
