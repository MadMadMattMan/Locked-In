using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ButtonsManager : MonoBehaviour
{
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

    void SetEye(int target) {
        for (int i = 0; i < Eyes.Length; i++) {
            Image text = Eyes[i].transform.parent.GetComponent<Image>();
            Eyes[i].enabled = false;
            text.color = dimAlpha;

            if (target == i) {
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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        Debug.LogWarning("yet to implement");
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
