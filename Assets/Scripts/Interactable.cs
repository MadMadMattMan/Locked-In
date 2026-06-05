using UnityEngine;

public class Interactable : MonoBehaviour {


    public InteractableType it = InteractableType.puzzle;

    public int puzzleID;

    public void OnInteract() {
        switch (it) {
            case InteractableType.puzzle:
                collect();
                break;
            case InteractableType.frame:
                place();
                break;
            case InteractableType.train:
                train();
                break;
        }
    }

    void collect() {
        GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().CollectPicture(puzzleID);
    }
    void place() {
        GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().PlacePicture();
    }
    void train() {
        GameObject.FindWithTag("Game Manager").GetComponent<GameManager>().StartTrain();
    }
}

public enum InteractableType {
    puzzle,
    frame,
    train
}
