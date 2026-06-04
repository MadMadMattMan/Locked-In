using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public float minPauseGap = 1f;
    public bool playSubtitles = true;
    AudioSource audioSource;
    Subtitles subtitleManager;
    bool waiting = true;
    List<(AudioClip, string)> clipQueue = new List<(AudioClip, string)>();

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        subtitleManager = GetComponent<Subtitles>();
    }


    void Update() {
        if (waiting && clipQueue.Count > 0) {
            StartCoroutine(IEClipWaiter(clipQueue[0].Item1, clipQueue[0].Item2));
            clipQueue.RemoveAt(0);
        }
    }

    public void Queue(AudioClip clip, string subtitleText) {
        clipQueue.Add((clip, subtitleText));
    }

    IEnumerator IEClipWaiter(AudioClip clip, string subtitle) {
        audioSource.PlayOneShot(clip);
        subtitleManager.SubtitleText(subtitle);
        waiting = false;
        yield return new WaitForSeconds(clip.length + minPauseGap);
        waiting = true;
    }
}
