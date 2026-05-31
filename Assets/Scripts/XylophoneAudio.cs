using System;
using UnityEngine;

public class XylophoneAudio : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            InvokeEvent();
        }
    }
    void InvokeEvent()
    {
        try
        {
            FindAnyObjectByType<PPEvents>().GetComponent<PPEvents>().keyStepped.Invoke();
        }
        catch (Exception ex)
        {
            Debug.Log("Could not find any events to invoke: " + ex.Message);
        }
    }
}
