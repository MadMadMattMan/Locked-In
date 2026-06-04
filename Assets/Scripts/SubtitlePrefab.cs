using UnityEngine;
using System.Collections;
public class SubtitlePrefab : MonoBehaviour
{
    Animator SubtitleAnimator;
    private void Awake()
    {
        SubtitleAnimator = GetComponent<Animator>();
    }
    public void SetUptime(float upTime) { StartCoroutine(SubtitleAnimation(upTime)); }
    IEnumerator SubtitleAnimation(float upTime)
    {
        yield return new WaitForSeconds(upTime);
        SubtitleAnimator.SetTrigger("Trigger");
    }
}
