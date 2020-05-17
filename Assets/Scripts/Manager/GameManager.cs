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
    public Text messageText;
    public GameObject resultScreen;

    private int stageNumber = 0;

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

    public void GameStart() {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop() {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GetGameStatus() == GameStatus.PlayerLose) {
            SceneManager.LoadScene(0);
        } else {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting() {

        ResetGame();
        DisableTankControl();

        stageNumber++;
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
