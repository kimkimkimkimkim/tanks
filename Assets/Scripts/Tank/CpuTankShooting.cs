using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuTankShooting : TankShooting {
    [HideInInspector] public GameObject targetObject;

    private bool canSeePlayer = false; //プレイヤーに射線が通っているかどうか
    private float fireFrequency = 4f;
    private bool isFire = false;
    private float time = 0f;

    private void Start() {
        time = fireFrequency;
    }


    private void Update() {
        CheckLineOfFire();

        time += Time.deltaTime;
        if (time >= fireFrequency && canSeePlayer) {
            Fire();
        }
    }

    private void CheckLineOfFire() {
        float y = 1;
        Vector3 myPosition = new Vector3(transform.position.x, y, transform.position.z);
        Vector3 targetPosition = new Vector3(targetObject.transform.position.x, y, targetObject.transform.position.z);
        Ray ray = new Ray(myPosition, targetPosition - myPosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);
        if (Physics.Raycast(ray, out hit, 100.0f)) {
            canSeePlayer = hit.collider.name == "PlayerTank(Clone)";
        }
    }

    public override void Fire() {
        time = 0f;
        tankTurret.transform.LookAt(new Vector3(targetObject.transform.position.x, tankTurret.transform.position.y, targetObject.transform.position.z));
        base.Fire();
    }
}
