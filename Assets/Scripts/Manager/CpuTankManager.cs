using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CpuTankManager : TankManager {
    [HideInInspector] public GameObject playerTank;
    [HideInInspector] public CpuTankMovement tankMovement;
    [HideInInspector] public CpuTankShooting tankShooting;

    // Start is called before the first frame update
    public void Setup() {
        SetTankMovement();
        SetTankShooting();
        SetMeshColor();
    }

    private void SetTankMovement() {
        tankMovement = instance.GetComponent<CpuTankMovement>();
        tankMovement.targetObject = playerTank;
    }

    private void SetTankShooting() {
        tankShooting = instance.GetComponent<CpuTankShooting>();
    }

    private void SetMeshColor() {
        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }
}
