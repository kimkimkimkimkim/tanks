using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public Transform playerPosition;
    public CpuTankManager[] cpuTankList;

    public CpuTankManager[] SetTanks(GameObject playerTankPrefab, PlayerTankManager playerTank, GameObject cpuTankPrefab, GameObject cpuTankMovementLandMarkPrefab) {
        //player
        GameObject player = (GameObject)Instantiate(playerTankPrefab, playerPosition.position, playerPosition.rotation);
        playerTank.instance = player;
        playerTank.Setup();

        //cpu
        for (int i = 0; i < cpuTankList.Length; i++) {
            GameObject landMark = (GameObject)Instantiate(cpuTankMovementLandMarkPrefab, cpuTankList[i].spawnPoint.position, cpuTankList[i].spawnPoint.rotation);
            landMark.GetComponent<CpuTankMovementLandMark>().targetObject = player;

            GameObject cpu = (GameObject)Instantiate(cpuTankPrefab, cpuTankList[i].spawnPoint.position, cpuTankList[i].spawnPoint.rotation);
            cpuTankList[i].instance = cpu;
            cpuTankList[i].playerTank = player;
            cpuTankList[i].tankMovementLandMark = landMark;
            cpuTankList[i].Setup();
        }

        return cpuTankList;
    }
}
