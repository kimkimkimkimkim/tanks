using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerTankManager : TankManager {
    public MovementJoystick movementJoystick;
    public ShootingJoystick shootingJoystick;
    public GameObject healthImageContainer;
    public Color onHealthColor;
    public Color offHealthColor;
    [HideInInspector] public PlayerTankMovement tankMovement;
    [HideInInspector] public PlayerTankShooting tankShooting;

    public void Setup() {
        SetTankMovement();
        SetTankshooting();
        SetTankHealth();
        SetHealthUI();
        SetMeshColor();
    }

    private void SetHealthUI() {
        var pTankHealth = instance.GetComponent<PlayerTankHealth>();
        var health = pTankHealth.maxHealth;

        var index = 0;
        foreach (Transform healthImage in healthImageContainer.transform) {
            healthImage.gameObject.SetActive(index < health);
            healthImage.GetComponent<Image>().color = onHealthColor;
            index++;
        }
    }

    private void SetTankMovement() {
        tankMovement = instance.GetComponent<PlayerTankMovement>();
        tankMovement.movementJoystick = movementJoystick;
    }

    private void SetTankshooting() {
        tankShooting = instance.GetComponent<PlayerTankShooting>();
        tankShooting.shootingJoystick = shootingJoystick;
        shootingJoystick.tankShooting = (PlayerTankShooting)tankShooting;
    }

    private void SetTankHealth() {
        PlayerTankHealth pTankHealth = instance.GetComponent<PlayerTankHealth>();
        pTankHealth.healthImageContainer = healthImageContainer;
        pTankHealth.onHealthColor = onHealthColor;
        pTankHealth.offHealthColor = offHealthColor;
    }

    private void SetMeshColor() {
        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = tankColor;
        }
    }

    public void DisableControl() {
        tankMovement.enabled = false;
        tankShooting.enabled = false;

        //m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl() {
        tankMovement.enabled = true;
        tankShooting.enabled = true;
        //m_NavMeshAgent.isStopped = false;

        //m_CanvasGameObject.SetActive(true);
    }


    public void Reset() {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}
