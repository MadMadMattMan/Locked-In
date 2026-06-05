using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PictureManager : MonoBehaviour {
    public GameObject completePicture;
    public List<GameObject> rippedPieces;
    public Animator ani;

    int n = 0;
    public bool place = false;

    private void Start() {
        completePicture.SetActive(false);
        foreach (GameObject p in rippedPieces) {
            if (p != rippedPieces.Last())
                p.SetActive(false);
        }
    }

    private void Update() {
        if (place) {
            PlacePiece(n++);
            place = !place;
        }
    }


    public void PlacePiece(int i) {
        rippedPieces[i].SetActive(true);
        foreach (GameObject p in rippedPieces)
            if (!p.activeSelf)
                return;
        completePicture.SetActive(true);
        ani.SetTrigger("Fall");
    }
}
