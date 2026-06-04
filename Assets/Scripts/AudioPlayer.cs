using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public bool playOnce = false;
    bool played;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Character") {
            if (playOnce && played)
                return;
            played = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
