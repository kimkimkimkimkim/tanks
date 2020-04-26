using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public MovementJoystick movementJoystick;
    public ShootingJoystick shootingJoystick;
    public GameObject cpuTankPrefab;
    public GameObject playerTankPrefab;
    public PlayerTankManager playerTank;
    public CpuTankManager[] cpuTanks;

    private void Start() {
        SpawnAllTanks();
    }

    private void SpawnAllTanks() {
        SpawnPlayerTank();
        SpawnCpuTanks();
    }

    private void SpawnPlayerTank() {
        GameObject tank = Instantiate(playerTankPrefab, playerTank.spawnPoint.position, playerTank.spawnPoint.rotation) as GameObject;
        playerTank.instance = tank;
        //playerTank.tankType = TankType.Player;
        //playerTank.movementJoystick = movementJoystick;
        //playerTank.shootingJoystick = shootingJoystick;
        playerTank.Setup();
    }

    private void SpawnCpuTanks() {
        for (int i = 0; i < cpuTanks.Length; i++) {
            GameObject tank = Instantiate(cpuTankPrefab, cpuTanks[i].spawnPoint.position, cpuTanks[i].spawnPoint.rotation) as GameObject;
            cpuTanks[i].instance = tank;
            cpuTanks[i].tankType = TankType.CPU1;
            cpuTanks[i].Setup();
        }
    }

}
