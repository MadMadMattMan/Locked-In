using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    AudioSource s;
    void Awake()
    {
        s = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        s.Play();
    }
}
