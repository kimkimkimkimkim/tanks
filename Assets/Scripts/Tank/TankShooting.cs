using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour {
    public Rigidbody shellPrefab;
    public Transform fireTransform;
    public float launchForce;
    public GameObject tankTurret;
    [HideInInspector] public TankType tankType;
    [HideInInspector] public ShootingJoystick shootingJoystick;

    public virtual void Fire(Color color) {
        Rigidbody shell = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);
        shell.GetComponent<MeshRenderer>().material.color = color;

        shell.velocity = launchForce * fireTransform.forward;
        shell.GetComponent<ShellManager>().ownerTankType = tankType;
    }
}
