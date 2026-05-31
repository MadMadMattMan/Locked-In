using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class FloatyBlock : MonoBehaviour
{
    public Animator anim;
    private bool isSteppedOn = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isSteppedOn)
            {
                StartCoroutine(ReactToLanding());
            }
        }
    }

    private IEnumerator ReactToLanding()
    {
        isSteppedOn = true;
        InvokeEvent();
        anim.SetTrigger("Trigger");
        yield return new WaitForSeconds(3.5f);
        isSteppedOn = false;
    }
    void InvokeEvent()
    {
        try
        {
            FindAnyObjectByType<PPEvents>().GetComponent<PPEvents>().blockStepped.Invoke();
        }
        catch (Exception ex)
        {
            Debug.Log("Could not find any events to invoke: " + ex.Message);
        }
    }
}
