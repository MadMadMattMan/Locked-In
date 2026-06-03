using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Global References")]
    public GameObject PlayerContainer;
    public GameObject BearObject;
    public List<Transform> BearPositions;

    [Header("Title Refs")]
    public GameObject titleCanvas;


    // Private Player refs
    HoleyShaderXray playerShader;

    [Header("Tracking vars")]
    public GameState state = 0;
    public bool showBear = false;

    private void Start() {
        // setup local refs
        playerShader = PlayerContainer.GetComponentInChildren<HoleyShaderXray>();
        playerShader.fogLiftedness = 5f;

        PlayerContainer.SetActive(false);
        titleCanvas.SetActive(true);

    }

    public void ProgressGame() {
        if (state == 0) {
            Destroy(titleCanvas);
            PlayerContainer.SetActive(true);
        }

        state++;
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
