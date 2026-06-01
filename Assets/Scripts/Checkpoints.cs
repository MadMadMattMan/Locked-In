using UnityEngine;
using KinematicCharacterController.Walkthrough.SimpleJumping;
public class Checkpoints : MonoBehaviour
{
    public static int CheckpointNum;
    public Checkpoint[] checkpoints;
    public GameObject player;
    public float yHeight;
    private void FixedUpdate()
    {
        if (player.transform.position.y < yHeight)
        {
            Debug.Log("Died");
            player.GetComponent<MyCharacterController>().SetPosition(checkpoints[CheckpointNum].Position());
        }
    }
}
