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

    private TankMovement tankMovement;

    public void Setup() {
        tankMovement = instance.GetComponent<TankMovement>();

        tankMovement.tankType = tankType;
        if (tankType == TankType.Player) tankMovement.movementJoystick = movementJoystick;

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }
}
