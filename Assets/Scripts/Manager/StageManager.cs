using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public Transform playerPosition;
    public CpuTankManager[] cpuTankList;

    public CpuTankManager[] SetTanks(GameObject playerTankPrefab, PlayerTankManager playerTank, GameObject cpuTankPrefab) {
        //player
        GameObject player = (GameObject)Instantiate(playerTankPrefab, playerPosition.position, playerPosition.rotation);
        playerTank.instance = player;
        playerTank.Setup();

        //cpu
        for (int i = 0; i < cpuTankList.Length; i++) {
            GameObject cpu = Instantiate(cpuTankPrefab, cpuTankList[i].spawnPoint.position, cpuTankList[i].spawnPoint.rotation) as GameObject;
            cpuTankList[i].instance = cpu;
            cpuTankList[i].playerTank = player;
            cpuTankList[i].Setup();
        }

        return cpuTankList;
    }
}
