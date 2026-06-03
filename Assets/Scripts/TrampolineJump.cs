using KinematicCharacterController.Walkthrough.SimpleJumping;
using UnityEngine;

public class TrampolineJump : MonoBehaviour {
    public float Force;
    AudioSource audioSource;

    private void Awake() {
       audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            MyCharacterController cc = collision.gameObject.GetComponent<MyCharacterController>();
            KinematicCharacterController.Walkthrough.SimpleJumping.MyPlayer.jump = true;

            cc.AddVelocity(transform.up * Force);
            audioSource.Play();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            KinematicCharacterController.Walkthrough.SimpleJumping.MyPlayer.jump = false;
    }
}
