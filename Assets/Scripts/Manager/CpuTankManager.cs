using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CpuTankManager : TankManager {
    public Transform spawnPoint;
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
        tankShooting.targetObject = playerTank;
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
            default:
                return new Color(42, 100, 178);
        }
    }

    public void DisableControl() {
        if (tankMovement.navMeshAgent) tankMovement.navMeshAgent.isStopped = true;
        tankMovement.enableControle = false;
        tankMovement.enabled = false;
        tankShooting.enabled = false;
    }


    public void EnableControl() {
        tankMovement.enableControle = true;
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
