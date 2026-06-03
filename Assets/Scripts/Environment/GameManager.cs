using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Global References")]
    public GameObject PlayerContainer;
    public GameObject PlayerCameraContainer;
    public GameObject BearObject;
    public List<Transform> BearPositions;

    [Header("Title Refs")]
    public GameObject titleCanvas;
    public Animator introDoor;

    // Private Player refs
    HoleyShaderXray playerShader;

    [Header("Tracking vars")]
    public GameState state = 0;
    public bool showBear = false;

    private void Start() {
        // setup local refs
        playerShader = PlayerCameraContainer.GetComponentInChildren<HoleyShaderXray>();
        playerShader.fogLiftedness = 2.5f;
        PlayerContainer.SetActive(false);
        titleCanvas.SetActive(true);
    }

    public void ProgressGame() {
        Debug.Log($"Progressing game from: {state}");
        if (state == GameState.Title) {
            Debug.Log("Title -> Door");
            TriggerCoroutine(IntroFade(5f));
        }
        if (state == GameState.Door) {
            Debug.Log("Door -> Tutorial");
            introDoor.SetTrigger("Open");
        }

        state++;
    }


    void TriggerCoroutine(IEnumerator coroutine) {
        StopAllCoroutines();
        StartCoroutine(coroutine);
    }

    IEnumerator IntroFade(float duration) {
        // set up
        float time = 0;
        Button[] bts = titleCanvas.GetComponentsInChildren<Button>();
        foreach (Button b in bts)
            b.interactable = false;
        Image[] imgs = titleCanvas.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
            img.CrossFadeAlpha(0, duration, false);

        // fade fog
        while (time < duration) {
            time += Time.deltaTime;
            playerShader.fogLiftedness = Mathf.Lerp(2.5f, 5f, time / duration);
            yield return null;
        }

        // clean up
        playerShader.fogLiftedness = 7.5f;
        Destroy(titleCanvas);
        PlayerContainer.SetActive(true);
    }
}

public enum GameState {
    Title,
    Door,
    Tutorial,
    Puzzle1,
    Puzzle2,
    Puzzle3,
    Puzzle4,
    TrainStation,
    Train,
    Ending
}
