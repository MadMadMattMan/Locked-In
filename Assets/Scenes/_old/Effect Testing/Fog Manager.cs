using System.Collections;
using UnityEngine;

public class FogManager : MonoBehaviour {

    [SerializeField] float duration = 10f;

    void Start() {
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 1.0f;
        StartCoroutine(fogFade());
    }

    IEnumerator fogFade() {
        float timer = 0.0f;
        yield return new WaitForSeconds(1.5f);
        while (timer < duration) {
            timer += Time.deltaTime;
            RenderSettings.fogDensity = Exrp(1f, 0.05f, 3f, timer / duration);
            yield return null;
        }
    }

    float Exrp(float a, float b, float p, float t) {
        return a + (b - a) * Mathf.Clamp01(t);
    }
}
