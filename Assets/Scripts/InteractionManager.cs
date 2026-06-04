using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float range = 50.0f;
    [SerializeField]
    private GameObject Eyeball;
    Interactable interactedObj;
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.TryGetComponent<Interactable>(out interactedObj))
            {
                Eyeball.SetActive(true);
                return;
            }
        }
        Eyeball.SetActive(false);
        interactedObj = null;
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            Interaction();
        }
    }
    public void Interaction()
    {

    }
}
