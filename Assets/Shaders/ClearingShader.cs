using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearingShader : MonoBehaviour {

    [SerializeField] float duration = 10;
    [Range(0, 1)][SerializeField] float strength = 0;

    InputAction action;
    private void Start() {
        action = InputSystem.actions.FindAction("Jump");
    }

    private void Update() {
        if (action.IsPressed())
            TriggerReveal();
    }

    private void TriggerReveal() {
        StartCoroutine(Revealer());
    }

    IEnumerator Revealer() {
        float elapsedTime = 0f;
        Material objMat = GetComponent<Renderer>().material;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            strength = Mathf.Lerp(0, 1, elapsedTime/duration);

            objMat.SetFloat("_Cut", strength);
            yield return null;
        }
    }
}