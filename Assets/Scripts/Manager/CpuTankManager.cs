﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CpuTankManager : TankManager {
    public Transform spawnPoint;
    [HideInInspector] public GameObject playerTank;
    [HideInInspector] public GameObject tankMovementLandMark;
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
        tankMovement.targetObject = tankMovementLandMark;
        tankMovement.tankType = tankType;
    }

    private void SetTankShooting() {
        tankShooting = instance.GetComponent<CpuTankShooting>();
        tankShooting.targetObject = playerTank;
        tankShooting.tankType = tankType;
        tankShooting.tankColor = GetTankColor();
    }

    private void SetMeshColor() {
        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();
        Color color = GetTankColor();
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = color;
        }
    }

    private Color GetTankColor() {
        switch (tankType) {
            case TankType.CPU1:
                return new Color(0.6980392f, 0.1647059f, 0.2117647f);
            case TankType.CPU2:
                return new Color(0.6224f,0.6415f,0.0938f);
            default:
                return new Color(42, 100, 178);
        }
    }

    public void DisableControl() {
        tankMovementLandMark.GetComponent<CpuTankMovementLandMark>().canMove = false;
        tankMovement.enabled = false;
        tankShooting.enabled = false;
    }


    public void EnableControl() {
        tankMovementLandMark.GetComponent<CpuTankMovementLandMark>().canMove = true;
        tankMovement.enabled = true;
        tankShooting.enabled = true;
    }


    public void Reset() {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}
