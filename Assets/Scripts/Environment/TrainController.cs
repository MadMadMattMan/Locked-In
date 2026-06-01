using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using System.Collections;

public class TrainController : MonoBehaviour {
    // train list (even=cart, odd=bar)
    public GameObject engine;
    public Vector3 target = Vector3.zero;
    public List<GameObject> carts ;
    Dictionary<Transform, Vector3> angleDifference = new Dictionary<Transform, Vector3>();
    public float offset, friction, spring;
    public bool doWiggle = true;

    private void Start() {
        engine.transform.localPosition = Vector3.zero;
        foreach (GameObject cart in carts) {
            Transform t = cart.GetComponentInChildren<AimConstraint>().gameObject.transform;
            angleDifference.Add(t, t.transform.localEulerAngles);
        }
        StartCoroutine(Wiggler());
    }

    // runs teh animation checks
    public void FixedUpdate() {
        // get engine pos
        Vector3 delta = new Vector3(engine.transform.localPosition.x, 0, engine.transform.localPosition.z);
        engine.transform.localPosition += target * Time.deltaTime;
        target = Vector3.MoveTowards(target, Vector3.zero, friction * Time.deltaTime);
        delta = engine.transform.localPosition - delta;

        // apply to carts
        Transform leader = engine.GetComponentInChildren<AimConstraint>().gameObject.transform;
        foreach (GameObject go in carts) {
            // delta pos
            Vector3 oldPos = leader.position;
            Vector3 newPos = oldPos - (leader.forward * offset) + (delta * spring);
            newPos.y = oldPos.y;
            go.transform.position = newPos;

            // delta angle
            Transform t = go.GetComponentInChildren<AimConstraint>().transform;
            Vector3 target = leader.position;
            Vector3 pos = go.transform.position;

            go.transform.LookAt(target);
            go.transform.Rotate(angleDifference.GetValueOrDefault(t).z, -angleDifference.GetValueOrDefault(t).y, -angleDifference.GetValueOrDefault(t).x, Space.Self);

            // load next cart
            leader = go.GetComponentInChildren<AimConstraint>().gameObject.transform;
        }
    }

    IEnumerator Wiggler() {
        float time = 0f;
        while (true) {
            time += Time.deltaTime;
            target.x = Mathf.Sin(8f*time)/10;
            target.y = Mathf.Cos(3f * time) / 5;
            if (doWiggle)
                target.z += Mathf.Sin(4f * time)/100;
            yield return null;
        }
    }
}
