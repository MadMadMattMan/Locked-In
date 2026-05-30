using System.Collections;
using UnityEngine;

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
        anim.SetTrigger("Trigger");
        yield return new WaitForSeconds(3.5f);
        isSteppedOn = false;
    }
}
