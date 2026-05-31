using System;
using System.Collections;
using UnityEngine;

public class ClearingShader : MonoBehaviour {

    [SerializeField] float duration = 5;
    [Range(0, 1)][SerializeField] float strength = 0;

    private void Start() {
        StartCoroutine(Revealer());
    }

    public void OnTest() {
        Debug.Log("Testing");
        StopCoroutine(Revealer());
        StartCoroutine(Revealer());
    }

    IEnumerator Revealer() {
        float elapsedTime = 0f;
        Material objMat = GetComponent<Renderer>().material;
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            strength = Mathf.Lerp(0, 1, elapsedTime/duration);

            objMat.SetFloat("_Cut", strength);
            yield return null;
        }
    }
}