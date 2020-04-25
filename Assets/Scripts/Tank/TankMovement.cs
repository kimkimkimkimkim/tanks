﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {
    public float speed = 0.2f;
    public GameObject tankChassis;
    public GameObject tankTracksLeft;
    public GameObject tankTracksRight;
    [HideInInspector] public TankType tankType;
    [HideInInspector] public MovementJoystick movementJoystick;

    private Rigidbody myRigidbody;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (tankType == TankType.Player) {
            JoystickMovement();
        }
    }

    private void JoystickMovement() {
        Vector3 vector = movementJoystick.Direction;
        vector = new Vector3(vector.x, 0, vector.y);
        vector = vector.normalized; //長さ1に正規化

        myRigidbody.MovePosition(myRigidbody.position + vector * speed);
        tankChassis.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
        tankTracksLeft.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
        tankTracksRight.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
    }
}
