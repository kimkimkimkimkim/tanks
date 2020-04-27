using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {
    public Rigidbody shellPrefab;
    public Transform fireTransform;
    public float launchForce;
    public GameObject tankTurret;
    [HideInInspector] public TankType tankType;
    [HideInInspector] public ShootingJoystick shootingJoystick;

    public virtual void Fire() {
        Rigidbody shell = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);

        shell.velocity = launchForce * fireTransform.forward;
    }
}
