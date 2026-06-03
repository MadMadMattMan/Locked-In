using UnityEngine;

public class PhotoPickup : MonoBehaviour {
    public int photoID = -1;




    GameManager gameManager;
    private void Awake() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Character") {
            gameManager.CollectPicture(photoID);
        }
    }
}
