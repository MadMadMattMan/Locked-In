using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public float minPauseGap = 1f;

    AudioSource audioSource;
    bool waiting = true;
    List<AudioClip> clipQueue = new List<AudioClip>();

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }


    void Update() {
        if (waiting && clipQueue.Count > 0) {
            StartCoroutine(IEClipWaiter(clipQueue[0]));
            clipQueue.RemoveAt(0);
        }
    }

    public void Queue(AudioClip clip) {
        clipQueue.Add(clip);
    }

    IEnumerator IEClipWaiter(AudioClip clip) {
        audioSource.PlayOneShot(clip);
        waiting = false;
        yield return new WaitForSeconds(clip.length + minPauseGap);
        waiting = true;
    }
}
