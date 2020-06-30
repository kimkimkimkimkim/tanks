using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankType {
    Player,
    CPU1,
    CPU2
}

[Serializable]
public class TankManager {
    public TankType tankType;
    [HideInInspector] public GameObject instance;
}
