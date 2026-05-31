using UnityEngine;

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
            player.transform.position = checkpoints[CheckpointNum].Position();
        }
    }
}
