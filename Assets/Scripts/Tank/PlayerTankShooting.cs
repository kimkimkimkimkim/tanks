using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankShooting : TankShooting {
    private void FixedUpdate() {
        JoystickTurn();
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
