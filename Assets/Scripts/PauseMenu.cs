using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuParent;
    bool togglePause = false;
    public Image[] Eyes;
    public RectTransform canvasRect;
    public Color dimAlpha, litAlpha;
    private void Update()
    {
        int eyeIndex;
        Vector2 localPoint = GetMouseCanvasPosition();
        float mouseY = localPoint.y;
        if (mouseY > -18) eyeIndex = 0;
        else if (mouseY < -18 && mouseY > -90) eyeIndex = 1;
        else eyeIndex = 2;
        SetEye(eyeIndex);
    }

    void SetEye(int target)
    {
        for (int i = 0; i < Eyes.Length; i++)
        {
            Image text = Eyes[i].transform.parent.GetComponent<Image>();
            Eyes[i].enabled = false;
            text.color = dimAlpha;

            if (target == i)
            {
                Eyes[i].enabled = true;
                text.color = litAlpha;
            }
        }
    }

    public Vector2 GetMouseCanvasPosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePos,
            null,
            out localPoint
        );
        return localPoint;
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame()) { togglePause = !togglePause; Pause(togglePause); }
    }
    void Pause(bool isPausing)
    {
        if (isPausing)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            PauseMenuParent.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            PauseMenuParent.SetActive(false);
        }
    }
    public void ToggleOptions()
    {
        Debug.LogWarning("yet to implement");
    }
    public void Resume()
    {
        togglePause = false;
        Pause(false);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
