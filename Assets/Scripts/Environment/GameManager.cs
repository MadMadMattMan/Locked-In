using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [Header("Global References")]
    public GameObject PlayerContainer;
    public GameObject PlayerCameraContainer;

    [Header("Picture Refs")]
    public List<GameObject> rippedPictures;

    [Header("Bear Refs")]
    public GameObject BearObject;
    Transform bearXrayTarget;
    public List<Transform> BearPositions;

    [Header("Title Refs")]
    public GameObject titleCanvas;
    public float fogStart = 3.5f;
    public Animator doorAnimatior;    

    // Private Player refs
    HoleyShaderXray playerShader;

    [Header("Tracking vars")]
    public GameState state = 0;
    public bool showBear = false;
    public int orderedCollectedPictures = 0;

    private void Start() {
        // setup local refs
        playerShader = PlayerCameraContainer.GetComponentInChildren<HoleyShaderXray>();
        bearXrayTarget = BearObject.transform.GetChild(0);

        // defaults
        playerShader.fogLiftedness = fogStart;
        PlayerContainer.SetActive(false);
        titleCanvas.SetActive(true);
    }
    
    // Progresses the game by n steps
    public void ProgressGame(int steps) {
        state += steps - 1;

        if (state == GameState.Title) {
            Debug.Log("Title -> Tutorial");
            StartCoroutine(IEIntroFade(5f));
            BearObject.transform.position = BearPositions[(int)state].position;
        }
        if (state == GameState.Tutorial) {
            Debug.Log("Tutorial -> Puzzle1");
            StartBearTracking((int)state);
            // play voicelines
        }

        state++;
    }


    // Called when picture collected
    public void CollectPicture(int photoID) {
        // set collected picture to collected
        rippedPictures[photoID].SetActive(false);
        rippedPictures[photoID] = null;
        // count collected in order
        int i = 0;
        foreach (GameObject go in rippedPictures) {
            if (go)
                return;
            i++;
        }

        // steps to do
        int diff = i - orderedCollectedPictures;
        if (diff > 0)
            ProgressGame(diff);
    }


    /**
     *  Coroutine Helpers
     */
    // Setup for bear tracking
    void StartBearTracking(int i) {
        StopCoroutine("IEWaitForBearScreen");
        BearObject.transform.position = BearPositions[i].position;
        BearObject.transform.rotation = BearPositions[i].rotation;
        StartCoroutine(IEWaitForBearScreen(1000f));
    }
    // public method for Fog Expanding
    public void FadeFogTo(FogExpander fe) {
        StartCoroutine(IEFadeFog(fe));
    }

    /**
     *  Coroutines
     */
    // intro fade in from title
    IEnumerator IEIntroFade(float duration) {
        // fade ui
        Button[] bts = titleCanvas.GetComponentsInChildren<Button>();
        foreach (Button b in bts)
            b.interactable = false;
        Image[] imgs = titleCanvas.GetComponentsInChildren<Image>();
        foreach (Image img in imgs)
            img.CrossFadeAlpha(0, duration, false);

        // when done, start game
        yield return new WaitForSeconds(duration);
        Destroy(titleCanvas);
        PlayerContainer.SetActive(true);
        doorAnimatior.SetTrigger("Open");
    }
    // fog fade called by FogExpander
    IEnumerator IEFadeFog(FogExpander fe) {
        float startFog = playerShader.fogLiftedness;
        float time = 0;
        while (time < fe.fadeTime) {
            time += Time.deltaTime;
            playerShader.fogLiftedness = Mathf.Lerp(startFog, fe.targetFog, time/fe.fadeTime);
            yield return null;
        }
        playerShader.fogLiftedness = fe.targetFog;
        Destroy(fe.gameObject);
    }
    // waits for bear to appear on screen
    IEnumerator IEWaitForBearScreen(float screenTime) {
        playerShader.xRayFocusObject = bearXrayTarget;
        Renderer bearRenderer = BearObject.GetComponent<MeshRenderer>();
        while (!bearRenderer.isVisible) {
            yield return null;
        }
        float timer = 0;
        while (timer < screenTime) {
            timer += Time.deltaTime;
            yield return null;
        }
        playerShader.xRayFocusObject = null;
    }
}

public enum GameState {
    Title,
    Tutorial,
    Puzzle1,
    Puzzle2,
    Puzzle3,
    Puzzle4,
    TrainStation,
    Train,
    Ending
}
