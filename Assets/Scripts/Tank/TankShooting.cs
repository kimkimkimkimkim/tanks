using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {
    public Rigidbody shellPrefab;
    public Transform fireTransform;
    public float launchForce = 20f;
    public GameObject tankTurret;
    [HideInInspector] public TankType tankType;
    [HideInInspector] public ShootingJoystick shootingJoystick;
}
