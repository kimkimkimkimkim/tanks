using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum GameStatus {
    DuringTheGame,
    PlayerWin,
    PlayerLose
}

public class GameManager : MonoBehaviour {

    public float startDelay = 3f;
    public float endDelay = 0f;
    public MovementJoystick movementJoystick;
    public ShootingJoystick shootingJoystick;
    public GameObject cpuTankPrefab;
    public GameObject cpuTankMovementLandMarkPrefab;
    public GameObject playerTankPrefab;
    public PlayerTankManager playerTank;
    private CpuTankManager[] cpuTanks;
    public List<GameObject> stagePrefabList;
    public Text messageText;
    public ResultScreenManager resultScreen;
    public NavMeshSurface navMeshSurface;

    private int stageNumber;

    private void Start() {

    }

    public void GameStart(int stageNumber) {
        this.stageNumber = stageNumber;
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop() {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        var score = playerTank.instance.GetComponent<TankHealth>().currentHealth;
        resultScreen.SetScreen(score, stageNumber);
    }

    private IEnumerator RoundStarting() {

        ResetGame();
        DisableTankControl();

        messageText.text = "STAGE " + stageNumber;

        yield return new WaitForSeconds(startDelay);
    }


    private IEnumerator RoundPlaying() {
        navMeshSurface.BuildNavMesh();
        EnableTankControl();

        messageText.text = "";

        while (GetGameStatus() == GameStatus.DuringTheGame) {
            yield return null;
        }


    }


    private IEnumerator RoundEnding() {

        DisableTankControl();
        HideShells();

        string message = EndMessage();
        messageText.text = message;

        yield return new WaitForSeconds(endDelay);
    }

    private void HideShells() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("HideTarget");
        foreach (GameObject obj in objects) {
            Destroy(obj);
        }
    }

    private GameStatus GetGameStatus() {
        if (playerTank.instance.activeSelf == false) {
            return GameStatus.PlayerLose;
        } else {
            for (int i = 0; i < cpuTanks.Length; i++) {
                if (cpuTanks[i].instance.activeSelf) {
                    return GameStatus.DuringTheGame;
                }
            }
        }
        return GameStatus.PlayerWin;
    }

    private string EndMessage() {
        string message = "DRAW!";

        message = (GetGameStatus() == GameStatus.PlayerWin) ? " WIN!" : "LOSE...";

        return message;
    }

    private void ResetAllTanks() {

        playerTank.Reset();

        for (int i = 0; i < cpuTanks.Length; i++) {
            cpuTanks[i].Reset();
        }
    }

    private void ResetGame() {

        GameObject[] stages = GameObject.FindGameObjectsWithTag("Stage");
        foreach (GameObject stage in stages) {
            Destroy(stage);
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("ResetTarget");
        foreach (GameObject obj in objects) {
            Destroy(obj);
        }

        SetStage();
    }

    private void SetStage() {
        GameObject stage = (GameObject)Instantiate(stagePrefabList[stageNumber - 1]);
        stage.transform.SetParent(navMeshSurface.transform);
        stage.transform.position = new Vector3(0, 0, 0);
        stage.transform.localScale = new Vector3(1, 1, 1);

        cpuTanks = stage.GetComponent<StageManager>().SetTanks(playerTankPrefab, playerTank, cpuTankPrefab, cpuTankMovementLandMarkPrefab);
    }

    private void EnableTankControl() {
        playerTank.EnableControl();

        for (int i = 0; i < cpuTanks.Length; i++) {
            cpuTanks[i].EnableControl();
        }
    }


    private void DisableTankControl() {
        playerTank.DisableControl();

        for (int i = 0; i < cpuTanks.Length; i++) {
            cpuTanks[i].DisableControl();
        }
    }

}
