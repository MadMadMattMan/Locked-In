using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FogExpander : MonoBehaviour {
    public GameManager manager;
    public float targetFog;
    public float fadeTime;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Character") {
            manager.FadeFogTo(this);
            gameObject.SetActive(false);
        }
    }
}
