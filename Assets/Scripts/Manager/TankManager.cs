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
        tankMovement = instance.GetComponent<TankMovement>();
        tankShooting = instance.GetComponent<TankShooting>();

        tankMovement.tankType = tankType;
        tankShooting.tankType = tankType;
        if (tankType == TankType.Player) {
            tankMovement.movementJoystick = movementJoystick;
            tankShooting.shootingJoystick = shootingJoystick;
            shootingJoystick.tankShooting = tankShooting;
        }

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }
}
