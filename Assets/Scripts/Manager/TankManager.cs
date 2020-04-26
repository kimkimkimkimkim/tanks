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
    [HideInInspector] public TankMovement tankMovement;
    [HideInInspector] public TankShooting tankShooting;
}
