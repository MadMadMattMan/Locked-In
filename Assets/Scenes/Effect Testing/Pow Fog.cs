using System.Collections;
using UnityEngine;

public class PowFog : MonoBehaviour {

    [SerializeField] float duration = 10f;
    [SerializeField] float power = 3f;

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
            RenderSettings.fogDensity = Exrp(timer, duration, power);
            yield return null;
        }
    }

    float Exrp(float t, float d,  float p) {
        return 1 - Mathf.Pow(t/d, 1/p);
    }
}
