using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CpuTankMovement : TankMovement {
    public bool isDebug;
    [HideInInspector] public GameObject targetObject;

    private GameObject sphere;
    private GameObject point;

    private Vector3 targetPos;
    private float maxRadius = 4f;

    public override void Start(){
      base.Start();
      sphere = GameObject.Find("Sphere");
      point = GameObject.Find("Point");

      if(sphere)sphere.SetActive(isDebug);
      if(point)point.SetActive(isDebug);
      targetObject.transform.GetChild(0).gameObject.SetActive(isDebug);

      StartCoroutine(MoveTank());
    }

    private IEnumerator MoveTank() {
        CalculateTargetPos();
        yield return StartCoroutine(TankMoving());
    }

    private IEnumerator TankMoving(){
        Vector3 vector = targetPos - transform.position;
        vector = new Vector3(vector.x, 0, vector.z);
        vector = vector.normalized; //長さ1に正規化

        float prevDistance;
        do{
          prevDistance = (targetPos - transform.position).magnitude;

          myRigidbody.MovePosition(myRigidbody.position + vector * speed);
          tankChassis.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
          tankTracksLeft.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する
          tankTracksRight.transform.rotation = Quaternion.LookRotation(vector); //向きを変更する

          yield return null;
        }while(prevDistance >= (targetPos - transform.position).magnitude);

        yield return StartCoroutine(MoveTank());
    }

    private void CalculateTargetPos(){
        var tankPos = transform.position;
        var landMarkPos = targetObject.transform.position;
        var middlePoint = (tankPos + landMarkPos)/2;
        middlePoint.y = 0.1f;

        if(sphere)sphere.transform.position = middlePoint;
        if(sphere)sphere.transform.localScale = new Vector3(maxRadius,0.1f,maxRadius);

        var randomVector = new Vector3(1,0,0);
        var ampli = UnityEngine.Random.Range(0, maxRadius);
        var sheta = UnityEngine.Random.Range(0, 360);
        randomVector *= ampli;
        randomVector = Quaternion.Euler( 0, sheta, 0 ) * randomVector;

        targetPos = middlePoint + randomVector;
        targetPos.y = 0.2f;
        if(point)point.transform.position = targetPos;
        targetPos.y = 0;
    }
}
