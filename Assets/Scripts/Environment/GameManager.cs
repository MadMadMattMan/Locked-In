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
    public PictureManager pictureManager;

    [Header("Bear Refs")]
    public GameObject BearObject;
    Transform bearXrayTarget;
    public List<Transform> BearPositions;

    [Header("Title Refs")]
    public GameObject titleCanvas;
    public float fogStart = 3.5f;
    public Animator doorAnimatior;

    public GameObject p1, p2, p3, p4, p5, tutorial;

    // Private Player refs
    HoleyShaderXray playerShader;

    public List<Animator> trainAnimators;

    [Header("Tracking vars")]
    public GameState state = 0;
    public bool showBear = false;
    public int orderedCollectedPictures = 0;
    public bool[] placed = new bool[5];
    private void Start() {
        // setup local refs
        playerShader = PlayerCameraContainer.GetComponentInChildren<HoleyShaderXray>();
        //bearXrayTarget = BearObject.transform.GetChild(0);
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        p5.SetActive(false);

        // defaults
        playerShader.fogLiftedness = fogStart;
        PlayerContainer.SetActive(false);
        titleCanvas.SetActive(true);
    }
    
    // Progresses the game by n steps
    public void ProgressGame() {
        if (state == GameState.Title) {
            Debug.Log("Title -> Tutorial");
            StartCoroutine(IEIntroFade(1.5f));
            tutorial.SetActive(true);
        }
        if (state == GameState.Tutorial) {
            Debug.Log("Tutorial -> Puzzle1");
            p1.SetActive(true);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
        }
        if (state == GameState.Puzzle1) {
            Debug.Log("Puzzle1 -> Puzzle2");
            p1.SetActive(false);
            p2.SetActive(true);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
        }
        if (state == GameState.Puzzle2)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(true);
            p4.SetActive(false);
            p5.SetActive(false);
        }
        if (state == GameState.Puzzle3)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(true);
            p5.SetActive(false);
        }
        if (state == GameState.Puzzle4)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(true);
        }
        if (state == GameState.Ending)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
        }

        state++;
    }


    // Called when picture collected
    public void CollectPicture(int photoID) {
        // set collected picture to collected
        ProgressGame();

        rippedPictures[photoID].SetActive(false);
        rippedPictures[photoID] = null;

        // count collected in order
        int i = 0;
        foreach (GameObject go in rippedPictures) {
            if (go)
                break;
            i++;
        }
    }

    // Called when picture collected
    public void PlacePicture() {
        int i = 0;
        for (; i < 6; i++) {
            if (!placed[i]) {
                placed[i] = true;
                break;
            }
        }
        pictureManager.PlacePiece(i);
    }

    public void StartTrain() {
        foreach(Animator a in trainAnimators) {
            a.SetTrigger("Start");
        }
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
        StartCoroutine(IEFadeFog(fe, 0));
    }
    public void FadeFogTo(float i)
    {
        FogExpander fe = new FogExpander();
        StartCoroutine(IEFadeFog(fe, i));
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
    IEnumerator IEFadeFog(FogExpander fe, float i) {
        float startFog = playerShader.fogLiftedness;
        float tf = i==0 ? fe.targetFog : i;
        float d = i==0 ? fe.fadeTime : 2;
        float time = 0;
        while (time < fe.fadeTime) {
            time += Time.deltaTime;
            playerShader.fogLiftedness = Mathf.Lerp(startFog, tf, time/d);
            yield return null;
        }
        playerShader.fogLiftedness = tf;
        
        if (i==0) 
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
    Ending
}
