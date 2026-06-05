using UnityEngine;

public class traintrigger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject dilight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            gameManager.StartTrain();
            gameManager.FadeFogTo(60);
        }
    }
}
