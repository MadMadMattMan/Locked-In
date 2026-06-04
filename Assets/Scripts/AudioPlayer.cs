using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] AudioClip voiceLine;
    [SerializeField] string voiceLineText;
    [SerializeField] bool playOnce = false;
    bool played;

    private void Awake() {
        audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Character") {
            if (playOnce && played)
                return;
            played = true;
            audioManager.Queue(voiceLine, voiceLineText);
        }
    }
}
