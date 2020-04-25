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

    private void FixedUpdate() {
        if (tankType == TankType.Player) {
            JoystickTurn();
        }
    }

    private void JoystickTurn() {
        Vector3 vector = shootingJoystick.Direction;
        vector = new Vector3(vector.x, 0, vector.y);

        if (vector == Vector3.zero) return;
        tankTurret.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
    }

    public void Fire() {

        Rigidbody shell = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);

        shell.velocity = launchForce * fireTransform.forward;


    }
}
