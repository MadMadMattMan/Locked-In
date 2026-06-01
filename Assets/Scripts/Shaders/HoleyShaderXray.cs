using System.Collections;
using UnityEngine;

public class HoleyShaderXray : MonoBehaviour {
    public float fogLiftedness = 250;

    public Camera playerCamera;
    public Transform xRayFocusObject;
    public float falloff = 0.1f;

    public float maxSize = 5;
    float currentSize = 0;
    float targetSize = 0;

    public float changeTime = 0.1f;

    Material shaderMaterial;
    Renderer targetRenderer;

    Vector2 cutoutPos = new Vector2();


    private void Start() {
        shaderMaterial = GetComponent<Renderer>().material;
    }

    private void Update() {
        shaderMaterial.SetFloat("_fogLiftedness", fogLiftedness);

        bool do_xRay = false;
        if (xRayFocusObject != null) {
            targetRenderer = xRayFocusObject.parent.GetComponent<MeshRenderer>();
            do_xRay = targetRenderer.isVisible;
            if (do_xRay) {
                RaycastHit hitdata;
                if (Physics.Raycast(transform.position, (xRayFocusObject.position - transform.position).normalized * 100f, out hitdata)) {
                    do_xRay = hitdata.collider.gameObject == xRayFocusObject.parent.gameObject;
                    Debug.DrawRay(transform.position, (xRayFocusObject.position - transform.position).normalized * 100f, Color.aliceBlue);
                }
                Debug.DrawRay(transform.position, (xRayFocusObject.position - transform.position).normalized * 100f, Color.red);
            }
        }

        if (do_xRay) {
            if (targetSize == 0f)
                setCutoutState(true);

            cutoutPos = playerCamera.WorldToViewportPoint(xRayFocusObject.position);
            cutoutPos.y /= (Screen.width / Screen.height);

            float cutoutSize = Mathf.Sqrt(Vector3.Distance(transform.position, xRayFocusObject.position)) * (currentSize / 1000);
            shaderMaterial.SetFloat("_cutoutSize", cutoutSize);

            shaderMaterial.SetVector("_cutoutPos", cutoutPos);
            shaderMaterial.SetFloat("_cutoutFalloff", falloff);
            return;
        }
        else if (targetSize == maxSize)
            setCutoutState(false);

        shaderMaterial.SetFloat("_cutoutSize", 0f);
    }

    void setCutoutState(bool state) {
        StopAllCoroutines();
        StartCoroutine(changeCutoutState(state));
    }

    IEnumerator changeCutoutState(bool state) {
        targetSize = state ? maxSize : 0f;
        float start = state ? 0f : maxSize;
        float timer = state ? (currentSize / maxSize) : ((maxSize / currentSize)-1)*-1;
        timer *= changeTime;

        while (timer < changeTime) {
            timer += Time.deltaTime;
            currentSize = Mathf.Lerp(start, targetSize, timer/changeTime);
            yield return new WaitForEndOfFrame();
        }
        currentSize = state ? maxSize : 0f;
    }
}