using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankMovement : TankMovement {

    private void FixedUpdate() {
        JoystickMovement();
    }

    private void JoystickMovement() {
        Vector3 vector = movementJoystick.Direction;
        vector = new Vector3(vector.x, 0, vector.y);
        vector = vector.normalized; //長さ1に正規化

        if (vector == Vector3.zero) return;
        myRigidbody.MovePosition(myRigidbody.position + vector * speed);
        tankChassis.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
        tankTracksLeft.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
        tankTracksRight.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
    }

}
