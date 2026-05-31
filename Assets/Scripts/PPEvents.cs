using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.Events;
public class PPEvents : MonoBehaviour
{
    public Volume volume;
    public UnityEvent blockStepped;
    private ChromaticAberration ca;
    void Start()
    {
        if (volume.profile.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.overrideState = true;
            blockStepped.AddListener(StartEffect);
        }
    }
    public void StartEffect() { StartCoroutine(nameof(ChromaticAberrationEffect)); }
    private IEnumerator ChromaticAberrationEffect()
    {
        float timer = 1f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            ca.intensity.value = timer;
            yield return null;
        }
        yield return null;
    }
}
