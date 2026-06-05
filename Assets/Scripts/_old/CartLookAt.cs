using UnityEngine;
using System.Collections;
public class CartLookAt : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;
    private void LateUpdate()
    {
        transform.LookAt(target);
        transform.rotation *= Quaternion.Euler(Offset);
    }
}
