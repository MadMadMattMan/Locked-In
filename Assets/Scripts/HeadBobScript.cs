using UnityEngine;
public class HeadBobScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool enableHeadBob = true;

    [Header("Amplitude & Frequency")]
    [SerializeField] private float bobFrequency = 14f;
    [SerializeField] private float bobHorizontalAmplitude = 0.03f;
    [SerializeField] private float bobVerticalAmplitude = 0.05f;
    [Range(0, 1)] [SerializeField] private float headBobSmoothing = 0.1f;

    [Header("References")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody playerRb;
    private float timer = 0f;
    private Vector3 defaultPosition;

    private void Start()
    {
        if (cameraHolder != null)
        {
            defaultPosition = cameraHolder.localPosition;
        }
    }

    private void Update()
    {
        if (!enableHeadBob || cameraHolder == null || playerController == null) return;

        CheckMotion();
    }

    private void CheckMotion()
    {
        Vector3 lateralVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);
        float speed = lateralVelocity.magnitude;
        //if (speed < 0.1f || !playerController.grounded)
        {
            //ResetCameraPosition();
            //return;
        }
        timer += Time.deltaTime * speed * bobFrequency;
        Vector3 targetPosition = CalculateBobOffset();
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetPosition, headBobSmoothing);
    }

    private Vector3 CalculateBobOffset()
    {
        float posX = Mathf.Cos(timer / 2f) * bobHorizontalAmplitude;
        float posY = Mathf.Sin(timer) * bobVerticalAmplitude;

        return new Vector3(posX, defaultPosition.y + posY, defaultPosition.z);
    }

    private void ResetCameraPosition()
    {
        if (cameraHolder.localPosition != defaultPosition)
        {
            cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, defaultPosition, headBobSmoothing);
            timer = 0f;
        }
    }
}
