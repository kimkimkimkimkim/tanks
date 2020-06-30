using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {
    public float speed;
    public GameObject tankChassis;
    public GameObject tankTracksLeft;
    public GameObject tankTracksRight;
    [HideInInspector] public TankType tankType {
      set{
        this._tankType = value;
        switch(value){
          case TankType.Player:
            this.speed = 0.1f;
            break;
          case TankType.CPU1:
            this.speed = 0.02f;
            break;
          case TankType.CPU2:
            this.speed = 0.15f;
            break;
        }
      }
      get { return this._tankType; }
    }
    [HideInInspector] public Rigidbody myRigidbody;

    private TankType _tankType;

    public virtual void Start() {
        myRigidbody = GetComponent<Rigidbody>();
    }
}
