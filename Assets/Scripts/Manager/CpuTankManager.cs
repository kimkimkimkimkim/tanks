using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CpuTankManager : TankManager {
    // Start is called before the first frame update
    public void Setup() {
        tankMovement = instance.GetComponent<CpuTankMovement>();
        tankShooting = instance.GetComponent<CpuTankShooting>();

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }
}
