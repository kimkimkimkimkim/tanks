using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankType {
    Player,
    CPU1
}

[Serializable]
public class TankManager {
    public Color tankColor;
    public Transform spawnPoint;
    public TankType tankType;
    [HideInInspector] public GameObject instance;
    [HideInInspector] public MovementJoystick movementJoystick;
    [HideInInspector] public ShootingJoystick shootingJoystick;

    private TankMovement tankMovement;
    [HideInInspector] public TankShooting tankShooting;

    public void Setup() {
        if (tankType == TankType.Player) {
            PlayerTankSetup();
        } else {
            CpuTankSetup();
        }
    }

    private void PlayerTankSetup() {
        tankMovement = instance.GetComponent<PlayerTankMovement>();
        tankShooting = instance.GetComponent<PlayerTankShooting>();

        tankMovement.movementJoystick = movementJoystick;
        tankShooting.shootingJoystick = shootingJoystick;
        shootingJoystick.tankShooting = (PlayerTankShooting)tankShooting;

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }

    private void CpuTankSetup() {
        tankMovement = instance.GetComponent<CpuTankMovement>();
        tankShooting = instance.GetComponent<CpuTankShooting>();

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }
}
