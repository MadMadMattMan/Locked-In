using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.Events;
public class PPEvents : MonoBehaviour
{
    public Volume volume;
    public UnityEvent blockStepped;
    public UnityEvent keyStepped;
    private ChromaticAberration ca;
    private LensDistortion ld;
    void Start()
    {
        if (volume.profile.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.overrideState = true;
            blockStepped.AddListener(StartCA);
        }
        if (volume.profile.TryGet<LensDistortion>(out ld))
        {
            ld.intensity.overrideState = true;
            keyStepped.AddListener(StartLD);
        }
    }
    public void StartCA() { StartCoroutine(nameof(ChromaticAberrationEffect)); }
    public void StartLD() { StartCoroutine(nameof(LensDistortionEffect)); }
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
    private IEnumerator LensDistortionEffect()
    {
        float timer = 0.2f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            ld.intensity.value = -1f * timer;
            yield return null;
        }
        ld.intensity.value = 0f;
        yield return null;
    }
}
