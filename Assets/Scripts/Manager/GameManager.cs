using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public MovementJoystick movementJoystick;
    public GameObject tankPrefab;
    public TankManager[] tanks;

    private void Start() {
        SpawnAllTanks();
    }

    private void SpawnAllTanks() {
        for (int i = 0; i < tanks.Length; i++) {
            GameObject tank = Instantiate(tankPrefab, tanks[i].spawnPoint.position, tanks[i].spawnPoint.rotation) as GameObject;
            tanks[i].instance = tank;
            if (i == 0) {
                tanks[i].tankType = TankType.Player;
                tanks[i].movementJoystick = movementJoystick;

            }
            tanks[i].Setup();
        }
    }

}
