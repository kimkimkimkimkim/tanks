using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStatus {
    DuringTheGame,
    PlayerWin,
    PlayerLose
}

public class GameManager : MonoBehaviour {

    public float startDelay = 3f;
    public float endDelay = 3f;
    public MovementJoystick movementJoystick;
    public ShootingJoystick shootingJoystick;
    public GameObject cpuTankPrefab;
    public GameObject playerTankPrefab;
    public PlayerTankManager playerTank;
    public CpuTankManager[] cpuTanks;
    public List<GameObject> stagePrefabList;
    public Text messageText;
    public ResultScreenManager resultScreen;

    private void Start() {

    }

    private void SpawnAllTanks() {
        SpawnPlayerTank();
        SpawnCpuTanks();
    }

    private void SpawnPlayerTank() {
        GameObject tank = Instantiate(playerTankPrefab, playerTank.spawnPoint.position, playerTank.spawnPoint.rotation) as GameObject;
        playerTank.instance = tank;
        playerTank.Setup();
    }

    private void SpawnCpuTanks() {
        for (int i = 0; i < cpuTanks.Length; i++) {
            GameObject tank = Instantiate(cpuTankPrefab, cpuTanks[i].spawnPoint.position, cpuTanks[i].spawnPoint.rotation) as GameObject;
            cpuTanks[i].instance = tank;
            cpuTanks[i].tankType = TankType.CPU1;
            cpuTanks[i].playerTank = playerTank.instance;
            cpuTanks[i].Setup();
        }
    }

    public void GameStart(int stageNumber) {
        StartCoroutine(GameLoop(stageNumber));
    }

    private IEnumerator GameLoop(int stageNumber) {
        SetStage(stageNumber);
        yield return StartCoroutine(RoundStarting(stageNumber));
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GetGameStatus() == GameStatus.PlayerLose) {
            resultScreen.SetScreen(0, stageNumber);
        } else {
            resultScreen.SetScreen(3, stageNumber);
        }
    }

    private void SetStage(int stageNumber) {
        GameObject stage = (GameObject)Instantiate(stagePrefabList[stageNumber - 1]);
        stage.transform.position = new Vector3(0, 0, 0);
        stage.transform.localScale = new Vector3(1, 1, 1);
    }


    private IEnumerator RoundStarting(int stageNumber) {

        ResetGame();
        DisableTankControl();

        messageText.text = "STAGE " + stageNumber;

        yield return new WaitForSeconds(startDelay);
    }


    private IEnumerator RoundPlaying() {
        EnableTankControl();

        messageText.text = "";

        while (GetGameStatus() == GameStatus.DuringTheGame) {
            yield return null;
        }


    }


    private IEnumerator RoundEnding() {

        DisableTankControl();

        string message = EndMessage();
        messageText.text = message;

        yield return new WaitForSeconds(endDelay);
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

        GameObject[] objects = GameObject.FindGameObjectsWithTag("ResetTarget");
        foreach (GameObject obj in objects) {
            Destroy(obj);
        }

        SpawnAllTanks();
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
