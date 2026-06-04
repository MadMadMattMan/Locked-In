using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public bool playOnce = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Character") {
            GetComponent<AudioSource>().Play();
            if (playOnce)
                gameObject.SetActive(false);
        }
    }
}
