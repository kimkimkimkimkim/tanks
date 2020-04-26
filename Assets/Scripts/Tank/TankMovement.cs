using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {
    public float speed = 0.2f;
    public GameObject tankChassis;
    public GameObject tankTracksLeft;
    public GameObject tankTracksRight;
    [HideInInspector] public TankType tankType;
    [HideInInspector] public Rigidbody myRigidbody;

    public virtual void Start() {
        myRigidbody = GetComponent<Rigidbody>();
    }
}
