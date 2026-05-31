using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int CheckpointIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Checkpoints.CheckpointNum < CheckpointIndex)
            {
                Checkpoints.CheckpointNum = CheckpointIndex;
            }
        }
    }
    public Vector3 Position()
    {
        return transform.position;
    }
}
